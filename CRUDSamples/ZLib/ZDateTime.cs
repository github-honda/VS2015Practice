using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Globalization;

namespace ZLib
{
    public static class ZDateTime
    {
        public static Boolean ZTryParseExact(string sValue, string sFormat, out DateTime t1)
        {
            return DateTime.TryParseExact(sValue, sFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out t1); // CodeHelper
        }

        static public Boolean ZParseTime(this string sDateTime, string sFormat, out DateTime t1)
        {
            try
            {
                return ZTryParseExact(sDateTime, sFormat, out t1);
            }
            catch
            {
                t1 = DateTime.MinValue;
                return false;
            }
        }
        static public Boolean ZParseTime_MsDash(this string sDateTime, out DateTime t1)
        {
            return sDateTime.ZParseTime("yyyy-MM-dd HH:mm:ss.fff", out t1);
        }
    }
}
