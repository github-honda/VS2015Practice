using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Globalization;
using ZLib;

namespace ZLib
{
    public static class ZDateTime
    {

        /// <summary>
        /// 將指定格式字串, 例如yyyyMMddHHmmssfff, 轉為 DateTime.
        /// 若轉換失敗, 則拋出Exception.
        /// 這是 ZLib 將字串轉為 DateTime 的最基礎函數, 不受系統設定的時間格式影響.
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="sFormat"></param>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static Boolean ZTryParseExact(string sValue, string sFormat, out DateTime t1)
        {
            // DateTime.TryParseExact 是會拋出exception的!
            return DateTime.TryParseExact(sValue, sFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out t1); // CodeHelper
        }

        /// <summary>
        /// 將指定格式字串, 例如yyyyMMddHHmmssfff, 轉為 DateTime.
        /// 若轉換失敗, 則回傳 DateTime.MinValue, 不會拋出Exception.
        /// 這是 ZLib 將字串轉為 DateTime 的最基礎函數, 不受系統設定的時間格式影響.
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <param name="sFormat"></param>
        /// <returns></returns>
        static public DateTime ZParseTime(this string sDateTime, string sFormat)
        {
            try
            {
                DateTime t1;
                ZTryParseExact(sDateTime, sFormat, out t1);
                return t1;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// 將指定格式字串, 例如yyyyMMddHHmmssfff, 轉為 DateTime.
        /// 若轉換失敗, 則回傳 false 及 DateTime.MinValue, 不會拋出Exception.
        /// 這是 ZLib 將字串轉為 DateTime 的最基礎函數, 不受系統設定的時間格式影響.
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <param name="sFormat"></param>
        /// <param name="t1"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 將 yyyy-MM-dd HH:mm:ss.fff 格式字串, 轉為 DateTime.
        /// 若轉換失敗, 則回傳 DateTime.MinValue.
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <returns></returns>
        static public DateTime ZParseTime_MsDash(this string sDateTime)
        {
            return sDateTime.ZParseTime("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 將 yyyy-MM-dd HH:mm:ss.fff 格式字串, 轉為 DateTime.
        /// 若轉換失敗, 則回傳 false 及 DateTime.MinValue.
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <param name="t1"></param>
        /// <returns></returns>
        static public Boolean ZParseTime_MsDash(this string sDateTime, out DateTime t1)
        {
            return sDateTime.ZParseTime("yyyy-MM-dd HH:mm:ss.fff", out t1);
        }

        /// <summary>
        /// 回傳格式為 yyyy-MM-dd HH:mm:ss.fff 的時間字串.
        /// 若 EmptyAsMinValue = true, 則當傳入 DateTime.MinValue 時, 回傳 string.Empty.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="EmptyAsMinValue"></param>
        /// <returns></returns>
        public static string ZToString_MsDash(this DateTime dt, Boolean EmptyAsMinValue = true)
        {
            if (EmptyAsMinValue)
                if (dt == DateTime.MinValue)
                    return string.Empty;
            // 回傳格式為 yyyy-MM-dd HH:mm:ss:fff 的時間字串. 同SQLServer格式.
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

    }
}
