namespace smartLaywer.Repository.InterfaceRepository
{
    public interface ICaseRepository : IGenericRepository<Case>
    {
        // Returns a case with ALL navigation properties needed for CaseDetails page
        Task<Case?> GetCaseWithDetailsAsync(int id);

        // Returns stats calculated in DB, not in memory
        Task<CaseStatsDto> GetCaseStatsAsync();
    }
}