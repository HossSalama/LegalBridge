
namespace smartLaywer.DTO.Finance
{
    public class FeeDetailsDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } 
        public decimal CollectedAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - CollectedAmount; 
        public double ProgressPercentage => TotalAmount > 0 ? (double)(CollectedAmount / TotalAmount * 100) : 0; 

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty; 

     
        public List<PaymentLogDto> Payments { get; set; } = new();
    }
}
