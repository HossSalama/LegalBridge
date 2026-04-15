using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Agenda
{
    public class AgendaItemDto
    {
        public int Id { get; set; }
        public AgendaItemType ItemType { get; set; } // Hearing or Appointment
        public string Title { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string? Location { get; set; }
        public string? CaseNumber { get; set; }
        public string? ClientName { get; set; }
        public string TypeLabel { get; set; } = string.Empty;
        public string TypeColor { get; set; } = "blue"; 
        public bool IsHighPriority { get; set; }
        public bool IsCompleted { get; set; } //For hearing
        public string? CourtName { get; set; }
        public string? DepartmentName { get; set; }
        public string? JudgeName { get; set; }
        public string? Result { get; set; }
        public AttendanceStatusEnum? AttendanceStatus { get; set; }
        public HearingTypeEnum? HearingType { get; set; }
        public AppointmentTypeEnum? AppointmentType { get; set; }
        public AppointmentPriorityEnum? Priority { get; set; }
        public string? PriorityLabel { get; set; }
        public string? Description { get; set; }
        public string? CreatedByName { get; set; }
    }

    public enum AgendaItemType { Hearing, Appointment }
}
