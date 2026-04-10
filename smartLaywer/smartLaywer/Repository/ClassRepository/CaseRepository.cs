using smartLaywer.Models;
using smartLaywer.Services;
using smartLaywer.Enum;

namespace smartLaywer.Repository.ClassRepository
{
    public class CaseRepository : GenericRepository<Case>, ICaseRepository
    {
        public CaseRepository(LegalManagementContext context) : base(context)
        {
        }

        public async Task<Case?> GetCaseWithDetailsAsync(int id)
        {
            return await _context.Cases
                .Include(c => c.Client)
                .Include(c => c.CaseType)
                .Include(c => c.Status)
                .Include(c => c.Court)
                .Include(c => c.Dept)
                .Include(c => c.AssignedLawyer)
                .Include(c => c.Hearings)
                    .ThenInclude(h => h.Court)
                .Include(c => c.Hearings)
                    .ThenInclude(h => h.Dept)
                .Include(c => c.Documents)
                    .ThenInclude(d => d.UploadedByNavigation)
                .Include(c => c.CaseLawyers)
                    .ThenInclude(cl => cl.User)
                .Include(c => c.CaseOpponents)
                    .ThenInclude(co => co.Opponent)
                .Include(c => c.Appeals)
                    .ThenInclude(a => a.Court)
                .Include(c => c.Fees)
                    .ThenInclude(f => f.ActualPayments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CaseStatsDto> GetCaseStatsAsync()
        {
            var total = await _context.Cases.CountAsync();

            var active = await _context.Cases
                .CountAsync(c => c.Status.StatusName == CaseStatusEnum.Open);

            var pending = await _context.Cases
                .CountAsync(c => c.Status.StatusName == CaseStatusEnum.Pending);

            var closed = await _context.Cases
                .CountAsync(c =>
                    c.Status.StatusName == CaseStatusEnum.Closed ||
                    c.Status.StatusName == CaseStatusEnum.Won ||
                    c.Status.StatusName == CaseStatusEnum.Lost);

            return new CaseStatsDto
            {
                Total = total,
                Active = active,
                Pending = pending,
                Closed = closed
            };
        }
    }
}
