using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum LawyerCaseRole
    {
        [Display(Name = "„Õ«„Ì √”«”Ì")]
        Primary = 1,

        [Display(Name = "„Õ«„Ì „”«⁄œ")]
        Assistant = 2
    }
}
