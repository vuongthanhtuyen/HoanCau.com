using CMS.DataAsscess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class DanhSachDuAnTieuBieu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void GetAllDuAnTieuBieu(List<BaiVietDto> postList)
        {
            string duAnTieuBieuShow = "";
            foreach (var post in postList)
            {
                duAnTieuBieuShow += string.Format($@"
                      <div class=""itemList col-lg-12 col-sm-6 col2"">
                            <div class=""row rowItem row3"">
                                <div class=""col-lg-8 colSlide col3"">
                                    <div class=""wrapImg""><a class=""wrapImgResize img16And9"" href=""DuAnTieuBieuPublish?id={post.Id}"" title=""{post.TieuDe}"">
                                        <img src=""/Administration/UploadImage/{post.ThumbnailUrl}"" alt=""{post.TieuDe}"" /></a></div>
                                </div>
                                <div class=""col-lg-4 colText align-self-center col3"">
                                    <div class=""contentCol"">
                                        <h2 class=""titleItem""><a class=""linkItem"" href=""DuAnTieuBieuPublish?id={post.Id}"" title=""{post.TieuDe}"">{post.TieuDe}</a></h2>
                                        <div class=""wrapDes"">{Regex.Replace(post.MoTaNgan, "<.*?>", string.Empty)}</div>
                                        <div class=""wrapBtnItem"">
                                            <a class=""btn1 btnLinkItem"" href=""DuAnTieuBieuPublish?id={post.Id}"" title=""Chi tiết"">Chi tiết<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                                <path d=""M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z"" />
                                            </svg></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                       ");

            }

            ltlPost.Text = duAnTieuBieuShow;
        }
    }
}