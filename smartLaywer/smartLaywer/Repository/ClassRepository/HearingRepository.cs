namespace smartLaywer.Repository.ClassRepository
{
    public class HearingRepository : GenericRepository<Hearing>, IHearingRepository
    {
        private readonly LegalManagementContext _context;
        private readonly IMapper _mapper;
        public HearingRepository(LegalManagementContext context , IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<PaginatedList<HearingDisplayDto>> GetPagedHearingsAsync(
          int pageNumber,
          int pageSize,
          string? statusFilter,
          string? searchTerm) 
        {
            var query = _context.Hearings.AsNoTracking();
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "ÌãíÚ ÇáÌáÓÇÊ")
            {
                query = statusFilter switch
                {
                    "ÞÇÏãÉ" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Incoming),
                    "ãßÊãáÉ" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Attended),
                    "ãáÛíÉ" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Absent),
                    _ => query
                };
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                query = query.Where(h => h.Case.CaseNumber.Contains(searchTerm)
                                      || h.Case.Client.FullName.Contains(searchTerm));
            }

            return await query
                .OrderByDescending(h => h.HearingDateTime)
                .ProjectTo<HearingDisplayDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
    }
}
