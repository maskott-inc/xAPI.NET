using System;
using System.Linq;
using System.Runtime.Serialization;

namespace xAPI.Client.Utils
{
    internal static class EnumHelper
    {
        public static string ToEnumString<T>(T type)
        {
            Type enumType = typeof(T);
            string name = Enum.GetName(enumType, type);
            EnumMemberAttribute enumMemberAttribute = (EnumMemberAttribute)enumType
                .GetField(name)
                .GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)
                .FirstOrDefault();
            return enumMemberAttribute?.Value ?? name;
        }
    }
}
