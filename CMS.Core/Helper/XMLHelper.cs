using SweetCMS.Core.Manager;
using SweetCMS.Core.SEO;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SweetCMS.Core.Helper
{
    public class XMLHelper
    {
        #region Methods
        public static void BuildXmlUrlMenuToFile(string serverPath)
        {
            XmlDocument xmlMenuDoc = new XmlDocument();
            xmlMenuDoc.LoadXml(GetUrlSetMenu()); //Build XML
            const string fileName = "sitemap.xml";
            string fileNamePath = string.Format("{0}\\{1}", serverPath, fileName);
            if (!File.Exists(fileNamePath))
                File.Delete(fileNamePath);
            XmlTextWriter xmlWriter = new XmlTextWriter(fileNamePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 3;
            xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            xmlWriter.WriteComment(string.Format("{0} sitemap",CommonHelper.GetHostPath().TrimEnd('/')));
            xmlMenuDoc.WriteTo(xmlWriter);
            xmlWriter.Flush();
            xmlWriter.Close();
        }
        private static string GetUrlSetMenu()
        {
            StringBuilder urlsetmenu = new StringBuilder();
            urlsetmenu.Append("<urlset");
            urlsetmenu.Append(" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            urlsetmenu.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            urlsetmenu.Append(" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
            urlsetmenu.Append(GetAllUrl()); //Create node
            urlsetmenu.AppendLine("</urlset>");
            return urlsetmenu.ToString();
        }
        private static string GetAllUrl()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<ItemFriendlyURLExtend> objColl = FriendlyURLManager.GetAll();
                if(objColl != null && objColl.Count > 0)
                {
                    foreach (var item in objColl)
                    {
                        if (item != null)
                        {
                            if(item.PostType == FriendlyURLTypeHelper.Article)
                            {
                                sb.Append("<url>");
                                sb.Append("<loc>");
                                sb.Append(XmlEncodeAttribute(string.Format("{0}{1}", CommonHelper.GetHostPath().TrimEnd('/'),
                                    SEOHelper.GetArticleSEOUrl(CategoryManager.GetCategoryIdByArticleId(item.PostId), item.SlugUrl))));
                                sb.Append("</loc>");
                                sb.AppendFormat("<lastmod>{0}</lastmod>", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                                sb.Append("<changefreq>weekly</changefreq>");
                                sb.Append("</url>");
                            }
                            else if(item.PostType == FriendlyURLTypeHelper.Category)
                            {
                                sb.Append("<url>");
                                sb.Append("<loc>");
                                sb.Append(XmlEncodeAttribute(string.Format("{0}{1}", CommonHelper.GetHostPath(), item.SlugUrl.TrimStart('/'))));
                                sb.Append("</loc>");
                                sb.AppendFormat("<lastmod>{0}</lastmod>", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                                sb.Append("<changefreq>daily</changefreq>");
                                sb.Append("</url>");
                            }
                            //else if (item.PostType == FriendlyURLTypeHelper.Video)
                            //{
                            //    sb.Append("<url>");
                            //    sb.Append("<loc>");
                            //    sb.Append(XmlEncodeAttribute(string.Format("{0}{1}", CommonHelper.GetHostPath(), item.SlugUrl.TrimStart('/'))));
                            //    sb.Append("</loc>");
                            //    sb.AppendFormat("<lastmod>{0}</lastmod>", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                            //    sb.Append("<changefreq>weekly</changefreq>");
                            //    sb.Append("</url>");
                            //}

                        }
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        private static string CreatUrlNode(int menuId)
        {
            StringBuilder urlSetMenu = new StringBuilder();
            List<ItemMenuExtend> lstMenu = null;
            if (menuId == 0)
                lstMenu = MenuManager.GetAllActiveMenu(menuId, MenuTypeHelper.Top);
            if (lstMenu != null && lstMenu.Count > 0)
            {
                foreach (var menu in lstMenu)
                {
                    urlSetMenu.Append("<url>");
                    urlSetMenu.Append("<loc>");
                    urlSetMenu.Append(XmlEncodeAttribute(string.Format("{0}{1}", CommonHelper.GetHostPath(), menu.Link.TrimStart('/'))));
                    urlSetMenu.Append("</loc>");
                    urlSetMenu.AppendFormat("<lastmod>{0}</lastmod>", DateTime.Now.ToShortDateString());
                    urlSetMenu.Append("<changefreq>daily</changefreq>");
                    urlSetMenu.Append("</url>");

                    if (MenuManager.CheckMenuHasChild(menu.Id))
                        urlSetMenu.Append(CreatUrlNode(menu.Id));
                }
            }
            return urlSetMenu.ToString();
        }
        /// <summary>
        /// XML Encode
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncode(string s)
        {
            if (s == null)
                return null;
            s = Regex.Replace(s, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAsIs(s);
        }

        /// <summary>
        /// XML Encode as is
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncodeAsIs(string s)
        {
            if (s == null)
                return null;
            using (StringWriter sw = new StringWriter())
            using (XmlTextWriter xwr = new XmlTextWriter(sw))
            {
                xwr.WriteString(s);
                String sTmp = sw.ToString();
                return sTmp;
            }
        }

        /// <summary>
        /// Encodes an attribute
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Encoded attribute</returns>
        public static string XmlEncodeAttribute(string s)
        {
            if (s == null)
                return null;
            s = Regex.Replace(s, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAttributeAsIs(s);
        }

        /// <summary>
        /// Encodes an attribute as is
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Encoded attribute</returns>
        public static string XmlEncodeAttributeAsIs(string s)
        {
            return XmlEncodeAsIs(s).Replace("\"", "&quot;");
        }

        /// <summary>
        /// Decodes an attribute
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Decoded attribute</returns>
        public static string XmlDecode(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            return sb.Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").ToString();
        }

        /// <summary>
        /// Serializes a datetime
        /// </summary>
        /// <param name="dateTime">Datetime</param>
        /// <returns>Serialized datetime</returns>
        public static string SerializeDateTime(DateTime dateTime)
        {
            XmlSerializer xmlS = new XmlSerializer(typeof(DateTime));
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                xmlS.Serialize(sw, dateTime);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Deserializes a datetime
        /// </summary>
        /// <param name="dateTime">Datetime</param>
        /// <returns>Deserialized datetime</returns>
        public static DateTime DeserializeDateTime(string dateTime)
        {
            XmlSerializer xmlS = new XmlSerializer(typeof(DateTime));
            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(dateTime))
            {
                object test = xmlS.Deserialize(sr);
                return (DateTime)test;
            }
        }
        #endregion
    }
}
