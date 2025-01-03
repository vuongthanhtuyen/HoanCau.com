using CMS.Core.Manager;
using CMS.DataAsscess;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class ChiTietNganhDaoTao : System.Web.UI.UserControl
    {

        public NganhDaoTao ObjNganhDaoTao { get; set; }

        public string TenChuongTrinh { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ObjNganhDaoTao != null)
                {
                    ltrMain.Text =  BindNganhDaoTao(ObjNganhDaoTao);
                }
            }
        }
        private string BindNganhDaoTao(NganhDaoTao nganhDaoTao)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(templateDetail.InnerHtml,
                nganhDaoTao.TenNganh,
                TenChuongTrinh,
                BindChuyenNganh(nganhDaoTao.Id),
                Helpers.GetThumbnailUrl(nganhDaoTao.ThumbnailUrl),
                nganhDaoTao.MaNganh,
                nganhDaoTao.SoTinChi,
                nganhDaoTao.SoNamDaoTao,
                nganhDaoTao.HocPhi,
                nganhDaoTao.DieuKienNhapHoc,
                Server.HtmlDecode(nganhDaoTao.NoiDung)
                );
            return sb.ToString();
            
        }

        private string BindChuyenNganh(int idCate)
        {
            List<FileAttactment> listFile = FileAttactmentBLL.GetAllFileDinhKemByCatId(idCate);

            string phanTu = @"<span class=""divide-box-tech-compare""></span>";
            StringBuilder sb = new StringBuilder();

            if (listFile != null && listFile.Count > 0)
            {
                for (int i = 0; i < listFile.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.AppendFormat(phanTu);
                    }
                    sb.AppendFormat(@"<a href=""{0}"" target=""_blank"" class=""product-detail-read-more"">
                                       <span>{1}</span>
                                  </a>",
                                string.Format("{0}", listFile[i].FileUrl.TrimStart('/')),
                                listFile[i].Title
                    );

                }
            }
            return sb.ToString();
        }
    }
}
