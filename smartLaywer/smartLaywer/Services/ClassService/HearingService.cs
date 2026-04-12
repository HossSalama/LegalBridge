
using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;
using smartLaywer.Repository.UnitWork;

namespace smartLaywer.Services.ClassService
{
    public class HearingService : IHearingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        const int pageSize = 10;


        public HearingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// „·Œ’ «·≈Õ’«∆Ì«  ··ÂÌœ— (ﬁ«œ„… / «·ÌÊ„ / „ﬂ „·…)
        /// </summary>
        public async Task<HearingSummaryDto> GetHearingsSummaryAsync()
        {
            var today = DateTime.Today;
            var now = DateTime.Now;

            var data = await _unitOfWork.Hearing
                .GetAllQueryableNoTracking()
                .Select(h => new { h.HearingDateTime, h.AttendanceStatus })
                .ToListAsync();

            return new HearingSummaryDto
            {
                UpcomingHearingsCount = data.Count(h => h.HearingDateTime > now),
                TodayHearingsCount = data.Count(h => h.HearingDateTime.Date == today),
                CompletedHearingsCount = data.Count(h => h.AttendanceStatus == AttendanceStatusEnum.Incoming)
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
        /// <summary>
        /// «·Ã·”«  „ﬁ”„… ·’›Õ«  „⁄ »ÕÀ Ê›· —
        /// </summary>
        //public async Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
        //    string? searchTerm, string? statusFilter, int pageNumber) =>
        //    await _unitOfWork.Hearing.GetPagedHearingsAsync(searchTerm, statusFilter, pageNumber, PageSize);

        ///// <summary>
        ///// ﬂ· «·ﬁ÷«Ì« «··Ì ⁄‰œÂ« Ã·”«  (··’›Õ… «· ›’Ì·Ì…)
        ///// </summary>
        //public async Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync() =>
        //    await _unitOfWork.Hearing.GetCasesWithHearingsAsync();

        ///// <summary>
        ///// Ã·”«  ﬁ÷Ì… Ê«Õœ… „— »…
        ///// </summary>
        //public async Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId) =>
        //    await _unitOfWork.Hearing.GetCaseHearingsAsync(caseId);

        ///// <summary>
        ///// ≈÷«›… Ã·”… ÃœÌœ…
        ///// </summary>
        //public async Task<bool> CreateHearingAsync(HearingCreateDto dto)
        //{
        //    var hearing = _mapper.Map<Hearing>(dto);
        //    hearing.CreatedAt = DateTime.Now;
        //    await _unitOfWork.Hearing.AddAsync(hearing);
        //    return await _unitOfWork.CompleteAsync() > 0;
        //}
    }
}