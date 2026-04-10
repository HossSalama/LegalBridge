using smartLaywer.Models;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetAllWithCasesAsync();
        Task<Client?> GetByIdWithCasesAsync(int id);
    }
}