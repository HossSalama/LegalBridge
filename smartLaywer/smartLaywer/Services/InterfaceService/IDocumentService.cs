using smartLaywer.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface IDocumentService
    {
        Task<string> UploadDocumentAsync(UploadDocumentDto dto);
        Task<List<DocumentDto>> GetAllAsync();
    }
}
