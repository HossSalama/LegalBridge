namespace smartLaywer.DTO.Finance
{
    public class PaymentLogDto
    {
        public string ReceiptNumber { get; set; } = string.Empty; 
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; } = string.Empty; 
    }
}
