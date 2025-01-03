using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Caching;
using System.Web;

namespace TBDCMS.Core
{
    public class TCache
    {
        private static readonly Cache _cache = HttpRuntime.Cache;
        private static CacheLoaderErrorDelegate _cacheLoaderErrorDelegate = null;
        public static readonly int DayFactor = 0x4380;
        private static int Factor = 5;
        public static readonly int HourFactor = 720;
        public static readonly int MinuteFactor = 12;
        public static readonly double SecondFactor = 0.2;

        private TCache()
        {
        }
        public static object Get(string key)
        {
            return _cache[key];
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds((double)(Factor * seconds)), TimeSpan.Zero, priority, onRemoveCallback);
            }
        }

        internal static object InternalCallback(string key)
        {
            CacheEntry cacheEntry = CacheLockbox.GetCacheEntry(key);
            if (cacheEntry == null)
            {
                return null;
            }
            CacheLoaderDelegate cacheLoader = cacheEntry.CacheLoader;
            if (cacheLoader == null)
            {
                return null;
            }
            object obj2 = null;
            try
            {
                obj2 = cacheLoader();
            }
            catch (Exception exception)
            {
                if (_cacheLoaderErrorDelegate != null)
                {
                    try
                    {
                        _cacheLoaderErrorDelegate(key, exception);
                    }
                    catch
                    {
                    }
                }
            }
            if (obj2 != null)
            {
                Insert(key, obj2, null, (int)cacheEntry.RefreshInterval.TotalSeconds, CacheItemPriority.Normal, new CacheItemRemovedCallback(TCache.ItemRemovedCallback));
                CacheLockbox.UpdateCacheEntry(key, DateTime.Now);
            }
            return obj2;
        }

        internal static void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.Expired)
            {
                CacheEntry cacheEntry = CacheLockbox.GetCacheEntry(key);
                if (cacheEntry.LastUse.Add(cacheEntry.SlidingExpiration) > DateTime.Now)
                {
                    _cache.Insert(key, value, null, DateTime.Now.Add(TimeSpan.FromSeconds(30.0)), TimeSpan.Zero, CacheItemPriority.Low, null);
                    ThreadPool.QueueUserWorkItem(delegate(object o)
                    {
                        string _key = o.ToString();
                        lock (CacheLockbox.GetInternalLock(_key))
                        {
                            InternalCallback(_key);
                        }
                    }, key);
                }
            }
        }

        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

     

      
        public delegate object CacheLoaderDelegate();

        public delegate void CacheLoaderErrorDelegate(string cacheKey, Exception e);
    }

    public class TCache<T> where T : class
    {
        private TCache()
        {
        }
    
    }
}
