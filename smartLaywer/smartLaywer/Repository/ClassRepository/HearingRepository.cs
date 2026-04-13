using AutoMapper.QueryableExtensions;
using smartLaywer.DTO.Hearing;
using smartLaywer.Helper;

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
            var query = _context.Hearings
                .Include(h => h.Case)
                    .ThenInclude(c => c.Client) 
                .Include(h => h.Court)
                .Include(h => h.Dept)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(statusFilter) && statusFilter.Trim() != "Çáßá")
            {
                query = statusFilter.Trim() switch
                {
                    "ÞÇÏã" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Incoming),
                    "ãßÊãáå" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Attended),
                    "ÛÇÆÈ" => query.Where(h => h.AttendanceStatus == AttendanceStatusEnum.Absent),
                    _ => query
                };
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                query = query.Where(h => h.Case.CaseNumber.Contains(searchTerm)
                                        || h.Case.Client.FullName.Contains(searchTerm));
            }

            var pagedEntities = await query
                .OrderByDescending(h => h.HearingDateTime)
                .ToPaginatedListAsync(pageNumber, pageSize);

            var dtos = _mapper.Map<List<HearingDisplayDto>>(pagedEntities.Items);

            return new PaginatedList<HearingDisplayDto>(
                dtos,
                pagedEntities.TotalCount,
                pageNumber,
                pageSize);
        }
        
    }
}
