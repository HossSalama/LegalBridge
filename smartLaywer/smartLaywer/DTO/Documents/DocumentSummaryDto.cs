using smartLaywer.Enum;

namespace smartLaywer.DTO.Documents
{
    public class DocumentListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DocumentTypeEnum DocType { get; set; }
        public string DocTypeDisplay { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string FileSizeDisplay { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public string UploadedByName { get; set; } = string.Empty;
        public int CaseId { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string CaseTitle { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsArchived { get; set; }
    }
}
