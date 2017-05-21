using System;

namespace Zags.Utilities.Alias
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AliasAttribute : Attribute
    {
        public string Alias { get; set; }

        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
