using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class HearingDetailsDto
    {
        public int Id { get; set; }
        public DateTime HearingDateTime { get; set; }
        public string HearingType { get; set; } 
        public string JudgeName { get; set; }
        public string Period { get; set; }
        public string AttendanceStatus { get; set; }
        public string Result { get; set; }
        public DateTime? NextHearingDate { get; set; }
        public string? NextHearingPeriod { get; set; }

        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string CaseTitle { get; set; }
        public string CaseStage { get; set; }

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }

        public string CourtName { get; set; }
        public string DepartmentName { get; set; }
        public string CourtLocation { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
