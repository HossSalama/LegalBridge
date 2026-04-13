 global using smartLaywer.DTO.Cases;

namespace smartLaywer.Services.InterfaceService
{
    public interface ICaseService
    {
        Task<List<CaseSummaryDto>> GetAllCasesAsync(string? searchTerm = null);
        Task<Case?> GetCaseWithDetailsAsync(int id);
        Task<CaseStatsDto> GetCaseStatsAsync();
        Task AddCaseAsync(Case newCase, Fee? fee = null);
        Task UpdateCaseAsync(CaseEditDto dto);
        Task DeleteCaseAsync(int id);
        Task ValidateClientIdentifierUniquenessAsync(Client client);
        Task<IEnumerable<CaseViewDto>> GetAllCasesForDropdownAsync();
    }
}
