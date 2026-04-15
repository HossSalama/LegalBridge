using smartLaywer.Models;

namespace smartLaywer.Services.InterfaceService;

public interface ILookupService
{
    Task<List<Client>> GetActiveClientsAsync();
    Task<List<CaseType>> GetCaseTypesAsync();
    Task<List<CaseStatus>> GetCaseStatusesAsync();
    Task<List<Court>> GetCourtsAsync();
    Task<List<User>> GetActiveLawyersAsync();
    Task<List<Case>> GetActiveCasesAsync();      
    Task<List<Department>> GetDepartmentsByCourtAsync(int courtId);  
}
