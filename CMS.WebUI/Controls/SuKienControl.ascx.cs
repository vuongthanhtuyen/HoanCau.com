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
        public string slugUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (slugUrl != null) {
                    Binding();
                }
            }
        }
        private void Binding()
        {
            var objSuKien = DuAnTieuBieuPublishBLL.GetByMa(slugUrl);
            List<NhomHinhAnh> nhomHinhAnh = DuAnTieuBieuPublishBLL.GetNhomHinhAnh(objSuKien.Id);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sbMain = new StringBuilder();

            foreach (var hinhanh in nhomHinhAnh)
            {
                sb1.AppendFormat(templateItemSlide.InnerHtml, objSuKien.TieuDe, hinhanh.HinhAnhUrl);
                sb2.AppendFormat(templateItemSubSlide.InnerHtml, objSuKien.TieuDe, hinhanh.HinhAnhUrl);
            }
            objSuKien.ViewCount += 1;
            objSuKien = BaiVietPublishBLL.Update(objSuKien);


            sbMain.AppendFormat(templateDanhMuc.InnerHtml,
                objSuKien.TieuDe,
                Server.HtmlDecode(objSuKien.MoTaNgan),
                Server.HtmlDecode(objSuKien.NoiDungChinh),
                sb1,
                sb2,
                GetVideo(objSuKien.Id));
            ltrMain.Text = sbMain.ToString();
            //SlideTop.ShowBreadcrumb(objSuKien.TieuDe, "Administration/UploadImage/" + objSuKien.ThumbnailUrl,
            //    new List<Common.ExtendWeb.Breadcrumb>()
            //{
            //    new Common.ExtendWeb.Breadcrumb()
            //    {
            //        Title = "DỰ ÁN TIÊU BIỂU",
            //        Url = "DanhSachDuAnTieuBieuPublish"
            //    }
            //});
            //ltlPostView.Text = duAnView;
            GetCoTheBanSeThichDuAnTieuBieu(objSuKien.Id);
            Page.Title = objSuKien.TieuDe;
            
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