using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartLaywer.DTO.HearingDetails;

namespace smartLaywer.Services.InterfaceService
{
    public interface IHearingDetailsService
    {
        Task<List<smartLaywer.DTO.HearingDetails.HearingDetailsDto>> GetAllAsync();
        Task<smartLaywer.DTO.HearingDetails.HearingDetailsDto?> GetByCaseIdAsync(int caseId);
    }
}
