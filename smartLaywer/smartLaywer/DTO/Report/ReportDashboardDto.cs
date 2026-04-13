using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTOs.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ReportTypeName { get; set; } = string.Empty; // القيمة المترجمة
        public string GeneratedByName { get; set; } = string.Empty; // اسم المستخدم
        public DateTime GeneratedAt { get; set; }
        public string? FilePath { get; set; }
    }
    public class ReportDashboardDto
    {
        public List<StatDto> Stats { get; set; } = new();
        public List<MonthlyDto> Monthly { get; set; } = new();
        public List<CaseTypeDto> CaseTypes { get; set; } = new();
        public List<CaseStatusDto> CaseStatuses { get; set; } = new();
    }

    public class StatDto
    {
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public string Trend { get; set; } = "";
    }

    public class MonthlyDto
    {
        public string Month { get; set; } = "";
        public int Cases { get; set; }
        public int Hearings { get; set; }
    }

    public class CaseTypeDto
    {
        public string Name { get; set; } = "";
        public int Value { get; set; }
    }

    public class CaseStatusDto
    {
        public string Name { get; set; } = "";
        public int Value { get; set; }
    }
}