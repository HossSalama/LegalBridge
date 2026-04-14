namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IFinancialRepository : IGenericRepository<Fee>
    {
        Task<FinancialStatDto> GetFinancialSummaryAsync();
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string searchTerm, int pageNumber, int pageSize);

    }
}
