namespace smartLaywer.Services.InterfaceService
{
    public interface IHearingService
    {
        Task<HearingSummaryDto> GetHearingsSummaryAsync();
        Task<PaginatedList<HearingDisplayDto>> GetHearingsForGridAsync(int pageNumber, string? statusFilter, string? searchTerm);
        Task<bool> CreateHearingAsync(HearingCreationDto dto);
    }
}
