using smartLaywer.Repository.UnitWork;

namespace smartLaywer.Services.ClassService
{
    public class HearingService : IHearingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HearingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
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


    }
}
