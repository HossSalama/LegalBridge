using smartLaywer.DTO.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface ICourtService
    {
        Task<IEnumerable<CourtViewDto>> GetAllCourtsAsync();
    }
}
