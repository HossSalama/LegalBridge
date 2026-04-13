using smartLaywer.DTO.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    internal interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();

    }
}
