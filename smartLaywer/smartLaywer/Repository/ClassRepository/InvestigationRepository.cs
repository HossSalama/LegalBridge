using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.ClassRepository
{
    public class InvestigationRepository : IInvestigationRepository
    {
        private readonly LegalManagementContext _context;

        public InvestigationRepository(LegalManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Case>> GetAllCasesAsync()
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
                    .ThenInclude(h => h.CreatedByNavigation)
                .Include(c => c.Documents)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Case?> GetCaseByIdAsync(int id)
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
                    .ThenInclude(h => h.CreatedByNavigation)
                .Include(c => c.Documents)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
