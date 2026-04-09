using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum NoteTypeEnum
    {
        [Display(Name = "⁄«„…")] General = 1,
        [Display(Name = "ﬁ«‰Ê‰Ì…")] Legal = 2,
        [Display(Name = "œ«Œ·Ì…")] Internal = 3,
        [Display(Name = " Õ–Ì—")] Warning = 4,
        [Display(Name = "„ «»⁄…")] FollowUp = 5
    }
}
