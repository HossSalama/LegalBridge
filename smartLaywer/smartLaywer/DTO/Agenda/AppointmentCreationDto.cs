using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Agenda
{
    public class AppointmentCreationDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Today;
        public AppointmentTypeEnum AppointmentType { get; set; } = AppointmentTypeEnum.ClientMeeting;
        public AppointmentPriorityEnum Priority { get; set; } = AppointmentPriorityEnum.Normal;
        public string? Location { get; set; }
        public int? CaseId { get; set; }
        public int? ClientId { get; set; }
        public int CreatedBy { get; set; }
    }
}
