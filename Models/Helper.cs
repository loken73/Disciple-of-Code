using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D_of_C_Blog.Models
{
    public class Helpers
    {
        public readonly string MyPassword = "frank12345@!";

        public static readonly string GPassword = "fr3448687";

        public static string[] ExtractAllTags(string s)
        {
            string [] comma = new string [] {","};

            string [] separated;

            separated = s.Split(comma, StringSplitOptions.RemoveEmptyEntries);

            return separated;
        }
    }
}
