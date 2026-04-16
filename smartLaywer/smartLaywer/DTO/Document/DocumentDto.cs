using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Document
{
    public class DocumentDto
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string? ClientName { get; set; }

        public string? CaseNumber { get; set; }

        public DocumentTypeEnum DocType { get; set; }

        public DateTime UploadedAt { get; set; }

        public bool IsArchived { get; set; }

        public DateTime? ArchivedAt { get; set; }

        public string? Notes { get; set; }
    }
}
