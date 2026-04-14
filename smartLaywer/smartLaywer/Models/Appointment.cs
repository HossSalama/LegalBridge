using System;
using System.ComponentModel.DataAnnotations;
using smartLaywer.Enum;

namespace smartLaywer.Models;

public partial class Appointment
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime AppointmentDate { get; set; }

    public AppointmentTypeEnum AppointmentType { get; set; }

    public AppointmentPriorityEnum Priority { get; set; } = AppointmentPriorityEnum.Normal;
    public string? Location { get; set; }
    public int? CaseId { get; set; }
    public int? ClientId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }

    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedAt { get; set; }
    public virtual Case? Case { get; set; }
    public virtual Client? Client { get; set; }
    public virtual User CreatedByNavigation { get; set; } = null!;
}
