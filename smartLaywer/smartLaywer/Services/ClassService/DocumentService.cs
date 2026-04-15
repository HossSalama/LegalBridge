using smartLaywer.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unit;

        public DocumentService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<List<DocumentDto>> GetAllAsync()
        {
            var docs = await _unit.Document.GetAllAsync();

            return docs.Select(d => new DocumentDto
            {
                Id = d.Id,
                FilePath = d.FilePath,
                ClientName = d.Case?.Client?.FullName ?? "—",
                CaseNumber = d.Case?.CaseNumber ?? "—",
                DocType = d.DocType,
                UploadedAt = d.UploadedAt,
                IsArchived = d.IsArchived,
                ArchivedAt = d.ArchivedAt,
                Notes = d.Notes
            }).ToList();
        }
        public async Task<string> UploadDocumentAsync(UploadDocumentDto dto)
        {
            if (dto.FileStream == null)
                throw new Exception("Invalid file");

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.FileName);
            // تأكد من استخدام Path.Combine بشكل صحيح للتعامل مع الـ Root
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "poa");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                // يفضل استخدام CopyToAsync مباشرة
                await dto.FileStream.CopyToAsync(stream);
            }

            var caseId = (await _unit.Cases.GetAllAsync()).FirstOrDefault()?.Id;
            var userId = (await _unit.Users.GetAllAsync()).FirstOrDefault()?.Id;

            if (caseId == null || userId == null)
                throw new Exception("Case or User not found");

            var doc = new Document
            {
                Title = dto.FileName,
                FilePath = "/uploads/poa/" + fileName,
                MimeType = dto.ContentType,
                UploadedAt = DateTime.Now,
                CaseId = caseId.Value,
                UploadedBy = userId.Value,
                DocType = DocumentTypeEnum.PowerOfAttorney
            };

            await _unit.Document.AddAsync(doc);
            await _unit.CompleteAsync();


            return doc.FilePath;
        }
    }
}
