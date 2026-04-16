using smartLaywer.DTO.Investigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    public class InvestigationService : IInvestigationService
    {
        private readonly IInvestigationRepository _repo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public InvestigationService(IInvestigationRepository repo, IUnitOfWork unit, IMapper mapper)
        {
            _repo = repo;
            _unit = unit;
            _mapper = mapper;
        }

        // ── Get All ───────────────────────────────────────────
        public async Task<List<InvestigationDto>> GetInvestigationsAsync()
        {
            var cases = await _repo.GetAllCasesAsync();
            return cases.Select(MapToDto).ToList();
        }

        // ── Get By Id ─────────────────────────────────────────
        public async Task<InvestigationDto?> GetInvestigationByIdAsync(int id)
        {
            var c = await _repo.GetCaseByIdAsync(id);
            return c == null ? null : MapToDto(c);
        }

        // ── Add Investigation ──────────────────────────────────
        // بما إن مفيش Investigation model منفصل،
        // هنضيف Hearing جديدة على القضية المختارة كبداية للتحقيق
        public async Task AddInvestigationAsync(
            int caseId, string type, string status,
            DateTime startDate, string prosecutor, string location)
        {
            // جيب القضية عشان نعرف الـ CourtId والـ DeptId
            var existingCase = await _repo.GetCaseByIdAsync(caseId);
            if (existingCase == null)
                throw new Exception("القضية غير موجودة");

            // جيب أي user موجود كـ CreatedBy (لازم يكون موجود)
            var firstUser = await _unit.Users
                .GetAllQueryableNoTracking()
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (firstUser == 0)
                throw new Exception("لا يوجد مستخدمون في النظام");

            var newHearing = new Hearing
            {
                CaseId = caseId,
                CourtId = existingCase.CourtId,
                DeptId = existingCase.DeptId ?? 0,
                HearingType = MapTypeToEnum(type),
                HearingDateTime = startDate,
                JudgeName = prosecutor,
                Period = startDate.Hour < 12
                                    ? HearingPeriodEnum.Morning
                                    : HearingPeriodEnum.Evening,
                AttendanceStatus = AttendanceStatusEnum.Incoming,
                Result = $"بدء مرحلة التحقيق - {location}",
                NextHearingDate = null,
                CreatedAt = DateTime.Now,
                CreatedBy = firstUser,
            };

            await _unit.Hearing.AddAsync(newHearing);
            await _unit.CompleteAsync();
        }

        // ── Mapping ───────────────────────────────────────────
        private static InvestigationDto MapToDto(Case c)
        {
            var firstHearing = c.Hearings.OrderBy(h => h.HearingDateTime).FirstOrDefault();

            return new InvestigationDto
            {
                Id = c.Id,
                CaseNumber = c.CaseNumber,
                ClientName = c.Client?.FullName ?? "—",
                Type = MapCaseType(c.CaseType?.TypeName.ToString()),
                Status = MapCaseStatus(c.Status?.StatusName.ToString()),
                StartDate = c.OpenDate.ToString("yyyy-MM-dd"),
                Prosecutor = firstHearing?.JudgeName ?? c.AssignedLawyer?.FullName ?? "—",
                Location = c.Court?.CourtName ?? "—",

                Sessions = c.Hearings
                    .OrderBy(h => h.HearingDateTime)
                    .Select(h => new SessionDto
                    {
                        Id = h.Id,
                        Date = h.HearingDateTime.ToString("yyyy-MM-dd"),
                        Time = h.HearingDateTime.ToString("hh:mm tt"),
                        Summary = h.Result ?? string.Empty,
                        Attendees = new List<string>
                        {
                            c.Client?.FullName ?? "العميل",
                            h.CreatedByNavigation?.FullName ?? "المحامي",
                            "النيابة"
                        },
                        Decisions = string.IsNullOrEmpty(h.Result) ? "—" : h.Result
                    }).ToList(),

                Witnesses = new List<WitnessDto>(),

                Evidence = c.Documents
                    .Select(d => new EvidenceDto
                    {
                        Type = MapDocType(d.DocType.ToString()),
                        Description = d.Title,
                        DateCollected = d.UploadedAt.ToString("yyyy-MM-dd"),
                        Status = d.IsArchived ? "approved" : "pending"
                    }).ToList()
            };
        }

        // ── Helpers ───────────────────────────────────────────
        private static HearingTypeEnum MapTypeToEnum(string type) => type switch
        {
            "criminal" => HearingTypeEnum.Investigation,
            "civil" => HearingTypeEnum.Hearing,
            //"administrative" => HearingTypeEnum.Other,
            _ => HearingTypeEnum.Investigation
        };
        

        private static string MapCaseType(string? t) => t switch
        {
            "Criminal" => "criminal",
            "Civil" => "civil",
            "Administrative" => "administrative",
            "Commercial" => "civil",
            "Family" => "civil",
            _ => "civil"
        };

        private static string MapCaseStatus(string? s) => s switch
        {
            "Open" => "ongoing",
            "Pending" => "ongoing",
            "Closed" => "completed",
            "Won" => "completed",
            "Lost" => "completed",
            "Archived" => "suspended",
            _ => "ongoing"
        };

        private static string MapDocType(string? t) => t switch
        {
            "Contract" => "عقد",
            "Report" => "تقرير",
            "Photo" => "صورة",
            "Audio" => "تسجيل صوتي",
            "Video" => "تسجيل مرئي",
            _ => "مستند"
        };
    }
}
