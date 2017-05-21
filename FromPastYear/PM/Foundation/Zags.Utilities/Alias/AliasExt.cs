using System;

namespace Zags.Utilities.Alias
{
    public static class AliasExt
    {
        public static string GetAlias(this Enum e)
        {
            var type = e.GetType();
            var memInfo = type.GetMember(e.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AliasAttribute), false);
            return attributes.Length == 0 ? String.Empty : ((AliasAttribute)attributes[0]).Alias;
        }

        public static bool HasAlias(this Enum e)
        {
            var type = e.GetType();
            var memInfo = type.GetMember(e.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AliasAttribute), false);
            return attributes.Length != 0;
        }
    }
}
