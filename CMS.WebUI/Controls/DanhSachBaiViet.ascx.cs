using CMS.DataAsscess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class DanhSachBaiViet : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void GetAllPost(List<BaiVietDto> postList,string url = "BaiVietPublish.aspx?id=")
        {
            string postListShow = "";
            foreach (var post in postList)
            {
                postListShow += string.Format($@" <div class=""col-sm-6 col-lg-4 colItem"">
                        <div class=""contentCol wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                            <div class=""contentImg"">
                                <div class=""wrapImg"">
                                    <a class=""wrapImgResize img16And9"" href=""{url}{post.Id}"" title=""{post.TieuDe}"">
                                        <img src=""/Administration/UploadImage/{post.ThumbnailUrl}"" alt=""{post.TieuDe}"" /></a>
                                </div>

                                <div class=""time"">
                                    <div class=""contentItem"">
                                        <p class=""txtDate"">{post.NgayTao.ToString("dd")}</p>
                                        <p class=""txtMonthYear"">{post.NgayTao.ToString("MM/yyyy")}</p>
                                    </div>
                                </div>
                            </div>

                            <div class=""wrapText"">
                                <h4 class=""titleMain""><a class=""title5 linkTitle"" href=""{url}{post.Id}"" title=""{post.TieuDe}"">{post.TieuDe}</a></h4>
                                <div class=""wrapInfo clearfix"">
                                    <p class=""author"">
                                        <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 512 512"">
                                            <path fill=""currentColor"" d=""M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z""></path>
                                        </svg>TẬP ĐOÀN HOÀN CẦU KHU VỰC KHÁNH HÒA
                                   
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>");
            }
            ltlPost.Text = postListShow;
        }
    }
}