using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Investigation
{
    public class InvestigationDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; }
        public string ClientName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string Prosecutor { get; set; }
        public string Location { get; set; }

        public List<SessionDto> Sessions { get; set; } = new();
        public List<WitnessDto> Witnesses { get; set; } = new();
        public List<EvidenceDto> Evidence { get; set; } = new();
    }
}
