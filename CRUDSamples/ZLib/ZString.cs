using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib
{
    public static class ZString
    {
        public static string ZToString(this object v1, string ValueAsDBNull = "")
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;

            return v1.ToString();
        }
        public static string ZToString(this int v1, int ValueAsEmpty = -1)
        {
            if (v1 == ValueAsEmpty)
                return string.Empty;
            return v1.ToString();
        }
        public static string ZToString_MsDash(this DateTime dt, Boolean EmptyAsMinValue = true)
        {
            if (EmptyAsMinValue)
                if (dt == DateTime.MinValue)
                    return string.Empty;
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}
