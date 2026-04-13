using smartLaywer.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface IReportService
    {
        Task<ReportDashboardDto> GetReportDashboardAsync();
    }
}
