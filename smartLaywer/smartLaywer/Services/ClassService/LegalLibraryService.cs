using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.ClassService
{
    public class LegalLibraryService : ILegalLibraryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _uploadPath;

        public LegalLibraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _uploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "Uploads", "LegalLibrary");
        }
        public async Task<bool> AddDocumentAsync(IBrowserFile file, LegalLibrary document)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
            var fullPath = Path.Combine(_uploadPath, document.Category.ToString(), fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using var stream = file.OpenReadStream(10 * 1024 * 1024);
            using var fs = new FileStream(fullPath, FileMode.Create);
            await stream.CopyToAsync(fs);

            document.FilePath = Path.Combine("Uploads", "LegalLibrary", document.Category.ToString(), fileName);
            document.MimeType = file.ContentType;

            if (string.IsNullOrEmpty(document.Title)) document.Title = file.Name;

            await _unitOfWork.LegalLibraries.AddAsync(document);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0;
        }

        public async Task<IEnumerable<LegalLibrary>> GetAllDocumentsAsync()
        {
            return await _unitOfWork.LegalLibraries.GetAllQueryableNoTracking().Include(d => d.AddedByNavigation).Order().ToListAsync();
        }

        public string GetFullPath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", relativePath);
        }
        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _unitOfWork.LegalLibraries.GetByIdAsync(id);

            if (document == null) return false;

            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", document.FilePath);

            try
            {
                _unitOfWork.LegalLibraries.Delete(document.Id);
                var result = await _unitOfWork.CompleteAsync();

                if (result > 0 && File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    }
