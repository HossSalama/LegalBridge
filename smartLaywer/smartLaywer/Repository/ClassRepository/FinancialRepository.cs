namespace smartLaywer.Repository.ClassRepository
{
    public class FinancialRepository : GenericRepository<Fee>, IFinancialRepository
    {
        private readonly LegalManagementContext _context;
        private readonly IMapper _mapper;
        public FinancialRepository(LegalManagementContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialStatDto> GetFinancialSummaryAsync()
        {
            var feeAnalytics = await _context.Fees
                .Select(f => new
                {
                    TotalAmount = f.TotalAmount,
                    Collected = f.ActualPayments.Sum(ap => (decimal?)ap.Amount) ?? 0
                })
                .ToListAsync();

            var totalFeesAmount = feeAnalytics.Sum(x => x.TotalAmount);
            var totalCollected = feeAnalytics.Sum(x => x.Collected);
            var fullyPaidCount = feeAnalytics.Count(x => x.Collected >= x.TotalAmount && x.TotalAmount > 0);

            var totalOverdue = await _context.PaymentSchedules
                .Where(ps => ps.DueDate < DateTime.Now && ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => (decimal?)ps.PlannedAmount) ?? 0;

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
            var query = _context.Fees.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(f => f.Case.CaseNumber.Contains(searchTerm) || f.Client.FullName.Contains(searchTerm));
            }

            return await query
                .OrderByDescending(f => f.CreatedAt)
                .ProjectTo<FeeDetailsDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }



        public async Task AddFeeAsync(Fee fee)
        {
            await _context.Fees.AddAsync(fee);
        }






    }
}
