using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IHearingDetailsRepository : IGenericRepository<Hearing>
    {
        Task<Case?> GetHearingDetailsAsync(int caseId);

    }
}
