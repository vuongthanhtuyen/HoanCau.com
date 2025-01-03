using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using System.Net.Mail;
using TBDCMS.Core.Manager;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Threading;
using ImageResizer;

namespace TBDCMS.Core.Helper
{
   
    public class Helpers
    {
        
        private Helpers()
        {
        }

        #region video extractor helper

        public static string GetYoutubeID(string youTubeUrl)
        {
            if (string.IsNullOrEmpty(youTubeUrl) == false)
                youTubeUrl = youTubeUrl.Trim();
            else
                return string.Empty;

            string result = "";
            if (string.IsNullOrEmpty(youTubeUrl) == false)
            {
                youTubeUrl = youTubeUrl.Trim();
                string strRegex = @"(?:.+?)?(?:\/v\/|watch\/|\?v=|\&v=|youtu\.be\/|\/v=|^youtu\.be\/|watch\%3Fv\%3D)([a-zA-Z0-9_-]{11})+";
                Regex check = new Regex(strRegex, RegexOptions.IgnorePatternWhitespace);
                Match regexMatch = check.Match(youTubeUrl);
                if (regexMatch.Success)
                    return regexMatch.Groups[1].ToString();
                return youTubeUrl;
            }
            else
            {
                return result;
            }
        }

        static string patternVimeo = @"(https?:\/\/)?(www\.)?(player\.)?vimeo\.com\/([a-z]*\/)*([0-9]{6,11})[?]?.*";
        static string patternYoutube = @"(?:.+?)?(?:\/v\/|watch\/|\?v=|\&v=|youtu\.be\/|\/v=|^youtu\.be\/|watch\%3Fv\%3D)([a-zA-Z0-9_-]{11})+";



        static string GetVideo(string videoPattern, string testUrl)
        {
            Regex l_expression =
                new Regex(videoPattern,
                    RegexOptions.IgnoreCase);

            MatchCollection maColl = l_expression.Matches(testUrl);
            if (maColl != null && maColl.Count > 0)
            {
                int count = maColl[0].Groups.Count;
                return maColl[0].Groups[count - 1].Value;
            }

            return string.Empty;
        }
     
      
        #endregion

        public static Dictionary<CMSImageType, string> GetListImageDimension()
        {
            Dictionary<CMSImageType, string> dic = new Dictionary<CMSImageType, string>();
            Array values = Enum.GetValues(typeof(CMSImageType));
            if (values != null && values.Length > 0)
            {
                RenderAttribute type = null;
                foreach (var val in values)
                {
                    type = Helpers.GetRenderAttribute((CMSImageType)val);
                    if (type != null)
                    {
                        if (type.Width > 0 && type.Height > 0)
                            dic.Add((CMSImageType)val, type.Width + "px x " + type.Height + "px");
                    }
                }
            }
            return dic;
        }

     
        [Flags]
        public enum Status : byte
        {
            Active = 1,
            InActive = 0
        }

        public static bool IsValidWwwPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            Regex r = new Regex(@"(http|https|ftp)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return r.IsMatch(path);
        }
        #region Image helper
        public static string GetThumbnailUrl(string fileName)
        {
            try
            {
                string _thum = string.Empty;
                if (string.IsNullOrEmpty(fileName) || fileName.ToLower() == "no-image.jpg")
                    return "/Images/no-image.jpg";
                if (IsValidWwwPath(fileName))
                    _thum = fileName;
                else
                    _thum = ReplaceServerPath(fileName);
                return _thum;
            }
            catch
            {
                return "/Images/no-image.jpg";
            }
        }
        #endregion
        public static class UploadType
        {
            public const string UserAvatar = "UserAvatar";
            public const string MenuPhoto = "MenuPhoto";
            public const string GuestBookImage = "Review";
        }
        public static RenderAttribute GetRenderAttribute(CMSImageType imageType)
        {
            Type type = imageType.GetType();
            try
            {
                System.Reflection.FieldInfo fieldInfo = type.GetField(imageType.ToString());
                RenderAttribute attribute = fieldInfo.GetCustomAttributes(typeof(RenderAttribute), false).FirstOrDefault() as RenderAttribute;
                if (attribute == null)
                    return null;

                return attribute;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public static string HostPath = CommonHelper.GetSiteRoot() + "/";
        public static string HostPath
        {
            get
            {
                return CommonHelper.GetSiteRoot() + "/"; ;
            }
        }

        public static string GetServerIP()
        {
            string myHost = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(myHost).AddressList[0].ToString();
            return myIP;
        }

      

        static string ReplaceCapText(Match ma)
        {
            string ip = TBDCMS.Core.Helper.Helpers.GetServerIP();

            //Dictionary<string, Dictionary<string, string>> dicServer = TBDCMS.Core.Manager.SettingManager.DicServer;
            Dictionary<string, Dictionary<string, string>> dicServer = null;

            string ss = ma.Groups[1].Value.Trim();

            //if (string.IsNullOrEmpty(dicServer["sv1"]["PrivateIP"]) == true
            //    && string.IsNullOrEmpty(dicServer["sv1"]["PublicIP"]) == true)
            //{
                if (ss.StartsWith("\""))
                    return "\"/uploads";
                else if (ss.StartsWith("'"))
                    return "'/uploads";
                else
                    return "/uploads";
            //}

            bool isbreak = false;
            bool isSetmanual = false;

            if (ss.StartsWith("\""))
                ss = ss.TrimStart('"');
            else if (ss.StartsWith("'"))
                ss = ss.TrimStart('\'');

            if (ss.Length == 0)
                isSetmanual = true;
            else if (ss.ToLower().StartsWith("sv"))
            {
                foreach (var item in dicServer)
                {
                    if (string.IsNullOrEmpty(item.Key.Trim()))
                        continue;

                    if (ss.ToLower().StartsWith(item.Key.ToLower()))
                    {
                        ss = ma.Value;
                        isbreak = true;
                        break;
                    }
                }

                if (isbreak == false)
                    isSetmanual = true;
            }
            else
            {
                foreach (var item in dicServer)
                {

                    if (string.IsNullOrEmpty(item.Key.Trim()))
                        continue;

                    if (ss.ToLower().Contains(item.Value["PrivateIP"].ToLower())
                        || ss.ToLower().Contains(item.Value["PublicIP"].ToLower()))
                    {
                        isbreak = true;
                        if (ma.Value.Trim().StartsWith("\""))
                            ss = "\"" + item.Key.TrimEnd('/') + "/uploads";
                        else if (ma.Value.Trim().StartsWith("'"))
                            ss = "'" + item.Key.TrimEnd('/') + "/uploads";
                        else
                            ss = item.Key.TrimEnd('/') + "/uploads";
                        break;
                    }
                }

                if (isbreak == false)
                    isSetmanual = false;
            }

            if (isbreak == false)
            {
                string host = TBDCMS.Core.Helper.Helpers.HostPath;
                foreach (var item in dicServer)
                {

                    if (string.IsNullOrEmpty(item.Key.Trim()))
                        continue;

                    if (item.Value["PrivateIP"].ToLower() == host.ToLower()
                        || item.Value["PublicIP"].ToLower() == host.ToLower())
                    {
                        isbreak = true;

                        if (ma.Value.Trim().StartsWith("\""))
                            ss = "\"" + item.Key.TrimEnd('/') + "/" + ma.Value.Trim().TrimStart('"').TrimStart('/');
                        else if (ma.Value.Trim().StartsWith("'"))
                            ss = "'" + item.Key.TrimEnd('/') + "/" + ma.Value.Trim().TrimStart('\'').TrimStart('/');
                        else
                            ss = item.Key.TrimEnd('/') + "/" + ma.Value.Trim().TrimStart('\'').TrimStart('/');

                        break;
                    }
                }
            }

            if (isbreak == false && isSetmanual == true)
            {
                foreach (var item in dicServer)
                {

                    if (string.IsNullOrEmpty(item.Key.Trim()))
                        continue;

                    if (item.Value["PrivateIP"].ToLower() == ip.ToLower()
                        || item.Value["PublicIP"].ToLower() == ip.ToLower())
                    {
                        if (ma.Value.Trim().StartsWith("\""))
                            ss = "\"" + item.Key.TrimEnd('/') + "/uploads";
                        else if (ma.Value.Trim().StartsWith("'"))
                            ss = "'" + item.Key.TrimEnd('/') + "/uploads";
                        else
                            ss = item.Key.TrimEnd('/') + "/uploads";

                        isbreak = true;
                        break;
                    }
                }
            }

            if (isbreak == false)
            {
                if (ss.ToLower().StartsWith("sv") || ss.Length == 0)
                {
                    if (ma.Value.Trim().StartsWith("\""))
                        ss = "\"sv1/uploads";
                    else if (ma.Value.Trim().StartsWith("'"))
                        ss = "'sv1/uploads";
                    else
                        ss = "sv1/uploads";
                }
                else
                    ss = ma.Value;
            }

            if (ss.Length == 0)
            {
                if (ma.Value.Trim().StartsWith("\""))
                    ss = "\"sv1/uploads";
                else if (ma.Value.Trim().StartsWith("'"))
                    ss = "'sv1/uploads";
                else
                    ss = "sv1/uploads";
            }

            return ss;
        }

        public static string ConvertToSavePath(string content, bool isPath)
        {

            Regex re = null;
            if (isPath == false)
                re = new Regex(@"([""|']+https?:\/\/\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b|[""|']sv([\d]+)?|[""'])\/uploads", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            else
                re = new Regex(@"(https?:\/\/\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b|sv([\d]+)?|)\/uploads", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Match ma = re.Match(content);

            while (ma.Success == true)
            {
                content = re.Replace(content, new MatchEvaluator(ReplaceCapText));

                ma = ma.NextMatch();
            }

            return content;
        }

        public static string ReplaceServerPath(string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                //Dictionary<string, Dictionary<string, string>> dicServer = SettingManager.DicServer;
                Dictionary<string, Dictionary<string, string>> dicServer = null;
                if (dicServer != null && dicServer.Count > 0)
                {
                    Regex re = new Regex("(sv|sv\\d+\\/)uploads\\/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    Match m = re.Match(content);
                    while (m.Success)
                    {
                        bool found = false;
                        foreach (var item in dicServer)
                        {
                            if (string.IsNullOrEmpty(item.Key.Trim()))
                                continue;

                            if (item.Key.ToLower() == m.Groups[1].Value.TrimEnd('/').ToLower())
                            {
                                found = true;
                                content = re.Replace(content,
                                    (item.Value["PublicIP"].StartsWith("http") ? "" : (item.Value["PublicIP"].Length > 0 ? "https://" : "")) +
                                    item.Value["PublicIP"].TrimEnd('/') + "/uploads/", 1);
                                break;
                            }
                        }

                        if (found == false)
                        {
                            string ip = TBDCMS.Core.Helper.Helpers.GetServerIP();
                            foreach (var item in dicServer)
                            {
                                if (string.IsNullOrEmpty(item.Key.Trim()))
                                    continue;

                                if (item.Value["PrivateIP"].ToLower() == ip.ToLower()
                                    || item.Value["PublicIP"].ToLower() == ip.ToLower())
                                {
                                    content = re.Replace(content,
                                        (item.Value["PublicIP"].StartsWith("http") ? "" : (item.Value["PublicIP"].Length > 0 ? "https://" : "")) +
                                        item.Value["PublicIP"].TrimEnd('/') + "/uploads/", 1);
                                    break;
                                }
                            }
                        }

                        if (found == false)
                        {
                            content = re.Replace(content,
                                (dicServer["sv1"]["PublicIP"].StartsWith("http") ? "" : (dicServer["sv1"]["PublicIP"].Length > 0 ? "https://" : "")) +
                                dicServer["sv1"]["PublicIP"].TrimEnd('/') + "/uploads/", 1);
                        }

                        if (found == false)
                            break;
                        else
                        {
                            content = content.Replace("//uploads", "/uploads");
                            m = m.NextMatch();
                        }
                    }

                    return content;
                }
            }

            return content;
        }
        #region Append suffix to filename

        public const string ActiveUserHashing = "3d97c505-de70-490e-8f18-";
        public const string CookiesUserHashing = "9d0f1b17-75b5-448f-b7f6-";
        public const string ForgotPasswordHashing = "258301f8-6086-44b0-a2d8-";
        public const string ClientHashing = "93b36275-c08b-4e91-8ce0-";
        public static class EncryptType
        {
            public const string Active = "Active";
            public const string Forgot = "Forgot";
            public const string Cookies = "Cookies";
            public const string Client = "Client";
        }
        public static byte[] ResultArray(string resultString, bool isEnCode, string type)
        {
            string hashing = string.Empty;
            switch (type)
            {
                case EncryptType.Active:
                    hashing = ActiveUserHashing;
                    break;
                case EncryptType.Cookies:
                    hashing = CookiesUserHashing;
                    break;
                case EncryptType.Forgot:
                    hashing = ForgotPasswordHashing;
                    break;
                case EncryptType.Client:
                    hashing = ClientHashing;
                    break;
            }
            byte[] keyArray = Encoding.UTF8.GetBytes(hashing);
            byte[] toEncryptArray;
            if (isEnCode)
                toEncryptArray = Encoding.UTF8.GetBytes(resultString);
            else
                toEncryptArray = Convert.FromBase64String(resultString);
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform;
            if (isEnCode)
                cTransform = tdes.CreateEncryptor();
            else
                cTransform = tdes.CreateDecryptor();
            return cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        }

      
      
        #endregion

        #region Append suffix to filename

        private static string numberPattern = "_{0}";

        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
                return path;

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
      
        #endregion
       
    }
}
