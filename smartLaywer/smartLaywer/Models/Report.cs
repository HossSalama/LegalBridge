using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class Report
{
    public int Id { get; set; }

    public ReportTypeEnum ReportType { get; set; }

    public string Title { get; set; } = null!;

    public int GeneratedBy { get; set; }

    public DateTime GeneratedAt { get; set; }

    public string? Parameters { get; set; }

    public string? FilePath { get; set; }

    public virtual User GeneratedByNavigation { get; set; } = null!;
}
