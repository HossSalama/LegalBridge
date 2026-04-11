using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Hearing
{
    public class HearingDisplayDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty; 
        public string CaseTitle { get; set; } = string.Empty;  
        public string ClientName { get; set; } = string.Empty; 
        public DateTime HearingDateTime { get; set; }        
        public string CourtName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty;     
        public string Result { get; set; } = string.Empty; 
        public string Notes { get; set; } = string.Empty;
    }
}
