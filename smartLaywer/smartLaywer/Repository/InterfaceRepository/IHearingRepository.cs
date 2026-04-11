using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;
using smartLaywer.Repository.InterfaceRepository;
using smartLaywer.Services.InterfaceService;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IHearingRepository : IGenericRepository<Hearing>
    {
        // جلب الجلسات مقسمة لصفحات مع بحث
        Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
            string? searchTerm, string? statusFilter, int pageNumber, int pageSize);

        // جلب كل جلسات قضية معينة مرتبة
        Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId);

        // جلب القضايا اللي عندها جلسات (للصفحة التفصيلية)
        Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync();
    }
}