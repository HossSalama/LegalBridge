namespace smartLaywer.Services.ClassService
{
    public class CaseService : ICaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper     _mapper;

        public CaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper     = mapper;
        }

        /// <summary>
        /// Returns a flat list of CaseSummaryDto for the cases table.
        /// Optional search by case number, title, or client name.
        /// </summary>
        public async Task<List<CaseSummaryDto>> GetAllCasesAsync(string? searchTerm = null)
        {
            var query = _unitOfWork.Cases
                .GetAllQueryableNoTracking()
                .Include(c => c.Client)
                .Include(c => c.CaseType)
                .Include(c => c.Status)
                .Include(c => c.Hearings)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c =>
                    c.Title.Contains(searchTerm) ||
                    (c.CaseNumber != null && c.CaseNumber.Contains(searchTerm)) ||
                    c.Client.FullName.Contains(searchTerm));
            }

            var cases = await query
                .OrderByDescending(c => c.OpenDate)
                .ToListAsync();

            return _mapper.Map<List<CaseSummaryDto>>(cases);
        }

        /// <summary>
        /// Returns a fully-loaded Case entity for the details modal.
        /// </summary>
        public async Task<Case?> GetCaseWithDetailsAsync(int id)
            => await _unitOfWork.Cases.GetCaseWithDetailsAsync(id);

        public async Task<CaseStatsDto> GetCaseStatsAsync()
            => await _unitOfWork.Cases.GetCaseStatsAsync();

        public async Task AddCaseAsync(Case caseEntity)
        {
            caseEntity.UpdatedAt = DateTime.Now;
            await _unitOfWork.Cases.AddAsync(caseEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCaseAsync(Case caseEntity)
        {
            caseEntity.UpdatedAt = DateTime.Now;
            _unitOfWork.Cases.Update(caseEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCaseAsync(int id)
        {
            _unitOfWork.Cases.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
