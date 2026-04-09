using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum AppealTypeEnum
    {
        [Display(Name = "«” ∆‰«›")] Appeal = 1,
        [Display(Name = "ÿ⁄‰ »«·‰ﬁ÷")] Cassation = 2,
        [Display(Name = "≈⁄«œ… ‰Ÿ—")] Reconsideration = 3,
        [Display(Name = "√Œ—Ï")] Other = 4
    }
}
