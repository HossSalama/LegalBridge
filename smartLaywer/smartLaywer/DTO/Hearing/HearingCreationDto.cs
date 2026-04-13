using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class HearingCreationDto
    {
        public int CaseId { get; set; }
        public int CourtId { get; set; }
        public int DeptId { get; set; }
        public HearingTypeEnum HearingType { get; set; }
        public DateTime HearingDateTime { get; set; } = DateTime.Now;
        public HearingPeriodEnum Period { get; set; }  
        public string JudgeName { get; set; } = string.Empty; 
        public AttendanceStatusEnum AttendanceStatus { get; set; } = AttendanceStatusEnum.Incoming;
        public string Result { get; set; } = string.Empty;
        public DateTime? NextHearingDate { get; set; } 
        public HearingPeriodEnum? NextHearingPeriod { get; set; }
        public int CreatedBy { get; set; }

    }
}
