namespace smartLaywer.Service.InterfaceService
{
    public interface IFinancialsService
    {
        Task<FinancialStatDto> GetDashboardStatsAsync();
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string? searchTerm, int pageNumber);
        Task<bool> RegisterPaymentAsync(PaymentCreationDto dto);
        Task<decimal> GetExpectedIncomeAsync(int month, int year);
        Task<bool> DeletePaymentAsync(int paymentId);
        Task<ClientFinancialProfileDto> GetClientFullFinancialHistoryAsync(int clientId);
        Task<decimal> GetTotalOverdueAmountAsync();
        Task<string> GenerateNextReceiptNumberAsync();
    }
}
