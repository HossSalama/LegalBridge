using smartLaywer.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.User
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "íÌÈ ÅÏÎÇá ÇáÇÓã ÈÇáßÇãá")]
        [StringLength(100, ErrorMessage = "ÇáÇÓã Øæíá ÌÏÇğ")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "ÇáÈÑíÏ ÇáÅáßÊÑæäí ãØáæÈ")]
        [EmailAddress(ErrorMessage = "ÕíÛÉ ÇáÈÑíÏ ÇáÅáßÊÑæäí ÛíÑ ÕÍíÍÉ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "íÌÈ ÊÚííä ßáãÉ ãÑæÑ ãÄŞÊÉ")]
        [MinLength(6, ErrorMessage = "ßáãÉ ÇáãÑæÑ íÌÈ ÃáÇ ÊŞá Úä 6 ÃÍÑİ")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "ÑŞã ÇáåÇÊİ ãØáæÈ")]
        [Phone(ErrorMessage = "ÑŞã ÇáåÇÊİ ÛíÑ ÕÍíÍ")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "ÑŞã ÇáåÇÊİ ÇáãÕÑí ÛíÑ ÕÍíÍ")]
        public string PhoneNumber { get; set; } = null!;

        public string? SecondNumber { get; set; }

        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "ÇáÑŞã ÇáŞæãí íÌÈ Ãä íßæä 14 ÑŞã")]
        public string? NationalId { get; set; }

        [Required(ErrorMessage = "íÌÈ ÊÍÏíÏ ÕáÇÍíÉ ÇáãÓÊÎÏã")]
        public UserRole Role { get; set; }

        public int RoleId { get; set; }
    }
}
