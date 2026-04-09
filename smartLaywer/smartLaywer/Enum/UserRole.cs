using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.NewFolder
{
    public enum UserRole
    {
        [Display(Name = "„œÌ— «·‰Ÿ«„")]
        Admin = 1,

        [Display(Name = "„Õ«„Ì")]
        Lawyer = 2,

        [Display(Name = "”ﬂ— «—Ì…")]
        Secretary = 3
    }
}
