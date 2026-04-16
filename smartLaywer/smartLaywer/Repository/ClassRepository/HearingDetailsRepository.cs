using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using smartLaywer.Models;

namespace smartLaywer.Repository.ClassRepository
{
    public class HearingDetailsRepository : GenericRepository<Hearing>, IHearingDetailsRepository
    {
        private readonly LegalManagementContext _context;

        public HearingDetailsRepository(LegalManagementContext context) : base(context)
        {
            _context = context;
        }

        // تنفيذ الدالة الناقصة
        public async Task<Case?> GetHearingDetailsAsync(int caseId)
        {
            return await _context.Cases
                .Include(c => c.Hearings) // بنجيب كل الجلسات (مراحل الجلسة) المرتبطة بالقضية
                .Include(c => c.Client) // لو محتاج اسم العميل
                .FirstOrDefaultAsync(c => c.Id == caseId);
        }
    }
}
