using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TBDCMS.WebUI.RichFilemanager.imageeditor
{
    public partial class ImageHandler : System.Web.UI.Page
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


        protected void Page_Load(object sender, EventArgs e)
        {
            string file = Request.QueryString["f"];
            if (string.IsNullOrEmpty(file))
                return;
            string folder = Request.QueryString["folder"];

            object fileContent = null;

            file = (string.IsNullOrEmpty(folder) ? "" : folder.TrimEnd('/')) + "/" + file.TrimStart('/');
            string resolvePath = CorrectPath(file);

            fileContent = Server.MapPath(resolvePath);

            Response.ContentType = "image/jpeg";

            System.Collections.Specialized.NameValueCollection val = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (val != null)
            {
                val.Remove("f");//file
                val.Remove("numrandom");//random number for disable cache image

                string settingColletion = ImageResizer.Util.PathUtils.BuildQueryString(val);
                using (var ms = new MemoryStream())
                {
                    try
                    {
                        ImageBuilder.Current.Build(new ImageJob(fileContent, ms,
                            new Instructions(settingColletion), false, true));

                        Response.AddHeader("Content-Disposition", "inline; filename=" + "preview.jpg");
                        ms.WriteTo(Response.OutputStream);
                    }
                    catch (Exception ex)
                    {
                        //not a image
                    }
                }
            }
        }
    }
}