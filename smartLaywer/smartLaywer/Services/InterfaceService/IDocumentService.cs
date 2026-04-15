using smartLaywer.DTO.Documents;
using smartLaywer.Models;
using Microsoft.AspNetCore.Components.Forms;

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
    }
}
