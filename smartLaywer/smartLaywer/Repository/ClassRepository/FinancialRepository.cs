namespace smartLaywer.Repository.ClassRepository
{
    public class FinancialRepository : GenericRepository<Fee>, IFinancialRepository
    {
        private readonly LegalManagementContext _context;
        private readonly IMapper _mapper;
        public FinancialRepository(LegalManagementContext context , IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FinancialStatDto> GetFinancialSummaryAsync()
        {
            var totalCollected = await _context.ActualPayments.SumAsync(ap => ap.Amount);

            var totalFeesAmount = await _context.Fees.SumAsync(f => f.TotalAmount);

            var totalOverdue = await _context.PaymentSchedules
                .Where(ps => ps.DueDate < DateTime.Now && ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => ps.PlannedAmount);

            var fullyPaidCount = await _context.Fees
                .Where(f => f.ActualPayments.Sum(ap => ap.Amount) >= f.TotalAmount)
                .CountAsync();

            return new FinancialStatDto
            {
                TotalCollected = totalCollected,
                TotalOutstanding = totalFeesAmount - totalCollected,
                TotalOverdue = totalOverdue,
                FullyPaidCount = fullyPaidCount
            };
        }

        public async Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string searchTerm, int pageNumber, int pageSize)
        {
            var query = _context.Fees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(f => f.Case.CaseNumber.Contains(searchTerm) || f.Client.FullName.Contains(searchTerm));
            }

            return await query
                .OrderByDescending(f => f.CreatedAt)
                .ProjectTo<FeeDetailsDto>(_mapper.ConfigurationProvider) 
                .ToPaginatedListAsync(pageNumber, pageSize);
        }

        public async Task<List<PaymentSchedule>> GetUnpaidSchedulesAsync(int feeId)
        {
            return await _context.PaymentSchedules
                .Where(ps => ps.FeeId == feeId && ps.Status != PaymentStatusEnum.Paid)
                .OrderBy(ps => ps.DueDate)
                .ToListAsync();
        }
    }
}
