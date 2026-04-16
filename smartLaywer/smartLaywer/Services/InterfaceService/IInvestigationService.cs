using smartLaywer.DTO.Investigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface IInvestigationService
    {
        Task<List<InvestigationDto>> GetInvestigationsAsync();
        Task<InvestigationDto?> GetInvestigationByIdAsync(int id);
        Task AddInvestigationAsync(int caseId, string type, string status, DateTime startDate, string prosecutor, string location);
    }
}
