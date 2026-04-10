namespace smartLaywer.Services.InterfaceService
{
    public interface ICaseService
    {
        Task<List<CaseSummaryDto>> GetAllCasesAsync(string? searchTerm = null);
        Task<Case?> GetCaseWithDetailsAsync(int id);
        Task<CaseStatsDto> GetCaseStatsAsync();
        Task AddCaseAsync(Case caseEntity);
        Task UpdateCaseAsync(Case caseEntity);
        Task DeleteCaseAsync(int id);
    }
}
