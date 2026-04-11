using smartLaywer.Repository.UnitWork;

namespace smartLaywer.Services.ClassService
{
    public class HearingService : IHearingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        const int pageSize = 10;

        public HearingService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }
        public async Task<HearingSummaryDto> GetHearingsSummaryAsync()
        {
            var today = DateTime.Today;
            var now = DateTime.Now;
            var hearingsData = await _unitOfWork.Hearing.GetAllQueryableNoTracking()
                .Select(h => new { h.HearingDateTime, h.AttendanceStatus })
                .ToListAsync();
            return new HearingSummaryDto
            {
                UpcomingHearingsCount = hearingsData.Count(h => h.HearingDateTime > now),

                TodayHearingsCount = hearingsData.Count(h => h.HearingDateTime.Date == today),

                CompletedHearingsCount = hearingsData.Count(h => h.AttendanceStatus == AttendanceStatusEnum.Incoming)
            };
        }

        public async Task<PaginatedList<HearingDisplayDto>> GetHearingsForGridAsync(int pageNumber, string? statusFilter , string? searchTerm)
        {
            return await _unitOfWork.Hearing.GetPagedHearingsAsync(pageNumber, pageSize, statusFilter , searchTerm);
        }
        public async Task<bool> CreateHearingAsync(HearingCreationDto dto)
        {
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

    }
}
