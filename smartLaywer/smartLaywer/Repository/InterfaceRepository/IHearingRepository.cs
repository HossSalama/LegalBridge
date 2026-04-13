using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;
using smartLaywer.Repository.InterfaceRepository;
using smartLaywer.Services.InterfaceService;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IHearingRepository : IGenericRepository<Hearing>
    {
        Task<PaginatedList<HearingDisplayDto>> GetPagedHearingsAsync(
                  int pageNumber,
                  int pageSize,
                  string? statusFilter,
                  string? searchTerm);
        Task<IEnumerable<Hearing>> GetAllWithDetailsAsync();
    }
}