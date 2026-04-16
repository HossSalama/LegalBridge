using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.HearingDetails
{
    public class HearingDetailsCreateDto
    {
        public int CaseId { get; set; }
        public int CourtId { get; set; }
        public int DeptId { get; set; }

        public HearingTypeEnum HearingType { get; set; }

        public DateTime HearingDateTime { get; set; }

        public string JudgeName { get; set; } = string.Empty;

        public HearingPeriodEnum Period { get; set; }

        public AttendanceStatusEnum AttendanceStatus { get; set; }

        public string Result { get; set; } = string.Empty;

        public DateTime? NextHearingDate { get; set; }

        public HearingPeriodEnum? NextHearingPeriod { get; set; }

        public int CreatedBy { get; set; }
    }
}
