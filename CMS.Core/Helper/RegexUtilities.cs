using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SweetCMS.Core.Helper
{
    public class RegexUtilities
    {
        static bool invalid;

        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            try
            {
                // Valid unicode
                invalid = Regex.IsMatch(strIn, @"[ă|â|đ|ê|ô|ơ|ư|à|ả|ã|ạ|á|ằ|ẳ|ẵ|ặ|ắ|ầ|ẩ|ẫ|ậ|ấ|è|ẻ|ẽ|ẹ|é|ề|ể|ễ|ệ|ế|ì|ỉ|ĩ|ị|í|ò|ỏ|õ|ọ|ó|ồ|ổ|ỗ|ộ|ố|ờ|ở|ỡ|ợ|ớ|ù|ủ|ũ|ụ|ú|ừ|ử|ữ|ự|ứ|ỳ|ỷ|ỹ|ỵ|ý|Ă|Â|Đ|Ê|Ô|Ơ|Ư|À|Ả|Ã|Ạ|Á|Ằ|Ẳ|Ẵ|Ặ|Ắ|Ầ|Ẩ|Ẫ|Ậ|Ấ|È|Ẻ|Ẽ|Ẹ|É|Ề|Ể|Ễ|Ệ|Ế|Ì|Ỉ|Ĩ|Ị|Í|Ò|Ỏ|Õ|Ọ|Ó|Ồ|Ổ|Ỗ|Ộ|Ố|Ờ|Ở|Ỡ|Ợ|Ớ|Ù|Ủ|Ũ|Ụ|Ú|Ừ|Ử|Ữ|Ự|Ứ|Ỳ|Ỷ|Ỹ|Ỵ|Ý]");
            }
            catch
            {
                return false;
            }

            if (invalid)
                return false;
            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length > 11)
                return false;
            try
            {
                return Regex.IsMatch(phone, @"(0[3|5|7|8|9]|01[2|6|8|9])+([0-9]{8})\b");
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsValidDomainName(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+.*)$");
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
