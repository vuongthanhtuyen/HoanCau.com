using CMS.Core.Publich;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls.ControlContentPage
{
    public partial class DuAnTieuBieu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Binding(string slugString)
        {
            string slide1 = "";
            string slide2 = "";
            string duAnView = "";
            var baiVietDuAn = DuAnTieuBieuPublishBLL.GetByMa(slugString);
            List<NhomHinhAnh> nhomHinhAnh = DuAnTieuBieuPublishBLL.GetNhomHinhAnh(baiVietDuAn.Id);

            baiVietDuAn.ViewCount += 1;
            baiVietDuAn = BaiVietPublishBLL.Update(baiVietDuAn);


            foreach (var hinhanh in nhomHinhAnh)
            {
                slide1 += string.Format($@"
                     <div class=""itemSlide"" data-src=""Administration/UploadImage/{hinhanh.HinhAnhUrl}"">
                                        <div class=""contentImg"">
                                            <div class=""wrapImgResize img16And9"">
                                                <img src=""Administration/UploadImage/{hinhanh.HinhAnhUrl}"" alt=""{baiVietDuAn.TieuDe}"">
                                            </div>
                                        </div>
                                    </div>
            ");


                slide2 += string.Format($@"
                                <div class=""itemSlide"">
                                        <div class=""contentImg"">
                                            <div class=""wrapImgResize img16And9"">
                                                <img src=""Administration/UploadImage/{hinhanh.HinhAnhUrl}"" alt=""{baiVietDuAn.TieuDe}"">
                                            </div>
                                        </div>
                                    </div>

            ");
            }


            duAnView = string.Format($@"<div class=""contentItem"">
                <div class=""row rowItem rowSlideImg"">
                    <div class=""col-xl-9 colImg"">
                        <div class=""contentItem"">
                            <div class=""wrapSlideMainImg"">
                                <div class=""showSlideMainImg"">
                                    {slide1}
                                </div>
                            </div>

                            <div class=""wrapSlideCtrlImg"">
                                <div class=""showSlideCtrlImg"">
                                    {slide2}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""col-xl-3 colText"">
                        <div class=""contentItem"">
                            <p class=""titleMain titleNewsMain"">{baiVietDuAn.TieuDe}</p>

                            <div class=""wrapInfoText showTextDetail"">
                                {baiVietDuAn.MoTaNgan}
                            </div>
                        </div>
                    </div>
                </div>


                <div class=""wrapContentDetail"">
                    <div class=""wrapMenuTab"">
                        <div class=""list-group"" id=""tabInfoProject"" role=""tablist"">
                            <a class=""list-group-item list-group-item-action active"" data-toggle=""list"" href=""#project-text"" role=""tab"" title=""Thông tin chi tiết"">Thông tin chi tiết</a></div>
                    </div>

                    <div class=""tab-content tab1"">
                        <div class=""tab-pane active"" role=""tabpanel"" id=""project-text"">
                            <div class=""textDescription"">
                                <div class=""wrapText showTextDetail"">
                                    {baiVietDuAn.NoiDungChinh}
                                </div>
                                <div class=""wrapShare"">
                                    <div class=""titleShare"">Chia sẻ:</div>

                                    <div class=""listBtnSharePost"">
                                        <div class=""sharethis-inline-share-buttons""></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            ");

            SlideTop.ShowBreadcrumb(baiVietDuAn.TieuDe, "Administration/UploadImage/" + baiVietDuAn.ThumbnailUrl,
                new List<Common.ExtendWeb.Breadcrumb>()
            {
                new Common.ExtendWeb.Breadcrumb()
                {
                    Title = "DỰ ÁN TIÊU BIỂU",
                    Url = "DanhSachDuAnTieuBieuPublish"
                }
            });
            ltlPostView.Text = duAnView;
            GetCoTheBanSeThichDuAnTieuBieu(baiVietDuAn.Id);
            Page.Title = baiVietDuAn.TieuDe;
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