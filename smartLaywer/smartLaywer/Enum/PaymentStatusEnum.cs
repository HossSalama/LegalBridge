using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum PaymentStatusEnum
    {
        [Display(Name = "„⁄·ﬁ")] Pending = 1,
        [Display(Name = "„œ›Ê⁄")] Paid = 2,
        [Display(Name = "„ √Œ—")] Overdue = 3,
        [Display(Name = "„·€Ì")] Cancelled = 4
    }
}
