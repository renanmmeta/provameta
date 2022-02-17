using System;

namespace ProvaMeta.Extensions
{
    public static class ProvaMetaStringExtensions
    {
        public static int GetValueOrDefault(this string stringValue, int defaultValue)
        {
            if (String.IsNullOrWhiteSpace(stringValue))
            {
                return defaultValue;
            }
            else
            {
                return Convert.ToInt32(stringValue);
            }
        }
    }
}
