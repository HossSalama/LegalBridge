namespace smartLaywer.DTO.Clients
{
    public class ClientSummaryDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? SecondaryPhone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ClientType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int ActiveCasesCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}