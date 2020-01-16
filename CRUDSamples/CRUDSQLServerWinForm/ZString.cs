using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using ZLib;

namespace ZLib
{
    public static class ZString
    {
        /// <summary>
        /// 將 object 物件轉為 string. 
        /// 若為 null 或 DBNull, 則轉為 string.Empty.
        /// </summary>
        /// <returns></returns>
        public static string ZToString(this object v1)
        {
            // 20190912, 取消 Extension, 以免混亂. 並改為 ZGetString()
            // 20191204, 再度改為 extension. 因配合函數已經調整, 應不至於混亂. 並改為ZToString()
            return v1.ZIsNullOrDBNull() ? string.Empty : v1.ToString();
        }

        /// <summary>
        /// 將 int  轉換為 string. 
        /// int資料 若為 ValueAsEmpty, 則轉換為 string.Empty.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsEmpty"></param>
        /// <returns></returns>
        public static string ZToString(this int v1, int ValueAsEmpty = -1)
        {
            if (v1 == ValueAsEmpty)
                return string.Empty;
            return v1.ToString();
        }
        /// <summary>
        /// 將 long 轉換為 string. 
        /// long 資料 若為 ValueAsEmpty, 則轉換為 string.Empty.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="ValueAsEmpty"></param>
        /// <returns></returns>
        public static string ZToString(this long v1, long ValueAsEmpty = -1)
        {
            if (v1 == ValueAsEmpty)
                return string.Empty;
            return v1.ToString();
        }

        ///// <summary>
        ///// 將 Boolean 資料 轉換為 string. 
        ///// Boolean 資料 若為 ValueAsEmpty, 則轉換為 string.Empty.
        ///// </summary>
        ///// <param name="v1"></param>
        ///// <param name="ValueAsEmpty"></param>
        ///// <returns></returns>
        //public static string ZToString(this Boolean v1, Boolean ValueAsEmpty = false)
        //{
        //    if (v1 == ValueAsEmpty)
        //        return string.Empty;
        //    return v1.ToString();
        //}

        ///// <summary>
        ///// 將 DateTime 資料 轉換為 string. 
        ///// DateTime 資料 若為 ValueAsEmpty, 則轉換為 string.Empty.
        ///// </summary>
        ///// <param name="v1"></param>
        ///// <param name="ValueAsEmpty"></param>
        ///// <returns></returns>
        //public static string ZToString(this DateTime v1, DateTime ValueAsEmpty)
        //{
        //    if (v1 == ValueAsEmpty)
        //        return string.Empty;
        //    return v1.ToString();
        //}

        ///// <summary>
        ///// 將 DateTime 資料 轉換為 string. 
        ///// DateTime 資料 若為 DateTime.MinValue, 則轉換為 string.Empty.
        ///// </summary>
        ///// <param name="v1"></param>
        ///// <returns></returns>
        //public static string ZToString(this DateTime v1)
        //{
        //    if (v1 == DateTime.MinValue)
        //        return string.Empty;
        //    return v1.ToString();
        //}
    }
}
