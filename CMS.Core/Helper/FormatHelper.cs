using SweetCMS.Core.UI.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCMS.Core.Helper
{
    public class FormatHelper
    {
        #region public method
        #region ConvertDecimalToStringByLanguage
        public static string ConvertDecimalToStringByLanguage(decimal value, int? langId)
        {
            if (langId == null)
                langId = LanguageHelper.CurrentLanguageId;
            return value.ToString("#,##0.#########", CultureInfo.CreateSpecificCulture("en-GB"));
        }
        public static string ConvertDecimalToStringByLanguageClient(decimal value, int? langId)
        {
            if (langId == null)
                langId = LanguageHelper.CurrentLanguageId;
            return value.ToString("#,##0.#########", CultureInfo.CreateSpecificCulture(LanguageHelper.LanguageCode[ApplicationContext.Current.CurrentLanguageId]));
        }
        #endregion
        #region ConvertDoubleToStringByLanguage
        public static string ConvertDoubleToStringByLanguage(object value, int? langId)
        {
            if (value == null)
                return string.Empty;
            return ConvertDoubleToStringByLanguage(value.ToString(), langId);
        }
        public static string ConvertDoubleToStringByLanguage(string value, int? langId)
        {
            try
            {
                double valueInDouble;
                if (double.TryParse(value, out valueInDouble))
                    return ConvertDoubleToStringByLanguage(valueInDouble, langId);
                return value;
            }
            catch
            {
                return value;
            }
        }
        public static string ConvertDoubleToStringByLanguage(double value, int? langId)
        {
            try
            {
                if (langId == null)
                    langId = LanguageHelper.Defaultlanguage;
                return value.ToString("#,##0.#########", CultureInfo.CurrentCulture);
            }
            catch
            {
                return value.ToString();
            }
        }
        #endregion
        #region ConvertDateTimeToStringByLanguage
        public static string ConvertDateTimeToStringByLanguage(object dateTime, bool? hasTime, int? langId)
        {
            return ConvertDateTimeToStringByLanguage(dateTime.ToString(), hasTime, langId);
        }
        public static string ConvertDateTimeToStringByLanguage(string dateTime, bool? hasTime, int? langId)
        {
            DateTime dateTimeD;
            if (LanguageHelper.TryParseDateTimeByLanguage(dateTime, out dateTimeD, langId))
                return ConvertDateTimeToStringByLanguage(dateTimeD, hasTime, langId);
            else
                return dateTime;
        }
        public static string ConvertDateTimeToStringByLanguage(DateTime dateTime, bool? hasTime, int? langId)
        {
            return hasTime != null && hasTime.Value ? ConvertDateTimeToStringByLanguage(dateTime, false, true, true, langId)
                : ConvertDateTimeToStringByLanguage(dateTime, false, false, false, langId);
        }
        public static string ConvertDateTimeToStringByLanguage(object dateTime, bool? hasSeconds, bool? hasMinutes, bool? hasHour, int? langId)
        {
            return ConvertDateTimeToStringByLanguage(dateTime.ToString(), hasSeconds, hasMinutes, hasHour, langId);
        }
        public static string ConvertDateTimeToStringByLanguage(string dateTime, bool? hasSeconds, bool? hasMinutes, bool? hasHour, int? langId)
        {
            DateTime dateTimeD;
            if (LanguageHelper.TryParseDateTimeByLanguage(dateTime, out dateTimeD, langId))
                return ConvertDateTimeToStringByLanguage(dateTimeD, hasSeconds, hasMinutes, hasHour, langId);
            else
                return dateTime;
        }
        public static string ConvertDateTimeToStringByLanguage(DateTime dateTime, bool? hasSeconds, bool? hasMinutes, bool? hasHour, int? langId)
        {
            if (dateTime != null && dateTime != DateTime.MinValue && dateTime != DateTimeHelper.MinDateTimeValue)
            {
                string formatString = LanguageHelper.GetLanguageDateTimeFormatString(hasSeconds, hasMinutes, hasHour, langId);
                return dateTime.ToString(formatString);
            }
            return string.Empty;
        }
        #endregion
        #endregion
    }
}
