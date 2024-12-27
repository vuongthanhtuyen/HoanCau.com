using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class ChiTietThanhVien : System.Web.UI.UserControl
    {

        public BaiViet ThanhVien {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                
                if (ThanhVien != null) {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendFormat(templateDetail.InnerHtml,
                        ThanhVien.TieuDe,
                        ThanhVien.MoTaNgan,
                        Server.HtmlDecode(ThanhVien.NoiDungChinh),
                        Helpers.GetThumbnailUrl(ThanhVien.ThumbnailUrl)
                        );
                    ltrMain.Text = stringBuilder.ToString();
                }
                
            }
        }
    }
}