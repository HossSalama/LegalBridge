using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.HearingDetails
{
    public class HearingItemDto
    {
        public int Id { get; set; }

        public string HearingType { get; set; }
        public DateTime HearingDateTime { get; set; }
        public string JudgeName { get; set; }

        public string Period { get; set; }
        public string AttendanceStatus { get; set; }

        public string Result { get; set; }

        public DateTime? NextHearingDate { get; set; }
        public string? NextHearingPeriod { get; set; }

        public string CreatedBy { get; set; }
    }
}