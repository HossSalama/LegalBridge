using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class Note
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public NoteTypeEnum NoteType { get; set; } 

    public string RelatedTable { get; set; } = null!;

    public int RelatedId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
