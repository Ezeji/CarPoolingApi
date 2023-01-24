using CarPoolingCore.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Helpers
{
    public static class TextHelpers
    {
        public static DateTime FormatDate(string? date)
        {
            string[] formats = new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy",
                "d-M-yyyy", "d-MM-yyyy", "dd-M-yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "yyyy/M/d", "yyyy/MM/d", "yyyy/M/dd",
                "yyyy-M-d", "yyyy-MM-d", "yyyy-M-dd" };

            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime formattedDate))
            {
                return formattedDate;
            }

            return Convert.ToDateTime("2000-01-01");
        }

        public static string RandomNumberString(int characters = 6)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string code = string.Empty;

            string chars = ServiceConstants.NumberSeed;
            code = new string(Enumerable.Repeat(chars, characters).Select(s => s[random.Next(s.Length)]).ToArray());

            return code;
        }

    }
}
