using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Repository.InterfaceRepository
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document doc);
        Task<List<Document>> GetAllAsync();
    }
}
