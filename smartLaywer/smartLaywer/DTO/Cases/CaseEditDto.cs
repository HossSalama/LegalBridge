namespace smartLaywer.DTO.Case
{
    /// <summary>
    /// Only the fields the UI is allowed to change after a case is created.
    /// ClientId, CaseTypeId, and OpenDate are intentionally excluded — they are read-only after creation.
    /// </summary>
    public class CaseEditDto
    {
        public int Id { get; set; }   

        // Editable lookup FKs
        public int StatusId { get; set; }
        public int CourtId { get; set; }
        public int? DeptId { get; set; }
        public int AssignedLawyerId { get; set; }

        // Editable free-text fields
        public string Title { get; set; } = string.Empty;
        public string? Stage { get; set; }
        public DateTime? CloseDate { get; set; }
        public string? ArchiveNote { get; set; }
    }
}