using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Dashboard
{
    public class DashboardDto
    {
        public int TotalCases { get; set; }
        public int ActiveClients { get; set; }
        public int WeeklyHearings { get; set; }
        public int UrgentAlerts { get; set; }
        public List<UpcomingHearingDto> UpcomingHearings { get; set; } = new();
        public List<MonthlyStatDto> MonthlyStats { get; set; } = new();
        public List<CaseStatusStatDto> CaseStatusStats { get; set; } = new();
    }

    public class UpcomingHearingDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime HearingDateTime { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class MonthlyStatDto
    {
        public string Month { get; set; } = string.Empty;
        public int Cases { get; set; }
    }

    public class CaseStatusStatDto
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}