using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Agenda
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentTypeEnum AppointmentType { get; set; }
        public string AppointmentTypeLabel { get; set; } = string.Empty;
        public AppointmentPriorityEnum Priority { get; set; }
        public string PriorityLabel { get; set; } = string.Empty;
        public string? Location { get; set; }
        public int? CaseId { get; set; }
        public string? CaseNumber { get; set; }
        public string? CaseTitle { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
    }
}
