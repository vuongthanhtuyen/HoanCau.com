using CMS.Core.Publich;
using CMS.DataAsscess;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls.ControlContentPage
{
    public partial class Category : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        public void Binding(string slugCate)
        {
            try
            {
                DanhMuc danhMuc = new DanhMuc();
                List<BaiVietDto> postList = new List<BaiVietDto>();
                int totalRow = 0;

                //if (!string.IsNullOrEmpty(idParam))
                //{
                //    // Thử parse id, nếu không parse được sẽ không gây lỗi
                //    if (int.TryParse(idParam, out int result))
                //    {
                //        idDanhMuc = result; // Gán giá trị nếu parse thành công


                danhMuc = DanhMucPublishBLL.GetByMa(slugCate);

                postList = DanhMucPublishBLL.GetPaging(10, 1, null, null, danhMuc.Id,ApplicationContext.Current.CurrentLanguageId, out totalRow);
                SlideTop.ShowBreadcrumb(danhMuc.Ten);
                Page.Title = danhMuc.Ten.ToString();


                DanhSachBaiViet.GetAllPost(postList);

                //if (idCate == 3)
                //{
                //    DanhSachBaiViet.GetAllPost(postList, "BaiVietBigPublish?id=");
                //}
                //else
                //{
                //}

                //else
                //{
                //    postList = DanhMucPublishBLL.GetPaging(12, 1, null, null, null, out totalRow);
                //    SlideTop.ShowBreadcrumb("Tất cả bài viết");
                //    Page.Title = "Danh sách các bài viết";
                //    DanhSachBaiViet.GetAllPost(postList);
                //}

            }
            catch
            {

            }
        }
    }
}