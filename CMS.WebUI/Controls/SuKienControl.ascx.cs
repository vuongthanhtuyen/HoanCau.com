using CMS.Core.Publich;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SweetCMS.Core.Helper;
using CMS.Core.Manager;

namespace CMS.WebUI.Controls
{
    public partial class SuKienControl : System.Web.UI.UserControl
    {
        public BaiViet ObjBaiViet { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (ObjBaiViet != null) {

                    Binding();
                }
            }
        }
        private void Binding()
        {
           
            List<NhomHinhAnh> nhomHinhAnh = DuAnTieuBieuPublishBLL.GetNhomHinhAnh(ObjBaiViet.Id);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sbMain = new StringBuilder();

            foreach (var hinhanh in nhomHinhAnh)
            {
                sb1.AppendFormat(templateItemSlide.InnerHtml, ObjBaiViet.TieuDe, hinhanh.HinhAnhUrl);
                sb2.AppendFormat(templateItemSubSlide.InnerHtml, ObjBaiViet.TieuDe, hinhanh.HinhAnhUrl);
            }
            ObjBaiViet.ViewCount += 1;
            ObjBaiViet = BaiVietPublishBLL.Update(ObjBaiViet);


            sbMain.AppendFormat(templateDanhMuc.InnerHtml,
                ObjBaiViet.TieuDe,
                Server.HtmlDecode(ObjBaiViet.MoTaNgan),
                Server.HtmlDecode(ObjBaiViet.NoiDungChinh),
                sb1,
                sb2,
                GetVideo(ObjBaiViet.Id));
            ltrMain.Text = sbMain.ToString();
            //SlideTop.ShowBreadcrumb(ObjBaiViet.TieuDe, "Administration/UploadImage/" + ObjBaiViet.ThumbnailUrl,
            //    new List<Common.ExtendWeb.Breadcrumb>()
            //{
            //    new Common.ExtendWeb.Breadcrumb()
            //    {
            //        Title = "DỰ ÁN TIÊU BIỂU",
            //        Url = "DanhSachDuAnTieuBieuPublish"
            //    }
            //});
            //ltlPostView.Text = duAnView;
            GetCoTheBanSeThichDuAnTieuBieu(ObjBaiViet.Id);
            Page.Title = ObjBaiViet.TieuDe;
            
        }

        private string GetVideo(int id)
        {
            List<Video> videos = VideoBLL.GetListVideoByCatId(id);
            StringBuilder sb1 = new StringBuilder();

            foreach (var item in videos)
            {
                sb1.AppendFormat(templateItemVideo.InnerHtml, item.Title, item.ThumbnailUrl, item.VideoLink);
            }
            return sb1.ToString();
        }
        private void GetCoTheBanSeThichDuAnTieuBieu(int duAnTieuBieuId)
        {
            int totalRow = 0;
            var postList = DanhMucPublishBLL.GetPaging(null, null, null, null, 4, ApplicationContext.Current.ContentCurrentLanguageId, out totalRow);
            postList = postList.Where(x => x.Id != duAnTieuBieuId).ToList();
            string show = "";
            foreach (var post in postList)
            {
                show += string.Format($@"<div class=""itemSlide"">
                    <div class=""contentItem wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                        <div class=""wrapImg""><a class=""wrapImgResize img16And9"" href=""{post.Slug}"" title=""{post.TieuDe}""><img src=""/Administration/UploadImage/{post.ThumbnailUrl}"" alt=""{post.TieuDe}"" /></a></div>
                        <div class=""wrapTextItem"">
                            <h3 class=""wrapTitle""><a class=""linkTitle"" href=""{post.Slug}"" title=""{post.TieuDe}"">{post.TieuDe}</a></h3>
                            <div class=""wrapDes"">{Regex.Replace(post.MoTaNgan, "<.*?>", string.Empty)}</div>
                        </div>
                    </div>
                </div>");
            }
            ltrCoTheBanThich.Text = show;
        }


    }
}