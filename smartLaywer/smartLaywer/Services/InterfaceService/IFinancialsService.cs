using smartLaywer.DTO.Finance;
using smartLaywer.Helper;

namespace smartLaywer.Service.InterfaceService
{
    public interface IFinancialsService
    {
        // 1. ááãáÎÕ ÇáãÇáí İí ÇáÏÇÔÈæÑÏ
        Task<FinancialStatDto> GetDashboardStatsAsync();

        // 2. áÚÑÖ ÌÏæá ÇáÑÓæã (Fees) ãÚ ÇáÈÍË æÇáÊŞÓíã áÕİÍÇÊ
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string? searchTerm, int pageNumber);

        // 3. áÊÓÌíá ÏİÚÉ ÌÏíÏÉ
        Task<bool> RegisterPaymentAsync(PaymentCreationDto dto);

        // 4. áÍÓÇÈ ÇáÏÎá ÇáãÊæŞÚ (ÇáÊÏİŞ ÇáãÇáí)
        Task<decimal> GetExpectedIncomeAsync(int month, int year);

        // 5. áÍĞİ ÏİÚÉ (İí ÍÇáÉ ÇáÎØÃ ãËáÇğ)
        Task<bool> DeletePaymentAsync(int paymentId);

        // 6. áÌáÈ ÇáÊŞÑíÑ ÇáãÇáí ÇáÔÇãá ááÚãíá (Çááí ÈÊİÊÍå ãä ÒÑÇÑ ÇáÊİÇÕíá)
        Task<ClientFinancialProfileDto> GetClientFullFinancialHistoryAsync(int clientId);

        // 7. áÍÓÇÈ ÅÌãÇáí ÇáãÊÃÎÑÇÊ ÇáÚÇã (ááÊäÈíåÇÊ)
        Task<decimal> GetTotalOverdueAmountAsync();

        // 8. áÊæáíÏ ÑŞã ÇáÅíÕÇá ÊáŞÇÆíÇğ (ÚÔÇä íÙåÑ ááíæÒÑ æåæ ÈíÓÌá ÇáÏİÚÉ)
        Task<string> GenerateNextReceiptNumberAsync();
    }
}
