using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using System.IO;
using SweetCMS.Core.Manager;
using System.Data;
using SweetCMS.DataAccess;

namespace SweetCMS.Core.Helper
{
    public class CommonHelper
    {
        public static bool IsValidEmail(string Email)
        {
            return !string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        #region QueryForm

        /// <summary>
        /// Gets query form value by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query Form value</returns>
        public static string QueryForm(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.Form[Name] != null)
                result = HttpContext.Current.Request.Form[Name].ToString();
            return result;
        }

        /// <summary>
        /// Gets boolean value from Query Form 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query Form value</returns>
        public static bool QueryFormBool(string Name)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
        }

        /// <summary>
        /// Gets integer value from Query Form 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query Form value</returns>
        public static int QueryFormInt(string Name)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        public static byte QueryFormByte(string Name)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            byte result = byte.MaxValue;
            if (!string.IsNullOrEmpty(resultStr))
                result = byte.Parse(resultStr);
            return result;
        }

        /// <summary>
        /// Gets integer value from Query Form 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query Form value</returns>
        public static long QueryFormLong(string Name)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            long result;
            Int64.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Gets integer value from Query Form 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <param name="DefaultValue">Default value</param>
        /// <returns>Query Form value</returns>
        public static int QueryFormInt(string Name, int DefaultValue)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            if (resultStr.Length > 0)
            {
                return Int32.Parse(resultStr);
            }
            return DefaultValue;
        }

        /// <summary>
        /// Gets GUID value from Query Form 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query Form value</returns>
        public static Guid? QueryFormGUID(string Name)
        {
            string resultStr = QueryForm(Name).ToUpperInvariant();
            Guid? result = null;
            try
            {
                result = new Guid(resultStr);
            }
            catch
            {
            }
            return result;
        }


        #endregion

        #region QueryString

        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
        }

        /// <summary>
        /// Gets boolean value from query string 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static bool QueryStringBool(string Name)
        {
            try
            {
                string resultStr = QueryString(Name).ToUpperInvariant();
                if (string.IsNullOrEmpty(resultStr))
                    return false;
                return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
            }
            catch
            {
                return false;
            }
           
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            int result = -1;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <param name="DefaultValue">Default value</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string queryName, int defaultValue)
        {
            string resultStr = QueryString(queryName).ToUpperInvariant();
            if (resultStr.Length > 0)
            {
                return Int32.Parse(resultStr);
            }
            return defaultValue;
        }

        public static byte QueryStringByte(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            byte result = byte.MaxValue;
            if (!string.IsNullOrEmpty(resultStr))
                result = byte.Parse(resultStr);
            return result;
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static long QueryStringLong(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            long result;
            Int64.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Gets GUID value from query string 
        /// </summary>
        /// <param name="Name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static Guid QueryStringGUID(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            Guid result = Guid.Empty;
            try
            {
                result = new Guid(resultStr);
            }
            catch
            {
                result = Guid.Empty;
            }

            return result;
        }

        #endregion


        /// <summary>
        /// Check file is opened, return true if file is opened 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileIsOpen(string filePath)
        {
            bool results = false;
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    try
                    {
                        stream.ReadByte();
                    }
                    catch (IOException)
                    {
                        results = true;
                    }
                    finally
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
            catch (IOException)
            {
                results = true;  //file is opened at another location
            }

            return results;
        }

        /// <summary>
        /// Status of object in Project
        /// </summary>
        /// <param name="objStatus"></param>
        /// <returns></returns>

        public static string GetFullApplicationPath()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
        }

        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageURL(bool includeQueryString)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                string storeHost = GetSiteRoot();
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                URL = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            return URL;
        }

        public static string GetSiteRoot()
        {
            string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

            if (sOut.EndsWith("/"))
            {
                sOut = sOut.Substring(0, sOut.Length - 1);
            }

            return sOut;
        }

        public static string GetAdminLocationHostPath()
        {
            return GetSiteRoot() + "/Administration";
        }

        public static string TrimLongString(string text, int numberOfCharacters)
        {
            //if (text.Length > numberOfCharacters)
            //{
            //    text = text.Substring(0, numberOfCharacters);
            //    return text + "...";
            //}
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            if (text.Length <= numberOfCharacters)
                return text;

            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > numberOfCharacters)
                return words[0];
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > numberOfCharacters)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}...", sb.ToString().TrimEnd(' '));
        }


        public static bool IsRunningSSL()
        {
            bool isHttps = false;
            if (HttpContext.Current.Request.Url.Scheme == "https")
                isHttps = true;
            return isHttps;
        }
        public static string GetHostPath()
        {
            string httpHost = ServerVariables("HTTP_HOST");
            if (string.IsNullOrEmpty(httpHost))
                httpHost = "domain.com";

            string result = "http://" + httpHost;

            if (IsRunningSSL())
            {
                //if (!String.IsNullOrEmpty(SettingManager.GetSettingValue("Common.SharedSSL")))
                //{
                //    result = SettingManager.GetSettingValue("Common.SharedSSL");
                //}
                //else
                //{
                //    result = result.Replace("http:/", "https:/");
                //    result = result.Replace("www.www", "www");
                //}
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }
        public static string ServerVariables(string Name)
        {
            string tmpS = String.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[Name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[Name];
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath))
                        tmpS += HttpContext.Current.Request.ApplicationPath;
                }
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }
    }

}
