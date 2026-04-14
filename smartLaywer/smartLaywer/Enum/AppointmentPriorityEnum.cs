using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum AppointmentPriorityEnum
    {
        [Display(Name = "عادي")] Normal = 1,
        [Display(Name = "مرتفع")] High = 2,
        [Display(Name = "عاجل")] Urgent = 3
    }
}
