using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using smartLaywer.DTO.Documents;
using smartLaywer.Enum;
using smartLaywer.Models;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.InterfaceService;

namespace smartLaywer.Services.ClassService
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private const long MaxFileSizeBytes = 500 * 1024 * 1024;

        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png", ".gif",
            ".xls", ".xlsx", ".txt", ".ppt", ".pptx"
        };

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

 
        public async Task<List<DocumentListDto>> GetAllDocumentsAsync(string? searchTerm = null)
        {
            var query = _unitOfWork.Documents
                .GetAllQueryableNoTracking()
                .Include(d => d.Case)
                .Include(d => d.UploadedByNavigation)
                .Where(d => !d.IsArchived)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                query = query.Where(d =>
                    d.Title.Contains(searchTerm) ||
                    d.Case.Title.Contains(searchTerm) ||
                    (d.Case.CaseNumber != null && d.Case.CaseNumber.Contains(searchTerm)));
            }

            var docs = await query
                .OrderByDescending(d => d.UploadedAt)
                .ToListAsync();

            return docs.Select(d => MapToListDto(d)).ToList();
        }

        public async Task<DocumentListDto?> GetDocumentByIdAsync(int id)
        {
            var doc = await _unitOfWork.Documents
                .GetAllQueryableNoTracking()
                .Include(d => d.Case)
                .Include(d => d.UploadedByNavigation)
                .FirstOrDefaultAsync(d => d.Id == id);

            return doc == null ? null : MapToListDto(doc);
        }
        public async Task<DocumentStatsDto> GetDocumentStatsAsync()
        {
            var docs = await _unitOfWork.Documents
                .GetAllQueryableNoTracking()
                .Where(d => !d.IsArchived)
                .ToListAsync();

            long totalBytes = 0;
            int pdfCount = 0, wordCount = 0, imageCount = 0, otherCount = 0;

            foreach (var d in docs)
            {
                var ext = Path.GetExtension(d.FilePath).ToLower();
                var size = GetFileSizeFromPath(d.FilePath);
                totalBytes += size;

                if (ext == ".pdf") pdfCount++;
                else if (ext is ".doc" or ".docx") wordCount++;
                else if (ext is ".jpg" or ".jpeg" or ".png" or ".gif") imageCount++;
                else otherCount++;
            }

            return new DocumentStatsDto
            {
                Total = docs.Count,
                PdfCount = pdfCount,
                WordCount = wordCount,
                ImageCount = imageCount,
                OtherCount = otherCount,
                TotalSizeBytes = totalBytes,
                TotalSizeDisplay = FormatFileSize(totalBytes)
            };
        }

        public async Task<Document> UploadDocumentAsync(
            DocumentCreateDto dto,
            IBrowserFile file,
            int uploadedByUserId,
            string wwwrootPath)
        {
            if (dto.CaseId <= 0)
                throw new InvalidOperationException("يجب اختيار قضية.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new InvalidOperationException("عنوان المستند مطلوب.");

            if (file == null)
                throw new InvalidOperationException("يجب اختيار ملف.");

            if (file.Size > MaxFileSizeBytes)
                throw new InvalidOperationException($"حجم الملف ({FormatFileSize(file.Size)}) يتجاوز الحد المسموح به (10 MB).");

            var ext = Path.GetExtension(file.Name).ToLower();
            if (!AllowedExtensions.Contains(ext))
                throw new InvalidOperationException($"نوع الملف ({ext}) غير مدعوم. الأنواع المدعومة: {string.Join(", ", AllowedExtensions)}");

            var caseExists = await _unitOfWork.Cases
                .GetAllQueryableNoTracking()
                .AnyAsync(c => c.Id == dto.CaseId);
            if (!caseExists)
                throw new InvalidOperationException("القضية المحددة غير موجودة.");

            var uploadsFolder = Path.Combine(wwwrootPath, "uploads", "documents");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsFolder, uniqueName);

            await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            await file.OpenReadStream(MaxFileSizeBytes).CopyToAsync(stream);

            var mimeType = GetMimeType(ext);

            var relativePath = Path.Combine("uploads", "documents", uniqueName);

            var document = new Document
            {
                CaseId = dto.CaseId,
                Title = dto.Title.Trim(),
                DocType = dto.DocType,
                FilePath = relativePath,
                MimeType = mimeType,
                UploadedBy = uploadedByUserId,
                UploadedAt = DateTime.Now,
                IsArchived = false,
                Notes = dto.Notes?.Trim()
            };

            await _unitOfWork.Documents.AddAsync(document);
            await _unitOfWork.CompleteAsync();

            return document;
        }

        public async Task DeleteDocumentAsync(int id, string wwwrootPath)
        {
            var doc = await _unitOfWork.Documents.GetByIdAsync(id)
                ?? throw new InvalidOperationException("المستند غير موجود.");

            var fullPath = Path.Combine(wwwrootPath, doc.FilePath);
            if (File.Exists(fullPath))
            {
                try { File.Delete(fullPath); }
                catch { }
            }

            await _unitOfWork.Documents.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

 
        public async Task<(byte[] FileBytes, string MimeType, string FileName)> GetFileForDownloadAsync(
            int id, string wwwrootPath)
        {
            var doc = await _unitOfWork.Documents.GetByIdAsync(id)
                ?? throw new InvalidOperationException("المستند غير موجود.");

            var fullPath = Path.Combine(wwwrootPath, doc.FilePath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("الملف غير موجود على الخادم.");

            var bytes = await File.ReadAllBytesAsync(fullPath);
            var fileName = doc.Title + Path.GetExtension(doc.FilePath);

            return (bytes, doc.MimeType, fileName);
        }


        private static DocumentListDto MapToListDto(Document d)
        {
            var ext = Path.GetExtension(d.FilePath).ToLower();
            var size = GetFileSizeFromPath(d.FilePath);
            return new DocumentListDto
            {
                Id = d.Id,
                Title = d.Title,
                DocType = d.DocType,
                DocTypeDisplay = GetDocTypeDisplay(d.DocType),
                FilePath = d.FilePath,
                FileName = Path.GetFileName(d.FilePath),
                MimeType = d.MimeType,
                FileExtension = ext.TrimStart('.').ToUpper(),
                FileSizeBytes = size,
                FileSizeDisplay = FormatFileSize(size),
                UploadedAt = d.UploadedAt,
                UploadedByName = d.UploadedByNavigation?.FullName ?? "غير معروف",
                CaseId = d.CaseId,
                CaseNumber = d.Case?.CaseNumber ?? string.Empty,
                CaseTitle = d.Case?.Title ?? string.Empty,
                Notes = d.Notes,
                IsArchived = d.IsArchived
            };
        }

        private static long GetFileSizeFromPath(string relativePath)
        {
            try
            {
                var info = new FileInfo(relativePath);
                if (info.Exists) return info.Length;
            }
            catch { }
            return 0;
        }

        private static string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
            if (bytes < 1024L * 1024 * 1024) return $"{bytes / (1024.0 * 1024):F1} MB";
            return $"{bytes / (1024.0 * 1024 * 1024):F1} GB";
        }

        private static string GetMimeType(string ext) => ext.ToLower() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".txt" => "text/plain",
            _ => "application/octet-stream"
        };

        private static string GetDocTypeDisplay(DocumentTypeEnum type) => type switch
        {
            DocumentTypeEnum.Memo => "مذكرة",
            DocumentTypeEnum.PowerOfAttorney => "توكيل",
            DocumentTypeEnum.Judgment => "حكم",
            DocumentTypeEnum.Contract => "عقد",
            DocumentTypeEnum.Image => "صورة",
            DocumentTypeEnum.Report => "تقرير",
            DocumentTypeEnum.Receipt => "إيصال",
            DocumentTypeEnum.Other => "أخرى",
            _ => "غير محدد"
        };
    }
}
