using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Investigation
{
    public class EvidenceDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string DateCollected { get; set; }
        public string Status { get; set; }
    }
}
