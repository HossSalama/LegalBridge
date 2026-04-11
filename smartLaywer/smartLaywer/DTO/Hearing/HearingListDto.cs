using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class HearingListDto
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string CaseTitle { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string CourtName { get; set; } = string.Empty;
        public string DeptName { get; set; } = string.Empty;
        public string JudgeName { get; set; } = string.Empty;
        public DateTime HearingDateTime { get; set; }
        public string HearingType { get; set; } = string.Empty;   // Hearing / Investigation ...
        public string Period { get; set; } = string.Empty;         // Morning / Evening
        public string AttendanceStatus { get; set; } = string.Empty; // Incoming / Absent / Postponed
        public string Result { get; set; } = string.Empty;
        public DateTime NextHearingDate { get; set; }
        public string? NextHearingPeriod { get; set; }
    }
}
