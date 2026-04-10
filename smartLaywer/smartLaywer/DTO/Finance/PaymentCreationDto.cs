using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Finance
{
    public class PaymentCreationDto
    {
        public int FeeId { get; set; }
        public int? InstallmentId { get; set; } 
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public int PaymentMethod { get; set; } 
        public string? ReceiptNumber { get; set; }
        public string? Notes { get; set; }
    }
}
