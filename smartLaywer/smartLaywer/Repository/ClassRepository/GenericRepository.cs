


global using smartLaywer.Repository.InterfaceRepository;
global using smartLaywer.Models;

namespace smartLaywer.Repository.ClassRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LegalManagementContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(LegalManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public IQueryable<T> GetAllQueryable() => _dbSet.AsNoTracking().AsQueryable();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public async Task Delete(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item != null)
                _dbSet.Remove(item);
        }
    }
}
