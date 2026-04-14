using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.InterfaceService;

namespace smartLaywer.Services.ClassService;
public class LookupService : ILookupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly LegalManagementContext _context;

    public LookupService(IUnitOfWork unitOfWork, LegalManagementContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
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
    // for agenda and cases dropdowns
    public async Task<List<Case>> GetActiveCasesAsync()
        => await _context.Cases
            .AsNoTracking()
            .Where(c => !c.IsArchived)
            .OrderByDescending(c => c.OpenDate)
            .Take(200)
            .ToListAsync();

    public async Task<List<Department>> GetDepartmentsByCourtAsync(int courtId)
        => await _context.Departments
            .AsNoTracking()
            .Where(d => d.CourtId == courtId)
            .OrderBy(d => d.DeptName)
            .ToListAsync();
}
