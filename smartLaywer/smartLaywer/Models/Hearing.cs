using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class Hearing
{
    public int Id { get; set; }

    public int CaseId { get; set; }

    public int CourtId { get; set; }

    public int DeptId { get; set; }

    public HearingTypeEnum HearingType { get; set; } 

    public DateTime HearingDateTime { get; set; }

    public string JudgeName { get; set; } = null!;

    public HearingPeriodEnum Period { get; set; }

    public AttendanceStatusEnum AttendanceStatus { get; set; } 

    public string Result { get; set; } = null!;

    public DateTime NextHearingDate { get; set; }

    public HearingPeriodEnum? NextHearingPeriod { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Court Court { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Department Dept { get; set; } = null!;
}
