using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.HearingDetails
{
    public class HearingDetailsDto
    {
        public int CaseId { get; set; }

        public string CaseNumber { get; set; }
        public string Title { get; set; }

        public string ClientName { get; set; }
        public string Court { get; set; }
        public string Department { get; set; }

        public List<HearingItemDto> Hearings { get; set; } = new();
    }
}
