using smartLaywer.DTO.Court;
using System;
using System.Collections.Generic;
using System.Linq;

namespace smartLaywer.Services.ClassService
{
    public class CourtService :ICourtService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourtService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<IEnumerable<CourtViewDto>> GetAllCourtsAsync()
        {
            var courts = await _unitOfWork.Courts
                .GetAllQueryableNoTracking()
                .Select(c => new CourtViewDto
                {
                    Id = c.Id,
                    Name = c.CourtName
                })
                .ToListAsync();
            return courts;
        }
    }
}
