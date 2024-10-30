using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SweetCMS.DataAccess;
using System.Collections;
using System.Collections.Specialized;
using System.Web.SessionState;
using System.IO;
using SweetCMS.Core.Helper;
using System.Web.Security;
using System.Configuration;

namespace SweetCMS.Core.Helper
{
    /// <summary>
    /// The ApplicationContext represents common properties and settings used through out of a Request. All data stored
    /// in the context will be cleared at the end of the request
    /// 
    /// This object should be safe to use outside of a web request, but querystring and other values should be prepopulated
    /// 
    /// Each CS thread must create an instance of the ApplicationContext using one of the Three Create overloads. In some cases, 
    /// the CreateEmptyContext method may be used, but it is NOT recommended.
    /// </summary>
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

        //#endregion

        //#region Core Properties
        ///// <summary>
        ///// Simulates Context.Items and provides a per request/instance storage bag
        ///// </summary>
        //public IDictionary Items
        //{
        //    get { return m_Items; }
        //}

        ///// <summary>
        ///// Provides direct access to the .Items property
        ///// </summary>
        //public object this[string key]
        //{
        //    get
        //    {
        //        return this.Items[key];
        //    }
        //    set
        //    {
        //        this.Items[key] = value;
        //    }
        //}

        ///// <summary>
        ///// Allows access to QueryString values
        ///// </summary>
        //public NameValueCollection QueryString
        //{
        //    get { return m_QueryString; }
        //}

        ///// <summary>
        ///// Quick check to see if we have a valid web reqeust. Returns false if HttpContext == null
        ///// </summary>
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

        //public string SiteUrl
        //{
        //    get { return m_SiteUrl; }
        //}

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

        //private string _hostPath = null;
        ///// <summary>
        ///// 
        ///// </summary>
        //public string HostPath
        //{
        //    get
        //    {
        //        if (_hostPath == null)
        //        {
        //            string portInfo = CurrentUri.Port == 80 ? string.Empty : ":" + CurrentUri.Port.ToString();
        //            _hostPath = string.Format("{0}://{1}{2}", CurrentUri.Scheme, CurrentUri.Host, portInfo);
        //        }
        //        return _hostPath;
        //    }
        //}

        //#endregion

        //#region Initialize and contructors

        //public static void ClearAdminData()
        //{
        //    ApplicationContext.Current.ClearSession("CurrentUser");
        //    ApplicationContext.Current.ClearSession("CurrentUserName");
        //    ApplicationContext.Current.ClearSession("CurrentUserID");
        //    ApplicationContext.Current.ClearSession("CurrentUserIp");
        //    ApplicationContext.Current.ClearSession("CurrentUserFunctions");
        //}

        //public void ClearSession(string sessionName)
        //{
        //    Session[SessionPrefix + sessionName] = null;
        //}
        ///// <summary>
        ///// Create/Instatiate items that will vary based on where this object 
        ///// is first created
        ///// 
        ///// We could wire up Path, encoding, etc as necessary
        ///// </summary>
        private void Initialize(NameValueCollection qs, Uri uri, string rawUrl, string siteUrl)
        {
            m_QueryString = qs;
            m_SiteUrl = siteUrl;
            m_CurrentUri = uri;
            m_RawUrl = rawUrl;
        }

        ///// <summary>
        ///// cntr called when no HttpContext is available
        ///// </summary>
        private ApplicationContext(Uri uri, string siteUrl)
        {
            Initialize(new NameValueCollection(), uri, uri.ToString(), siteUrl);
        }

        ///// <summary>
        ///// cnst called when HttpContext is avaiable
        ///// </summary>
        ///// <param name="context"></param>
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

        //#endregion

        //#region State

        private static readonly string dataKey = "ApplicationContextStore";

        //[ThreadStatic]
        private static ApplicationContext currentContext = null;

        ///// <summary>
        ///// Returns the current instance of the CMSContext from the ThreadData Slot. If one is not found and a valid HttpContext can be found,
        ///// it will be used. Otherwise, an exception will be thrown. 
        ///// </summary>
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

        ///// <summary>
        ///// Remove current context out of memmory
        ///// </summary>
        public static void Unload()
        {
            currentContext = null;
        }
        //#endregion

        //#region Session context

        public const string SessionPrefix = SecurityHelper.APPLICATION_NAME;

        public HttpSessionState Session
        {
            get
            {
                return Context.Session;
            }
        }

        //#endregion

        //#region URL context
        ///// <summary>
        ///// Gets server variable by name
        ///// </summary>
        ///// <param name="Name">Name</param>
        ///// <returns>Server variable</returns>

        //public string MapPath(string path)
        //{
        //    if (Context != null)
        //        return Context.Server.MapPath(path);
        //    else
        //        return PhysicalPath(path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("~", ""));
        //}

        //public string PhysicalPath(string path)
        //{
        //    return string.Concat(RootPath().TrimEnd(Path.DirectorySeparatorChar), Path.DirectorySeparatorChar.ToString(), path.TrimStart(Path.DirectorySeparatorChar));
        //}

        //private string m_RootPath;
        //private string RootPath()
        //{
        //    if (m_RootPath == null)
        //    {
        //        m_RootPath = AppDomain.CurrentDomain.BaseDirectory;
        //        string dirSep = Path.DirectorySeparatorChar.ToString();

        //        m_RootPath = m_RootPath.Replace("/", dirSep);
        //    }
        //    return m_RootPath;
        //}

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
        private const string CURRENT_LANGUAGE_ID = "SWEETSOFT_CURRENT_LANGUAGE_ID";
        private const string CURRENT_LANGUAGE_CODE = "SWEETSOFT_CURRENT_LANGUAGE_CODE";

        ///// <summary>
        ///// Get current language code
        ///// </summary>
        public string CurrentLanguageCode
        {
            get
            {
                return GetCurrentLanguageCode(CurrentLanguageId);
            }


        }

        //private void WriteLanguageIdToCookie(string languaugeCode)
        //{
        //    if (HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_CODE] != null)
        //    {
        //        HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_CODE].Value = languaugeCode;
        //        HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CURRENT_LANGUAGE_CODE]);
        //    }
        //    else
        //        HttpContext.Current.Response.Cookies.Set(new HttpCookie(CURRENT_LANGUAGE_CODE, languaugeCode));

        //    //Cookie will be expired in 7 days
        //    HttpContext.Current.Response.Cookies[CURRENT_LANGUAGE_CODE].Expires = DateTime.Now.AddDays(7);

        //}

        ////Hiền Trần 13/10/2014
        ///// <summary>
        ///// Get current language Id
        ///// </summary>
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

        public void UpdateCurrentLanguage()
        {
            WriteLanguageIdToCookie(CurrentLanguageId);
        }

        ///// <summary>
        ///// Return current language id (1: english, 2:vietnamese)
        ///// </summary>
        ///// <returns></returns>
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

        private string GetCurrentLanguageCode(int languageId)
        {
            Dictionary<int, string> languageCode = LanguageHelper.LanguageCode;
            if (languageCode.ContainsKey(languageId))
                return languageCode[languageId];
            else
                return languageCode[LanguageHelper.English];
        }
        //#endregion

        #region CMS Language Context
        private int _cmsLanguageId = 0;
        private int _cmsdefaultLanguageId = LanguageHelper.Defaultlanguage;
        private const string CMS_CURRENT_LANGUAGE_ID = "SWEETSOFT_CMS_CURRENT_LANGUAGE_ID";
        private const string CMS_CURRENT_LANGUAGE_CODE = "SWEETSOFT_CMS_CURRENT_LANGUAGE_CODE";
        public string CMSCurrentLanguageCode
        {
            get
            {
                return GetCurrentCMSLanguageCode(CMSCurrentLanguageId);
            }


        }
        private void WriteCMSLanguageIdToCookie(string languaugeCode)
        {
            if (HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_CODE] != null)
            {
                HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_CODE].Value = languaugeCode;
                HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CMS_CURRENT_LANGUAGE_CODE]);
            }
            else
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(CMS_CURRENT_LANGUAGE_CODE, languaugeCode));

            //Cookie will be expired in 7 days
            HttpContext.Current.Response.Cookies[CMS_CURRENT_LANGUAGE_CODE].Expires = DateTime.Now.AddDays(7);

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

        public void UpdateCMSCurrentLanguage()
        {
            WriteCMSLanguageIdToCookie(CMSCurrentLanguageId);
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
        private const string CONTENT_CURRENT_LANGUAGE_ID = "SWEETSOFT_CONTENT_CURRENT_LANGUAGE_ID";
        private const string CONTENT_CURRENT_LANGUAGE_CODE = "SWEETSOFT_CONTENT_CURRENT_LANGUAGE_CODE";
        public string ContentCurrentLanguageCode
        {
            get
            {
                return GetCurrentContentLanguageCode(ContentCurrentLanguageId);
            }


        }
        private void WriteContentLanguageIdToCookie(string languaugeCode)
        {
            if (HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_CODE] != null)
            {
                HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_CODE].Value = languaugeCode;
                HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CONTENT_CURRENT_LANGUAGE_CODE]);
            }
            else
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(CONTENT_CURRENT_LANGUAGE_CODE, languaugeCode));

            //Cookie will be expired in 7 days
            HttpContext.Current.Response.Cookies[CONTENT_CURRENT_LANGUAGE_CODE].Expires = DateTime.Now.AddDays(7);

        }
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

        public void UpdateContentCurrentLanguage()
        {
            WriteContentLanguageIdToCookie(ContentCurrentLanguageId);
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

        private string GetCurrentContentLanguageCode(int languageId)
        {
            Dictionary<int, string> languageCode = LanguageHelper.LanguageCode;
            if (languageCode.ContainsKey(languageId))
                return languageCode[languageId];
            else
                return languageCode[LanguageHelper.English];
        }
        #endregion

        //#region Gridview page size

        //private const string CURRENT_PAGE_SIZE = "SWEETSOFT_PAGE_SIZE";

        //private string m_CurrentPageSize;
        //public string CurrentPageSize
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(m_CurrentPageSize))
        //        {   //Try to get from cookie
        //            HttpCookie cookie = HttpContext.Current.Request.Cookies[CURRENT_PAGE_SIZE];
        //            if (cookie != null)
        //                m_CurrentPageSize = cookie.Value;
        //            else
        //            {
        //                int pageSize = SettingManager.GetSettingValueIntAdmin(SettingNames.DataGridItemsPerPage, 20);
        //                m_CurrentPageSize = pageSize.ToString();
        //            }
        //        }

        //        return m_CurrentPageSize;
        //    }
        //    set
        //    {
        //        m_CurrentPageSize = value;
        //        WritePageSizeToCookie(m_CurrentPageSize);
        //    }
        //}

        //private void WritePageSizeToCookie(string pageSize)
        //{
        //    if (HttpContext.Current.Request.Cookies[CURRENT_PAGE_SIZE] != null)
        //    {
        //        HttpContext.Current.Request.Cookies[CURRENT_PAGE_SIZE].Value = pageSize;
        //        HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CURRENT_PAGE_SIZE]);
        //    }
        //    else
        //        HttpContext.Current.Response.Cookies.Set(new HttpCookie(CURRENT_PAGE_SIZE, pageSize));

        //    //Cookie will be expired in 7 days
        //    HttpContext.Current.Response.Cookies[CURRENT_PAGE_SIZE].Expires = DateTime.Now.AddDays(7);

        //}

        //#endregion

        //#region SweetCMS Data

        ///// <summary>
        ///// Return the current logged in UserName. This is read-only, value has bee set by the UserAccountship Provider
        ///// </summary>
        public string UserName
        {
            get
            {
                // if (!this.IsWebRequest || this.Context.User == null || this.Context.User.Identity.Name == string.Empty)
                if (Session[SessionPrefix + "CurrentUserName"] == null)
                {
                    //use UserAccountship
                    //UserAccountship.GetUser().UserName;

                    //string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();

                    if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                        return "Anonymous";
                    else
                        return HttpContext.Current.User.Identity.Name;
                }

                return Session[SessionPrefix + "CurrentUserName"].ToString();
            }
            set
            {
                Session[SessionPrefix + "CurrentUserName"] = value;
            }
        }

        //public bool CheckFunctionPermission(string userName, string FUNCTION_PAGE_ID)
        //{
        //    List<string> currentUserFunctions = CurrentUserFunctions;
        //    if (currentUserFunctions == null || currentUserFunctions.Count == 0)
        //    {
        //        currentUserFunctions = RoleManager.GetFunctionIdsByUserName(userName);
        //        CurrentUserFunctions = currentUserFunctions;
        //    }

        //    if (currentUserFunctions.Contains(FUNCTION_PAGE_ID) || ApplicationContext.Current.IsAdministrator)
        //        return true;
        //    else
        //        return false;
        //}

        //public List<string> CurrentUserFunctions
        //{
        //    get
        //    {
        //        if (Session[SessionPrefix + "CurrentUserFunctions"] != null)
        //            return (List<string>)Session[SessionPrefix + "CurrentUserFunctions"];

        //        return null;
        //    }
        //    set
        //    {
        //        Session[SessionPrefix + "CurrentUserFunctions"] = value;
        //    }
        //}

        ///// <summary>
        ///// Return the current logged in UserID. This is read-only, value has bee set by the UserAccountship Provider
        ///// </summary>
        //public Guid CMSUserID
        //{
        //    get
        //    {
        //        object id = Session[SessionPrefix + "CurrentUserID"];
        //        Guid tempId = Guid.Empty;
        //        if (id != null)
        //            Guid.TryParse(id.ToString(), out tempId);

        //        // if (!this.IsWebRequest || this.Context.User == null || this.Context.User.Identity.Name == string.Empty)
        //        if (tempId == Guid.Empty)
        //        {
        //            TblAdminUser user = User;
        //            if (user == null)
        //                return Guid.Empty;
        //            else
        //            {
        //                Session[SessionPrefix + "CurrentUserID"] = user.UserID;
        //                return user.UserID;
        //            }
        //        }

        //        return tempId;
        //    }
        //    set
        //    {
        //        Session[SessionPrefix + "CurrentUserID"] = value;
        //    }
        //}

        //public bool IsLogin
        //{
        //    get
        //    {
        //        return this.User != null;
        //    }
        //}

        ///// <summary>
        ///// Return the current logged in User. This user value to be anonymous if no user is logged in.
        ///// This value can be set if necessary
        ///// </summary>
        //public TblAdminUser User
        //{
        //    get
        //    {
        //        TblAdminUser m_CurrentUser = null;
        //        if (Session[SessionPrefix + "CurrentUser"] != null)
        //            m_CurrentUser = (TblAdminUser)Session[SessionPrefix + "CurrentUser"];

        //        if (m_CurrentUser == null)
        //        {
        //            m_CurrentUser = CMSUserManager.GetUserByName(UserName);
        //            Session[SessionPrefix + "CurrentUser"] = m_CurrentUser;
        //        }

        //        return m_CurrentUser;
        //    }
        //}

        //public string AdminFullName
        //{
        //    get
        //    {
        //        TblAdminUser user = User;
        //        return user != null ? User.DisplayName : null;
        //    }
        //}

        //const string ApplicationID_Cache = SecurityHelper.APPLICATION_NAME + ".ApplicationId";
        //public Guid ApplicationId
        //{
        //    get
        //    {
        //        Guid appId = Guid.Empty;
        //        object data = AppCache.Get(ApplicationID_Cache);
        //        if (data != null)
        //            Guid.TryParse(data.ToString(), out appId);

        //        if (appId == null || appId == Guid.Empty)
        //        {
        //            appId = new SubSonic.InlineQuery().ExecuteScalar<Guid>("SELECT ApplicationId FROM aspnet_Applications WHERE ApplicationName=@appName",
        //            SecurityHelper.APPLICATION_NAME);
        //            AppCache.Remove(ApplicationID_Cache);
        //            AppCache.Max(ApplicationID_Cache, appId);
        //        }

        //        return appId;
        //    }
        //}

        //public string CurrentUserIp
        //{
        //    get
        //    {
        //        if (Session[SessionPrefix + "CurrentUserIp"] != null)
        //            return Session[SessionPrefix + "CurrentUserIp"] as string;
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        Session[SessionPrefix + "CurrentUserIp"] = value;
        //    }
        //}

        //public bool IsAdministrator
        //{
        //    get
        //    {
        //        return ApplicationContext.Current.User.UserName == "administrator" || System.Web.Security.Roles.IsUserInRole(ApplicationContext.Current.User.UserName, "Administration");
        //    }
        //}

        //public void ReloadUser()
        //{
        //    if (!string.IsNullOrEmpty(UserName))
        //        Session[SessionPrefix + "CurrentUser"] = CMSUserManager.GetUserByName(UserName);
        //}

        //#endregion

        //#region UserAccount account
        //public TblUserAccount UserAccount
        //{
        //    get
        //    {
        //        if (Session[SessionPrefix + "UserAccount"] != null && Session[SessionPrefix + "UserAccount"] is TblUserAccount)
        //            return Session[SessionPrefix + "UserAccount"] as TblUserAccount;
        //        else
        //        {
        //            if(HttpContext.Current.Request.Cookies["shdtravel_userId"] != null)
        //            {
        //                TblUserAccount userAccount = UserManager.GetUserByID(HttpContext.Current.Request.Cookies["shdtravel_userId"].Value);
        //                if(userAccount != null)
        //                {
        //                    return userAccount;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        Session[SessionPrefix + "UserAccount"] = value;
        //    }
        //}

        //public string CurrentUserAccountIp
        //{
        //    get
        //    {
        //        if (Session[SessionPrefix + "UserAccountIP"] != null)
        //            return Session[SessionPrefix + "UserAccountIP"] as string;
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        Session[SessionPrefix + "UserAccountIP"] = value;
        //    }
        //}

        //public string FullName
        //{
        //    get
        //    {
        //        return UserAccount != null ? UserAccount.DisplayName : string.Empty;
        //    }
        //}

        //public long MemberId
        //{
        //    get
        //    {
        //        return UserAccount != null ? UserAccount.Id : 0;
        //    }
        //}
        //public void AddUserAccount(TblUserAccount UserAccount)
        //{
        //    Session[SessionPrefix + "UserAccount"] = UserAccount;
        //}

        //public void ClearUserAccount()
        //{
        //    Session[SessionPrefix + "UserAccount"] = null;
        //    Session.Remove(SessionPrefix + "UserAccount");
        //}
        //public string DefaultPassword
        //{
        //    get
        //    {
        //        try
        //        {
        //            AppSettingsReader settingsReader = new AppSettingsReader();
        //            string strDefaultPassword = (string)settingsReader.GetValue("DefaultPassword", typeof(string));
        //            if (string.IsNullOrEmpty(strDefaultPassword))
        //                return string.Empty;
        //            return strDefaultPassword;
        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}
        //#endregion
    }
}
