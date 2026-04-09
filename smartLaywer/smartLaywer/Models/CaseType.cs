using smartLaywer.Enum;
using System;

namespace smartLaywer.Models;

public partial class CaseType
{
    public int Id { get; set; }

    public CaseTypeEnum TypeName { get; set; } 

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
