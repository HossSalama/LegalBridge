namespace smartLaywer.Service.InterfaceService
{
    public interface IFinancialsService
    {
        Task<ScheduleValidationResult> AddInstallmentToScheduleAsync(PaymentSchedule newInstallment);
        Task<bool> SaveSchedulesAsync(CreateSchedulesDto dto);
        Task<FinancialStatDto> GetDashboardStatsAsync();
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string? searchTerm, int pageNumber);

        Task<bool> CollectPaymentAsync(int feeId, decimal amount, PaymentMethodEnum method, int currentUserId);



        Task<bool> RegisterPaymentAsync(PaymentCreationDto dto);
        Task<decimal> GetExpectedIncomeAsync(int month, int year);
        Task<bool> DeletePaymentAsync(int paymentId);
        Task<ClientFinancialProfileDto> GetClientFullFinancialHistoryAsync(int clientId);
        Task<decimal> GetTotalOverdueAmountAsync();
        Task<string> GenerateNextReceiptNumberAsync();
        Task<bool> CreatePaymentSchedulesAsync(int feeId, List<InstallmentCreationDto> newSchedules);
        Task<List<RevenueSummaryDto>> GetUpcomingRevenueAsync();
    }
}
