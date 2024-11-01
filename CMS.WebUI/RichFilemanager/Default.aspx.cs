using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SweetCMS.Core.Helper;
using SweetCMS.Core.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SweetCMS.WebUI.RichFilemanager
{
    public partial class Default : System.Web.UI.Page
    {
        static string GetFolder(string key)
        {
            string folder = string.Empty;

            //key is guid id = userid
            Guid id = Guid.Empty;
            if (Guid.TryParse(key, out id) == false)
            {
                try
                {
                    folder = SecurityHelper.Decrypt(key.Replace(" ", "+"));
                }
                catch (Exception ex)
                {
                    folder = string.Empty;
                }
            }
            else
                folder = id.ToString();

            return folder;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ApplicationContext.Current.User == null)
            //    Response.Redirect("/403");
            RichFilemanager.connectors.filemanager.ResetPath();
            string key = CommonHelper.QueryString("key");
            string fm = CommonHelper.QueryString("fm");
            string folder = string.Empty;
            if (!string.IsNullOrEmpty(fm) && fm == "1")
                folder = "/uploads";
            else
                folder = GetFolder(key);

            //+ DateTime.Now.ToString("yyyy/MM")


            //register image dimension
            bool found = false;
            Dictionary<CMSImageType, string> dic = Helpers.GetListImageDimension();
            Dictionary<string, string> dicInfo = new Dictionary<string, string>();
            KeyValuePair<string, string> keySpecial = new KeyValuePair<string, string>();
            if (dic != null && dic.Count > 0)
            {
                string fi = "";

                foreach (KeyValuePair<CMSImageType, string> keyValuePairCMSImageType in dic)
                {
                    fi = "";
                    switch (keyValuePairCMSImageType.Key)
                    {
                        case CMSImageType.Normal:
                            fi = "/uploads/";
                            break;
                        case CMSImageType.Video:
                            fi = "/uploads/videos/";
                            break;
                        case CMSImageType.SocialNetwork:
                            fi = "/uploads/socialnetwork/";
                            break;
                    }

                    if (string.IsNullOrEmpty(fi) == false)
                    {
                        if (folder.ToLower().Contains(fi))
                        {
                            keySpecial = new KeyValuePair<string, string>(fi, keyValuePairCMSImageType.Value);
                            found = true;
                        }
                        dicInfo.Add(fi, keyValuePairCMSImageType.Value);
                    }
                }
            }

            if (found == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "init2",
                    "var imageDimessionText=[{'info':'" + keySpecial.Value + "','p':'" + keySpecial.Key + "'}];", true);
            }
            else
            {
                if (folder.Length < 2)
                {
                    if (dicInfo != null && dicInfo.Count > 0)
                    {
                        List<object> lstInfp = new List<object>();
                        foreach (KeyValuePair<string, string> keyValuePairString in dicInfo)
                            lstInfp.Add(new { info = keyValuePairString.Value, p = keyValuePairString.Key });

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "init2",
                            "var imageDimessionText=" + JsonConvert.SerializeObject(lstInfp) + ";", true);
                    }
                }
            }

            if (string.IsNullOrEmpty(folder))
            {
                ////only master admin can access this root folder
                ////check is master admin
                //if (ApplicationContext.Current.User != null
                //   && CMSUserManager.IsAdministrationOrArticleManagement(ApplicationContext.Current.CMSUserID))
                //{
                //    return;
                //}
                //else
                //{
                //    if (ApplicationContext.Current.User != null)
                //        Response.Redirect("/Administration/AccessDenied.aspx");
                //    else
                //        Response.Redirect("/AccessDenied.aspx");
                //    return;
                //}
            }

            if (folder == "/" || folder == "~")
            {
                //only master admin can access this root folder
                //check is master admin
                //if (ApplicationContext.Current.User != null
                // && CMSUserManager.IsAdministrationOrArticleManagement(ApplicationContext.Current.CMSUserID))
                //{

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "init", "var forcePath='" + folder + "';", true);
                //}
                //else
                //{
                //    if (ApplicationContext.Current.User != null)
                //        Response.Redirect("/Administration/AccessDenied.aspx");
                //    else
                //        Response.Redirect("/AccessDenied.aspx");
                //    return;
                //}
            }
            else
            {
                string path = RichFilemanager.connectors.filemanager.CorrectPath(folder);

                //create folder if not exists;

                string physicalPath = HttpContext.Current.Server.MapPath(path);
                if (Directory.Exists(physicalPath) == false)
                {
                    try
                    {
                        Directory.CreateDirectory(physicalPath);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                //check folder
                #region article

                if (path.ToLower().TrimEnd('/').EndsWith("/uploads"))
                {
                    //, DateTime.Now.ToString("yyyy/MM")
                    string temp = Path.Combine(physicalPath);
                    if (Directory.Exists(temp) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(temp);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    temp = Path.Combine(physicalPath, "Images");
                    if (Directory.Exists(temp) == false)
                    {
                        try
                        {
                            Directory.CreateDirectory(temp);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                #endregion


                if (folder.EndsWith("/") == false)
                    folder += "/";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "init", "var forcePath='" + path.TrimStart('~') + "';", true);

            }

        }

        [WebMethod(EnableSession = true)]
        public static string GetSetting(string key, string fm)
        {
            bool isFileManager = false;
            if (fm == "1")
                isFileManager = true;
            else
                bool.TryParse(fm, out isFileManager);
            
            string folder = GetFolder(key);
            if (isFileManager)
                folder = "/uploads/";
            JToken data = null;
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/RichFilemanager/scripts/filemanager.config.json");
                data = JObject.Parse(File.ReadAllText(path));
            }
            catch (Exception ex)
            {

            }

            if (data != null)
            {

                if (true)
                //if (isFileManager == true)
                {
                    data["upload"]["multiple"] = true;
                    data["upload"]["numberOfFiles"] = 20;
                    data["upload"]["fileSizeLimit"] = 50 * 1000 * 1000;//100Mb
                    data["edit"]["enabled"] = true;
                }
                //else
                //{
                //    data["security"]["uploadRestrictions"] = JToken.FromObject(new List<string>() { "jpg", "jpeg", "png", "xml", "mp4", "mp3", "svg", "webp" });

                //    //if (folder.ToLower().Contains("/slide/image")
                //    //    || folder.ToLower().Contains("/slide/videothumb"))
                //    //    data["security"]["uploadRestrictions"] = JToken.FromObject(new List<string>() { "jpg", "jpeg", "png", "gif", "bmp" });

                //    //select image from admin
                //    data["options"]["capabilities"] = JToken.FromObject(new List<string>() { "select", "upload" });

                //    data["upload"]["fileSizeLimit"] = 50 * 1000 * 1000;//100Mb

                //    //allow all capacities for article
                //    if (folder.ToLower().Contains("/uploads"))
                //    {
                //        data["options"]["capabilities"] = JToken.FromObject(new List<string>() {
                //             "select", "upload", "download", "rename", "move", "replace", "delete"});
                //    }

                //    if (ApplicationContext.Current.User != null && CMSUserManager.IsAdministrationOrArticleManagement(ApplicationContext.Current.CMSUserID))
                //    {
                //        data["upload"]["multiple"] = true;
                //        data["edit"]["enabled"] = true;
                //        if (folder.ToLower().Contains("/uploads/videos"))
                //            data["upload"]["fileSizeLimit"] = 50 * 1000 * 1000;//100Mb
                //    }
                //    else
                //    {
                //        data["upload"]["multiple"] = false;
                //        data["edit"]["enabled"] = false;
                //        if (folder.ToLower().Contains("/uploads/videos"))
                //            data["upload"]["fileSizeLimit"] = 50 * 1000 * 1000;//100Mb
                //    }
                //}
            }

            return JsonConvert.SerializeObject(data);
        }
    }
}