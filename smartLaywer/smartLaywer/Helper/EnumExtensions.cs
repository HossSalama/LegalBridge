using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace smartLaywer.Helper
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the [Display(Name = "...")] Arabic label for any enum value.
        /// Falls back to the enum member name if no attribute is present.
        /// Usage: myEnum.GetDisplayName()
        /// </summary>
        public static string GetDisplayName(this System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field is null) return value.ToString();

            var attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? value.ToString();
        }
    }
}
