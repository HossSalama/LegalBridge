using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class Appeal
{
    public int Id { get; set; }

    public int CaseId { get; set; }

    public int CourtId { get; set; }

    public string? AppealNumber { get; set; }

    public AppealTypeEnum AppealType { get; set; } 
    public DateOnly AppealDate { get; set; }

    public int StatusId { get; set; }

    public string? Grounds { get; set; }

    public string? Result { get; set; }

    public DateTime? ResultDate { get; set; }

    public int AssignedLawyerId { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User AssignedLawyer { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual Court Court { get; set; } = null!;

    public virtual CaseStatus Status { get; set; } = null!;
}
