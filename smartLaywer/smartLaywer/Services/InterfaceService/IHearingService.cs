using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;

namespace smartLaywer.Services.InterfaceService
{
    public interface IHearingService
    {
        Task<HearingSummaryDto> GetHearingsSummaryAsync();
        Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
            string? searchTerm, string? statusFilter, int pageNumber);
        Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync();
        Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId);
        Task<bool> CreateHearingAsync(HearingCreateDto dto);
    }
}