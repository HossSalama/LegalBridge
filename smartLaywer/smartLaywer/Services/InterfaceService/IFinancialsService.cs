namespace smartLaywer.Service.InterfaceService
{
    public interface IFinancialsService
    {
        Task<ScheduleValidationResult> AddInstallmentToScheduleAsync(PaymentSchedule newInstallment);
        Task<bool> SaveSchedulesAsync(CreateSchedulesDto dto);
        Task<FinancialStatDto> GetDashboardStatsAsync();
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string? searchTerm, int pageNumber);

        Task<bool> CollectPaymentAsync(int feeId, decimal amount, PaymentMethodEnum method, int currentUserId);



    }
}
