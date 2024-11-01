using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using SweetCMS.Core.Helper;
using ImageResizer;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SweetCMS.WebUI.RichFilemanager.connectors
{

    public partial class filemanager : System.Web.UI.Page
    {

        // ** Filemanager ASP.NET connector
        //
        // ** .NET Framework >= 2.0
        //
        // ** license : MIT License
        // ** author : Ondřej "Yumi Yoshimido" Brožek | <cholera@hzspraha.cz>
        // ** Copyright : Author


        //===================================================================
        //==================== EDIT CONFIGURE HERE ==========================
        //===================================================================

        // Icon directory for filemanager. [string]
        public static string defaultPath = "~/uploads";
        static string loadPath = string.Empty;
        static string queryPath = string.Empty;
        public string IconDirectory = "/RichFilemanager/images/fileicons/";
        //public string[] imgExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" }; // Only allow this image extensions. [string]

        static Dictionary<string, Dictionary<CMSImageType, string>> DicResizeSetting = new Dictionary<string, Dictionary<CMSImageType, string>>() {
              {
                    "Uploads\\Videos", new Dictionary<CMSImageType, string>() {
                        { CMSImageType.Video, "Uploads\\Videos" }
                    }
               },
                {
                    "Uploads", new Dictionary<CMSImageType, string>() {
                        { CMSImageType.Normal, "Uploads" },
                        { CMSImageType.SocialNetwork, "Uploads\\SocialNetwork" },
                    }
               },{
                    "Article", new Dictionary<CMSImageType, string>() {
                        { CMSImageType.Normal, "Article" }
                    }
               }
        };

        static Dictionary<CMSImageType, string> GetResizeSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            foreach (KeyValuePair<string, Dictionary<CMSImageType, string>> keyValuePairString in DicResizeSetting)
            {
                if (keyValuePairString.Key.ToLower() == key.ToLower())
                {
                    return keyValuePairString.Value;
                }
            }
            return null;
        }

        //===================================================================
        //========================== END EDIT ===============================
        //===================================================================       

        Dictionary<string, object> GetStorageStatic(string path)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            string folderPath = GetLoadPath();
            if (string.IsNullOrEmpty(path) || path.Length < folderPath.Length)
                path = folderPath;

            if (Directory.Exists(HttpContext.Current.Server.MapPath(path)) == false)
                return dic;

            if (string.IsNullOrEmpty(path) == false)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));

                    Int32 folderCount = 0;
                    var found = Directory.GetDirectories(dirInfo.FullName, "*", SearchOption.AllDirectories);
                    if (found != null)
                        folderCount = found.Length;

                    Int64 length = 0;
                    Int32 fileCount = 0;

                    foreach (FileInfo fileInfo in dirInfo.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        if (fileInfo.Name.ToLower() == ".htaccess"
                            || fileInfo.Name.ToLower() == "web.config")
                            continue;

                        length += fileInfo.Length;
                        fileCount++;
                    }

                    dic.Add("Code", 0);
                    dic.Add("Error", "");
                    dic.Add("Files", fileCount);
                    dic.Add("Folders", folderCount);
                    dic.Add("Size", length);
                }
                catch (Exception ex)
                {
                    dic.Add("Code", -1);
                    dic.Add("Error", "");
                    dic.Add("Files", 0);
                    dic.Add("Folders", 0);
                    dic.Add("Size", 0);
                }
            }

            return dic;
        }

        static string RenderPaging(int currentPage, int totalRow, int pageSize)
        {
            StringBuilder sbPaging = new StringBuilder();

            sbPaging.Append("<div class='fm-container'>");

            int totalPage = totalRow / pageSize;
            if (totalRow % pageSize > 0)
                totalPage += 1;

            /*
            NameValueCollection valQuery = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
            valQuery.Remove("p");
            string url = valQuery.ToString();

            url += "&";
            */

            if (totalPage > 1)
            {
                sbPaging.Append("<button class='btn-paging' title='Go to page 1'>");
                sbPaging.Append("<span data-indx='1'>");
                sbPaging.Append("<<");
                sbPaging.Append("</span>");
                sbPaging.Append("</button>");

                //sbPaging.Append("\n");

                if (currentPage == 1)
                { }
                else
                {
                    sbPaging.Append("<button class='btn-paging' title='Go to page " + (currentPage - 1) + "'>");
                    sbPaging.Append("<span data-indx='" + (currentPage - 1) + "'>");
                    sbPaging.Append("<");
                    sbPaging.Append("</span>");
                    sbPaging.Append("</button>");

                    //sbPaging.Append("\n");

                }


                sbPaging.Append("<div class='paging-info'>");
                sbPaging.Append("<p style='margin:0;color:#231F20'>");
                sbPaging.Append("<span>" + currentPage + "/</span>");
                sbPaging.Append("<span>" + totalPage + "</span>");
                sbPaging.Append("</p>");
                sbPaging.Append("</div>");

                //sbPaging.Append("\n");

                if (totalPage > currentPage)
                {
                    sbPaging.Append("<button class='btn-paging' title='Go to page " + (currentPage + 1) + "'>");
                    sbPaging.Append("<span data-indx='" + (currentPage + 1) + "'>");
                    sbPaging.Append(">");
                    sbPaging.Append("</span>");
                    sbPaging.Append("</button>");

                    //sbPaging.Append("\n");

                }


                sbPaging.Append("<button class='btn-paging' title='Go to page " + totalPage + "'>");
                sbPaging.Append("<span data-indx='" + totalPage + "'>");
                sbPaging.Append(">>");
                sbPaging.Append("</span>");
                sbPaging.Append("</button>");

                //sbPaging.Append("\n");

            }

            sbPaging.Append("</div>");

            return sbPaging.ToString();
        }

        private string getFolderInfo(string path)
        {
            string resolvePath = CorrectPath(path);
            int pageIndex = 1;
            if (string.IsNullOrEmpty(Request.QueryString["pi"]) == false)
            {
                if (int.TryParse(Request.QueryString["pi"], out pageIndex) == false)
                    pageIndex = 1;
            }

            if (pageIndex < 1)
                pageIndex = 1;

            int pageSize = 20;
            if (string.IsNullOrEmpty(Request.QueryString["ps"]) == false)
            {
                if (int.TryParse(Request.QueryString["ps"], out pageSize) == false)
                    pageSize = 20;
            }

            if (pageSize < 1)
                pageSize = 20;

            DirectoryInfo RootDirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(resolvePath));
            if (RootDirInfo.Exists == true)
            {
                List<Dictionary<string, object>> lstData = new List<Dictionary<string, object>>();

                string storagePhysicalPath = HttpContext.Current.Server.MapPath(GetLoadPath()).TrimEnd('\\');
                string storagePhysicalPath2 = HttpContext.Current.Server.MapPath("~/").TrimEnd('\\');
                //string sitePhysicalPath = HttpContext.Current.Server.MapPath(siteRoot);

                foreach (DirectoryInfo dirInfo in RootDirInfo.GetDirectories())
                {
                    lstData.Add(getInfoDictionary(storagePhysicalPath, dirInfo.FullName, true));
                }

                int count = RootDirInfo.GetFiles().Length;

                foreach (FileInfo fileInfo in RootDirInfo.GetFiles().Skip(pageSize * (pageIndex - 1)).Take(pageSize))
                {
                    if (fileInfo.Name.ToLower() == ".htaccess"
                        || fileInfo.Name.ToLower() == "web.config")
                        continue;

                    lstData.Add(getInfoDictionary(storagePhysicalPath2, fileInfo.FullName, false));
                }

                if (count > pageSize)
                    lstData.Add(new Dictionary<string, object> { { "Paging", RenderPaging(pageIndex, count, pageSize) } });
                else
                    lstData.Add(new Dictionary<string, object> { { "Paging", "" } });

                return JsonConvert.SerializeObject(lstData);
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    Error = "The folder '" + resolvePath + "' does not exists.",
                    Code = -1
                });
            }
        }

        private Dictionary<string, object> getInfoDictionary(string sitePhysicalPath,
            string path, bool getInfo)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(sitePhysicalPath))
                return dic;

            FileAttributes attr = File.GetAttributes(path);

            string virtualPath = path.Remove(0, sitePhysicalPath.Length).Replace("\\", "/");


            #region directory
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                /*
                sb.AppendLine("{");
                sb.AppendLine("\"Path\": \"" + path + "\",");
                sb.AppendLine("\"Filename\": \"" + DirInfo.Name + "\",");
                sb.AppendLine("\"File Type\": \"dir\",");
                sb.AppendLine("\"Preview\": \"" + IconDirectory + "_Close.png\",");
                sb.AppendLine("\"Properties\": {");
                sb.AppendLine("\"Date Created\": \"" + DirInfo.CreationTime.ToString() + "\", ");
                sb.AppendLine("\"Date Modified\": \"" + DirInfo.LastWriteTime.ToString() + "\", ");
                sb.AppendLine("\"Height\": 0,");
                sb.AppendLine("\"Width\": 0,");
                sb.AppendLine("\"Size\": 0 ");
                sb.AppendLine("},");
                sb.AppendLine("\"Error\": \"\",");
                sb.AppendLine("\"Code\": 0");
                sb.AppendLine("}");
                */

                #region RegionName

                Dictionary<string, object> dicProp = new Dictionary<string, object>();
                dicProp.Add("Date Created", dirInfo.CreationTime.ToString());
                dicProp.Add("Date Modified", dirInfo.LastWriteTime.ToString());
                string sortModified = dirInfo.LastWriteTime.ToString("yyyyMMdd-hh:mm:ss tt");
                dicProp.Add("Sort Modified", sortModified);
                dicProp.Add("Height", 0);
                dicProp.Add("Width", 0);
                dicProp.Add("Size", 0);

                #endregion

                dic.Add("Path", "/" + virtualPath.Trim('/') + (getInfo ? "/" : ""));
                //dic.Add("Path", virtualPath.Remove(0, loadPath.TrimStart('~').Length));
                dic.Add("Filename", dirInfo.Name);
                dic.Add("File Type", "dir");
                dic.Add("Preview", IconDirectory + "_Close.png");
                dic.Add("Error", "No error");
                dic.Add("Code", 0);
                dic.Add("Properties", dicProp);
                dic.Add("Protected", 0);
            }
            #endregion
            else
            #region file
            {
                FileInfo fileInfo = new FileInfo(path);

                //dic.Add("Path", virtualPath);
                //dic.Add("Path", virtualPath.Remove(0, loadPath.TrimStart('~').Length));
                //dic.Add("Path", "/" + queryPath + "/" + fileInfo.Name);


                string temp = queryPath.TrimEnd('/') + "/" +
                    virtualPath.Remove(0, loadPath.TrimStart('~').TrimEnd('/').Length).TrimStart('/');
                if (temp.StartsWith("/") == false)
                    temp = "/" + temp;
                string foldertrim = "/" + queryPath.Trim('/');
                if (foldertrim.Length > 1)
                    temp = temp.Remove(0, foldertrim.Length);
                dic.Add("Path", temp);
                //dic.Add("Path", MakeRelativePath(path, true));


                dic.Add("Filename", fileInfo.Name);
                dic.Add("File Type", fileInfo.Extension.Replace(".", ""));

                dic.Add("Error", "No error");
                dic.Add("Code", 0);

                if (MIMEAssistant.IsImage(fileInfo.FullName))
                {
                    dic.Add("Preview", virtualPath);
                }
                else
                {
                    string mime = MIMEAssistant.GetMimeTypeByFileName(fileInfo.Name);
                    if (string.IsNullOrEmpty(mime) == false
                        && mime.ToLower().StartsWith("video"))
                    {
                        dic.Add("Preview", String.Format("{0}{1}.png", IconDirectory,
                            fileInfo.Extension.Replace(".", "")));
                        dic.Add("videourl", "/" + virtualPath.TrimStart('/'));
                    }
                    else
                    {
                        dic.Add("Preview", String.Format("{0}{1}.png", IconDirectory,
                            fileInfo.Extension.Replace(".", "")));
                    }
                }

                #region RegionName

                Dictionary<string, object> dicProp = new Dictionary<string, object>();
                dicProp.Add("Date Created", fileInfo.CreationTime.ToString());
                dicProp.Add("Date Modified", fileInfo.LastWriteTime.ToString());
                string sortModified = fileInfo.LastWriteTime.ToString("yyyyMMdd-hh:mm:ss tt");
                dicProp.Add("Sort Modified", sortModified);

                //if (MIMEAssistant.IsWebpImage(path))
                //{
                //    using (var fs = File.OpenRead(path))
                //    {
                //        using (System.Drawing.Bitmap img = new WebPDecoderPlugin().DecodeStream(fs,
                //               new ImageResizer.ResizeSettings("decoder=webp"), string.Empty))
                //        {
                //            if (img != null)
                //            {
                //                dicProp.Add("Height", img.Height);
                //                dicProp.Add("Width", img.Width);
                //            }
                //            else
                //            {
                //                dicProp.Add("Height", 0);
                //                dicProp.Add("Width", 0);
                //            }
                //        }
                //    }
                //}
                if (MIMEAssistant.IsImage(path))
                {
                    try
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(path))
                        {
                            if (img != null)
                            {
                                dicProp.Add("Height", img.Height);
                                dicProp.Add("Width", img.Width);
                            }
                            else
                            {
                                dicProp.Add("Height", 0);
                                dicProp.Add("Width", 0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dicProp.Add("Height", 0);
                        dicProp.Add("Width", 0);
                    }
                }
                else
                {
                    dicProp.Add("Height", 0);
                    dicProp.Add("Width", 0);
                }

                dicProp.Add("Size", fileInfo.Length);

                #endregion

                dic.Add("Properties", dicProp);
                dic.Add("Protected", 0);
            }

            #endregion

            return dic;

        }

        private string getInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
                return JsonConvert.SerializeObject(new { });

            path = HttpContext.Current.Server.UrlDecode(path);
            string resolvePath = CorrectPath(path);
            string physicalPath = HttpContext.Current.Server.MapPath(resolvePath);

            bool isExist = false;
            bool isFile = false;
            if (resolvePath.EndsWith("/"))
                isExist = Directory.Exists(physicalPath);
            else
            {
                isExist = File.Exists(physicalPath);
                isFile = true;
            }

            Dictionary<string, object> dicData = null;
            if (isExist)
            {
                FileAttributes attr = File.GetAttributes(physicalPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string storagePhysicalPath = HttpContext.Current.Server.MapPath(GetLoadPath());
                    dicData = getInfoDictionary(storagePhysicalPath, physicalPath, true);
                }
                else
                {
                    string storagePhysicalPath = HttpContext.Current.Server.MapPath("~");
                    dicData = getInfoDictionary(storagePhysicalPath, physicalPath, true);
                }

                if (dicData != null)
                    return JsonConvert.SerializeObject(dicData);
                else
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "The file '" + resolvePath + "' does not exists.",
                        Code = -1
                    });
            }
            else
            {
                if (isFile)
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "The file '" + resolvePath + "' does not exists.",
                        Code = -1
                    });
                else
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "The folder '" + resolvePath + "' does not exists.",
                        Code = -1
                    });
            }
        }

        private string Rename(string path, string newName)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(newName))
                return JsonConvert.SerializeObject(dic);

            string resolvePath = CorrectPath(path);
            string physicalPath = HttpContext.Current.Server.MapPath(resolvePath);

            bool isExist = false;
            bool isFile = false;
            if (Path.GetExtension(physicalPath).Length == 0)
                isExist = Directory.Exists(physicalPath);
            else
            {
                isExist = File.Exists(physicalPath);
                isFile = true;
            }

            if (isExist)
            {
                #region RegionName

                FileAttributes attr = File.GetAttributes(physicalPath);

                dic.Add("Error", "No error");
                dic.Add("Code", 0);
                dic.Add("Old Path", path);
                dic.Add("New Path", string.Empty);
                dic.Add("New Name", string.Empty);

                #region folder
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    newName = PathValidation.CleanPath(newName);

                    DirectoryInfo dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(resolvePath));
                    string oldName = dirInfo.Name;
                    if (newName == oldName)
                    {
                        dic["Error"] = "Source and destination path must be different.";
                        dic["Code"] = -1;
                    }
                    else
                    {
                        DirectoryInfo dirInfo2 = new DirectoryInfo(Path.Combine(dirInfo.Parent.FullName, newName));

                        Directory.Move(dirInfo.FullName, dirInfo2.FullName);

                        /*
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("{");
                        sb.AppendLine("\"Error\": \"No error\",");
                        sb.AppendLine("\"Code\": 0,");
                        sb.AppendLine("\"Old Path\": \"" + path + "\",");
                        sb.AppendLine("\"Old Name\": \"" + newName + "\",");
                        sb.AppendLine("\"New Path\": \"" +
                           ;
                        sb.AppendLine("\"New Name\": \"" + fileInfo2.Name + "\"");
                        sb.AppendLine("}");
                        */

                        dic["Old Path"] = "/" + MakeRelativePath(dirInfo.FullName, true).Trim('/') + "/";
                        dic["Old Name"] = oldName;
                        dic["New Path"] = "/" + MakeRelativePath(dirInfo2.FullName, true).Trim('/') + "/";
                        dic["New Name"] = dirInfo2.Name;
                    }
                }
                #endregion
                else
                #region file
                {
                    newName = PathValidation.CleanFileName(newName);

                    FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(resolvePath));
                    string oldName = fileInfo.Name;
                    if (newName == oldName)
                    {
                        dic["Error"] = "Source and destination path must be different.";
                        dic["Code"] = -1;
                    }
                    else
                    {
                        FileInfo fileInfo2 = new FileInfo(Path.Combine(fileInfo.Directory.FullName, newName));

                        File.Move(fileInfo.FullName, fileInfo2.FullName);


                        /*
                        sb.AppendLine("{");
                        sb.AppendLine("\"Error\": \"No error\",");
                        sb.AppendLine("\"Code\": 0,");
                        sb.AppendLine("\"Old Path\": \"" + path + "\",");
                        sb.AppendLine("\"Old Name\": \"" + newName + "\",");
                        sb.AppendLine("\"New Path\": \"" +
                            fileInfo2.FullName.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/') + "\",");
                        sb.AppendLine("\"New Name\": \"" + fileInfo2.Name + "\"");
                        sb.AppendLine("}");
                        */
                        string op = "/" + MakeRelativePath(fileInfo.FullName, false).TrimStart('/');
                        string foldertrim = "/" + queryPath.Trim('/');
                        if (foldertrim.Length > 1)
                            op = op.Remove(0, foldertrim.Length);
                        string op2 = "/" + MakeRelativePath(fileInfo2.FullName, false).TrimStart('/');
                        if (foldertrim.Length > 1)
                            op2 = op2.Remove(0, foldertrim.Length);

                        dic["Old Path"] = op;
                        dic["Old Name"] = oldName;
                        dic["New Path"] = op2;
                        dic["New Name"] = fileInfo2.Name;
                    }
                }
                #endregion

                #endregion

                return JsonConvert.SerializeObject(dic);
            }
            else
            {
                if (isFile)
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "The file '" + resolvePath + "' does not exists.",
                        Code = -1
                    });
                else
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "The folder '" + resolvePath + "' does not exists.",
                        Code = -1
                    });
            }
        }

        private string Delete(string path)
        {
            string resolvePath = CorrectPath(path);
            string newPath = HttpContext.Current.Server.MapPath(resolvePath);
            FileAttributes attr = FileAttributes.Temporary;
            try
            {
                attr = File.GetAttributes(newPath);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    Error = "The file '" + resolvePath + "' does not exists.",
                    Code = -1
                });
            }

            StringBuilder sb = new StringBuilder();

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {
                    Directory.Delete(newPath, true);
                    return JsonConvert.SerializeObject(new
                    {
                        Path = "/" + MakeRelativePath(newPath, true).TrimStart('/'),
                        Error = "No error",
                        Code = 0
                    });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "Cannot delete folder " + MakeRelativePath(newPath, true),
                        Code = -1
                    });
                }
            }
            else
            {
                try
                {
                    File.Delete(newPath);
                    string foldertrim = "/" + queryPath.Trim('/');
                    string temp = "/" + MakeRelativePath(newPath, false).TrimStart('/');
                    if (foldertrim.Length > 1)
                        temp = temp.Remove(0, foldertrim.Length);
                    return JsonConvert.SerializeObject(new
                    {
                        Path = temp,
                        Error = "No error",
                        Code = 0
                    });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        Error = "Cannot delete file " + MakeRelativePath(newPath, false),
                        Code = -1
                    });
                }
            }
        }

        private string AddFolder(string path, string NewFolder)
        {
            string resolvePath = CorrectPath(path);

            if (string.IsNullOrEmpty(NewFolder))
                return "{}";

            /*
            StringBuilder sb = new StringBuilder();

            Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath(resolvePath), NewFolder));

            sb.AppendLine("{");
            sb.AppendLine("\"Parent\": \"" + path + "\",");
            sb.AppendLine("\"Name\": \"" + NewFolder + "\",");
            sb.AppendLine("\"Error\": \"No error\",");
            sb.AppendLine("\"Code\": 0");
            sb.AppendLine("}");

            return sb.ToString();
            */

            NewFolder = PathValidation.CleanFileName(NewFolder);

            string newPath = Path.Combine(HttpContext.Current.Server.MapPath(resolvePath), NewFolder);

            try
            {
                Directory.CreateDirectory(newPath);

                return JsonConvert.SerializeObject(new
                {
                    Parent = "/" + MakeRelativePath(HttpContext.Current.Server.MapPath(resolvePath), true).TrimStart('/'),
                    Name = NewFolder,
                    Error = "No error",
                    Code = 0
                });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    Error = "Cannot create folder '" + resolvePath + "'",
                    Code = -1
                });
            }
        }

        public static string CorrectPath(string path)
        {
            string folderPath = GetLoadPath();
            if (string.IsNullOrEmpty(path))
                return folderPath;

            string resolvePath = path;
            if (string.IsNullOrEmpty(resolvePath) == false &&
                resolvePath.ToLower().StartsWith(folderPath.ToLower()) == true)
                return resolvePath;

            if (string.IsNullOrEmpty(resolvePath) == false &&
                resolvePath.ToLower().StartsWith(folderPath.ToLower()) == false)
            {
                string dd = resolvePath.TrimStart('~');
                dd = dd.TrimStart('/');

                string parent = string.Empty;
                if (string.IsNullOrEmpty(dd.Trim()) == false)
                    parent = Path.GetDirectoryName(dd.TrimEnd('/'));

                if (string.IsNullOrEmpty(dd))
                    return folderPath;

                if (("~/" + dd).TrimEnd('/').ToLower().StartsWith(folderPath.ToLower()))
                {
                    if (resolvePath.StartsWith("/") == false)
                        resolvePath = "/" + resolvePath;
                    if (resolvePath.StartsWith("~") == false)
                        resolvePath = "~" + resolvePath;
                    return resolvePath;
                }
                else if (folderPath.Length < defaultPath.Length)
                    return folderPath.TrimEnd('/') + "/" + dd.TrimStart('/');
                else if (folderPath.Remove(0, defaultPath.Length).Trim('/') == resolvePath.Trim('/'))
                    return folderPath;
                else if (folderPath.ToLower().EndsWith(Path.GetDirectoryName(dd).Replace("\\", "/").ToLower()))
                    return folderPath.TrimEnd('/') + "/" + Path.GetFileName(dd);
                else if (string.IsNullOrEmpty(parent) == false && folderPath.ToLower().EndsWith(parent.Replace("\\", "/").ToLower()))
                    return folderPath.TrimEnd('/') + "/" + Path.GetFileName(dd.TrimEnd('/'));
                else if (folderPath.ToLower().StartsWith(defaultPath.ToLower()) == true)
                    return folderPath.TrimEnd('/') + "/" + dd.TrimStart('/');
                else
                    return folderPath;

                /*
                if (folderPath.TrimEnd('/').StartsWith(("~/" + dd).TrimEnd('/')))
                {
                    if (resolvePath.StartsWith("/") == false)
                        resolvePath = "/" + resolvePath;

                    if (resolvePath.StartsWith("~") == false)
                        resolvePath = "~" + resolvePath;
                    return resolvePath;
                }

                resolvePath = folderPath + "/" + dd;
                */
            }

            return resolvePath;
        }

        static string MakeRelativePath(string physicalPath, bool isFolder)
        {
            string storagePhysicalPath = string.Empty;
            if (isFolder == true)
                storagePhysicalPath = HttpContext.Current.Server.MapPath(GetLoadPath());
            else
                storagePhysicalPath = HttpContext.Current.Server.MapPath("~/");
            return physicalPath.Remove(0, storagePhysicalPath.Length).Replace("\\", "/");
        }

        static string GetLoadPath()
        {
            if (string.IsNullOrEmpty(loadPath))
                return defaultPath;
            else
                return loadPath;
        }

        public static void ResetPath()
        {
            loadPath = string.Empty;
        }

        public static string SaveImage(Stream fileContent, string path, string settings)
        {
            using (var ms = new MemoryStream())
            {
                if (settings.Contains("quality") == false)
                {
                    //luon luon dung chat luong tot nhat =100
                    if (settings.Length > 0)
                        settings += "&";

                    settings += "quality=100";
                }
                try
                {
                    ImageBuilder.Current.Build(new ImageJob(fileContent, ms,
                    new Instructions(settings == null ? "" : settings), false, true));
                    //ImageBuilder.Current.Build(new ImageJob(fileContent, ms,
                    //    new Instructions(string.Empty), false, true));
                }
                catch { }



                string savePath = FileHelper.NextAvailableFilename(path);

                if (savePath.ToLower().Contains("\\small\\small"))
                    return savePath;

                string folder = Path.GetDirectoryName(savePath);
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
                
                //Save image with watermark
                //string _strWaterMark = HttpContext.Current.Server.MapPath("~/Uploads/gym.png");
                //using (Image image = Image.FromStream(fileContent))
                //using (Image watermarkImage = Image.FromFile(_strWaterMark))
                //using (Graphics imageGraphics = Graphics.FromImage(image))
                //using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
                //{
                //    int x = (image.Width / 2 - watermarkImage.Width / 2);
                //    int y = (image.Height / 2 - watermarkImage.Height / 2);
                //    watermarkBrush.TranslateTransform(x, y);
                //    imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
                //    image.Save(savePath);
                //}
                
                using (var fileStream = new FileStream(FileHelper.NextAvailableFilename(savePath), FileMode.Create, FileAccess.ReadWrite))
                    ms.WriteTo(fileStream);

                if (fileContent.CanSeek)
                    fileContent.Seek(0, SeekOrigin.Begin);

                return savePath;
            }
        }

        void SaveFile()
        {
            System.Web.HttpPostedFile file = Request.Files[0];

            #region RegionName

            string path = Request["currentpath"];
            string resolvePath1 = CorrectPath(path);

            string fname = PathValidation.CleanFileName(file.FileName);
            fname = FileHelper.ChangeFileName(fname);

            string addPath = Server.MapPath(Path.Combine(resolvePath1, Path.GetFileName(fname)));

            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
                if (file.InputStream.CanSeek)
                    file.InputStream.Seek(0, SeekOrigin.Begin);

                string mimetype = MIMEAssistant.GetMimeTypeWithByteArray(fileData, file.FileName);
                if (string.IsNullOrEmpty(mimetype) == false
                    && mimetype.ToLower().StartsWith("image") )
                {
                    /*
                    SaveImage(file.InputStream, addPath, Helpers.GetRenderAttribute(CMSImageType.ServicePhoto).ToImageResizeString());

                    file.InputStream.Seek(0, SeekOrigin.Begin);

                    string currentFolder = Path.GetDirectoryName(addPath);
                    SaveImage(file.InputStream,
                        Path.Combine(Path.Combine(currentFolder, "Small"), file.FileName),
                        Helpers.GetRenderAttribute(CMSImageType.SmallServicePhoto).ToImageResizeString());
                    */
                    if(mimetype.ToLower().Equals("image/webp") || mimetype.ToLower().Equals("image/svg+xml"))
                    {
                        file.SaveAs(FileHelper.NextAvailableFilename(addPath));
                    }
                    else
                    {
                        #region process image
                        string currentFolder = Path.GetDirectoryName(addPath);
                        string masterFolder = Server.MapPath(defaultPath);
                        string key = currentFolder.Remove(0, masterFolder.Length).TrimStart('\\');
                        Dictionary<CMSImageType, string> dicResize = GetResizeSetting(key);
                        if (dicResize == null)
                        {
                            SaveImage(file.InputStream, addPath, string.Empty);
                        }
                        else
                        {
                            string folder = string.Empty;
                            string fileName = string.Empty;
                            foreach (KeyValuePair<CMSImageType, string> keyValuePairCMSImageType in dicResize)
                            {
                                folder = Path.Combine(masterFolder, keyValuePairCMSImageType.Value);
                                //fileName = Path.Combine(folder, file.FileName);
                                fileName = Path.Combine(folder, FileHelper.ChangeFileName(file.FileName));
                                fileName = FileHelper.NextAvailableFilename(fileName);
                                SaveImage(file.InputStream, fileName,
                                    Helpers.GetRenderAttribute(keyValuePairCMSImageType.Key).ToImageResizeString());

                                if (file.InputStream.CanSeek)
                                    file.InputStream.Seek(0, SeekOrigin.Begin);

                            }
                        }
                        #endregion
                    }
                }
                else
                    file.SaveAs(FileHelper.NextAvailableFilename(addPath));
            }

            #endregion

            Response.ContentType = "application/json";
            Response.ContentEncoding = Encoding.UTF8;

            /*
            StringBuilder sb = new StringBuilder();

            string dd = Path.GetFileName(file.FileName);

            sb.AppendLine("{");
            sb.AppendLine("\"Path\": \"" + path.Replace("\"", @"\""") + "\",");
            sb.AppendLine("\"Name\": \"" + dd.Replace("\"", @"\""") + "\",");
            sb.AppendLine("\"Error\": \"No error\",");
            sb.AppendLine("\"Code\": 0");
            sb.AppendLine("}");

            Response.Write(sb.ToString());
            */
            Response.Write(JsonConvert.SerializeObject(new
            {
                Path = path,
                Name = Path.GetFileName(file.FileName),
                Error = "No error",
                Code = 0
            }));
        }

        public static void AddWaterMark(MemoryStream ms, string watermarkText, MemoryStream outputStream)
        {
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            Graphics gr = Graphics.FromImage(img);
            Font font = new Font("Tahoma", (float)40);
            Color color = Color.FromArgb(50, 241, 235, 105);
            double tangent = (double)img.Height / (double)img.Width;
            double angle = Math.Atan(tangent) * (180 / Math.PI);
            double halfHypotenuse = Math.Sqrt((img.Height * img.Height) + (img.Width * img.Width)) / 2;
            double sin, cos, opp1, adj1, opp2, adj2;

            for (int i = 100; i > 0; i--)
            {
                font = new Font("Tahoma", i, FontStyle.Bold);
                SizeF sizef = gr.MeasureString(watermarkText, font, int.MaxValue);

                sin = Math.Sin(angle * (Math.PI / 180));
                cos = Math.Cos(angle * (Math.PI / 180));
                opp1 = sin * sizef.Width;
                adj1 = cos * sizef.Height;
                opp2 = sin * sizef.Height;
                adj2 = cos * sizef.Width;

                if (opp1 + adj1 < img.Height && opp2 + adj2 < img.Width)
                    break;
                //
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.RotateTransform((float)angle);
            gr.DrawString(watermarkText, font, new SolidBrush(color), new Point((int)halfHypotenuse, 0), stringFormat);

            img.Save(outputStream, ImageFormat.Jpeg);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ApplicationContext.Current.User == null)
            //    Response.Redirect("/403");
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();

            loadPath = "";
            queryPath = defaultPath.TrimStart('~');

            string folderLoad = Request["folder"];
            string path = Request["Path"];
            string resolvePath = path;
            if (string.IsNullOrEmpty(folderLoad) == false)
            {
                if (string.IsNullOrEmpty(path) == false)
                {
                    if (folderLoad.Trim('/').ToLower() == path.Trim('/').ToLower())
                        resolvePath = folderLoad;
                    else if (path.TrimStart('/').ToLower().StartsWith(folderLoad.ToLower().TrimStart('/')))
                        resolvePath = path;
                    else
                        resolvePath = folderLoad.TrimEnd('/') + "/" + path.TrimStart('/');
                }
                else
                    resolvePath = folderLoad;
            }

            resolvePath = CorrectPath(resolvePath);

            if (string.IsNullOrEmpty(folderLoad) == false)
            {
                loadPath = CorrectPath(folderLoad);
                queryPath = loadPath.TrimStart('~');
            }
            else
                loadPath = defaultPath;

            switch (Request["mode"])
            {
                case "getinfo":
                    //System.Threading.Thread.Sleep(6000);
                    Response.ContentType = "plain/text";
                    Response.ContentEncoding = Encoding.UTF8;
                    int pos = resolvePath.IndexOf('?');
                    if (pos >= 0)
                        resolvePath = resolvePath.Substring(0, pos);
                    Response.Write(getInfo(resolvePath));
                    break;
                case "getfolder":
                    //System.Threading.Thread.Sleep(6000);
                    Response.ContentType = "plain/text";
                    Response.ContentEncoding = Encoding.UTF8;
                    Response.Write(getFolderInfo(resolvePath));
                    break;
                case "rename":
                    Response.ContentType = "plain/text";
                    Response.ContentEncoding = Encoding.UTF8;
                    Response.Write(Rename(Request["old"], Request["new"]));
                    break;
                case "delete":
                    Response.ContentType = "plain/text";
                    Response.ContentEncoding = Encoding.UTF8;
                    Response.Write(Delete(resolvePath));
                    break;
                case "addfolder":
                    Response.ContentType = "plain/text";
                    Response.ContentEncoding = Encoding.UTF8;
                    Response.Write(AddFolder(resolvePath, Request["name"]));
                    break;
                case "download":
                    FileInfo fi = new FileInfo(Server.MapPath(resolvePath));
                    if (fi.Exists == false)
                    {
                        Response.Write(JsonConvert.SerializeObject(new
                        {
                            Error = "The file '" + resolvePath + "' does not exists.",
                            Code = -1
                        }));
                    }
                    else
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlPathEncode(fi.Name));
                        Response.AddHeader("Content-Length", fi.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.TransmitFile(fi.FullName);
                    }
                    break;
                case "add":
                    SaveFile();

                    break;
                case "replace":
                    Response.ContentType = "application/json";
                    Response.ContentEncoding = Encoding.UTF8;

                    #region RegionName

                    System.Web.HttpPostedFile fileReplace = Request.Files[0];
                    string newFilePath = Request["newfilepath"];

                    string resolvePath2 = CorrectPath(newFilePath);

                    if (File.Exists(HttpContext.Current.Server.MapPath(resolvePath2)))
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(resolvePath2));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    fileReplace.SaveAs(Server.MapPath(Path.Combine(resolvePath2)));

                    Response.Write(JsonConvert.SerializeObject(new
                    {
                        Error = "No error",
                        Code = 0
                    }));

                    #endregion

                    break;
                case "savefile":
                    Response.ContentType = "application/json";
                    Response.ContentEncoding = Encoding.UTF8;

                    #region RegionName

                    string content = Request["content"];

                    string np = HttpContext.Current.Server.MapPath(resolvePath);
                    try
                    {
                        File.WriteAllText(np, content);
                        Response.Write(JsonConvert.SerializeObject(new
                        {
                            Path = MakeRelativePath(np, false),
                            Error = "No error",
                            Code = 0
                        }));
                    }
                    catch (Exception ex)
                    {
                        Response.Write(JsonConvert.SerializeObject(new
                        {
                            Error = "Error save file.",
                            Code = -1
                        }));
                    }

                    #endregion

                    break;
                case "editfile":
                    Response.ContentType = "application/json";
                    Response.ContentEncoding = Encoding.UTF8;

                    #region RegionName
                    //FileAttributes attr = File.GetAttributes(HttpContext.Current.Server.MapPath(resolvePath2));
                    string fullPath = Server.MapPath(resolvePath);

                    StringBuilder sb1 = new StringBuilder();

                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            Response.Write(JsonConvert.SerializeObject(new
                            {
                                Path = path,
                                Content = File.ReadAllText(fullPath),
                                Error = "No error",
                                Code = 0
                            }));
                        }
                        catch (Exception ex)
                        {
                            Response.Write(JsonConvert.SerializeObject(new
                            {
                                Error = ex.Message,
                                Code = -1
                            }));
                        }
                    }
                    else
                    {
                        Response.Write(JsonConvert.SerializeObject(new
                        {
                            Path = path,
                            Content = "",
                            Error = "File does not exists.",
                            Code = 404
                        }));
                    }
                    #endregion

                    break;
                case "summarize":
                    Response.Write(JsonConvert.SerializeObject(GetStorageStatic(resolvePath)));
                    break;
                case "move":
                    string oldPath = Request["old"];
                    string newPath = Request["new"];

                    string resolveOldPath = CorrectPath(oldPath.TrimEnd('/'));
                    string resolveNewPath = CorrectPath(newPath.TrimEnd('/').TrimStart('~').TrimStart('.').TrimStart('/'));
                    string physicalOld = HttpContext.Current.Server.MapPath(resolveOldPath);
                    string physicalNew = HttpContext.Current.Server.MapPath(resolveNewPath);
                    FileAttributes attr = File.GetAttributes(physicalOld);

                    #region folder

                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        if (Directory.Exists(physicalNew))
                        {
                            try
                            {
                                physicalNew = Path.Combine(physicalNew, Path.GetFileName(physicalOld));

                                Directory.Move(physicalOld, physicalNew);
                                Response.Write(JsonConvert.SerializeObject(new Dictionary<string, object>()
                                    {
                                        { "New Name", Path.GetFileName(physicalNew) },
                                        { "New Path", "/"+ MakeRelativePath(physicalNew, true).TrimStart('/') },
                                        { "Code" , 0 }
                                    }));
                            }
                            catch (Exception ex)
                            {
                                Response.Write(JsonConvert.SerializeObject(new
                                {
                                    Error = "Cannot move this folder to " + resolveNewPath,
                                    Code = -1
                                }));
                            }
                        }
                        else
                        {
                            Response.Write(JsonConvert.SerializeObject(new
                            {
                                Error = "Folder '" + resolveNewPath + "' does not exists.",
                                Code = -1
                            }));
                        }
                    }
                    #endregion
                    else
                    #region file
                    {
                        if (File.Exists(physicalOld))
                        {
                            if (Directory.Exists(physicalNew))
                            {
                                try
                                {
                                    physicalNew = Path.Combine(physicalNew, Path.GetFileName(physicalOld));
                                    File.Move(physicalOld, physicalNew);

                                    string foldertrim = "/" + queryPath.Trim('/');
                                    string temp = "/" + MakeRelativePath(physicalNew, false).TrimStart('/');
                                    if (foldertrim.Length > 1)
                                        temp = temp.Remove(0, foldertrim.Length);

                                    Response.Write(JsonConvert.SerializeObject(new Dictionary<string, object>()
                                    {
                                        { "New Name", Path.GetFileName(physicalNew) },
                                        { "New Path", temp },
                                        { "Code" , 0 }
                                    }));
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(JsonConvert.SerializeObject(new
                                    {
                                        Error = "Cannot move this file to " + physicalNew,
                                        Code = -1
                                    }));
                                }
                            }
                            else
                            {
                                Response.Write(JsonConvert.SerializeObject(new
                                {
                                    Error = "Folder '" + resolveNewPath + "' does not exists.",
                                    Code = -1
                                }));
                            }
                        }
                    }
                    #endregion

                    break;
                default:
                    break;
            }
        }

    }
}