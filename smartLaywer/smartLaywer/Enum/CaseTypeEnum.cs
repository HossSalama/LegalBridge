using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum CaseTypeEnum
    {
        [Display(Name = "„œ‰Ì")]
        Civil = 1,

        [Display(Name = "Ã‰«∆Ì")]
        Criminal = 2,

        [Display(Name = " Ã«—Ì")]
        Commercial = 3,

        [Display(Name = "√”—…")]
        Family = 4,

        [Display(Name = "≈œ«—Ì")]
        Administrative = 5,

        [Display(Name = "√Œ—Ï")]
        Other = 7
    }
}
