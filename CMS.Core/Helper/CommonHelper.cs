using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using System.IO;
using TBDCMS.Core.Manager;
using System.Data;
using TBDCMS.DataAccess;

namespace TBDCMS.Core.Helper
{
    public class CommonHelper
    {
        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
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
