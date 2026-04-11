namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IHearingRepository : IGenericRepository<Hearing>
    {
        Task<PaginatedList<HearingDisplayDto>> GetPagedHearingsAsync(
                  int pageNumber,
                  int pageSize,
                  string? statusFilter,
                  string? searchTerm);
    }
}
