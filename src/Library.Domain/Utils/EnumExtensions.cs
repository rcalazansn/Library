using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Library.Domain.Utils
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            var displayName = enumValue.GetType().GetField(enumValue.ToString())?.GetCustomAttribute<DisplayAttribute>()?.GetName();

            if (String.IsNullOrEmpty(displayName))
                displayName = enumValue.ToString();

            return displayName;
        }
    }
}