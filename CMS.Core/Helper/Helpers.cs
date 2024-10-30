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
using SweetCMS.Core.Manager;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using ImageResizer;
using System.Threading;

namespace SweetCMS.Core.Helper
{
    /// <summary>
    /// Provides static helper methods to NotAClue.Web.BootstrapFriendlyControlAdapters controls. Singleton instance.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Private constructor forces singleton.
        /// </summary>
        private Helpers()
        {
        }

        #region Smtp Mail

        public static string EMAIL_SENDER_BACK_END = "Adamas hotel CMS";
        public static string EMAIL_SENDER_FRONT_END = "Adamas hotel Website";

        public static void SendSmtpMail(string emailSender, string emailFromAddress,
            string emailToAddress, string subject, string bodyMessage)
        {
            Thread T1 = new Thread(delegate ()
            {
                SendSmtpMail(emailSender, emailFromAddress, emailToAddress,
                subject, bodyMessage, true, null);
            });
            T1.Start();
        }

        public static void SendSmtpMail(string emailSender, string emailFromAddress,
            string emailToAddress, string subject, string bodyMessage, bool isBodyHtml)
        {
            Thread T1 = new Thread(delegate ()
            {
                SendSmtpMail(emailSender, emailFromAddress, emailToAddress,
                     subject, bodyMessage, isBodyHtml, null);
            });
            T1.Start();
        }

        public static void SendSmtpMail(string emailSender, string emailFromAddress,
            string emailToAddress, string subject, string bodyMessage,
            bool isBodyHtml, Dictionary<string, string> listUserBCC)
        {
            //if (SettingManager.GetSettingValueIntAdmin(SettingNames.SmtpPort, 25) == 465)
            //{
            //    MimeKit.MimeMessage email = new MimeKit.MimeMessage();

            //    email.From.Add(new MimeKit.MailboxAddress("Adamas hotel CMS", SettingManager.GetSettingValue(SettingNames.SmtpSenderEmail)));
            //    email.To.Add(new MimeKit.MailboxAddress("Customer", emailToAddress));

            //    if (listUserBCC != null && listUserBCC.Count > 0)
            //    {
            //        foreach (KeyValuePair<string, string> keyValuePairString in listUserBCC)
            //        {
            //            email.Bcc.Add(new MimeKit.MailboxAddress(keyValuePairString.Key, keyValuePairString.Value));
            //        }
            //    }

            //    email.Subject = subject;
            //    email.Body = new MimeKit.TextPart(isBodyHtml ? "html" : "plain")
            //    {
            //        Text = bodyMessage
            //    };
            //    using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            //    {
            //        smtp.Connect(SettingManager.GetSettingValue(SettingNames.SmtpMailServerAddress),
            //            SettingManager.GetSettingValueIntAdmin(SettingNames.SmtpPort, 25), SettingManager.GetSettingValueBoolean(SettingNames.SmtpUsingSSL));

            //        //Note: only needed if the SMTP server requires authentication
            //        smtp.Authenticate(SettingManager.GetSettingValue(SettingNames.SmtpSenderAccount), SettingManager.GetSettingValue(SettingNames.SmtpSenderPassword));

            //        smtp.Send(email);
            //        smtp.Disconnect(true);
            //    }
            //}
            //else
            //{
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(SettingManager.GetSettingValue(SettingNames.SmtpSenderEmail), emailSender);

                NetworkCredential credential = new NetworkCredential(
                              SettingManager.GetSettingValue(SettingNames.SmtpSenderAccount),
                              SettingManager.GetSettingValue(SettingNames.SmtpSenderPassword)
                              );
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = SettingManager.GetSettingValueBoolean(SettingNames.SmtpUsingSSL);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = credential;
                smtpClient.Host = SettingManager.GetSettingValue(SettingNames.SmtpMailServerAddress);
                smtpClient.Port = SettingManager.GetSettingValueIntAdmin(SettingNames.SmtpPort, 25);

                message.From = fromAddress;
                message.To.Add(emailToAddress);
                message.ReplyToList.Add(new MailAddress(emailFromAddress, emailSender));

                if (listUserBCC != null && listUserBCC.Count > 0)
                {
                    foreach (KeyValuePair<string, string> keyValuePairString in listUserBCC)
                    {
                        try
                        {
                            message.Bcc.Add(new MailAddress(keyValuePairString.Key, keyValuePairString.Value));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                message.Subject = subject;
                message.IsBodyHtml = true;
                StringBuilder sbContent = new StringBuilder();
                try
                {
                    sbContent.Append(bodyMessage);
                    sbContent.Append("<br/>");
                    sbContent.Append(SettingManager.GetSettingValue(SettingNames.EmailSignature));
                }
                catch
                {
                    sbContent.Append(bodyMessage);
                }
                message.Body = sbContent.ToString();

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html));

                htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
                message.AlternateViews.Add(htmlView);

                // Send SMTP mail
                smtpClient.Send(message);
            //}
        }

        public static void SendSmtpMailTest(string emailSender, string emailFromAddress,
            string emailToAddress, string subject, string bodyMessage,
            bool isBodyHtml, Dictionary<string, string> listUserBCC)
        {
            MimeKit.MimeMessage email = new MimeKit.MimeMessage();

            email.From.Add(new MimeKit.MailboxAddress("Adamas hotel CMS", "noreply@adamashotel.vn"));
            email.To.Add(new MimeKit.MailboxAddress("Customer", emailToAddress));

            if (listUserBCC != null && listUserBCC.Count > 0)
            {
                foreach (KeyValuePair<string, string> keyValuePairString in listUserBCC)
                {
                    email.Bcc.Add(new MimeKit.MailboxAddress(keyValuePairString.Key, keyValuePairString.Value));
                }
            }

            email.Subject = subject;
            email.Body = new MimeKit.TextPart(isBodyHtml ? "html" : "plain")
            {
                Text = bodyMessage
            };
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("pro54.emailserver.vn",465, true);

                //Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("noreply@adamashotel.vn", "AdamasHotel@2024");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        #endregion


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

        public static bool IsYouTubeUrl(string testUrl)
        {
            if (string.IsNullOrEmpty(testUrl) == false)
                testUrl = testUrl.Trim();
            if (string.IsNullOrEmpty(testUrl) == false)
                return TestUrl(patternYoutube, testUrl);
            else
                return false;
        }

        public static bool IsVimeoUrl(string testUrl)
        {
            if (string.IsNullOrEmpty(testUrl) == false)
                testUrl = testUrl.Trim();
            if (string.IsNullOrEmpty(testUrl) == false)
                return TestUrl(patternVimeo, testUrl);
            else
                return false;
        }

        static bool TestUrl(string pattern, string testUrl)
        {
            Regex l_expression =
                new Regex(pattern,
                    RegexOptions.IgnoreCase);

            return l_expression.Matches(testUrl).Count > 0;
        }

        public static string GetYouTubeVideo(string testUrl)
        {
            if (string.IsNullOrEmpty(testUrl) == false)
                testUrl = testUrl.Trim();
            if (string.IsNullOrEmpty(testUrl) == false)
                return GetVideo(patternYoutube, testUrl);
            else
                return string.Empty;
        }

        public static string GetVimeoVideo(string testUrl)
        {
            if (string.IsNullOrEmpty(testUrl) == false)
                testUrl = testUrl.Trim();
            if (string.IsNullOrEmpty(testUrl) == false)
                return GetVideo(patternVimeo, testUrl);
            else
                return string.Empty;
        }

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
        public static string GetYoutubeVideoIdByUrl(string urlYoutube)
        {
            return urlYoutube.Replace("http://www.youtube.com/watch?v=", string.Empty).Replace("https://www.youtube.com/watch?v=", string.Empty);
        }
        public static string ConvertYoutubeUrlToEmbedUrl(string urlYoutube)
        {
            try
            {
                return string.Format("https://www.youtube.com/embed/{0}", GetYoutubeVideoIdByUrl(urlYoutube));
            }
            catch
            {
                return urlYoutube;
            }
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

        public static bool IsYoutubeLink(string url)
        {
            string id = GetYoutubeID(url);
            return id.Length > 0;
        }

        public static string ConvertToUrl(string url)
        {
            if (url.StartsWith("/") == true)
                return url;

            if (string.IsNullOrEmpty(url) == false)
            {
                if (url.StartsWith("//") == false
                    && url.ToLower().StartsWith("www") == false
                    && url.ToLower().StartsWith("http") == false)
                    url = HostPath + url.TrimStart('/');

                if (url.StartsWith("http") == false)
                    url = "https://" + url.TrimStart('/');
            }
            return url;
        }

        public static bool IsCollection(Type t)
        {
            /*
            var genArgs = t.GetGenericArguments();
            if (genArgs.Length == 1 &&
                    typeof(IEnumerable<>).MakeGenericType(genArgs).IsAssignableFrom(t))
                return true;
            else
                return t.BaseType != null && IsGenericEnumerable(t.BaseType);
            */
            return t.GetInterfaces().Any(iface => iface.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public static int GetListItemIndex(ListControl control, ListItem item)
        {
            int index = control.Items.IndexOf(item);
            if (index == -1)
                throw new NullReferenceException("ListItem does not exist ListControl.");

            return index;
        }

        public static string GetListItemClientID(ListControl control, ListItem item)
        {
            if (control == null)
                throw new ArgumentNullException("Control can not be null.");

            int index = GetListItemIndex(control, item);

            return String.Format("{0}_{1}", control.ClientID, index.ToString());
        }

        public static string GetListItemUniqueID(ListControl control, ListItem item)
        {
            if (control == null)
                throw new ArgumentNullException("Control can not be null.");

            int index = GetListItemIndex(control, item);

            return String.Format("{0}${1}", control.UniqueID, index.ToString());
        }

        public static bool HeadContainsLinkHref(Page page, string href)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            foreach (Control control in page.Header.Controls)
            {
                if (control is HtmlLink && (control as HtmlLink).Href == href)
                    return true;
            }

            return false;
        }

        public static void RegisterEmbeddedCSS(string css, Type type, Page page)
        {
            string filePath = page.ClientScript.GetWebResourceUrl(type, css);

            // if filePath is not empty, embedded CSS exists -- register it
            if (!String.IsNullOrEmpty(filePath))
            {
                if (!Helpers.HeadContainsLinkHref(page, filePath))
                {
                    HtmlLink link = new HtmlLink();
                    link.Href = page.ResolveUrl(filePath);
                    link.Attributes["type"] = "text/css";
                    link.Attributes["rel"] = "stylesheet";
                    page.Header.Controls.Add(link);
                }
            }
        }

        public static void RegisterClientScript(string resource, Type type, Page page)
        {
            string filePath = page.ClientScript.GetWebResourceUrl(type, resource);

            // if filePath is empty, set to filename path
            if (String.IsNullOrEmpty(filePath))
            {
                string folderPath = WebConfigurationManager.AppSettings.Get("NotAClue.Web.BootstrapFriendlyControlAdapters-JavaScript-Path");
                if (String.IsNullOrEmpty(folderPath))
                {
                    folderPath = "~/JavaScript";
                }
                filePath = folderPath.EndsWith("/") ? folderPath + resource : folderPath + "/" + resource;
            }

            if (!page.ClientScript.IsClientScriptIncludeRegistered(type, resource))
            {
                page.ClientScript.RegisterClientScriptInclude(type, resource, page.ResolveUrl(filePath));
            }
        }

        /// <summary>
        /// Gets the value of a non-public field of an object instance. Must have Reflection permission.
        /// </summary>
        /// <param name="container">The object whose field value will be returned.</param>
        /// <param name="fieldName">The name of the data field to get.</param>
        /// <remarks>Code initially provided by LonelyRollingStar.</remarks>
        public static object GetPrivateField(object container, string fieldName)
        {
            Type type = container.GetType();
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (fieldInfo == null ? null : fieldInfo.GetValue(container));
        }

        public static string GetPhysicalPath(string folderName, string fileName)
        {
            return string.Format("{0}{1}\\{2}", HttpContext.Current.Request.PhysicalApplicationPath, folderName, fileName);
        }

        /// <summary>
        /// A case insenstive replace function.
        /// </summary>
        /// <param name="originalString">The string to examine.(HayStack)</param>
        /// <param name="oldValue">The value to replace.(Needle)</param>
        /// <param name="newValue">The new value to be inserted</param>
        /// <returns>A string</returns>
        public static string CaseInsenstiveReplace(string originalString, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue,
               RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(originalString, newValue);
        }

        public static string RandomString(int size)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        [Flags]
        public enum Status : byte
        {
            Active = 1,
            InActive = 0
        }

        public static bool ConverToBoolean(object value)
        {
            bool _bool = false;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                int temp = 0; int.TryParse(value.ToString(), out temp);
                switch (temp)
                {
                    case 1:
                        _bool = true;
                        break;
                    case 0:
                    default:
                        _bool = false;
                        break;
                }
            }
            return _bool;
        }


        /// <summary>
        /// method for determining is the user provided a valid Email
        /// We use regular expressions in this check, as it is a more thorough
        /// way of checking the address provided
        /// </summary>
        /// <param name="email">Email to validate</param>
        /// <returns>true is valid, false if not valid</returns>
        public static bool IsValidEmail(string email)
        {
            //regular expression pattern for valid email
            Regex check = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            bool valid = false;

            //make sure an Email was provided
            if (string.IsNullOrEmpty(email))
                valid = false;
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(email);
            }
            //return the value to the calling method
            return valid;
        }

        public static bool IsValidServerPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            Regex r = new Regex(@"/[a-zA-Z0-9\.]+/*[a-zA-Z0-9/\\%_.]*\?*[a-zA-Z0-9/\\%_.=&amp;]*");
            return r.IsMatch(path);
        }

        public static bool IsValidWwwPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            Regex r = new Regex(@"(http|https|ftp)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return r.IsMatch(path);
        }


        public static double[] GetSearchPoint(double latPos, double lngPos, double radius)
        {

            //Earth�s radius, sphere
            double R = 6378137;

            //offsets in meters
            //double dn = 150;
            //double de = 150;
            double dn = radius;
            double de = radius;

            //Coordinate offsets in radians
            double dLat = dn / R;
            double dLon = de / (R * Math.Cos(Math.PI * latPos / 180));

            //OffsetPosition, decimal degrees
            double maxlat = latPos + dLat * 180 / Math.PI;
            double minlat = latPos - dLat * 180 / Math.PI;
            double maxlng = lngPos + dLon * 180 / Math.PI;
            double minlng = lngPos - dLon * 180 / Math.PI;

            return new double[] { minlat, maxlat, minlng, maxlng };
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

        public static string GetBreadcrumThumbnailUrl(string fileName)
        {
            try
            {
                string _thum = string.Empty;
                if (string.IsNullOrEmpty(fileName) || fileName.ToLower() == "breacrum-no-image.jpg")
                    return "/uploads/breacrum-no-image.jpg";
                if (IsValidWwwPath(fileName))
                    _thum = fileName;
                else
                    _thum = ReplaceServerPath(fileName);
                return _thum;
            }
            catch
            {
                return "/uploads/breacrum-no-image.jpg";
            }
        }

        public static string GetSmallThumbnailUrl(string fileName)
        {
            try
            {
                string _thumSmall = string.Empty;
                if (string.IsNullOrEmpty(fileName) || fileName.ToLower() == "no-image.jpg")
                    return "/uploads/socialnetwork/no-image.jpg";

                if (IsValidWwwPath(fileName))
                    _thumSmall = fileName;
                else
                {
                    string hostPath = string.Empty;
                    hostPath = HostPath;
                    string ip = string.Empty;
                    Dictionary<string, Dictionary<string, string>> dicServer = SettingManager.DicServer;
                    if (dicServer != null && dicServer.Count > 0)
                    {
                        foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePairString in dicServer)
                        {
                            if (string.IsNullOrEmpty(keyValuePairString.Key.Trim()))
                                continue;

                            if (fileName.ToLower().StartsWith(keyValuePairString.Key.ToLower()))
                            {
                                ip = fileName.Substring(0, keyValuePairString.Key.Length);
                                //fileName = fileName.Substring(keyValuePairString.Key.Length);
                                hostPath = keyValuePairString.Value["PublicIP"];
                                break;
                            }
                        }
                    }
                    fileName = Path.GetFileName(fileName);
                    if (hostPath.StartsWith("http") == false)
                        hostPath = "https://" + hostPath;
                    _thumSmall = string.Format("{0}/uploads/socialnetwork/{1}", hostPath.TrimEnd('/'), fileName.TrimStart('/'));
                }
                return _thumSmall;
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

        public static bool IsLocal()
        {
            string ip = Helpers.GetServerIP();
            if (string.IsNullOrEmpty(ip) == false)
            {
                if (ip.Split(':').Length > 1)
                    return true;
                else
                    return false;
            }
            return true;
        }

        static string ReplaceCapText(Match ma)
        {
            string ip = SweetCMS.Core.Helper.Helpers.GetServerIP();

            Dictionary<string, Dictionary<string, string>> dicServer = SweetCMS.Core.Manager.SettingManager.DicServer;

            string ss = ma.Groups[1].Value.Trim();

            if (string.IsNullOrEmpty(dicServer["sv1"]["PrivateIP"]) == true
                && string.IsNullOrEmpty(dicServer["sv1"]["PublicIP"]) == true)
            {
                if (ss.StartsWith("\""))
                    return "\"/uploads";
                else if (ss.StartsWith("'"))
                    return "'/uploads";
                else
                    return "/uploads";
            }

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
                string host = SweetCMS.Core.Helper.Helpers.HostPath;
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
                Dictionary<string, Dictionary<string, string>> dicServer = SettingManager.DicServer;
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
                            string ip = SweetCMS.Core.Helper.Helpers.GetServerIP();
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

        #region member user photo

        public static string GetCurFile(string chefId, string fileName, string type)
        {
            return string.Format("/uploads/User/{0}/{1}/{2}", chefId, type, fileName);
        }

        public static string CreateMapPathSave(string chefId, string endWith, string type)
        {
            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), endWith);
            string curFile = GetCurFile(chefId, fileName, type);
            while (File.Exists(curFile))
            {
                fileName = string.Format("{0}.{1}", Guid.NewGuid(), endWith);
                curFile = GetCurFile(chefId, fileName, type);
            }
            return curFile;
        }

        public static string GetUserAvatar(string userId, string fileName)
        {
            if (IsValidWwwPath(fileName))
                return fileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = string.Format("/uploads/User/{0}/{1}/{2}", userId, UploadType.UserAvatar, fileName);
                bool exists = File.Exists(HttpContext.Current.Server.MapPath(filePath));
                if (!exists)
                    filePath = string.Format("/uploads/User/{0}/ACCOUNT_PROFILE_DEFAULT.svg", UploadType.UserAvatar);
                return filePath;
            }
            else
                return string.Format("/uploads/User/{0}/ACCOUNT_PROFILE_DEFAULT.svg", UploadType.UserAvatar);
        }

        #endregion

        #region Image album
        public static string GetImageAlbumUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName.ToLower() == "no-image.jpg")
                return "/Uploads/ImageAlbum/no-image.jpg";

            if (IsValidWwwPath(fileName))
                return fileName;

            return ReplaceServerPath(fileName);
        }
        public static string GetUploadImageImageUrl(string fileName)
        {
            string path = string.Format("~/Uploads/ImageAlbum");
            DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(path));
            if (!dir.Exists)
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath(path));
            }
            return string.Format("{0}/{1}", path.TrimStart('~'), fileName);
        }

        public static string GetUploadProjectImageImageUrl(string fileName)
        {
            string path = string.Format("~/Uploads/Project/Images");
            DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(path));
            if (!dir.Exists)
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath(path));
            }
            return string.Format("{0}/{1}", path.TrimStart('~'), fileName);
        }
        #endregion
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

        public static string EncryptCode(string code, string type)
        {
            byte[] resultArray = ResultArray(code, true, type);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string DecryptCode(string code, string type)
        {
            code = code.Replace(' ', '+');
            byte[] resultArray = ResultArray(code, false, type);
            return Encoding.UTF8.GetString(resultArray);
        }
        public static string RemoveHtml(string text)
        {
            try
            {
                return Regex.Replace(text, "<[^>]*>", string.Empty);
            }
            catch
            {
                return text;
            }
        }

        public static string CreateSlugUrl(string phrase)
        {
            //First to lower case 
            phrase = VnUnicodeHelper.ReplaceVietnameseCharacters(phrase).ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(phrase);

            phrase = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            phrase = Regex.Replace(phrase, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars 
            phrase = Regex.Replace(phrase, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            //Trim dashes from end 
            phrase = phrase.Trim('-', '_');

            //Replace double occurences of - or \_ 
            phrase = Regex.Replace(phrase, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return phrase;
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
        public static string SaveImages(Stream objFile, string imageUrl, CMSImageType iType, bool appendSuffixIfExist)
        {
            try
            {
                string settings = string.Empty;
                //settings = "w=780&h=520&quality=100&mode=max";
                RenderAttribute attr = GetRenderAttribute(iType);
                if (attr != null)
                    settings = attr.ToImageResizeString();

                return SaveImages(objFile, imageUrl, settings, appendSuffixIfExist);
            }
            catch
            {
                objFile.Position = 0;
                return string.Empty;
            }
        }
        #endregion
        public static string SaveImages(Stream objFile, string imageUrl, string settings, bool appendSuffixIfExist)
        {
            try
            {
                if (appendSuffixIfExist == true)
                    imageUrl = NextAvailableFilename(imageUrl);

                using (var ms = new MemoryStream())
                {
                    //luon luon dung chat luong tot nhat =100
                    if (settings.IndexOf('&') > 0)
                        settings += "&";

                    settings += "quality=100";
                    //settings == null ? "" : settings
                    ImageJob sp = new ImageJob(objFile, ms, new Instructions(string.Empty), false, true);
                    ImageBuilder.Current.Build(sp);
                    //ImageResizer.Resizing.ImageState abc = new ImageResizer.Resizing.ImageState();

                    string folder = Path.GetDirectoryName(imageUrl);
                    if (Directory.Exists(folder) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(folder);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    using (var fileStream = new FileStream(imageUrl, FileMode.Create, FileAccess.ReadWrite))
                        ms.WriteTo(fileStream);

                    objFile.Position = 0;
                    return Path.GetFileName(imageUrl);
                }
            }
            catch (Exception exc)
            {
                objFile.Position = 0;
            }
            return string.Empty;
        }
    }
}
