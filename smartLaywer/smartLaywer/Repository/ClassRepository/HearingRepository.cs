using AutoMapper.QueryableExtensions;
using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;

namespace smartLaywer.Repository.ClassRepository
{
    public class HearingRepository : GenericRepository<Hearing>, IHearingRepository
    {
        private readonly LegalManagementContext _context;
        private readonly IMapper _mapper;

        public HearingRepository(LegalManagementContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
        //    string? searchTerm, string? statusFilter, int pageNumber, int pageSize)
        //{
        //    var query = _context.Hearings
        //        .Include(h => h.Case).ThenInclude(c => c.Client)
        //        .Include(h => h.Court)
        //        .Include(h => h.Dept)
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        query = query.Where(h =>
        //            h.Case.CaseNumber.Contains(searchTerm) ||
        //            h.Case.Client.FullName.Contains(searchTerm) ||
        //            h.Court.CourtName.Contains(searchTerm));
        //    }

        //    if (!string.IsNullOrWhiteSpace(statusFilter))
        //    {
        //        if (statusFilter == "upcoming")
        //            query = query.Where(h => h.HearingDateTime > DateTime.Now);
        //        else if (statusFilter == "completed")
        //            query = query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Incoming);
        //        else if (statusFilter == "postponed")
        //            query = query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Postponed);
        //    }

        //    return await query
        //        .OrderByDescending(h => h.HearingDateTime)
        //        .ProjectTo<HearingListDto>(_mapper.ConfigurationProvider)
        //        .ToPaginatedListAsync(pageNumber, pageSize);
        //}

        //public async Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId)
        //{
        //    return await _context.Hearings
        //        .Where(h => h.CaseId == caseId)
        //        .Include(h => h.Court)
        //        .Include(h => h.Dept)
        //        .OrderBy(h => h.HearingDateTime)
        //        .ProjectTo<HearingListDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();
        //}

        //public async Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync()
        //{
        //    var cases = await _context.Cases
        //        .Where(c => c.Hearings.Any())
        //        .Include(c => c.Client)
        //        .Include(c => c.Court)
        //        .Include(c => c.CaseType)
        //        .Include(c => c.Hearings)
        //            .ThenInclude(h => h.Court)
        //        .Include(c => c.Hearings)
        //            .ThenInclude(h => h.Dept)
        //        .ToListAsync();

        //    return cases.Select(c => new CaseHearingsDto
        //    {
        //        CaseId = c.Id,
        //        CaseNumber = c.CaseNumber,
        //        CaseTitle = c.Title,
        //        ClientName = c.Client.FullName,
        //        CourtName = c.Court.CourtName,
        //        CaseType = c.CaseType.TypeName.ToString(),
        //        Hearings = c.Hearings
        //            .OrderBy(h => h.HearingDateTime)
        //            .Select(h => MapToDto(h))
        //            .ToList()
        //    }).ToList();
        //}

        //// ── Helper ──────────────────────────────────────────
        //private static HearingListDto MapToDto(Hearing h) => new()
        //{
        //    Id = h.Id,
        //    CaseId = h.CaseId,
        //    CaseNumber = h.Case?.CaseNumber ?? string.Empty,
        //    CaseTitle = h.Case?.Title ?? string.Empty,
        //    ClientName = h.Case?.Client?.FullName ?? string.Empty,
        //    CourtName = h.Court?.CourtName ?? string.Empty,
        //    DeptName = h.Dept?.DeptName ?? string.Empty,
        //    JudgeName = h.JudgeName,
        //    HearingDateTime = h.HearingDateTime,
        //    HearingType = GetHearingTypeLabel(h.HearingType),
        //    Period = GetPeriodLabel(h.Period),
        //    AttendanceStatus = GetAttendanceLabel(h.AttendanceStatus),
        //    Result = h.Result,
        //    NextHearingDate = h.NextHearingDate,
        //    NextHearingPeriod = h.NextHearingPeriod.HasValue
        //        ? GetPeriodLabel(h.NextHearingPeriod.Value)
        //        : null
        //};

        private static string GetHearingTypeLabel(HearingTypeEnum t) => t switch
        {
            HearingTypeEnum.Hearing => "جلسة",
            HearingTypeEnum.Investigation => "تحقيق",
            HearingTypeEnum.Expert => "خبرة",
            HearingTypeEnum.Other => "أخرى",
            _ => t.ToString()
        };
        private static string GetPeriodLabel(HearingPeriodEnum p) => p switch
        {
            HearingPeriodEnum.Morning => "صباحي",
            HearingPeriodEnum.Evening => "مسائي",
            _ => p.ToString()
        };
        private static string GetAttendanceLabel(AttendanceStatusEnum a) => a switch
        {
            AttendanceStatusEnum.Incoming => "قادم",
            AttendanceStatusEnum.Absent => "غائب",
            AttendanceStatusEnum.Postponed => "مؤجل",
            _ => a.ToString()
        };
    }
}
