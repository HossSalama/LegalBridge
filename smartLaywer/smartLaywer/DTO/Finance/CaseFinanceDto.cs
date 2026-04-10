

namespace smartLaywer.DTO.Finance
{
    public class CaseFinanceDto
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public decimal CaseTotalFee { get; set; }

        public List<FinancialTransactionDto> Transactions { get; set; } = new();

        public List<InstallmentDetailDto> Installments { get; set; } = new();
    }
}
