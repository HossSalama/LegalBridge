public class DocumentCreateDto
{
    public int CaseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DocumentTypeEnum DocType { get; set; } = DocumentTypeEnum.Other;
    public string? Notes { get; set; }
}