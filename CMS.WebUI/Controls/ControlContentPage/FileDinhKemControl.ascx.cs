using CMS.Core.Manager;
using CMS.DataAsscess;
using SubSonic;
using TBDCMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls.ControlContentPage
{
    public partial class FileDinhKemControl : System.Web.UI.UserControl
    {

        public int IdDanhMuc { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var fileDinhKem = DanhMucBaiVietBLL.GetById(IdDanhMuc);
                if(fileDinhKem != null)
                {
                    SlideTop.ImageThumbnail = Helpers.GetThumbnailUrl(fileDinhKem.ThumbnailUrl);
                    SlideTop.ShowBreadcrumb(fileDinhKem.Ten);
                    List<ItemDanhMucFileDinhKem> listChid = FileAttactmentBLL.GetChildInByParentDanhMuc(IdDanhMuc);
                    List<ItemFileDto> listFile = FileAttactmentBLL.GetAllFileDinhKemByListCate(listChid.Select(x => x.Id).ToArray());
                    if (listChid != null && listChid.Count > 0 && listFile != null && listFile.Count > 0)
                    {
                        ltrFiledinhKem.Text = BindDanhMuc(listChid, listFile, IdDanhMuc, true);
                    }
                }
            }
        }
        private string BindDanhMuc(List<ItemDanhMucFileDinhKem> listChid, List<ItemFileDto> listFile, int _danhMucCha, bool isParent)
        {
            StringBuilder sbTemplate = new StringBuilder();
            List<ItemFileDto> listFileChid = new List<ItemFileDto>();
            foreach (var item in listChid.Where(x => x.DanhMucChaId == _danhMucCha).OrderBy(x => x.DisplayOrder))
            {
                listFileChid = listFile.Where(x => x.CategoryId == item.Id).ToList();
                sbTemplate.AppendFormat(templateDanhMuc.InnerHtml,
                    item.Ten,
                    BindFileDinhKem(listFileChid),
                    BindDanhMuc(listChid, listFile, item.Id,false),
                    isParent ? " card-primary " : "  collapsed-card ",
                    isParent ? "block" : "none",
                    isParent ? " fas fa-minus " : "fas fa-plus"
                    );

            }
            return sbTemplate.ToString();
        }

        private void BindChid()
        {

        }
        private string BindFileDinhKem(List<ItemFileDto> listfile)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in listfile)
            {
                sb.AppendFormat(templateItemfile.InnerHtml,
                    item.Title,
                    string.Format("{0}", item.FileUrl.TrimStart('/')));
            }
            return sb.ToString();
        }
    }
}