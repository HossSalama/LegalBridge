using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    // 1. ‰Ê⁄ «·Ã·”…
    public enum HearingTypeEnum
    {
        [Display(Name = "Ã·”…")]
        Hearing = 1,
        [Display(Name = " ÕﬁÌﬁ")] 
        Investigation = 2,
        [Display(Name = "Œ»—…")]
        Expert = 3,
        [Display(Name = "√Œ—Ï")] 
        Other = 4
    }
}
