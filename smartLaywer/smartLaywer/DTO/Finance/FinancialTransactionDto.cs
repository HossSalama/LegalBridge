namespace smartLaywer.DTO.Finance
{
    public class FinancialTransactionDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}