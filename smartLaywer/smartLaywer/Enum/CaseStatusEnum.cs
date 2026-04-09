using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Enum
{
    public enum CaseStatusEnum
    {
        [Display(Name = "„› ÊÕ…")]
        Open = 1,

        [Display(Name = "„⁄·ﬁ…")]
        Pending = 2,

        [Display(Name = "„€·ﬁ…")]
        Closed = 3,

        [Display(Name = "«·Œ«”—…")]
        Lost = 4,

        [Display(Name = "«·—«»Õ…")]
        Won = 5,

        [Display(Name = "„ƒ—‘›…")]
        Archived = 6
    }
}
