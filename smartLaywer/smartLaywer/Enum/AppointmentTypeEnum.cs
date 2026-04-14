using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum AppointmentTypeEnum
    {
        [Display(Name = "اجتماع مع العميل")] ClientMeeting = 1,
        [Display(Name = "موعد تسليم المذكرة")] MemoDeadline = 2,
        [Display(Name = "موعد داخلي")] Internal = 3,
        [Display(Name = "موعد نهائي")] Deadline = 4,
        [Display(Name = "متابعة")] FollowUp = 5,
        [Display(Name = "أخرى")] Other = 6
    }
}
