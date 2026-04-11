using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class HearingCreateDto
    {
        public int CaseId { get; set; }
        public int CourtId { get; set; }
        public int DeptId { get; set; }
        public int HearingType { get; set; }       // HearingTypeEnum value
        public DateTime HearingDateTime { get; set; } = DateTime.Now;
        public int Period { get; set; }            // HearingPeriodEnum value
        public int AttendanceStatus { get; set; }  // AttendanceStatusEnum value
        public string Result { get; set; } = string.Empty;
        public DateTime NextHearingDate { get; set; } = DateTime.Now;
        public int? NextHearingPeriod { get; set; }
    }
}