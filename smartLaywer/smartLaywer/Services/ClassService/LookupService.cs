using smartLaywer.Repository.UnitWork;

namespace smartLaywer.Services.ClassService
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LookupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Client>> GetActiveClientsAsync()
            => await _unitOfWork.Clients
                .GetAllQueryableNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.FullName)
                .ToListAsync();

        public async Task<List<CaseType>> GetCaseTypesAsync()
            => (await _unitOfWork.CaseTypes.GetAllAsync()).ToList();

        public async Task<List<CaseStatus>> GetCaseStatusesAsync()
            => (await _unitOfWork.CaseStatuses.GetAllAsync()).ToList();

        public async Task<List<Court>> GetCourtsAsync()
            => await _unitOfWork.Courts
                .GetAllQueryableNoTracking()
                .OrderBy(c => c.CourtName)
                .ToListAsync();

        public async Task<List<User>> GetActiveLawyersAsync()
            => await _unitOfWork.Users
                .GetAllQueryableNoTracking()
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .ToListAsync();
    }
}
