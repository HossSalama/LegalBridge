using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.User
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? SecondNumber { get; set; }
        public string? NationalId { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }

        public List<CaseSummaryDTO> AssignedCases { get; set; } = new();
    }
    public class CaseSummaryDTO
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ClientName { get; set; } = null!; 
        public string CaseType { get; set; } = null!;   
    }
}
