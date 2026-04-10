using Microsoft.EntityFrameworkCore;
using smartLaywer.Models;
using smartLaywer.Repository.InterfaceRepository;

namespace smartLaywer.Repository.ClassRepository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly LegalManagementContext _context;

        public ClientRepository(LegalManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllWithCasesAsync()
        {
            return await _context.Clients
                .Include(c => c.Cases)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdWithCasesAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Cases)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}