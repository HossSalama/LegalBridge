using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface ILegalLibraryService
    {
        Task<bool> AddDocumentAsync(IBrowserFile file, LegalLibrary document);
        Task<IEnumerable<LegalLibrary>> GetAllDocumentsAsync();
        string GetFullPath(string relativePath);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
