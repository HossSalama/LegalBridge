using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.ClassRepository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly LegalManagementContext _context;

        public DocumentRepository(LegalManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Document doc)
        {
            await _context.Documents.AddAsync(doc);
        }
        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(d => d.Case)
                .ThenInclude(c => c.Client)
                .ToListAsync();
        }
    }
}
