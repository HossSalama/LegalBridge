using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Cases
{
    public class CaseViewDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = null!;
        public string ClientName { get; set; } = null!;
    }
}
