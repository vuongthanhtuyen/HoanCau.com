using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using TBDCMS.Core.ApplicationCache;
using System.Threading;
using CMS.Core.ApplicationCache;

namespace TBDCMS.Core.Helper
{
    public static class LanguageHelper
    {
        public static int[] AvailableLanguages = { 1, 2, 3,4,5 };
        public static int English = 1;
        public static int Vietnamese = 2;
        public static int Chinese = 3;
        public static int Korea = 4;
        public static int Russian = 5;
        public static int Defaultlanguage
        {
            get
            {
                int _defaultlanguage = 1;
                //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"].ToString(), out _defaultlanguage);
                return _defaultlanguage;
            }
        }
        private const string LANGUAGE_CODE_CACHE = "LANGUAGE_CODE_CACHE";
        private const string LANGUAGE_TEXT_CACHE = "LANGUAGE_TEXT_CACHE";
        private const string LANGUAGE_NAME_CACHE = "LANGUAGE_NAME_CACHE";
        private const string LANGUAGE_BOOTSTRAP_DATETIMEPICKER_CACHE = "LANGUAGE_BOOTSTRAP_DATETIMEPICKER_CACHE";
        private const string LANGUAGE_BOOTSTRAP_DATEPICKER_CACHE = "LANGUAGE_BOOTSTRAP_DATEPICKER_CACHE";
        private const string LANGUAGE_DATE_FORMAT_CACHE = "LANGUAGE_DATE_FORMAT_CACHE";
        private const string LANGUAGE_DATE_TIME_FORMAT_CACHE = "LANGUAGE_DATE_TIME_FORMAT_CACHE";
        private const string LANGUAGE_THOUSANDS_SEPARATORS = "LANGUAGE_THOUSANDS_SEPARATORS";
        private const string LANGUAGE_DECIMAL_POINT = "LANGUAGE_DECIMAL_POINT";

      
        public static Dictionary<int, string> LanguageCode
        {
            get
            {
                Dictionary<int, string> dic = AppCache.Get(LANGUAGE_CODE_CACHE) as Dictionary<int, string>;
                if (dic == null)
                {
                    dic = new Dictionary<int, string>();
                    dic[English] = "en-US";
                    dic[Vietnamese] = "vi-VN";
                    AppCache.Max(LANGUAGE_CODE_CACHE, dic);
                }
                return dic;
            }
        }
     
        public static Dictionary<string, string> FormatDate
        {
            get
            {
                Dictionary<string, string> dic = AppCache.Get(LANGUAGE_DATE_FORMAT_CACHE) as Dictionary<string, string>;
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                    dic[LanguageCode[English]] = "dd/MM/yyyy";
                    dic[LanguageCode[Vietnamese]] = "dd \"Tháng\" MM yyyy";
                    AppCache.Max(LANGUAGE_DATE_FORMAT_CACHE, dic);
                }
                return dic;
            }
        }
        public static Dictionary<string, string> FormatDateAndTime
        {
            get
            {
                Dictionary<string, string> dic = AppCache.Get(LANGUAGE_DATE_TIME_FORMAT_CACHE) as Dictionary<string, string>;
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                    dic[LanguageCode[English]] = "dd/MM/yyyy - hh:mm tt";
                    dic[LanguageCode[Vietnamese]] = "dd \"Tháng\" MM yyyy - hh:mm tt";
                    AppCache.Max(LANGUAGE_DATE_TIME_FORMAT_CACHE, dic);
                }
                return dic;
            }
        }
      
    }
}
