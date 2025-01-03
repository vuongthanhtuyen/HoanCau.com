using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TBDCMS.DataAccess;
using System.Collections;
using System.Collections.Specialized;
using System.Web.SessionState;
using System.IO;
using TBDCMS.Core.Helper;
using System.Web.Security;
using System.Configuration;

namespace TBDCMS.Core.Helper
{

    public sealed class ApplicationContext
    {
        //#region Private Containers

        ////Generally expect 10 or less items
        //private HybridDictionary m_Items = new HybridDictionary();
        private NameValueCollection m_QueryString = null;
        private string m_SiteUrl = null;
        private Uri m_CurrentUri;
        private string m_RawUrl;
        HttpContext m_HttpContext = null;

     
        public bool IsWebRequest
        {
            get { return this.Context != null; }
        }

        public HttpContext Context
        {
            get
            {
                return m_HttpContext;
            }
        }
        public Uri CurrentUri
        {
            get
            {
                if (m_CurrentUri == null)
                    m_CurrentUri = new Uri("https://localhost/");

                return m_CurrentUri;

            }
            set { m_CurrentUri = value; }
        }
        private void Initialize(NameValueCollection qs, Uri uri, string rawUrl, string siteUrl)
        {
            m_QueryString = qs;
            m_SiteUrl = siteUrl;
            m_CurrentUri = uri;
            m_RawUrl = rawUrl;
        }
        private ApplicationContext(HttpContext context, bool includeQueryString)
        {
            this.m_HttpContext = context;

            if (includeQueryString)
            {
                Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl, GetSiteUrl());
            }
            else
            {
                Initialize(null, context.Request.Url, context.Request.RawUrl, GetSiteUrl());
            }
        }

        private static readonly string dataKey = "ApplicationContextStore";

        private static ApplicationContext currentContext = null;

        public static ApplicationContext Current
        {
            get
            {
                HttpContext httpContext = HttpContext.Current;

                if (httpContext != null)
                {
                    if (httpContext.Items.Contains(dataKey))
                        return httpContext.Items[dataKey] as ApplicationContext;
                    else
                    {
                        ApplicationContext context = new ApplicationContext(httpContext, true);
                        SaveContextToStore(context);
                        return context;
                    }
                }

                if (currentContext == null)
                    throw new Exception("No ApplicationContext exists in the Current Application. AutoCreate fails since HttpContext.Current is not accessible.");
                return currentContext;
            }
        }
        private static void SaveContextToStore(ApplicationContext context)
        {
            if (context.IsWebRequest)
            {
                context.Context.Items[dataKey] = context;
            }
            else
            {
                currentContext = context;
            }
        }

        public const string SessionPrefix = SecurityHelper.APPLICATION_NAME;

        public HttpSessionState Session
        {
            get
            {
                return Context.Session;
            }
        }

        
        private string GetSiteUrl()
        {
            string hostName = Context.Request.Url.Host.Replace("www.", string.Empty);
            string applicationPath = Context.Request.ApplicationPath;

            if (applicationPath.EndsWith("/"))
                applicationPath = applicationPath.Remove(applicationPath.Length - 1, 1);

            return hostName + applicationPath;

        }

        //#endregion

        //#region Client Language Context
        private int _languageId = 0;
        private int _defaultLanguageId = LanguageHelper.Defaultlanguage;
        private const string CURRENT_LANGUAGE_ID = "TBD_CURRENT_LANGUAGE_ID";
        private const string CURRENT_LANGUAGE_CODE = "TBD_CURRENT_LANGUAGE_CODE";

        public int CurrentLanguageId
        {
            get
            {
                return GetCurrentLanguage();
            }
            set
            {
                _languageId = value;
                WriteLanguageIdToCookie(_languageId);
            }
        }

        private int GetCurrentLanguage()
        {
            if (_languageId != 0)
                return _languageId;

            if (_languageId == 0)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_ID];
                if (cookie != null)
                {
                    int.TryParse(cookie.Value, out _languageId);
                }
            }

            return _languageId == 0 ? _defaultLanguageId : _languageId;
        }

        private void WriteLanguageIdToCookie(int languaugeId)
        {
            if (HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_ID] != null)
            {
                HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_ID].Value = languaugeId.ToString();
                HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_ID]);
            }
            else
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(CURRENT_LANGUAGE_ID, languaugeId.ToString()));


            HttpContext.Current.Response.Cookies[CURRENT_LANGUAGE_ID].Expires = DateTime.Now.AddDays(1);

        }

      

        #region CMS Language Context
        private int _cmsLanguageId = 0;
        private int _cmsdefaultLanguageId = LanguageHelper.Defaultlanguage;
        private const string CMS_CURRENT_LANGUAGE_ID = "TBD_CMS_CURRENT_LANGUAGE_ID";
        private const string CMS_CURRENT_LANGUAGE_CODE = "TBD_CMS_CURRENT_LANGUAGE_CODE";
        public string CMSCurrentLanguageCode
        {
            get
            {
                return GetCurrentCMSLanguageCode(CMSCurrentLanguageId);
            }


        }
     
        public int CMSCurrentLanguageId
        {
            get
            {
                return GetCMSCurrentLanguage();
            }
            set
            {
                _cmsLanguageId = value;
                WriteCMSLanguageIdToCookie(_cmsLanguageId);
            }
        }
        private int GetCMSCurrentLanguage()
        {
            if (_cmsLanguageId != 0)
                return _cmsLanguageId;

            if (_cmsLanguageId == 0)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_ID];
                if (cookie != null)
                {
                    int.TryParse(cookie.Value, out _cmsLanguageId);
                }
            }

            return _cmsLanguageId == 0 ? _cmsdefaultLanguageId : _cmsLanguageId;
        }

        private void WriteCMSLanguageIdToCookie(int languaugeId)
        {
            if (HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_ID] != null)
            {
                HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_ID].Value = languaugeId.ToString();
                HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_ID]);
            }
            else
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(CMS_CURRENT_LANGUAGE_ID, languaugeId.ToString()));

            HttpContext.Current.Response.Cookies[CMS_CURRENT_LANGUAGE_ID].Expires = DateTime.Now.AddDays(1);
        }

        private string GetCurrentCMSLanguageCode(int languageId)
        {
            Dictionary<int, string> languageCode = LanguageHelper.LanguageCode;
            if (languageCode.ContainsKey(languageId))
                return languageCode[languageId];
            else
                return languageCode[LanguageHelper.English];
        }
        #endregion

        #region Content Language Context
        private int _contentLanguageId = 0;
        private int _deafultContentLanguageId = LanguageHelper.Defaultlanguage;
        private const string CONTENT_CURRENT_LANGUAGE_ID = "TBD_CONTENT_CURRENT_LANGUAGE_ID";
        private const string CONTENT_CURRENT_LANGUAGE_CODE = "TBD_CONTENT_CURRENT_LANGUAGE_CODE";
    
        public int ContentCurrentLanguageId
        {
            get
            {
                return GetContentCurrentLanguage();
            }
            set
            {
                _contentLanguageId = value;
                WriteContentLanguageIdToCookie(_contentLanguageId);
            }
        }

      
        private int GetContentCurrentLanguage()
        {
            if (_contentLanguageId != 0)
                return _contentLanguageId;

            if (_contentLanguageId == 0)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_ID];
                if (cookie != null)
                {
                    int.TryParse(cookie.Value, out _contentLanguageId);
                }
            }

            return _contentLanguageId == 0 ? _deafultContentLanguageId : _contentLanguageId;
        }

        private void WriteContentLanguageIdToCookie(int languaugeId)
        {
            if (HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_ID] != null)
            {
                HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_ID].Value = languaugeId.ToString();
                HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_ID]);
            }
            else
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(CONTENT_CURRENT_LANGUAGE_ID, languaugeId.ToString()));


            HttpContext.Current.Response.Cookies[CONTENT_CURRENT_LANGUAGE_ID].Expires = DateTime.Now.AddDays(1);

        }
        #endregion
        public int CurrentUserID
        {
            get
            {
                object id = Session["IdUser"];
                int tempId = 0;
                if (id != null)
                    int.TryParse(id.ToString(), out tempId);

                return tempId;
            }
            set
            {
                Session["IdUser"] = value;
            }
        }

     
    }
}
