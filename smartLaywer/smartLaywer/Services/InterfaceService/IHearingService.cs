namespace smartLaywer.Services.InterfaceService
{
    public interface IHearingService
    {
        Task<HearingSummaryDto> GetHearingsSummaryAsync();
    }
}
