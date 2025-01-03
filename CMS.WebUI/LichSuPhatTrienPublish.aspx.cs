using CMS.Core.Manager;
using CMS.Core.Publich;
using CMS.WebUI.Controls;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class LichSuPhatTrienPublish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                Binding();
                Page.Title = "Lịch sử phát triển";
            }
        }
        private void Binding()
        {
            List<LichSuPhatTrien> lichSuPhatTriens = new List<LichSuPhatTrien>();
            lichSuPhatTriens = LichSuPhatTrienPublishBLL.GetAll();
            string topNam = "";
            string belowContent = "";
            SlideTop.ShowBreadcrumb("Lịch sử phát triển");

            foreach (var ls in lichSuPhatTriens)
            {
                topNam += string.Format($@"
                        <div class=""itemSlide"">
                            <div class=""contentItemSlide"">
                                <p class=""titleItem"">Năm </p>
                                <p class=""number"">{ls.Nam}</p>
                            </div>
                        </div>");


                belowContent += string.Format($@"
                    <div class=""itemSlide"">
                            <div class=""row rowItem"">
                                <div class=""col-lg-5 colImgItem"">
                                    <div class=""contentCol"">
                                        <div class=""wrapImgResize img16And9"">
                                            <img src=""/Administration/UploadImage/{ls.HinhAnhUrl}"" alt=""{ls.TieuDe}""></div>
                                    </div>
                                </div>
                                <div class=""col-lg-7 colTextItem"">
                                    <div class=""contentCol"">
                                        <h5 class=""titleNewsMain"">{ls.TieuDe}</h5>
                                        <div class=""wrapTextItem"">
                                           {ls.MoTa}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>                
                ");

            }

            ltrTopShow.Text = topNam;
            ltrBelowShow.Text = belowContent;

            DanhSachBaiVietCoTheBanThich.GetAllPost(BaiVietPublishBLL.GetBaiVietBanCoTheThich(0));

        }

    }
}