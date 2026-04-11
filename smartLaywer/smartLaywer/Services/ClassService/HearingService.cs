<<<<<<< HEAD
﻿
using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;
=======
using smartLaywer.Repository.UnitWork;
>>>>>>> e259c2948f6b03c8bb58c86d316812b0abf35c97

namespace smartLaywer.Services.ClassService
{
    public class HearingService : IHearingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public HearingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// ملخص الإحصائيات للهيدر (قادمة / اليوم / مكتملة)
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

        /// <summary>
        /// الجلسات مقسمة لصفحات مع بحث وفلتر
        /// </summary>
        public async Task<PaginatedList<HearingListDto>> GetPagedHearingsAsync(
            string? searchTerm, string? statusFilter, int pageNumber) =>
            await _unitOfWork.Hearing.GetPagedHearingsAsync(searchTerm, statusFilter, pageNumber, PageSize);

        /// <summary>
        /// كل القضايا اللي عندها جلسات (للصفحة التفصيلية)
        /// </summary>
        public async Task<List<CaseHearingsDto>> GetCasesWithHearingsAsync() =>
            await _unitOfWork.Hearing.GetCasesWithHearingsAsync();

        /// <summary>
        /// جلسات قضية واحدة مرتبة
        /// </summary>
        public async Task<List<HearingListDto>> GetCaseHearingsAsync(int caseId) =>
            await _unitOfWork.Hearing.GetCaseHearingsAsync(caseId);

        /// <summary>
        /// إضافة جلسة جديدة
        /// </summary>
        public async Task<bool> CreateHearingAsync(HearingCreateDto dto)
        {
            var hearing = _mapper.Map<Hearing>(dto);
            hearing.CreatedAt = DateTime.Now;
            await _unitOfWork.Hearing.AddAsync(hearing);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}