using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib
{
    public static class ZSystem
    {
        public static int ZToInt(this object v1, int ValueAsDBNull = -1)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (int)v1;
        }
        public static Boolean ZIsNullOrDBNull(this object oInput)
        {
            if (oInput == null)
                return true;
            return ZIsDBNull(oInput);
        }
        public static DateTime ZToDateTime(this object v1, DateTime ValueAsDBNull)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (DateTime)v1;
        }
        public static DateTime ZToDateTime(this object v1)
        {
            if (v1.ZIsNullOrDBNull())
                return DateTime.MinValue;
            return (DateTime)v1;
        }
        public static Boolean ZIsDBNull(this object oField)
        {
            return (oField == DBNull.Value); // CodeHelper.
        }


        #region ZToObject
        public static object ZToObject(this Boolean v1)
        {
            return v1;
        }
        public static object ZToObject(this int v1)
        {
            return v1;
        }
        public static object ZToObject(this long v1)
        {
            return v1;
        }
        public static object ZToObject(this string v1)
        {
            return v1;
        }
        public static object ZToObject(this DateTime v1)
        {
            return v1;
        }
        public static object ZToObject_DBNull(this Boolean v1)
        {
            if (v1 == false)
                return DBNull.Value;
            return v1;
        }
        public static object ZToObject_DBNull(this int v1, int ValueAsDBNull = -1)
        {
            if (v1 == ValueAsDBNull)
                return DBNull.Value;
            return v1;
        }
        public static object ZToObject_DBNull(this long v1, long ValueAsDBNull = -1)
        {
            if (v1 == ValueAsDBNull)
                return DBNull.Value;
            return v1;
        }
        public static object ZToObject_DBNull(this DateTime v1)
        {
            if (v1 == DateTime.MinValue)
                return DBNull.Value;
            return v1;
        }
        public static object ZToObject_DBNull(this string v1)
        {
            if (string.IsNullOrEmpty(v1))
                return DBNull.Value;
            return v1;
        }
        #endregion

    }
}
