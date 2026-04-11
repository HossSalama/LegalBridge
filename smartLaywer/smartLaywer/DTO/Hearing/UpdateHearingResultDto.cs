using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class UpdateHearingResultDto
    {
        public int Id { get; set; } 
        public string Result { get; set; } = string.Empty;
        public DateTime? NextHearingDate { get; set; } 
        public HearingPeriodEnum? NextHearingPeriod { get; set; } 

        public AttendanceStatusEnum Status { get; set; } = AttendanceStatusEnum.Attended;
    }
}
