using AutoMapper;
using smartLaywer.DTO.HearingDetails;
using smartLaywer.Models;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    public class HearingDetailsService : IHearingDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HearingDetailsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<smartLaywer.DTO.HearingDetails.HearingDetailsDto>> GetAllAsync()
        {
            var cases = await _unitOfWork.Cases
                .GetAllQueryableNoTracking()
                .Include(c => c.Client)
                .Include(c => c.Court)
                .Include(c => c.Dept)
                .Include(c => c.Hearings)
                .ToListAsync();

            return _mapper.Map<List<smartLaywer.DTO.HearingDetails.HearingDetailsDto>>(cases);
        }

        public async Task<smartLaywer.DTO.HearingDetails.HearingDetailsDto?> GetByCaseIdAsync(int caseId)
        {
            var caseEntity = await _unitOfWork.Cases
                .GetCaseWithDetailsAsync(caseId);

            return caseEntity == null
                ? null
                : _mapper.Map<smartLaywer.DTO.HearingDetails.HearingDetailsDto>(caseEntity);
        }
    }
}
