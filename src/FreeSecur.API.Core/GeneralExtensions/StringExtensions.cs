using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.GeneralExtensions
{
    public static class StringExtensions
    {
        public static List<string> GetMatchedValues(this string input, char open, char close)
        {
            var pattern = @$"(?<!\{open})\{open}(?!\{open})([^{close}]*)\{close}";

            var matches = Regex.Matches(input, pattern);

            var selectorsDistincted = matches
                .Select(x => x.ToString())
                .Distinct();

            return selectorsDistincted.ToList();
        }
    }
}
