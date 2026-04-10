using smartLaywer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IFinancialRepository:IGenericRepository<Fee>
    {
        Task<FinancialStatDto> GetFinancialSummaryAsync();
        Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string searchTerm, int pageNumber, int pageSize);
        Task<List<PaymentSchedule>> GetUnpaidSchedulesAsync(int feeId);
    }
}
