using Newtonsoft.Json;
using SweetCMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace SweetCMS.WebUI.RichFilemanager.imageeditor
{
    public partial class ImageEditor : Page
    {
        public static string JoinKey
        {
            get
            {
                return SecurityHelper.Encrypt("*");
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CultureInfo currentLanguage = CultureInfo.GetCultureInfo("en-US");

                StringBuilder sbBuild = new StringBuilder();

                List<Dictionary<string, string>> lst = ParseSetting();
                if (lst != null && lst.Count > 0)
                {
                    //lst[0] - dataresize, lst[1] - data name
                    List<string[]> lstResize = new List<string[]>();
                    List<string[]> lstCrop = new List<string[]>();

                    string editorKey = CommonHelper.QueryString("editorKey");

                    if (string.IsNullOrEmpty(editorKey) == true)
                    {
                        lstResize.Add(new string[2] { "", "Select setting" });

                        lstCrop.Add(new string[3] { "0", "0", "Crop custom" });
                    }

                    NameValueCollection dataQuery = null;
                    string name = string.Empty;
                    string key = string.Empty;

                    if (string.IsNullOrEmpty(editorKey) == false)
                    {
                        for (int i = 0; i < lst[0].Count; i++)
                        {
                            name = lst[1].ElementAt(i).Value;
                            key = lst[1].ElementAt(i).Key;
                            if (key == editorKey)
                            {
                                dataQuery = HttpUtility.ParseQueryString(lst[0].ElementAt(i).Value);
                                if (dataQuery != null)
                                {
                                    lstResize.Add(new string[] { dataQuery.ToString(), name, key });
                                    lstCrop.Add(new string[] { dataQuery["w"], dataQuery["h"] ?? "0", name, key });
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lst[0].Count; i++)
                        {
                            name = lst[1].ElementAt(i).Value;
                            key = lst[1].ElementAt(i).Key;
                            dataQuery = HttpUtility.ParseQueryString(lst[0].ElementAt(i).Value);
                            if (dataQuery != null)
                            {
                                lstResize.Add(new string[] { dataQuery.ToString(), name, key });
                                lstCrop.Add(new string[] { dataQuery["w"], dataQuery["h"], name, key });
                            }
                        }
                    }

                    //data Crop
                    if (lstCrop != null && lstCrop.Count > 0)
                        sbBuild.Append("var ISDataCrop=" + JsonConvert.SerializeObject(lstCrop) + ";");

                    //data Resize
                    if (lstResize != null && lstResize.Count > 0)
                        sbBuild.Append("var ISDataResize=" + JsonConvert.SerializeObject(lstResize) + ";");

                }

                //resource text
                #region resource text

                var dataText = new DataText();
                dataText.icons = new { };
                dataText.labels = new { };

                #region RegionName
                /*
                if (CMS.Common.CMSContext.Current.CurrentLanguageId == LanguageHelper.Vietnamese)
                {
                    dataText.labels = new
                        {
                            pane_rotateflip = "Xoay và lật",
                            rotateleft = "Xoay trái",
                            rotateright = "Xoay phải",
                            flipvertical = "Lật dọc",
                            fliphorizontal = "Lật ngang",
                            reset = "Làm lại",
                            pane_crop = "Cắt",
                            aspectratio = "Tỉ lệ",

                            crop_crop = "Cắt",
                            crop_modify = "Chỉnh sửa lại",
                            crop_cancel = "Hủy",
                            crop_done = "Xong",
                            pane_adjust = "Điều chỉnh hình ảnh",
                            autofix = "Tự động sửa",
                            autowhite = "Tự động cân bằng",
                            contrast = "Độ tương phản",
                            saturation = "Độ bão hòa",
                            brightness = "Độ sáng",
                            pane_effects = "Hiệu ứng &amp; Bộ lọc",
                            blackwhite = "Đen và trắng",
                            sepia = "Ảnh nâu đỏ",
                            negative = "Ngược màu",
                            sharpen = "Độ sắc nét",
                            noiseremoval = "Loại bỏ nhiễu",
                            oilpainting = "Tranh sơn dầu",
                            posterize = "Posterize",
                            blur = "Gaussian Blur",
                            pane_redeye = "Xóa mắt đỏ",
                            redeye_auto = "Tự động nhận diện mắt",
                            redeye_start = "Sửa lỗi mắt đỏ",
                            redeye_preview = "Xem trước",
                            redeye_clear = "Xóa",
                            pane_faces = "Chọn khuôn mặt",
                            faces_auto = "Tự động nhận diện khuôn mặt",
                            faces_start = "Chọn khuôn mặt",
                            faces_clear = "Xóa",
                            cancel = "Hủy",
                            done = "Xong",
                            pane_carve = "Object Removal",
                            carve_start = "Xóa đối tượng",
                            carve_preview = "Xem trước kết quả",
                            pane_save = "Lưu tập tin",
                            save = "Lưu",
                            saveas = "Lưu như...",
                            save_Tilte = "Lưu tập tin mới",
                            save_Label = "Lưu với tên khác:",
                            save_close_btn = "Đóng",
                            save_btn = "Lưu",

                            pane_resize = "Thay đổi kích cỡ",

                            pane_advanced = "Mở rộng",
                            border_width = "Độ rộng viền",
                            border_color = "Màu sắc viền",
                            margin = "Khoảng cách canh lề",
                            padding_width = "Đồ rộng khung",
                            padding_color = "Màu sắc khung",
                            bg_color = "Màu nền ảnh",

                            pane_trimwhitespace = "Cắt khoảng màu",
                            whitespacetrim_threshold = "Ngưỡng",
                            whitespacetrim_percent = "Tỉ lệ cách khung"
                        };
                }
                */
                #endregion

                sbBuild.Append(String.Format("var ISText={0};", JsonConvert.SerializeObject(dataText)));

                #endregion

                sbBuild.Append("var nofile=1;");

                sbBuild.Append("var customTextNoSelected='" + "Select setting" + "';");

                System.Web.UI.ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                   GetType(), "joinKey", String.Format("var joinKey='{0}';", JoinKey), true);

                /* for custom panes
                if (Request.QueryString["crop"] != null && Request.QueryString["crop"] == "1")
                    System.Web.UI.ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as System.Web.UI.Page,
                       GetType(), "custompanes",
                       "var custompanes=['" + "crop" + "'];",
                       true);
                */
                ltData.Text = String.Format("<script type='text/javascript'>{0}</script>", sbBuild);
            }
        }

        static List<Dictionary<string, string>> ParseSetting()
        {
            List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();

            Array values = Enum.GetValues(typeof(CMSImageType));
            if (values != null && values.Length > 0)
            {
                RenderAttribute type = null;
                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                foreach (var val in values)
                {
                    type = GetRenderAttribute((CMSImageType)val);
                    if (type != null)
                    {
                        dic1.Add(val.ToString(), type.ToImageResizeString());
                        dic2.Add(val.ToString(), type.ToString());
                    }
                }

                lst.Add(dic1);
                lst.Add(dic2);
            }

            return lst;
        }

        // Capitalize the first character and add a space before
        // each capitalized letter (except the first character).
        public static string ToProperCase(string the_string)
        {
            const string pattern = @"(?<=\w)(?=[A-Z])";
            //const string pattern = @"(?<=[^A-Z])(?=[A-Z])";
            string result = Regex.Replace(the_string, pattern, " ", RegexOptions.None);
            return result.Substring(0, 1).ToUpper() + result.Substring(1);
        }
    }

    public class DataText
    {
        public object icons { get; set; }
        public object labels { get; set; }
    }
}