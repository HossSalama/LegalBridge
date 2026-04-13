using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "«·»—Ìœ «·≈·ﬂ —Ê‰Ì „ÿ·Ê»")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "ﬂ·„… «·„—Ê— „ÿ·Ê»…")]
        public string Password { get; set; } = null!;
    }
}
