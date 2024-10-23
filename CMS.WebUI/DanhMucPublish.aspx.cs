using CMS.Core.Manager;
using CMS.Core.Publich;
using CMS.DataAsscess;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class DanhMucPublish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Binding();
        }


        private void Binding()
        {
            try
            {
                DanhMuc danhMuc = new DanhMuc();
                List<BaiVietDto> postList = new List<BaiVietDto>();
                int totalRow = 0;
                string idParam = Request.QueryString["id"];
                int idDanhMuc = 0; // Hoặc giá trị mặc định mà bạn muốn

                if (!string.IsNullOrEmpty(idParam))
                {
                    // Thử parse id, nếu không parse được sẽ không gây lỗi
                    if (int.TryParse(idParam, out int result))
                    {
                        idDanhMuc = result; // Gán giá trị nếu parse thành công
                        danhMuc = DanhMucPublishBLL.GetById(idDanhMuc);
                        
                        postList = DanhMucPublishBLL.GetPaging(10, 1, null, null, danhMuc.Id, out totalRow);
                        SlideTop.ShowBreadcrumb(danhMuc.Ten);
                         Page.Title = danhMuc.Ten.ToString();
                        if (idDanhMuc == 3)
                        {
                            DanhSachBaiViet.GetAllPost(postList, "BaiVietBigPublish?id=");
                        }
                        else
                        {
                            DanhSachBaiViet.GetAllPost(postList);
                        }
                        
                    }
                }
                else
                {
                    postList = DanhMucPublishBLL.GetPaging(12, 1, null, null, null, out totalRow);
                    SlideTop.ShowBreadcrumb("Tất cả bài viết");
                    Page.Title = "Danh sách các bài viết";
                    DanhSachBaiViet.GetAllPost(postList);
                }
                
            }
            catch
            {
                
            }
            
            
        }
    }
}