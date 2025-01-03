using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Controls.ControlContentPage;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class DanhSachThanhVien : System.Web.UI.UserControl
    {
        private string _slug = string.Empty;
        public string SlugCategory
        {
            get
            {
                return _slug;
            }
            set
            {
                _slug = value;
            }
        }


        public int IdDanhMuc { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               ltrMain.Text=  BindChid();
            }
        }
        private string BindChid()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var danhMucCoCau = DanhMucBaiVietBLL.GetById(IdDanhMuc);
            if (danhMucCoCau != null)
            {
                List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetAllByParentId(danhMucCoCau.Id);
                foreach (var item in listDanhMuc)
                {
                    stringBuilder.AppendFormat(templateList.InnerHtml,
                        item.Ten,
                        BindDanhMuc(item.Id));
                }
                //SlideTop.ImageThumbnail = Helpers.GetThumbnailUrl(danhMucCoCau.ThumbnailUrl);
                //SlideTop.ShowBreadcrumb(danhMucCoCau.Ten);

            }
            return stringBuilder.ToString();
        }
        private string BindDanhMuc(int IdDanhMuc)
        {
            StringBuilder sbTemplate = new StringBuilder();
            List<BaiViet> listChid = DanhMucBaiVietBLL.GetBaiVietInDanhMuc(IdDanhMuc);
            foreach (var item in listChid)
            {
                sbTemplate.AppendFormat(templateSingleItem.InnerHtml,
                    item.TieuDe,
                    Helpers.GetThumbnailUrl(item.ThumbnailUrl),
                     string.Format("{0}{1}", CommonHelper.GetHostPath(),item.Slug.TrimStart('/')),
                     item.MoTaNgan ?? string.Empty);

            }
            return sbTemplate.ToString();
        }

      
      
    }
}