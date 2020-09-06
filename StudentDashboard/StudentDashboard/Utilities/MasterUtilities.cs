using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Utilities
{
    public static class MasterUtilities
    {
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}