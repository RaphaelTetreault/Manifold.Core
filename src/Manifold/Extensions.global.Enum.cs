using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Manifold
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the DescriptionAttribute string of the provided enum.
        /// </summary>
        /// <param name="enum">The enum value to retrieve the description from.</param>
        /// <returns>
        /// Returns the DescriptionAttribute string if present, otherwise returns
        /// enum.ToString().
        /// </returns>
        /// <remarks>
        /// Resource: <see href="https://www.codingame.com/playgrounds/2487/c---how-to-display-friendly-names-for-enumerations"/>
        /// </remarks>
        public static string GetDescription(this Enum @enum)
        {
            Type enumType = @enum.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(@enum.ToString());
            if (!memberInfo.IsNullOrEmpty())
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.IsNullOrEmpty())
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return @enum.ToString();
        }

        /// <summary>
        /// Gets an enumerable collection of flags from the specified enum.
        /// </summary>
        /// <param name="enum">The enum value to retrieve the flags from.</param>
        /// <param name="returnNull">Is true, returns null if flag is not present in <paramref name="enum"/>.</param>
        /// <returns>
        /// Returns an enumerable collection of the specified enum, on for each of flag present.
        /// If flag is not present and <paramref name="returnNull"/> is true, a null is returned instead.
        /// </returns>
        public static IEnumerable<Enum?> GetFlags(this Enum @enum, bool returnNull)
        {
            foreach (Enum flag in Enum.GetValues(@enum.GetType()))
                if (@enum.HasFlag(flag))
                    yield return flag;
                else if (returnNull)
                    yield return null;
        }
    }
}