using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using SweetCMS.Core.ApplicationCache;
using System.Threading;
using CMS.Core.ApplicationCache;

namespace SweetCMS.Core.Helper
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

        /// <summary>
        /// Key: Language value (English:1, Vietnamese: 2)
        /// Value: string of lagguage name
        /// </summary>
        public static Dictionary<int, string> LanguageName
        {
            get
            {
                Dictionary<int, string> dic = AppCache.Get(LANGUAGE_NAME_CACHE + ApplicationContext.Current.CurrentLanguageId) as Dictionary<int, string>;
                if (dic == null)
                {
                    dic = new Dictionary<int, string>();
                    dic[English] = "English";
                    dic[Vietnamese] = "Vietnamese";
                    AppCache.Max(LANGUAGE_NAME_CACHE + ApplicationContext.Current.CurrentLanguageId, dic);
                }
                return dic;
            }
        }

        public static Dictionary<int, string> LanguageText
        {
            get
            {
                Dictionary<int, string> dic = AppCache.Get(LANGUAGE_TEXT_CACHE) as Dictionary<int, string>;
                if (dic == null)
                {
                    dic = new Dictionary<int, string>();
                    dic[English] = "English";
                    dic[Vietnamese] = "Tiếng Việt";
                    AppCache.Max(LANGUAGE_TEXT_CACHE, dic);
                }
                return dic;
            }
        }

        /// <summary>
        /// Key: Language code (English:1, Vietnamese: 2)
        /// Value: string of language code (English: en-US, Vietnamese: vi-VN)
        /// </summary>
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
        public static Dictionary<string, string> BootstrapDatePickerLanguage
        {
            get
            {
                Dictionary<string, string> dic = AppCache.Get(LANGUAGE_BOOTSTRAP_DATEPICKER_CACHE) as Dictionary<string, string>;
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                    dic[LanguageCode[English]] = "DD/MM/YYYY";
                    dic[LanguageCode[Vietnamese]] = "DD/MM/YYYY";
                    AppCache.Max(LANGUAGE_BOOTSTRAP_DATEPICKER_CACHE, dic);
                }
                return dic;
            }
        }
        public static Dictionary<string, string> BootstrapDateTimePickerLanguage
        {
            get
            {
                Dictionary<string, string> dic = AppCache.Get(LANGUAGE_BOOTSTRAP_DATETIMEPICKER_CACHE) as Dictionary<string, string>;
                if (dic == null)
                {
                    dic = new Dictionary<string, string>();
                    dic[LanguageCode[English]] = "DD/MM/YYYY - hh:mm A";
                    dic[LanguageCode[Vietnamese]] = "DD/MM/YYYY - hh:mm A";
                    AppCache.Max(LANGUAGE_BOOTSTRAP_DATETIMEPICKER_CACHE, dic);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLanguageCode"></param>
        /// <returns>Language code (English:1, Vietnamese: 2)</returns>
        public static int GetLanguageCodeByCultureName(string strLanguageCode)
        {
            switch (strLanguageCode.ToLower())
            {
                case "en-us":
                case "en":
                case "us":
                    return English;
                case "vi":
                case "vn":
                case "vi-vn":
                    return Vietnamese;

                default:
                    return 0;
            }
        }

        public static int CurrentLanguageCode
        {
            get
            {
                switch (CultureInfo.CurrentUICulture.Name.ToLowerInvariant())
                {
                    case "vi":
                    case "vn":
                    case "vi-vn":
                        return Vietnamese;
                    case "en":
                    case "us":
                    case "en-us":
                        return English;
                    default:
                        return Vietnamese;
                }
            }
        }

        public static string GetFormatDate(object strDate, int langId)
        {
            try
            {
                DateTime date;
                DateTime.TryParse(strDate.ToString(), out date);
                CultureInfo ci = new CultureInfo(LanguageHelper.LanguageCode[langId]);
                return date.ToString(FormatDate[LanguageCode[langId]], ci);
            }
            catch
            {
                return strDate.ToString();
            }
        }
        public static string GetFormatDate(DateTime date, string langCode)
        {
            CultureInfo ci = new CultureInfo(langCode);
            return date.ToString(FormatDate[langCode], ci);
        }
        public static string GetFormatDateAndTime(DateTime date, int langId)
        {
            CultureInfo ci = new CultureInfo(LanguageHelper.LanguageCode[langId]);
            return date.ToString(FormatDateAndTime[LanguageCode[langId]], ci);
        }
        public static string GetFormatDateAndTime(DateTime date, string langCode)
        {
            CultureInfo ci = new CultureInfo(langCode);
            return date.ToString(FormatDateAndTime[langCode], ci);
        }
        public static string FormatByCultureInfo(this DateTime dt)
        {
            try
            {
                int langId = ApplicationContext.Current.CurrentLanguageId;
                CultureInfo ci = new CultureInfo(LanguageHelper.LanguageCode[langId]);
                if (langId == LanguageHelper.Vietnamese)
                    return dt.ToString("dd \"Tháng\" MM yyyy", ci);
                else
                    return dt.ToString("DD/MM/YYYY", ci);
            }
            catch
            {
                return dt.ToShortDateString();
            }
        }
        public static int CurrentLanguageId
        {
            get
            {
                switch (CultureInfo.CurrentUICulture.Name)
                {
                    case "en-US":
                        return English;
                    case "vi-VN":
                        return Vietnamese;
                    default:
                        return Vietnamese;
                }
            }
        }
        public static string GetLanguageDateTimeFormatString(bool? hasSeconds, bool? hasMinutes, bool? hasHour, int? langId)
        {
            string formatString;
            if (langId == null)
                formatString = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            else
            {
                switch (langId.Value)
                {
                    case 2:
                        formatString = "dd \"Tháng\" MM yyyy";
                        break;
                    case 1:
                        formatString = "MM/DD/YYYY";
                        break;
                    default:
                        formatString = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                        break;
                }
            }
            if (hasSeconds != null && hasSeconds.Value)
                formatString += " HH:mm:ss";
            else if (hasMinutes != null && hasMinutes.Value)
                formatString += " HH:mm";
            else if (hasHour != null && hasHour.Value)
                formatString += " HH";

            return formatString;
        }
        public static bool TryParseDateTimeByLanguage(string dateTimeS, out DateTime dateTimeD, int? langId)
        {
            if (langId == null)
                return DateTime.TryParse(dateTimeS, out dateTimeD);

            string languageCode = LanguageCode[langId.Value];
            CultureInfo ci = new CultureInfo(languageCode, false);
            return DateTime.TryParse(dateTimeS, ci, DateTimeStyles.None, out dateTimeD);
        }
        public static Dictionary<int, string> ThousandsSeparators
        {
            get
            {
                Dictionary<int, string> dic = AppCache.Get(LANGUAGE_THOUSANDS_SEPARATORS) as Dictionary<int, string>;
                if (dic == null)
                {
                    dic = new Dictionary<int, string>();
                    dic[English] = ",";
                    dic[Vietnamese] = ".";
                    AppCache.Max(LANGUAGE_THOUSANDS_SEPARATORS, dic);
                }
                return dic;
            }
        }
        public static Dictionary<int, string> DecimalPoint
        {
            get
            {
                Dictionary<int, string> dic = AppCache.Get(LANGUAGE_DECIMAL_POINT) as Dictionary<int, string>;
                if (dic == null)
                {
                    dic = new Dictionary<int, string>();
                    dic[English] = ".";
                    dic[Vietnamese] = ",";
                    AppCache.Max(LANGUAGE_DECIMAL_POINT, dic);
                }
                return dic;
            }
        }
        public static string PerfixLanguage
        {
            get
            {
                try
                {
                    switch (ApplicationContext.Current.CurrentLanguageId)
                    {
                        case 1:
                            return "en";
                        default:
                            return "vi";
                    }
                }
                catch
                {
                    return "vi";
                }
            }
        }


        public static string GetPrefixLanguage(int langId)
        {
            try
            {
                switch (langId)
                {
                    case 1:
                        return "en";
                    default:
                        return "vi";
                }
            }
            catch
            {
                return "vi";
            }
        }
        public static string GetCurrentLanguageCode(int languageId)
        {
            Dictionary<int, string> languageCode = LanguageHelper.LanguageCode;
            if (languageCode.ContainsKey(languageId))
                return languageCode[languageId];
            else
                return languageCode[LanguageHelper.English];
        }
    }
}
