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
        //// Ã·» «·Ã·”«  „ﬁ”„… ·’›Õ«  „⁄ »ÕÀ
        //Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
        //    string? searchTerm, string? statusFilter, int pageNumber, int pageSize);

        //// Ã·» ﬂ· Ã·”«  ﬁ÷Ì… „⁄Ì‰… „— »…
        //Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId);

        //// Ã·» «·ﬁ÷«Ì« «··Ì ⁄‰œÂ« Ã·”«  (··’›Õ… «· ›’Ì·Ì…)
        //Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync();
        Task<IEnumerable<Hearing>> GetAllWithDetailsAsync();
    }
}