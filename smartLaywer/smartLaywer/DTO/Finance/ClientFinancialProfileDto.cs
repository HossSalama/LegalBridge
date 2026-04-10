namespace smartLaywer.DTO.Finance
{
    public class ClientFinancialProfileDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } 
        public decimal TotalAgreedAmount { get; set; } 
        public decimal TotalPaid { get; set; }      
        public decimal TotalOverdue { get; set; }    

        public List<CaseFinanceDto> Cases { get; set; } = new();
    }
}
