using smartLaywer.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DeptViewDto>> GetDeptsByCourtIdAsync(int courtId);
    }
}
