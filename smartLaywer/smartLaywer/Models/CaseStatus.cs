global using smartLaywer.Enum;
using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class CaseStatus
{
    public int Id { get; set; }

    public CaseStatusEnum StatusName { get; set; } 

    public string Color { get; set; } = null!;

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
