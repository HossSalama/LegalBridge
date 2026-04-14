using smartLaywer.DTO.Agenda;

namespace smartLaywer.Services.InterfaceService
{
    public interface IAgendaService
    {
        Task<List<AgendaItemDto>> GetUpcomingItemsAsync(int daysAhead = 30);
        Task<List<AgendaItemDto>> GetItemsByDateAsync(DateTime date);
        Task<AgendaStatsDto> GetAgendaStatsAsync();
        Task<bool> AddAppointmentAsync(AppointmentCreationDto dto);
        Task<bool> UpdateAppointmentAsync(int id, AppointmentCreationDto dto);
        Task<AppointmentDto?> GetAppointmentDetailsAsync(int id);
        Task<AgendaItemDto?> GetHearingDetailsAsync(int id);
        Task<bool> MarkAppointmentCompletedAsync(int id);
        Task<List<DateTime>> GetDatesWithEventsAsync(int year, int month);
    }
}