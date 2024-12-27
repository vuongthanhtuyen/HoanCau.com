using CMS.Core.Manager;
using CMS.DataAsscess;
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
    public partial class ChuongTrinhDaoTao : System.Web.UI.UserControl
    {
        public int IdDanhMuc { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var objChuongTrinhDaoTao = DanhMucBaiVietBLL.GetById(IdDanhMuc);


                if (objChuongTrinhDaoTao != null)
                {
                    List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetAllByParentId(objChuongTrinhDaoTao.Id);

                    StringBuilder stringBuilder = new StringBuilder();
                    if (listDanhMuc != null && listDanhMuc.Count > 0)
                    {
                        foreach (var item in listDanhMuc)
                        {
                            List<NganhDaoTao> listBaVietInDanhMuc = NganhDaoTaoBLL.GetAllListNganhByDanhMucId(item.Id);
                            if (listBaVietInDanhMuc != null && listBaVietInDanhMuc.Count > 0)
                            {
                                stringBuilder.AppendFormat(
                                templateList.InnerHtml,
                                item.Ten,
                                BindItem(listBaVietInDanhMuc)

                            );
                            }

                        }
                    }
                    ltrMain.Text = stringBuilder.ToString();
                }
            }
        }


        private string BindItem(List<NganhDaoTao> list)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.AppendFormat(templateItem.InnerHtml,
                    item.TenNganh,
                    string.Format("{0}", item.Slug.TrimStart('/')),
                    Helpers.GetThumbnailUrl(item.ThumbnailUrl),
                    item.MoTaNgan,
                    item.SoNamDaoTao,
                    item.SoTinChi,
                    item.ViewCount
                    );
            }
            return sb.ToString();
        }
    }
}