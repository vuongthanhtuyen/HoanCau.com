using SweetCMS.Core.Helper;
using SweetCMS.Core.Manager;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SweetCMS.Core.SEO
{
    public class SEOHelper
    {
        private static string m_HostPath = null;
        private static string HostPath
        {
            get
            {
                if (m_HostPath != null)
                    return m_HostPath;

                return CommonHelper.GetHostPath();
            }
            set
            {
                m_HostPath = value;
            }
        }

        #region Methods
        /// <summary>
        /// Renders page meta tag
        /// </summary>
        /// <param name="page">Page instance</param>
        /// <param name="name">Meta name</param>
        /// <param name="content">ContentData</param>
        /// <param name="OverwriteExisting">Overwrite existing content if exists</param>
        public static void RenderMetaTag(Page page, string name, string content, bool OverwriteExisting)
        {
            if (page == null || page.Header == null)
                return;

            foreach (Control control in page.Header.Controls)
                if (control is HtmlMeta)
                {
                    HtmlMeta meta = (HtmlMeta)control;
                    if (meta.Name.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(content))
                    {
                        if (OverwriteExisting)
                            meta.Content = content;
                        else
                        {
                            if (String.IsNullOrEmpty(meta.Content))
                                meta.Content = content;
                        }
                    }
                }
        }

        /// <summary>
        /// Renders page title
        /// </summary>
        /// <param name="page">Page instance</param>
        /// <param name="title">Page title</param>
        /// <param name="OverwriteExisting">Overwrite existing content if exists</param>
        public static void RenderTitle(Page page, string title, bool OverwriteExisting)
        {
            if (page == null || page.Header == null)
                return;

            if (String.IsNullOrEmpty(title))
                return;

            if (OverwriteExisting)
                page.Title = title;
            else
            {
                if (String.IsNullOrEmpty(page.Title))
                    page.Title = title;
            }
        }

        /// <summary>
        /// Renders an RSS link to the page
        /// </summary>
        /// <param name="page">Page instance</param>
        /// <param name="title">RSS Title</param>
        /// <param name="href">Path to the RSS feed</param>
        public static void RenderHeaderRSSLink(Page page, string title, string href)
        {
            if (page == null || page.Header == null)
                return;

            HtmlGenericControl link = new HtmlGenericControl("LINK");
            link.Attributes.Add("rel", "alternate");
            link.Attributes.Add("type", "application/rss+xml");
            link.Attributes.Add("title", title);
            link.Attributes.Add("href", href);
            page.Header.Controls.Add(link);
        }
        
        #endregion


        //***************************************************************
        // Public SEO Function for front-end here: 
        //***************************************************************

        #region SEO Methods
        //public static string GetURLRSSByCategory(int categoryId)
        //{
        //    return string.Format("/RSS.aspx?CatId={0}", categoryId);
        //}

        //public static string GetRSSView()
        //{
        //    return "/RSSView.aspx";
        //}
        public const string CONTENT_ARCHOR = "";//"#_content";

        private static string ConvertToSEOFriendly(string title, int maxLength, bool noneUnicode)
        {
            if (string.IsNullOrEmpty(title))
                title = "Empty title";
            string seoTitle = title;
            if (noneUnicode)
                seoTitle = VnUnicodeHelper.ReplaceVietnameseCharacters(title);
            var match = Regex.Match(seoTitle.ToLower(), "[\\w]+");
            StringBuilder result = new StringBuilder("");
            bool maxLengthHit = false;
            while (match.Success && !maxLengthHit)
            {
                if (result.Length + match.Value.Length <= maxLength)
                {
                    result.Append(match.Value + "-");
                }
                else
                {
                    maxLengthHit = true;
                    // Handle a situation where there is only one word and it is greater than the max length.
                    if (result.Length == 0) result.Append(match.Value.Substring(0, maxLength));
                }
                match = match.NextMatch();
            }
            // Remove trailing '-'
            if (result[result.Length - 1] == '-') result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public static string GetArticleSEOUrl(int categoryId, string slugArticle)
        {
            try
            {
                string _slugCat = CategoryManager.GetCategorySlugUrlById(categoryId);
                return string.Format("/{0}/{1}{2}", _slugCat, slugArticle, CONTENT_ARCHOR);
            }
            catch
            {
                return slugArticle;
            }
        }

        public static string GetTourSEOUrl(int categoryId, string slugTour)
        {
            try
            {
                string _slugCat = CategoryManager.GetCategorySlugUrlById(categoryId);
                return string.Format("/{0}/{1}{2}", _slugCat, slugTour, CONTENT_ARCHOR);
            }
            catch
            {
                return slugTour;
            }
        }
        public static string GetServiceSEOUrl(int categoryId, string slugService)
        {
            try
            {
                string _slugCat = CategoryManager.GetCategorySlugUrlById(categoryId);
                return string.Format("/{0}/{1}{2}", _slugCat, slugService, CONTENT_ARCHOR);
            }
            catch
            {
                return slugService;
            }
        }
        #endregion
    }
}
