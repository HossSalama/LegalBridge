
using Microsoft.AspNetCore.Components.Authorization;
using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;
using smartLaywer.Repository.UnitWork;
using System.Security.Claims;

namespace smartLaywer.Services.ClassService
{
    public class HearingService : IHearingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        const int pageSize = 10;
        private readonly AuthenticationStateProvider _authStateProvider;

        public HearingService(IUnitOfWork unitOfWork, IMapper mapper , AuthenticationStateProvider authenticationStateProvider )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authStateProvider = authenticationStateProvider;
        }

        public async Task<HearingSummaryDto> GetHearingsSummaryAsync()
        {
            var today = DateTime.Today;
            var now = DateTime.Now;

            var query = _unitOfWork.Hearing.GetAllQueryableNoTracking();

            return new HearingSummaryDto
            {
                UpcomingHearingsCount = await query.CountAsync(h => h.HearingDateTime > now),
                TodayHearingsCount = await query.CountAsync(h => h.HearingDateTime.Date == today),
                CompletedHearingsCount = await query.CountAsync(h => h.AttendanceStatus == AttendanceStatusEnum.Attended)
            };
        }

        public async Task<PaginatedList<HearingDisplayDto>> GetHearingsForGridAsync(int pageNumber, string? statusFilter , string? searchTerm) =>
            await _unitOfWork.Hearing.GetPagedHearingsAsync(pageNumber, pageSize, statusFilter , searchTerm);

        public async Task<bool> CreateHearingAsync(HearingCreationDto dto)
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
                dto.CreatedBy = userId;

            var hearing = _mapper.Map<Hearing>(dto);
           
            await _unitOfWork.Hearing.AddAsync(hearing);

            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task<bool> UpdateHearingAndScheduleNextAsync(UpdateHearingResultDto dto)
        {
            var currentHearing = await _unitOfWork.Hearing.GetByIdAsync(dto.Id);
            if (currentHearing == null) return false;

            _mapper.Map(dto, currentHearing);
            if (dto.NextHearingDate.HasValue)
            {
                var nextHearing = _mapper.Map<Hearing>(currentHearing);

                nextHearing.HearingDateTime = dto.NextHearingDate.Value;
                nextHearing.Period = dto.NextHearingPeriod ?? currentHearing.Period;
                await _unitOfWork.Hearing.AddAsync(nextHearing);
            }

            return await _unitOfWork.CompleteAsync() > 0;
        }


        public async Task<HearingDetailsDto?> GetHearingDetailsAsync(int id)
        {
            var hearing = await _unitOfWork.Hearing.GetAllQueryableNoTracking()
                .Include(h => h.Case)
                    .ThenInclude(c => c.Client)
                .Include(h => h.Court)
                .Include(h => h.Dept)
                .Include(h => h.CreatedByNavigation)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hearing == null) return null;

            return _mapper.Map<HearingDetailsDto>(hearing);
        }

    }
}