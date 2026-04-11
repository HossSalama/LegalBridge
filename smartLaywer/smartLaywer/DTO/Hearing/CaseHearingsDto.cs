using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class CaseHearingsDto
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string CaseTitle { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string CourtName { get; set; } = string.Empty;
        public string CaseType { get; set; } = string.Empty;
        public List<HearingListDto> Hearings { get; set; } = new();
    }
}

