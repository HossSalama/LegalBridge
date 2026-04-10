namespace smartLaywer.DTO.Finance
{
    public class FinancialStatDto
    {
        public decimal TotalCollected { get; set; }
        public decimal TotalOutstanding { get; set; }
        public decimal TotalOverdue { get; set; }
        public int FullyPaidCount { get; set; }
    }
}
