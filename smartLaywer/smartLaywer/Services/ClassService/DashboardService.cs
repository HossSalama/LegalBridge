using smartLaywer.DTO.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var now = DateTime.Now;
            var weekAgo = now.AddDays(-7);

            var clientsCount = await _unit.Clients.GetAllQueryableNoTracking().CountAsync();
            var totalCasesCount = await _unit.Cases.GetAllQueryableNoTracking().CountAsync();

            var weeklyHearingsCount = await _unit.Hearing
                .GetAllQueryableNoTracking()
                .CountAsync(h => h.HearingDateTime >= weekAgo && h.HearingDateTime <= now);

            var urgentAlertsCount = await _unit.Hearing
                .GetAllQueryableNoTracking()
                .CountAsync(h => h.HearingDateTime > now && h.HearingDateTime <= now.AddHours(24));

            // 🔥 أهم تعديل هنا
            var upcomingHearingsQuery = await _unit.Hearing
             .GetAllQueryableNoTracking()
             .Include(h => h.Case).ThenInclude(c => c.Client)
             .Include(h => h.Court)
             .Where(h => h.HearingDateTime > now)
             .OrderBy(h => h.HearingDateTime)
             .Take(5)
             .ToListAsync();

            // هذا الجزء سيعمل الآن لأن الأعمدة أصبحت موجودة في SQL
            var upcomingHearings = upcomingHearingsQuery.Select(h =>
            {
                var dto = _mapper.Map<UpcomingHearingDto>(h);
                dto.Period = h.Period.ToString(); // أو الحساب اليدوي الذي كنت تفعله
                return dto;
            }).ToList();

            var caseStatusStats = await _unit.Cases
                .GetAllQueryableNoTracking()
                .Include(c => c.Status)
                .GroupBy(c => c.Status != null ? c.Status.StatusName.ToString() : "Open")
                .Select(g => new CaseStatusStatDto
                {
                    Name = g.Key,
                    Value = g.Count()
                }).ToListAsync();

            caseStatusStats.ForEach(s =>
            {
                s.Color = GetStatusColor(s.Name);
                s.Name = GetStatusArabic(s.Name);
            });

            var last6MonthsCases = await _unit.Cases
                .GetAllQueryableNoTracking()
                .Where(c => c.OpenDate >= now.AddMonths(-6))
                .Select(c => new { c.OpenDate.Month, c.OpenDate.Year })
                .ToListAsync();

            var monthlyStats = Enumerable.Range(0, 6)
                .Select(i =>
                {
                    var monthDate = now.AddMonths(-i);
                    return new MonthlyStatDto
                    {
                        Month = GetArabicMonth(monthDate.Month),
                        Cases = last6MonthsCases.Count(c =>
                            c.Month == monthDate.Month &&
                            c.Year == monthDate.Year)
                    };
                })
                .Reverse()
                .ToList();

            return new DashboardDto
            {
                TotalCases = totalCasesCount,
                ActiveClients = clientsCount,
                WeeklyHearings = weeklyHearingsCount,
                UrgentAlerts = urgentAlertsCount,
                UpcomingHearings = upcomingHearings,
                MonthlyStats = monthlyStats,
                CaseStatusStats = caseStatusStats
            };
        }
        //public async Task<DashboardDto> GetDashboardAsync()
        //{
        //    await Task.Delay(500); // simulate loading

        //    return new DashboardDto
        //    {
        //        TotalCases = 0,
        //        ActiveClients = 0,
        //        WeeklyHearings = 0,
        //        UrgentAlerts = 0,

        //        UpcomingHearings = new()
        //{
        //    new() { Id=1, CaseNumber="2024/156", ClientName="أحمد محمود",
        //            HearingDateTime=DateTime.Now.AddDays(1), CourtName="محكمة القاهرة",
        //            Period="صباحي", Status="pending" },
        //    new() { Id=2, CaseNumber="2024/142", ClientName="سارة علي",
        //            HearingDateTime=DateTime.Now.AddDays(2), CourtName="محكمة الجيزة",
        //            Period="صباحي", Status="pending" },
        //    new() { Id=3, CaseNumber="2024/189", ClientName="محمد حسن",
        //            HearingDateTime=DateTime.Now.AddHours(3), CourtName="محكمة الاستئناف",
        //            Period="مسائي", Status="urgent" },
        //},

        //        MonthlyStats = new()
        //{
        //    new() { Month="يناير",  Cases=0 },
        //    new() { Month="فبراير", Cases=0 },
        //    new() { Month="مارس",   Cases=0 },
        //    new() { Month="أبريل",  Cases=0  },
        //    new() { Month="مايو",   Cases=0 },
        //    new() { Month="يونيو",  Cases=0 },
        //},

        //        CaseStatusStats = new()
        //{
        //    new() { Name="جارية",  Value=0, Color="#3b82f6" },
        //    new() { Name="مكتملة", Value=0, Color="#10b981" },
        //    new() { Name="معلقة",  Value=0, Color="#f59e0b" },
        //}
        //    };
        //}

        // ── Helpers ───────────────────────────────────────────────
        private static string GetArabicMonth(int m) => m switch
        {
            1 => "يناير",
            2 => "فبراير",
            3 => "مارس",
            4 => "أبريل",
            5 => "مايو",
            6 => "يونيو",
            7 => "يوليو",
            8 => "أغسطس",
            9 => "سبتمبر",
            10 => "أكتوبر",
            11 => "نوفمبر",
            12 => "ديسمبر",
            _ => m.ToString()
        };

        private static string GetStatusArabic(string s) => s switch
        {
            "Open" => "جارية",
            "Pending" => "معلقة",
            "Closed" => "مغلقة",
            "Won" => "مكتملة",
            "Lost" => "خسرت",
            "Archived" => "مؤرشفة",
            _ => s
        };

        private static string GetStatusColor(string s) => s switch
        {
            "Open" => "#3b82f6",
            "Pending" => "#f59e0b",
            "Closed" => "#64748b",
            "Won" => "#10b981",
            "Lost" => "#dc2626",
            "Archived" => "#8b5cf6",
            _ => "#94a3b8"
        };
    }
}
