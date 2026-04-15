using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IInvestigationRepository
    {
        Task<List<Case>> GetAllCasesAsync();
        Task<Case?> GetCaseByIdAsync(int id);
    }
}
