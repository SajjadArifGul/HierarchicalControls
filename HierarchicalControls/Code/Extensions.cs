using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HierarchicalControls.Code
{
    public static class Extensions
    {

        public static dynamic GetValue(this object value)
        {
            return Convert.ChangeType(value, value.GetType());
        }

        public static string IfNullShowAlternative(this string str, string alternativeStr)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return alternativeStr;
            }
            else
            {
                return str;
            }
        }
    }
}