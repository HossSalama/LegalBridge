public class DocumentStatsDto
{
    public int Total { get; set; }
    public int PdfCount { get; set; }
    public int WordCount { get; set; }
    public int ImageCount { get; set; }
    public int OtherCount { get; set; }
    public long TotalSizeBytes { get; set; }
    public string TotalSizeDisplay { get; set; } = string.Empty;
}