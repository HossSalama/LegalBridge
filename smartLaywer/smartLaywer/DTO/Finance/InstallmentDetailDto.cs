namespace smartLaywer.DTO.Finance
{
    public class InstallmentDetailDto
    {
        public int Id { get; set; }
        public int InstallmentNumber { get; set; } 
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }   
        public string Status { get; set; } = string.Empty; 
        public string? Notes { get; set; }
    }
}
