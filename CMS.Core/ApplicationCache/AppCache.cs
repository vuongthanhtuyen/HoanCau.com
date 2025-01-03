using TBDCMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace CMS.Core.ApplicationCache
{
    public class AppCache
    {
        #region Constructors
        private AppCache() { }

        #endregion
        //>> Based on Factor = 5 default value
        public static readonly int DayFactor = TCache.DayFactor;
        public static readonly int HourFactor = TCache.HourFactor;
        public static readonly int MinuteFactor = TCache.MinuteFactor;
        public static readonly double SecondFactor = TCache.SecondFactor;

        public static void Max(string key, object obj)
        {
            TCache.Max(key, obj);
        }
        #region public static object Get(string key)

        public static object Get(string key)
        {
            return TCache.Get(key);
        }
        #endregion
    }
}
