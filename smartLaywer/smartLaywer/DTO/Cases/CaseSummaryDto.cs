namespace smartLaywer.DTO.Case
{
    public class CaseSummaryDto
    {
        public int    Id           { get; set; }
        public string CaseNumber   { get; set; } = string.Empty;
        public string Title        { get; set; } = string.Empty;
        public string ClientName   { get; set; } = string.Empty;
        public string CaseType     { get; set; } = string.Empty;
        public string Status       { get; set; } = string.Empty;
        public string StatusColor  { get; set; } = string.Empty;
        public DateTime OpenDate   { get; set; }
        public DateTime? NextHearingDate { get; set; }
    }
}
