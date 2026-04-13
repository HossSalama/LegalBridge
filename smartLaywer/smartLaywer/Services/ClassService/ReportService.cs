using Microsoft.EntityFrameworkCore;
using smartLaywer.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unit;

        public ReportService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<ReportDashboardDto> GetReportDashboardAsync()
        {
            var now = DateTime.Now;
            var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1);

            // 1. الإحصائيات العامة (Stats)
            var totalCases = await _unit.Cases.GetAllQueryableNoTracking().CountAsync();
            var totalClients = await _unit.Clients.GetAllQueryableNoTracking().CountAsync();
            var thisMonthCases = await _unit.Cases.GetAllQueryableNoTracking()
                .CountAsync(c => c.OpenDate >= firstDayOfCurrentMonth);

            var stats = new List<StatDto>
            {
                new() { Label = "إجمالي القضايا", Value = totalCases.ToString(), Trend = "+5%" },
                new() { Label = "العملاء النشطون", Value = totalClients.ToString(), Trend = "مستقر" },
                new() { Label = "قضايا الشهر الحالي", Value = thisMonthCases.ToString(), Trend = "+12%" }
            };

            // 2. الإحصائيات الشهرية (آخر 6 أشهر)
            var monthlyData = new List<MonthlyDto>();
            for (int i = 5; i >= 0; i--)
            {
                var targetMonth = now.AddMonths(-i);
                var monthName = targetMonth.ToString("MMMM", new CultureInfo("ar-EG"));

                var casesCount = await _unit.Cases.GetAllQueryableNoTracking()
                    .CountAsync(c => c.OpenDate.Month == targetMonth.Month && c.OpenDate.Year == targetMonth.Year);

                var hearingsCount = await _unit.Hearing.GetAllQueryableNoTracking()
                    .CountAsync(h => h.HearingDateTime.Month == targetMonth.Month && h.HearingDateTime.Year == targetMonth.Year);

                monthlyData.Add(new MonthlyDto
                {
                    Month = monthName,
                    Cases = casesCount,
                    Hearings = hearingsCount
                });
            }

            // 3. توزيع أنواع القضايا (مثال من جدول CaseTypes)
            var caseTypes = await _unit.Cases.GetAllQueryableNoTracking()
                .Include(c => c.CaseType)
                .GroupBy(c => c.CaseType.TypeName)
                .Select(g => new CaseTypeDto
                {
                    // نقوم بتحويل الـ Enum إلى نص
                    Name = g.Key.ToString(),
                    Value = g.Count()
                }).ToListAsync();

            // 4. حالات القضايا
            var caseStatuses = await _unit.Cases.GetAllQueryableNoTracking()
                .Include(c => c.Status)
                .GroupBy(c => c.Status.StatusName.ToString())
                .Select(g => new CaseStatusDto
                {
                    Name = g.Key, // يمكنك استخدام Helper لتحويلها للعربية كما فعلنا سابقاً
                    Value = g.Count()
                }).ToListAsync();

            return new ReportDashboardDto
            {
                Stats = stats,
                Monthly = monthlyData,
                CaseTypes = caseTypes,
                CaseStatuses = caseStatuses
            };
        }
    }
}