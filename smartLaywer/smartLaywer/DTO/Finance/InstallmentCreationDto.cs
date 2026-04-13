using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Finance
{
    public class InstallmentCreationDto
    {
        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public string? Note { get; set; }
    }
}
