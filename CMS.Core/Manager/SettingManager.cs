using CMS.Core.ApplicationCache;
using SweetCMS.Core.ApplicationCache;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SweetCMS.Core.Manager
{
    public class SettingNames
    {
        private static string SettingNamePrefix = "SweetCMS.Settings.{0}";
        public static string PrefixBooking = "PrefixBooking";

        #region Content home page
        //Slide
        public static string DisplaySlideOnHomePage = string.Format(SettingNamePrefix, "DisplaySlideOnHomePage");
        public static string NumberItemSlideOnHomePage = string.Format(SettingNamePrefix, "NumberItemSlideOnHomePage");


        //About Us
        public static string DisplayAboutUsContentHomePage = string.Format(SettingNamePrefix, "DisplayAboutUsContentHomePage");
        public static string TitleAboutUsHomePage = string.Format(SettingNamePrefix, "TitleAboutUsHomePage");
        public static string ContentAboutUsForHomePage = string.Format(SettingNamePrefix, "ContentAboutUsForHomePage");

        //Room
        public static string DisplayRoomContentHomePage = string.Format(SettingNamePrefix, "DisplayRoomContentHomePage");
        public static string TitleRoomHomePage = string.Format(SettingNamePrefix, "TitleRoomHomePage");
        public static string NumberItemRoomHomePage = string.Format(SettingNamePrefix, "NumberItemRoomHomePage");
        public static string RoomCategoryHomePage = string.Format(SettingNamePrefix, "RoomCategoryHomePage");

        //Service
        public static string DisplayServiceContentHomePage = string.Format(SettingNamePrefix, "DisplayServiceContentHomePage");
        public static string TitleServiceHomePage = string.Format(SettingNamePrefix, "TitleServiceHomePage");
        public static string NumberItemServiceHomePage = string.Format(SettingNamePrefix, "NumberItemServiceHomePage");
        public static string ServiceCategoryHomePage = string.Format(SettingNamePrefix, "ServiceCategoryHomePage");

        //Promotion
        public static string DisplayPromotionContentHomePage = string.Format(SettingNamePrefix, "DisplayPromotionContentHomePage");
        public static string ArticleCategoryPromotionHomepage = string.Format(SettingNamePrefix, "ArticleCategoryPromotionHomepage");
        public static string NumberItemPromotionHomePage = string.Format(SettingNamePrefix, "NumberItemPromotionHomePage");

        //Testimonials
        public static string DisplayTestimonialsContentHomePage = string.Format(SettingNamePrefix, "DisplayTestimonialsContentHomePage");
        public static string TitleTestimonialsHomePage = string.Format(SettingNamePrefix, "TitleTestimonialsHomePage");
        public static string NumberItemTestimonialsHomePage = string.Format(SettingNamePrefix, "NumberItemTestimonialsHomePage");

        //News event
        public static string DisplayNewsAndEventContentHomePage = string.Format(SettingNamePrefix, "DisplayNewsAndEventContentHomePage");
        public static string NumberItemNewsAndEventHomePage = string.Format(SettingNamePrefix, "NumberItemNewsAndEventHomePage");
        public static string ArticleCategoryNewsAndEventHomePage = string.Format(SettingNamePrefix, "ArticleCategoryNewsAndEventHomePage");

        //High-class amenities
        public static string DisplayHighclassAmenitiesContentHomePage = string.Format(SettingNamePrefix, "DisplayHighclassAmenitiesContentHomePage");
        public static string ImageHighclassAmenitiesHomePage = string.Format(SettingNamePrefix, "ImageHighclassAmenitiesHomePage");
        public static string SubTitleHighclassAmenitiesHomePage = string.Format(SettingNamePrefix, "SubTitleHighclassAmenitiesHomePage");
        public static string TitleHighclassAmenitiesHomePage = string.Format(SettingNamePrefix, "TitleHighclassAmenitiesHomePage");
        public static string LinkHighclassAmenitiesHomePage = string.Format(SettingNamePrefix, "LinkHighclassAmenitiesHomePage");

        public static string LinkGymHomePage = string.Format(SettingNamePrefix, "LinkGymHomePage");
        public static string LinkPoolHomePage = string.Format(SettingNamePrefix, "LinkPoolHomePage");
        public static string LinkSteamHomePage = string.Format(SettingNamePrefix, "LinkSteamHomePage");

        #endregion


        #region Social Setting
        public static string FacebookAppId = string.Format(SettingNamePrefix, "FacebookAppId");
        public static string FacebookUrl = string.Format(SettingNamePrefix, "FacebookUrl");
        public static string AdminFacebookPage = string.Format(SettingNamePrefix, "AdminFacebookPage");
        public static string FacebookPageId = string.Format(SettingNamePrefix, "FacebookPageId");
        public static string TwitterUrl = string.Format(SettingNamePrefix, "TwitterUrl");
        public static string YoutubeUrl = string.Format(SettingNamePrefix, "YoutubeUrl");
        public static string InstagramUrl = string.Format(SettingNamePrefix, "InstagramUrl");
        public static string GooglePlusUrl = string.Format(SettingNamePrefix, "GooglePlusUrl");
        public static string PinterestUrl = string.Format(SettingNamePrefix, "PinterestUrl");
        public static string TripAdvisorUrl = string.Format(SettingNamePrefix, "TripAdvisorUrl");
        public static string ContactSkypeOne = string.Format(SettingNamePrefix, "ContactSkypeOne");
        public static string ContactSkypeTwo = string.Format(SettingNamePrefix, "ContactSkypeTwo");
        #endregion

        #region SEO Helper
        public static string SaveLog = string.Format(SettingNamePrefix, "SaveLog");
        public static string UseSSLWebsite = string.Format(SettingNamePrefix, "UseSSLWebsite");
        public static string PreventSelection = string.Format(SettingNamePrefix, "PreventSelection");
        public static string PreventRightClick = string.Format(SettingNamePrefix, "PreventRightClick");
        #endregion

        #region Company Information and Website
        public static string EmailSignature = string.Format(SettingNamePrefix, "EmailSignature");
        public static string DefaultContactCMSUser = string.Format(SettingNamePrefix, "DefaultContactCMSUser");
        public static string CompanyName = string.Format(SettingNamePrefix, "CompanyName");
        public static string CompanyEmail = string.Format(SettingNamePrefix, "CompanyEmail");
        public static string WorkHours = string.Format(SettingNamePrefix, "WorkHours");
        public static string UrlDOffice = string.Format(SettingNamePrefix, "UrlDOffice");
        public static string CompanyAddress = string.Format(SettingNamePrefix, "CompanyAddress");
        public static string CompanyLinkAddress = string.Format(SettingNamePrefix, "CompanyLinkAddress");
        public static string MessageUrl = string.Format(SettingNamePrefix, "MessageUrl");
        public static string ZaloUrl = string.Format(SettingNamePrefix, "ZaloUrl");
        public static string CompanyPhone = string.Format(SettingNamePrefix, "CompanyPhone");
        public static string CompanyFax = string.Format(SettingNamePrefix, "CompanyFax");
        public static string CompanyHotline = string.Format(SettingNamePrefix, "CompanyHotline");
        public static string WebsiteUrl = string.Format(SettingNamePrefix, "WebsiteUrl");
        public static string TitleOfWebsite = string.Format(SettingNamePrefix, "TitleOfWebsite");
        public static string DataGridItemsPerPage = string.Format(SettingNamePrefix, "DataGridItemsPerPage");

        public static string ThumbnailHomePage = string.Format(SettingNamePrefix, "ThumbnailHomePage");
        public static string ThumbnailLogoHomePage = string.Format(SettingNamePrefix, "ThumbnailLogoHomePage");
        public static string ThumbnailLogoFooterHomePage = string.Format(SettingNamePrefix, "ThumbnailLogoFooterHomePage");
        public static string ImageQRCode = string.Format(SettingNamePrefix, "ImageQRCode");

        public static string MetaKeywordsHomePage = string.Format(SettingNamePrefix, "MetaKeywordsHomePage");
        public static string MetaDescriptionHomePage = string.Format(SettingNamePrefix, "MetaDescriptionHomePage");
        public static string ThumbnailContact = string.Format(SettingNamePrefix, "ThumbnailContact");
        public static string MetaKeywordsContact = string.Format(SettingNamePrefix, "MetaKeywordsContact");
        public static string MetaDescriptionContact = string.Format(SettingNamePrefix, "MetaDescriptionContact");
        public static string ThumbnailPhoto = string.Format(SettingNamePrefix, "ThumbnailPhoto");
        public static string MetaKeywordsPhoto = string.Format(SettingNamePrefix, "MetaKeywordsPhoto");
        public static string MetaDescriptionPhoto = string.Format(SettingNamePrefix, "MetaDescriptionPhoto");
        public static string ThumbnailVideo = string.Format(SettingNamePrefix, "ThumbnailVideo");
        public static string MetaKeywordsVideo = string.Format(SettingNamePrefix, "MetaKeywordsVideo");
        public static string MetaDescriptionVideo = string.Format(SettingNamePrefix, "MetaDescriptionVideo");
        public static string ThumbnailSearch = string.Format(SettingNamePrefix, "ThumbnailSearch");
        public static string MetaKeywordsSearch = string.Format(SettingNamePrefix, "MetaKeywordsSearch");
        public static string MetaDescriptionSearch = string.Format(SettingNamePrefix, "MetaDescriptionSearch");
        public static string ThumbnailHotTour = string.Format(SettingNamePrefix, "ThumbnailHotTour");
        public static string MetaKeywordsHotTour = string.Format(SettingNamePrefix, "MetaKeywordsHotTour");
        public static string MetaDescriptionHotTour = string.Format(SettingNamePrefix, "MetaDescriptionHotTour");

        public static string ThumbnailShareholder = string.Format(SettingNamePrefix, "ThumbnailShareholder");
        public static string MetaKeywordsShareholder = string.Format(SettingNamePrefix, "MetaKeywordsShareholder");
        public static string MetaDescriptionShareholder = string.Format(SettingNamePrefix, "MetaDescriptionShareholder");

        public static string ThumbnailPromotionTour = string.Format(SettingNamePrefix, "ThumbnailPromotionTour");
        public static string MetaKeywordsPromotionTour = string.Format(SettingNamePrefix, "MetaKeywordsPromotionTour");
        public static string MetaDescriptionPromotionTour = string.Format(SettingNamePrefix, "MetaDescriptionPromotionTour");

        public static string ThumbnailService = string.Format(SettingNamePrefix, "ThumbnailService");
        public static string MetaKeywordsService = string.Format(SettingNamePrefix, "MetaKeywordsService");
        public static string MetaDescriptionService = string.Format(SettingNamePrefix, "MetaDescriptionService");

        public static string ThumbnailLocation = string.Format(SettingNamePrefix, "ThumbnailLocation");
        public static string MetaKeywordsLocation = string.Format(SettingNamePrefix, "MetaKeywordsLocation");
        public static string MetaDescriptionLocation = string.Format(SettingNamePrefix, "MetaDescriptionLocation");

        public static string ThumbnailDesignTour = string.Format(SettingNamePrefix, "ThumbnailDesignTour");
        public static string MetaKeywordsDesignTour = string.Format(SettingNamePrefix, "MetaKeywordsDesignTour");
        public static string MetaDescriptionDesignTour = string.Format(SettingNamePrefix, "MetaDescriptionDesignTour");

        public static string ThumbnailPopularLocation = string.Format(SettingNamePrefix, "ThumbnailPopularLocation");
        public static string MetaKeywordsPopularLocation = string.Format(SettingNamePrefix, "MetaKeywordsPopularLocation");
        public static string MetaDescriptionPopularLocation = string.Format(SettingNamePrefix, "MetaDescriptionPopularLocation");

        public static string ThumbnailPopularTour = string.Format(SettingNamePrefix, "ThumbnailPopularTour");
        public static string MetaKeywordsPopularTour = string.Format(SettingNamePrefix, "MetaKeywordsPopularTour");
        public static string MetaDescriptionPopularTour = string.Format(SettingNamePrefix, "MetaDescriptionPopularTour");

        public static string ThumbnailBookingHotel = string.Format(SettingNamePrefix, "ThumbnailBookingHotel");
        public static string MetaKeywordsBookingHotel = string.Format(SettingNamePrefix, "MetaKeywordsBookingHotel");
        public static string MetaDescriptionBookingHotel = string.Format(SettingNamePrefix, "MetaDescriptionBookingHotel");

        public static string OpenHours = string.Format(SettingNamePrefix, "OpenHours");
        public static string EmbebMapUrl = string.Format(SettingNamePrefix, "EmbebMapUrl");

        #endregion

        #region SMTP
        //SMTP Settings
        public static string SmtpMailServerAddress = string.Format(SettingNamePrefix, "SmtpMailServerAddress");
        public static string SmtpPort = string.Format(SettingNamePrefix, "SmtpPort");
        public static string SmtpUsingSSL = string.Format(SettingNamePrefix, "SmtpUsingSSL");
        public static string SmtpSenderEmail = string.Format(SettingNamePrefix, "SmtpSenderEmail");
        public static string SmtpSenderAccount = string.Format(SettingNamePrefix, "SmtpSenderAccount");
        public static string SmtpSenderPassword = string.Format(SettingNamePrefix, "SmtpSenderPassword");
        public static string UseAmazonMailService = string.Format(SettingNamePrefix, "UseAmazonMailService");
        public static string FacebookAPIKey = string.Format(SettingNamePrefix, "FacebookAPIKey");
        public static string FacebookAPISecret = string.Format(SettingNamePrefix, "FacebookAPISecret");
        public static string MainServerPublicIP = string.Format(SettingNamePrefix, "MainServerPublicIP");
        public static string MainServerPrivateIP = string.Format(SettingNamePrefix, "MainServerPrivateIP");
        public static string AlternativeServerPublicIP = string.Format(SettingNamePrefix, "AlternativeServerPublicIP");
        public static string AlternativeServerPrivateIP = string.Format(SettingNamePrefix, "AlternativeServerPrivateIP");
        public static string GATrackingCode = string.Format(SettingNamePrefix, "GATrackingCode");
        public static string GoogleAPIKey = string.Format(SettingNamePrefix, "GoogleAPIKey");
        public static string GoogleAPISecret = string.Format(SettingNamePrefix, "GoogleAPISecret");
        public static string GoogleClientID = string.Format(SettingNamePrefix, "GoogleClientID");
        public static string GoogleAnalyticServiceAccount = string.Format(SettingNamePrefix, "GoogleAnalyticServiceAccount");
        public static string WebMasterCode = string.Format(SettingNamePrefix, "WebMasterCode");
        public static string FacebookPixelCode = string.Format(SettingNamePrefix, "FacebookPixelCode");
        public static string InternalAnnouncement = string.Format(SettingNamePrefix, "InternalAnnouncement");
        public static string ContentAPIPageHome = string.Format(SettingNamePrefix, "ContentAPIPageHome");
        public static string ContentAPIPageBooking = string.Format(SettingNamePrefix, "ContentAPIPageBooking");
        public static string AdministratorEmail = string.Format(SettingNamePrefix, "AdministratorEmail");
        public static string ErrorReceiverEmail = string.Format(SettingNamePrefix, "ErrorReceiverEmail");
        public static string ScheduleKey = string.Format(SettingNamePrefix, "ScheduleKey");
        public static string StatusPopUp = string.Format(SettingNamePrefix, "StatusPopUp");
        public static string WidthPopUp = string.Format(SettingNamePrefix, "WidthPopUp");
        public static string HeightPopUp = string.Format(SettingNamePrefix, "HeightPopUp");
        public static string ContentPopUp = string.Format(SettingNamePrefix, "ContentPopUp");
        #endregion

        #region Display setting 
        public static string Address = string.Format(SettingNamePrefix, "Address");
        public static string Hotline = string.Format(SettingNamePrefix, "Hotline");
        public static string Landline = string.Format(SettingNamePrefix, "Landline");
        public static string Email = string.Format(SettingNamePrefix, "Email");
        public static string InformationOnFooter = string.Format(SettingNamePrefix, "InformationOnFooter");
        public static string TopArticleOnHomePage = string.Format(SettingNamePrefix, "TopArticleOnHomePage");
        public static string TourCategoryOnHomePage = string.Format(SettingNamePrefix, "TourCategoryOnHomePage");
        public static string TopItemInCategory = string.Format(SettingNamePrefix, "TopItemInCategory");
        public static string NumberOfOtherImageCategory = string.Format(SettingNamePrefix, "NumberOfOtherImageCategory");
        public static string NumberOfArticleCategory = string.Format(SettingNamePrefix, "NumberOfArticleCategory");
        public static string ArticleCategoryPromotion = string.Format(SettingNamePrefix, "ArticleCategoryPromotion");
        public static string NumberOtherVideo = string.Format(SettingNamePrefix, "NumberOtherVideo");
        public static string TopImageInCategory = string.Format(SettingNamePrefix, "TopImageInCategory");
        public static string TopVideoInCategory = string.Format(SettingNamePrefix, "TopVideoInCategory");
        public static string NumberOtherArticle = string.Format(SettingNamePrefix, "NumberOtherArticle");
        public static string NumberResultSearch = string.Format(SettingNamePrefix, "NumberResultSearch");
        public static string AutoUpdateSitemap = string.Format(SettingNamePrefix, "AutoUpdateSitemap");

        public static string NumberOfProjectInCategory = string.Format(SettingNamePrefix, "NumberOfProjectInCategory");
        public static string NumberOfProjectHighlight = string.Format(SettingNamePrefix, "NumberOfProjectHighlight");
        public static string NumberOfActivityInCategory = string.Format(SettingNamePrefix, "NumberOfActivityInCategory");
        public static string NumberOfOtherActivity = string.Format(SettingNamePrefix, "NumberOfOtherActivity");

        public static string NumPromotionInCategory = string.Format(SettingNamePrefix, "NumPromotionInCategory");
        public static string NumPromotionOther = string.Format(SettingNamePrefix, "NumPromotionOther");


        //UserAccountClient
        public static string DefaultCMSUserCreatedMember = string.Format(SettingNamePrefix, "DefaultCMSUserCreatedMember");

        #endregion

        #region Gmap Setting 
        public static string LatitudeMap = string.Format(SettingNamePrefix, "LatitudeMap");
        public static string LongitudeMap = string.Format(SettingNamePrefix, "LongitudeMap");
        #endregion

        #region SMS
        public static string VHTServiceUrl = "VHTServiceUrl";
        public static string AccountSMS = "AccountSMS";
        public static string CodeAPISMS = "CodeAPISMS";
        public static string BrandNameSMS = "BrandNameSMS";
        #endregion

        #region Campaign email
        public static string CampaignEmailServerAddress = "CampaignEmailServerAddress";
        public static string CampaignEmailPort = "CampaignEmailPort";
        public static string CampaignEmailUsingSSL = "CampaignEmailUsingSSL";
        public static string CampaignEmailSenderEmail = "CampaignEmailSenderEmail";
        public static string CampaignEmailSenderAccount = "CampaignEmailSenderAccount";
        public static string CampaignEmailSenderPassword = "CampaignEmailSenderPassword";
        #endregion

        public static string ContentHeaderContactPage = string.Format(SettingNamePrefix, "ContentHeaderContactPage");
        public static string ContentContactPage = string.Format(SettingNamePrefix, "ContentContactPage");
        public static string MyTimeZone = string.Format(SettingNamePrefix, "MyTimeZone");
        public static string ContentContactSuccessfully = string.Format(SettingNamePrefix, "ContentContactSuccessfully");
        public static string ContentContactFailed = string.Format(SettingNamePrefix, "ContentContactFailed");
        public static string TemplateEmailContact = string.Format(SettingNamePrefix, "TemplateEmailContact");
        public static string SortDefault = string.Format(SettingNamePrefix, "SortDefault");
        public static string TemplateEmailDesignTour = string.Format(SettingNamePrefix, "TemplateEmailDesignTour");
        public static string TemplateEmailDesignTourSuccess = string.Format(SettingNamePrefix, "TemplateEmailDesignTourSuccess");
        public static string TemplateEmailDesignTourError = string.Format(SettingNamePrefix, "TemplateEmailDesignTourError");
        public static string TemplateEmailDesignTourStatus = string.Format(SettingNamePrefix, "TemplateEmailDesignTourStatus");
        public static string CountVisitors = string.Format(SettingNamePrefix, "CountVisitors");


        public static string TopItemArticleInParentCategory = string.Format(SettingNamePrefix, "TopItemArticleInParentCategory");

    }
    public class SettingManager
    {
        private const string APP_SETTINGS_CACHEKEY = "APP_SETTINGS_CACHEKEY";
        public static string VISITOR_COUNT_CACHE_KEY = "VISITOR_COUNT_CACHE_KEY";

        public static void UpdateVisitorCountInCache(string value)
        {
            AppCache.Remove(VISITOR_COUNT_CACHE_KEY);
            AppCache.Insert(VISITOR_COUNT_CACHE_KEY, value, 1200);
        }
        //public static Dictionary<string, Dictionary<string, string>> DicServer
        //{
        //    get
        //    {
        //        Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
        //        dic.Add("sv1", new Dictionary<string, string>() {
        //            { "PrivateIP", GetSettingValue(SettingNames.MainServerPrivateIP) },
        //            { "PublicIP", GetSettingValue(SettingNames.MainServerPublicIP) }
        //        });
        //        dic.Add("sv2", new Dictionary<string, string>() {
        //            { "PrivateIP", GetSettingValue(SettingNames.AlternativeServerPrivateIP) },
        //            { "PublicIP", GetSettingValue(SettingNames.AlternativeServerPublicIP) }
        //        });
        //        return dic;
        //    }
        //}
        ///// <summary>
        ///// Gets a setting int value
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <returns>The setting int value</returns>
        //public static int GetSettingValueInt(string name, int defaultValue)
        //{
        //    int intValue = 0;
        //    int.TryParse(GetSettingValueClient(name), out intValue);
        //    return intValue == 0 ? defaultValue : intValue;
        //}
        //public static int GetSettingValueIntAdmin(string name, int defaultValue)
        //{
        //    int intValue = 0;
        //    int.TryParse(GetSettingValue(name), out intValue);
        //    return intValue == 0 ? defaultValue : intValue;
        //}
        ///// <summary>
        ///// Gets a boolean value of a setting
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <returns>The setting value</returns>
        //public static bool GetSettingValueBoolean(string name)
        //{
        //    return GetSettingValueBoolean(name, true);
        //}
        //public static bool GetSettingValueBooleanClient(string name)
        //{
        //    try
        //    {
        //        return GetSettingValueBooleanClient(name, false);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// Gets a boolean value of a setting
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <param name="DefaultValue">The default value</param>
        ///// <returns>The setting value</returns>
        //public static bool GetSettingValueBoolean(string name, bool defaultValue)
        //{
        //    string value = GetSettingValue(name);
        //    bool ret = defaultValue;
        //    if (!string.IsNullOrEmpty(value))
        //        bool.TryParse(value, out ret);
        //    return ret;
        //}
        //public static bool GetSettingValueBooleanClient(string name, bool defaultValue)
        //{
        //    string value = GetSettingValueClient(name);
        //    bool ret = defaultValue;
        //    if (!string.IsNullOrEmpty(value))
        //        bool.TryParse(value, out ret);
        //    return ret;
        //}
        ///// <summary>
        ///// Gets a guid value of a setting
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <returns>The setting value</returns>
        //public static Guid GetSettingValueGuid(string name)
        //{
        //    return GetSettingValueGuid(name, new Guid());
        //}
        ///// <summary>
        ///// Gets a guid value of a setting
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <param name="DefaultValue">The default value</param>
        ///// <returns>The setting value</returns>
        //public static Guid GetSettingValueGuid(string name, Guid defaultValue)
        //{
        //    string value = GetSettingValue(name);
        //    Guid ret = defaultValue;
        //    if (!string.IsNullOrEmpty(value))
        //        Guid.TryParse(value, out ret);
        //    return ret;
        //}
        //public static TblSystemSettingCollection ApplicationSettings
        //{
        //    get
        //    {
        //        TblSystemSettingCollection m_CurrentSettings = AppCache.Get(APP_SETTINGS_CACHEKEY) as TblSystemSettingCollection;
        //        if (m_CurrentSettings == null || m_CurrentSettings.Count < 1)
        //        {
        //            m_CurrentSettings = SettingManager.GetAllSettings();
        //            AppCache.Remove(APP_SETTINGS_CACHEKEY);
        //            AppCache.Max(APP_SETTINGS_CACHEKEY, m_CurrentSettings);
        //        }
        //        return m_CurrentSettings;
        //    }
        //}
        ///// <summary>
        ///// Get all settings
        ///// </summary>
        ///// <returns></returns>
        //static TblSystemSettingCollection GetAllSettings()
        //{
        //    return new TblSystemSettingController().FetchAll();
        //}
        ///// <summary>
        ///// Gets a setting value
        ///// </summary>
        ///// <param name="Name">The setting name</param>
        ///// <returns>The setting value</returns>
        //public static string GetSettingValue(string Name)
        //{
        //    TblSystemSetting objSetting = GetSettingByName(Name);
        //    return (objSetting != null) ? objSetting.SettingValue : string.Empty;
        //}
        //public static string GetSettingValueClient(string Name)
        //{
        //    TblSystemSetting objSetting = GetSettingClientByName(Name);
        //    return (objSetting != null) ? objSetting.SettingValue : string.Empty;
        //}
        ///// <summary>
        ///// Get setting by Setting Name
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static TblSystemSetting GetSettingByName(string settingName)
        //{
        //    TblSystemSettingCollection allSetting = ApplicationSettings;
        //    if (allSetting != null && allSetting.Count > 0)
        //        return allSetting.Where(t => t.SettingName == settingName).FirstOrDefault();
        //    return null;
        //}
        ///// <summary>
        ///// Get setting by Setting Name
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static TblSystemSetting GetSettingAdminByName(string settingName)
        //{
        //    TblSystemSettingCollection allSetting = ApplicationSettings;
        //    if (allSetting != null && allSetting.Count > 0)
        //        return allSetting.Where(t => t.SettingName == string.Format("{0}_{1}", settingName, ApplicationContext.Current.ContentCurrentLanguageCode)).FirstOrDefault();
        //    return null;
        //}
        ///// <summary>
        ///// Get setting by Setting Name
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public static TblSystemSetting GetSettingClientByName(string settingName)
        //{
        //    TblSystemSettingCollection allSetting = ApplicationSettings;
        //    if (allSetting != null && allSetting.Count > 0)
        //        return allSetting.Where(t => t.SettingName == string.Format("{0}_{1}", settingName, ApplicationContext.Current.CurrentLanguageCode)).FirstOrDefault();
        //    return null;
        //}
        ///// <summary>
        ///// Update setting
        ///// </summary>
        ///// <param name="setting"></param>
        //public static void UpdateSetting(int settingId, string settingName, string settingValue)
        //{
        //    TblSystemSetting setting = new TblSystemSetting();
        //    setting.MarkOld();
        //    setting.Id = settingId;
        //    setting.SettingName = settingName;
        //    setting.SettingValue = settingValue;
        //    new TblSystemSettingController().Update(setting);
        //}
        ///// <summary>
        ///// Add new setting
        ///// </summary>
        ///// <param name="setting"></param>
        //public static void InsertSetting(string settingName, string settingValue)
        //{
        //    new TblSystemSettingController().Insert(settingName, settingValue);
        //}
        //public static void InsertSettingByLanguage(string settingName, string settingValue)
        //{
        //    new TblSystemSettingController().Insert(string.Format("{0}_{1}", settingName, ApplicationContext.Current.ContentCurrentLanguageCode), settingValue);
        //}
        //public static void ResetSettingsInCache()
        //{
        //    AppCache.Remove(APP_SETTINGS_CACHEKEY);
        //}
        public static string GetCurrentContentLanguageCode(int languageId)
        {
            Dictionary<int, string> languageCode = LanguageHelper.LanguageCode;
            if (languageCode.ContainsKey(languageId))
                return languageCode[languageId];
            else
                return languageCode[LanguageHelper.English];
        }
    }
}
