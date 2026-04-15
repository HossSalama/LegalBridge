using smartLaywer.DTO.Agenda;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.InterfaceService;

namespace smartLaywer.Services.ClassService;

public class AgendaService : IAgendaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly LegalManagementContext _context;

    public AgendaService(IUnitOfWork unitOfWork, LegalManagementContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<List<AgendaItemDto>> GetUpcomingItemsAsync(int daysAhead = 30)
    {
        var now = DateTime.Now;
        var end = now.AddDays(daysAhead);

        // Hearings
        var hearings = await _context.Hearings
            .AsNoTracking()
            .Include(h => h.Case).ThenInclude(c => c.Client)
            .Include(h => h.Court)
            .Include(h => h.Dept)
            .Include(h => h.CreatedByNavigation)
            .Where(h => h.HearingDateTime >= now && h.HearingDateTime <= end)
            .OrderBy(h => h.HearingDateTime)
            .ToListAsync();

        // Appointments
        var appointments = await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Case)
            .Include(a => a.Client)
            .Include(a => a.CreatedByNavigation)
            .Where(a => a.AppointmentDate >= now && a.AppointmentDate <= end && !a.IsCompleted)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        var items = new List<AgendaItemDto>();

        foreach (var h in hearings)
        {
            items.Add(new AgendaItemDto
            {
                Id = h.Id,
                ItemType = AgendaItemType.Hearing,
                Title = $"جلسة — {h.Case?.CaseNumber}",
                DateTime = h.HearingDateTime,
                Location = h.Court?.CourtName + (h.Dept != null ? $" — {h.Dept.DeptName}" : ""),
                CaseNumber = h.Case?.CaseNumber,
                ClientName = h.Case?.Client?.FullName,
                TypeLabel = h.HearingType.GetDisplayName(),
                TypeColor = h.HearingType switch
                {
                    HearingTypeEnum.Investigation => "amber",
                    HearingTypeEnum.Expert => "blue",
                    _ => "blue"
                },
                IsHighPriority = h.AttendanceStatus == AttendanceStatusEnum.Incoming,
                CourtName = h.Court?.CourtName,
                DepartmentName = h.Dept?.DeptName,
                JudgeName = h.JudgeName,
                Result = h.Result,
                AttendanceStatus = h.AttendanceStatus,
                HearingType = h.HearingType,
                CreatedByName = h.CreatedByNavigation?.FullName
            });
        }

        foreach (var a in appointments)
        {
            items.Add(new AgendaItemDto
            {
                Id = a.Id,
                ItemType = AgendaItemType.Appointment,
                Title = a.Title,
                DateTime = a.AppointmentDate,
                Location = a.Location,
                CaseNumber = a.Case?.CaseNumber,
                ClientName = a.Client?.FullName ?? a.Case?.Client?.FullName,
                TypeLabel = a.AppointmentType.GetDisplayName(),
                TypeColor = a.AppointmentType switch
                {
                    AppointmentTypeEnum.MemoDeadline => "red",
                    AppointmentTypeEnum.Deadline => "red",
                    AppointmentTypeEnum.ClientMeeting => "green",
                    _ => "green"
                },
                IsHighPriority = a.Priority == AppointmentPriorityEnum.Urgent,
                AppointmentType = a.AppointmentType,
                Priority = a.Priority,
                PriorityLabel = a.Priority.GetDisplayName(),
                Description = a.Description,
                CreatedByName = a.CreatedByNavigation?.FullName
            });
        }

        return items.OrderBy(i => i.DateTime).ToList();
    }

    public async Task<List<AgendaItemDto>> GetItemsByDateAsync(DateTime date)
    {
        var start = date.Date;
        var end = start.AddDays(1);
        return (await GetUpcomingItemsAsync(365))
            .Where(i => i.DateTime >= start && i.DateTime < end)
            .ToList();
    }

    public async Task<AgendaStatsDto> GetAgendaStatsAsync()
    {
        var today = DateTime.Today;
        var weekEnd = today.AddDays(7);

        var hearingStats = await _context.Hearings.AsNoTracking()
            .Where(h => h.HearingDateTime >= DateTime.Now)
            .Select(h => new { h.HearingDateTime })
            .ToListAsync();

        var apptStats = await _context.Appointments.AsNoTracking()
            .Where(a => !a.IsCompleted && a.AppointmentDate >= DateTime.Now)
            .Select(a => new { a.AppointmentDate, a.AppointmentType })
            .ToListAsync();

        return new AgendaStatsDto
        {
            TodayCount = hearingStats.Count(h => h.HearingDateTime.Date == today)
                       + apptStats.Count(a => a.AppointmentDate.Date == today),
            WeekCount = hearingStats.Count(h => h.HearingDateTime.Date <= weekEnd)
                      + apptStats.Count(a => a.AppointmentDate.Date <= weekEnd),
            UpcomingHearingsCount = hearingStats.Count,
            UpcomingAppointmentsCount = apptStats.Count,
            DeadlinesCount = apptStats.Count(a =>
                a.AppointmentType == AppointmentTypeEnum.Deadline ||
                a.AppointmentType == AppointmentTypeEnum.MemoDeadline)
        };
    }

    public async Task<bool> AddAppointmentAsync(AppointmentCreationDto dto)
    {
        var entity = new Appointment
        {
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            AppointmentDate = dto.AppointmentDate,
            AppointmentType = dto.AppointmentType,
            Priority = dto.Priority,
            Location = string.IsNullOrWhiteSpace(dto.Location) ? null : dto.Location.Trim(),
            CaseId = dto.CaseId == 0 ? null : dto.CaseId,
            ClientId = dto.ClientId == 0 ? null : dto.ClientId,
            CreatedBy = dto.CreatedBy,
            CreatedAt = DateTime.Now
        };
        _context.Appointments.Add(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<AppointmentDto?> GetAppointmentDetailsAsync(int id)
    {
        var a = await _context.Appointments
            .AsNoTracking()
            .Include(x => x.Case)
            .Include(x => x.Client)
            .Include(x => x.CreatedByNavigation)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (a == null) return null;

        return new AppointmentDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            AppointmentDate = a.AppointmentDate,
            AppointmentType = a.AppointmentType,
            AppointmentTypeLabel = a.AppointmentType.GetDisplayName(),
            Priority = a.Priority,
            PriorityLabel = a.Priority.GetDisplayName(),
            Location = a.Location,
            CaseId = a.CaseId,
            CaseNumber = a.Case?.CaseNumber,
            CaseTitle = a.Case?.Title,
            ClientId = a.ClientId,
            ClientName = a.Client?.FullName,
            IsCompleted = a.IsCompleted,
            CompletedAt = a.CompletedAt,
            CreatedAt = a.CreatedAt,
            CreatedByName = a.CreatedByNavigation?.FullName ?? string.Empty
        };
    }

    public async Task<AgendaItemDto?> GetHearingDetailsAsync(int id)
    {
        var h = await _context.Hearings
            .AsNoTracking()
            .Include(x => x.Case).ThenInclude(c => c.Client)
            .Include(x => x.Court)
            .Include(x => x.Dept)
            .Include(x => x.CreatedByNavigation)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (h == null) return null;

        return new AgendaItemDto
        {
            Id = h.Id,
            ItemType = AgendaItemType.Hearing,
            Title = $"جلسة — {h.Case?.CaseNumber}",
            DateTime = h.HearingDateTime,
            Location = h.Court?.CourtName + (h.Dept != null ? $" — {h.Dept.DeptName}" : ""),
            CaseNumber = h.Case?.CaseNumber,
            ClientName = h.Case?.Client?.FullName,
            TypeLabel = h.HearingType.GetDisplayName(),
            TypeColor = "blue",
            IsHighPriority = h.AttendanceStatus == AttendanceStatusEnum.Incoming,
            CourtName = h.Court?.CourtName,
            DepartmentName = h.Dept?.DeptName,
            JudgeName = h.JudgeName,
            Result = h.Result,
            AttendanceStatus = h.AttendanceStatus,
            HearingType = h.HearingType,
            CreatedByName = h.CreatedByNavigation?.FullName
        };
    }

    public async Task<bool> MarkAppointmentCompletedAsync(int id)
    {
        var entity = await _context.Appointments.FindAsync(id);
        if (entity == null) return false;
        entity.IsCompleted = true;
        entity.CompletedAt = DateTime.Now;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var entity = await _context.Appointments.FindAsync(id);
        if (entity == null) return false;
        _context.Appointments.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<DateTime>> GetDatesWithEventsAsync(int year, int month)
    {
        var start = new DateTime(year, month, 1);
        var end = start.AddMonths(1);

        var hearingDates = await _context.Hearings
            .AsNoTracking()
            .Where(h => h.HearingDateTime >= start && h.HearingDateTime < end)
            .Select(h => h.HearingDateTime.Date)
            .ToListAsync();

        var apptDates = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.AppointmentDate >= start && a.AppointmentDate < end && !a.IsCompleted)
            .Select(a => a.AppointmentDate.Date)
            .ToListAsync();

        return hearingDates.Concat(apptDates).Distinct().ToList();
    }
    public async Task<bool> UpdateAppointmentAsync(int id, AppointmentCreationDto dto)
    {
        var entity = await _context.Appointments.FindAsync(id);
        if (entity == null) return false;

        entity.Title = dto.Title.Trim();
        entity.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
        entity.AppointmentDate = dto.AppointmentDate;
        entity.AppointmentType = dto.AppointmentType;
        entity.Priority = dto.Priority;
        entity.Location = string.IsNullOrWhiteSpace(dto.Location) ? null : dto.Location.Trim();
        entity.CaseId = dto.CaseId == 0 ? null : dto.CaseId;
        entity.ClientId = dto.ClientId == 0 ? null : dto.ClientId;

        return await _context.SaveChangesAsync() > 0;

    }
}