using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Finance
{
    public class CreateSchedulesDto
    {
        public int FeeId { get; set; }
        public List<ScheduleItemDto> Schedules { get; set; }
    }

    public class ScheduleItemDto
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
