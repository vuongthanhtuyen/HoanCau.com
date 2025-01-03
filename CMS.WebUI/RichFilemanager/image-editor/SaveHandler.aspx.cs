using ImageResizer;
using TBDCMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TBDCMS.WebUI.RichFilemanager.imageeditor
{
    public partial class SaveHandler : System.Web.UI.Page
    {
        public static Byte[] ToByteArray(Stream stream)
        {
            Int32 length = stream.Length > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(stream.Length);
            Byte[] buffer = new Byte[length];
            stream.Read(buffer, 0, length);
            return buffer;
        }

        public static T[] CopyArray<T>(T[] a) where T : struct
        {
            T[] res = new T[a.Length];
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            //DateTime time1 = DateTime.Now;
            System.Buffer.BlockCopy(a, 0, res, 0, size * a.Length);
            //Console.WriteLine("Using Buffer blockcopy: {0}", (DateTime.Now - time1).Milliseconds);
            return res;
        }

        static string defaultPath = TBDCMS.WebUI.RichFilemanager.connectors.filemanager.defaultPath;

        static string CorrectPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return defaultPath;

            string resolvePath = path;
            if (string.IsNullOrEmpty(resolvePath) == false &&
                resolvePath.StartsWith(defaultPath))
                return resolvePath;

            if (string.IsNullOrEmpty(resolvePath) == false &&
                resolvePath.StartsWith(defaultPath) == false)
            {
                string dd = resolvePath.TrimStart('~');
                dd = dd.TrimStart('/');

                if (("~/" + dd).TrimEnd('/').StartsWith(defaultPath.TrimEnd('/')))
                {
                    if (resolvePath.StartsWith("/") == false)
                        resolvePath = "/" + resolvePath;

                    if (resolvePath.StartsWith("~") == false)
                        resolvePath = "~" + resolvePath;
                    return resolvePath;
                }

                resolvePath = defaultPath + "/" + dd;
            }

            return resolvePath;
        }


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

        protected void Page_Load(object sender, EventArgs e)
        {
            string file = Request.QueryString["f"];
            if (string.IsNullOrEmpty(file))
                return;
            string folderload = Request.QueryString["folder"];

            string pathSave = string.Empty;

            object fileContent = null;

            file = (string.IsNullOrEmpty(folderload) ? "" : folderload.TrimEnd('/')) + "/" + file.TrimStart('/');

            string resolvePath = CorrectPath(file);

            fileContent = Server.MapPath(resolvePath);
            pathSave = Server.MapPath(resolvePath);

            bool hasSaveAs = false;
            string newname = Request.QueryString["newname"];
            if (string.IsNullOrEmpty(newname))
                newname = Path.GetFileName(file);
            else
                hasSaveAs = true;

            newname = FileHelper.ChangeFileName(newname);

            System.Collections.Specialized.NameValueCollection val = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (val != null)
            {
                try
                {
                    val.Remove("f");//file
                    val.Remove("newname");//new file name
                    val.Remove("numrandom");//random number for disable cache image
                    val.Remove("maxwidth");//max width
                    val.Remove("maxheight");//max height

                    string folder = Path.GetDirectoryName(pathSave);
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

                    string settingColletion = ImageResizer.Util.PathUtils.BuildQueryString(val);
                    using (var ms = new MemoryStream())
                    {
                        ImageBuilder.Current.Build(new ImageJob(fileContent, ms,
                            new Instructions(settingColletion), false, true));

                        if (hasSaveAs == false)
                        {
                            using (FileStream fs = new FileStream(Path.Combine(folder, newname), FileMode.Create, FileAccess.Write))
                            {
                                ms.WriteTo(fs);

                                string url = fs.Name.Substring(System.Web.Hosting.HostingEnvironment.MapPath("~/").Length).Replace('\\', '/').Insert(0, "/");
                                if (string.IsNullOrEmpty(folderload) == false)
                                {
                                    string foldertrim = "/" + folderload.Trim('/');
                                    url = url.Remove(0, foldertrim.Length);
                                }

                                Response.Write("1||Successful save new image.||" + url);
                            }
                        }
                        else
                        {
                            //prevent dupplicate
                            string newpath = Path.Combine(folder, newname);
                            newpath = NextAvailableFilename(newpath);
                            using (FileStream fs = new FileStream(newpath, FileMode.Create, FileAccess.Write))
                            {
                                ms.WriteTo(fs);

                                //create small thumbnail: special for article
                                if (newpath.ToLower().Contains("\\uploads\\socialnetwork") == false)
                                {
                                    ms.Position = 0;
                                    string fileNameSmall = Path.GetFileName(newpath);
                                    using (var mssmall = new MemoryStream())
                                    {
                                        ImageBuilder.Current.Build(new ImageJob(ms, mssmall,
                                            new Instructions(Helpers.GetRenderAttribute(CMSImageType.Normal).ToImageResizeString()), false, true));

                                        newpath = newpath.ToLower().Replace("\\uploads\\", "\\uploads\\socialnetwork\\");
                                        newpath = Path.Combine(Path.GetDirectoryName(newpath), fileNameSmall);                                        
                                        newpath = FileHelper.NextAvailableFilename(newpath);
                                        using (FileStream fssmall = new FileStream(newpath, FileMode.Create, FileAccess.Write))
                                        {
                                            mssmall.WriteTo(fssmall);
                                        }
                                    }
                                }

                                string url = fs.Name.Substring(System.Web.Hosting.HostingEnvironment.MapPath("~/").Length).Replace('\\', '/').Insert(0, "/");
                                if (string.IsNullOrEmpty(folderload) == false)
                                {
                                    string foldertrim = "/" + folderload.Trim('/');
                                    url = url.Remove(0, foldertrim.Length);
                                }

                                Response.Write("1||Successful save new image.||" + url);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Could not find a part of the path"))
                        Response.Write("0||Directory does not exists.");
                    else
                        Response.Write("0||Error while process image.");
                }
            }
        }



    }
}