using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Agenda
{
    public class AgendaStatsDto
    {
        public int TodayCount { get; set; }
        public int WeekCount { get; set; }
        public int UpcomingHearingsCount { get; set; }
        public int UpcomingAppointmentsCount { get; set; }
        public int DeadlinesCount { get; set; }
    }
}
