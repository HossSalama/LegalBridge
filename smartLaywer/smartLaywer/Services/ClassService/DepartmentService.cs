using smartLaywer.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DeptViewDto>> GetDeptsByCourtIdAsync(int courtId)
        {
            var departments = await _unitOfWork.Departments
                .GetAllQueryableNoTracking()
                .Where(d => d.CourtId == courtId) 
                .Select(d => new DeptViewDto
                {
                    Id = d.Id,
                    Name = d.DeptName
                })
                .ToListAsync();

            return departments;
        }
    }
}
