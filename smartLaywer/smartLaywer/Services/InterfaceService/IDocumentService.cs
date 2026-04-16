using Microsoft.AspNetCore.Components.Forms;
using smartLaywer.DTO.Document;
﻿using smartLaywer.DTO.Documents;
using smartLaywer.Models;

namespace smartLaywer.Services.InterfaceService
{
    public interface IDocumentService
    {
        Task<List<DocumentListDto>> GetAllDocumentsAsync(string? searchTerm = null);
        Task<DocumentListDto?> GetDocumentByIdAsync(int id);
        Task<DocumentStatsDto> GetDocumentStatsAsync();
        Task<Document> UploadDocumentAsync(DocumentCreateDto dto, IBrowserFile file, int uploadedByUserId, string wwwrootPath);
        Task DeleteDocumentAsync(int id, string wwwrootPath);
        Task<(byte[] FileBytes, string MimeType, string FileName)> GetFileForDownloadAsync(int id, string wwwrootPath);
        Task<string> UploadDocumentAsync(UploadDocumentDto dto);
                Task<List<DocumentDto>> GetAllAsync();
    }
}
//=======
//﻿using smartLaywer.DTO.Document;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace smartLaywer.Services.InterfaceService
//{
//    public interface IDocumentService
//    {
//        Task<string> UploadDocumentAsync(UploadDocumentDto dto);
//        Task<List<DocumentDto>> GetAllAsync();
//    }
//}
//>>>>>>> 4b45134b5934d36d05d4a158da3ceae6c758dda6
