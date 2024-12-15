using CMS.Core.Publich;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.ExtendWeb;

namespace CMS.WebUI.Controls.ControlContentPage
{
    public partial class BaiVietGioiThieu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public void Binding(string slugPost)
        {
            //string idPostString = Request.QueryString["id"];
            //int idPost = 1023;
            //if (idPostString != null)
            //{
            //    idPost = int.Parse(idPostString);
            //}
            BaiViet baiViet = new BaiViet();
            baiViet = BaiVietPublishBLL.GetByMa(slugPost);
            baiViet.ViewCount += 1;
            baiViet = BaiVietPublishBLL.Update(baiViet);
            Page.Title = baiViet.TieuDe;
            string thm = "";
            if (baiViet.ThumbnailUrl != null)
            {
                //thm = string.Format($@" <img src = ""Administration/UploadImage/{baiViet.ThumbnailUrl}
                //"" alt = ""{baiViet.TieuDe} "">");
            }
            var postView = string.Format($@"
                
                            <div class=""wrapImg""> {thm}</div>
                            <div class=""contentText"">
                                <p class=""titleNewsMain"">{baiViet.TieuDe}</p>

                                <div class=""infoNumberItem"">
                                    <span class=""itemInfoNumber user"">
                                        <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 512 512"">
                                            <path fill=""currentColor"" d=""M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z""></path>
                                        </svg>ĐẠI HỌC THÁI BÌNH DƯƠNG</span><span class=""itemInfoNumber view"">
                                            <svg aria-hidden=""true"" focusable=""false"" role=""img"" xmlns=""http://www.w3.org/2000/svg"" viewbox=""0 0 576 512"">
                                                <path fill=""currentColor"" d=""M288 288a64 64 0 0 0 0-128c-1 0-1.88.24-2.85.29a47.5 47.5 0 0 1-60.86 60.86c0 1-.29 1.88-.29 2.85a64 64 0 0 0 64 64zm284.52-46.6C518.29 135.59 410.93 64 288 64S57.68 135.64 3.48 241.41a32.35 32.35 0 0 0 0 29.19C57.71 376.41 165.07 448 288 448s230.32-71.64 284.52-177.41a32.35 32.35 0 0 0 0-29.19zM288 96a128 128 0 1 1-128 128A128.14 128.14 0 0 1 288 96zm0 320c-107.36 0-205.46-61.31-256-160a294.78 294.78 0 0 1 129.78-129.33C140.91 153.69 128 187.17 128 224a160 160 0 0 0 320 0c0-36.83-12.91-70.31-33.78-97.33A294.78 294.78 0 0 1 544 256c-50.53 98.69-148.64 160-256 160z""></path>
                                            </svg>{baiViet.ViewCount} Lượt xem</span><span class=""itemInfoNumber date"">
                                                <svg aria-hidden=""true"" focusable=""false"" role=""img"" xmlns=""http://www.w3.org/2000/svg"" viewbox=""0 0 448 512"">
                                                    <path fill=""currentColor"" d=""M400 64h-48V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H128V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H48C21.5 64 0 85.5 0 112v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V112c0-26.5-21.5-48-48-48zM48 96h352c8.8 0 16 7.2 16 16v48H32v-48c0-8.8 7.2-16 16-16zm352 384H48c-8.8 0-16-7.2-16-16V192h384v272c0 8.8-7.2 16-16 16zM148 320h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 96h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm192 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12z""></path>
                                                </svg>{baiViet.ChinhSuaGanNhat}</span>
                                </div>
                                <div class=""wrapDesItem"">
                                    <div class=""media"">
                                        
                                        <div class=""media-body"">
                                            <p class=""desItem"">{baiViet.MoTaNgan}</p>
                                        </div>
                                    </div>
                                </div>

                                <div class=""wrapText showTextDetail"">
                                   {baiViet.NoiDungChinh}
                                </div>
                             </div>
                         
                ");

            ltlPostView.Text = postView;



            DanhMuc danhMuc = new DanhMuc();
            danhMuc = BaiVietPublishBLL.GetDanhMucByIdBaiViet(baiViet.Id);
            int danhMucId = 0;
            List<Breadcrumb> list = new List<Breadcrumb>();
            if (danhMuc != null)
            {
                Breadcrumb breadcrumb = new Breadcrumb()
                {
                    Title = danhMuc.Ten,
                    Url = danhMuc.Slug
                };
                danhMucId = danhMuc.Id;
                list.Add(breadcrumb);
            }
            else
            {
                Breadcrumb breadcrumb = new Breadcrumb()
                {
                    Title = "Tất cả bài viết",
                    Url = "DanhMucPublish"
                };
                list.Add(breadcrumb);
            }

            SlideTop.ShowBreadcrumb(baiViet.TieuDe, null, list);
        }
    }
}