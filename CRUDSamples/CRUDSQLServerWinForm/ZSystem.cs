using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib
{
    public static class ZSystem
    {
        /// <summary>
        /// 物件是否為 DBNull ?
        /// </summary>
        /// <param name="oField"></param>
        /// <returns></returns>
        public static Boolean ZIsDBNull(this object oField)
        {
            // 判斷 DBNull 的標準寫法
            // Represents a nonexistent value. 
            // 這樣寫也OK!
            //if (v1 is DBNull)
            //    return DefaultValue;
            //if (DBNull.Value.Equals(oField))
            //    return true;
            //else
            //    return false;
            //if (oField == System.DBNull.Value)
            //    return true;
            //else
            //    return false;
            return (oField == DBNull.Value); // CodeHelper.
        }
        /// <summary>
        /// 是否為 null 或 DBNull ?
        /// </summary>
        /// <param name="oInput"></param>
        /// <returns></returns>
        public static Boolean ZIsNullOrDBNull(this object oInput)
        {
            if (oInput == null)
                return true;
            return ZIsDBNull(oInput);
        }


        #region ZFetch, ZFlush

        /// <summary>
        /// 將物件 Cast 轉換為指定的型別. 
        /// 物件若為 DBNull 或 null, 則轉換為 ValueAsDBNull.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsDBNull"></param>
        /// <returns></returns>
        public static Boolean ZFetchBoolean(this object v1, Boolean ValueAsDBNull = false)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (Boolean)v1;
        }


        /// <summary>
        /// 將物件 Cast 轉換為指定的型別. 
        /// 物件若為 DBNull 或 null, 則轉換為 ValueAsDBNull.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        /// <returns></returns>
        public static int ZFetchInt(this object v1, int ValueAsDBNull = -1)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (int)v1;
        }
        /// <summary>
        /// 將物件 Cast 轉換為指定的型別. 
        /// 物件若為 DBNull 或 null, 則轉換為 ValueAsDBNull.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsDBNull"></param>
        /// <returns></returns>
        public static long ZFetchLong(this object v1, long ValueAsDBNull = -1)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (long)v1;
        }


        /// <summary>
        /// 將物件 Cast 轉換為指定的型別. 
        /// 物件若為 DBNull 或 null, 則轉換為 ValueAsDBNull.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        public static DateTime ZFetchDateTime(this object v1, DateTime ValueAsDBNull)
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (DateTime)v1;
        }

        /// <summary>
        /// 將物件 Cast 轉換為 DateTime. 
        /// 物件若為 DBNull 或 null, 則轉換為 DateTime.MinValue.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static DateTime ZFetchDateTime(this object v1)
        {
            if (v1.ZIsNullOrDBNull())
                return DateTime.MinValue;
            return (DateTime)v1;
        }

        /// <summary>
        /// 將物件 Cast 轉換為指定的型別. 
        /// 物件若為 DBNull 或 null, 則轉換為 ValueAsDBNull.
        /// 物件若無法Cast 轉換, 則拋出 exception.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsDBNull"></param>
        /// <returns></returns>
        public static string ZFetchString(this object v1, string ValueAsDBNull = "")
        {
            if (v1.ZIsNullOrDBNull())
                return ValueAsDBNull;
            return (string)v1;
        }


        /// <summary>
        /// 將指定型別轉為物件.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static object ZFlush(this int v1)
        {
            return v1;
        }
        public static object ZFlush(this long v1)
        {
            return v1;
        }
        public static object ZFlush(this string v1)
        {
            return v1;
        }
        public static object ZFlush(this DateTime v1)
        {
            return v1;
        }
        public static object ZFlush(this Boolean v1)
        {
            return v1;
        }

        /// <summary>
        /// 將指定型別轉為物件.
        /// 傳入值若為 False, 則轉換為 DBNull.Value.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static object ZFlush_DBNull(this Boolean v1)
        {
            if (v1 == false)
                return DBNull.Value;
            return v1;
        }

        /// <summary>
        /// 將指定型別轉為物件.
        /// 傳入值若為 ValueAsDBNull, 則轉換為 DBNull.Value.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsDBNull"></param>
        /// <returns></returns>
        public static object ZFlush_DBNull(this int v1, int ValueAsDBNull = 0)
        {
            if (v1 == ValueAsDBNull)
                return DBNull.Value;
            return v1;
        }

        /// <summary>
        /// 將指定型別轉為物件.
        /// 傳入值若為 ValueAsDBNull, 則轉換為 DBNull.Value.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsDBNull"></param>
        /// <returns></returns>
        public static object ZFlush_DBNull(this long v1, long ValueAsDBNull = 0)
        {
            if (v1 == ValueAsDBNull)
                return DBNull.Value;
            return v1;
        }

        /// <summary>
        /// 將指定型別轉為物件.
        /// 傳入值若為 DateTime.MinValue, 則轉換為 DBNull.Value.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static object ZFlush_DBNull(this DateTime v1)
        {
            if (v1 == DateTime.MinValue)
                return DBNull.Value;
            return v1;
        }


        /// <summary>
        /// 將指定型別轉為物件.
        /// 傳入值若為 string.IsNullOrEmpty(), 則轉換為 DBNull.Value.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="EmptyAsDBNull"></param>
        /// <returns></returns>
        public static object ZFlush_DBNull(this string v1)
        {
            if (string.IsNullOrEmpty(v1))
                return DBNull.Value;
            return v1;
        }
        #endregion
    }
}
