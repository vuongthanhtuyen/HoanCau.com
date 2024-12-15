using CMS.Core.Manager;
using CMS.Core.Publich;
using CMS.DataAsscess;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class _Default : Page
    {
        private static List<BaiVietDto> listBaiBiet = new List<BaiVietDto>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binding();

                
            }
        }

        private void Binding()
        {
            listBaiBiet = DefaultService.GetAllBaiViet();
            List<TrinhChieuAnh> list = new List<TrinhChieuAnh>();

            list = DefaultService.GetAllSlide();
            GetSlide(list);
            BindGioiThieu();
            BindVersionTamNhin();
            BindVersionQuyMo();
            BindLinhVucHoatDong();
            BindDuAnTieuBieu();
            BindCongTyThanhVien();
            BindDoiTac();
            BindCoTheBanThich();

        }


        #region Code binding
        private void GetSlide(List<TrinhChieuAnh> list)
        {
            string a = "";
            foreach (var item in list)
            {

                a += string.Format($@"
                    <div class=""elementSlideMain"">
                        <div class=""wrapImgResize wrapImgSlideMain""><img src=""/Administration/UploadImage/{item.HinhAnhUrl}"" alt=""ĐẠI HỌC THÁI BÌNH DƯƠNG"" /></div>
                            <div class=""wrapTextSlideMain"">
                                <div class=""container-xxl containerTextSlideMain"">
                                    <div class=""contentTextSlideMain"">
                                        <p class=""wrapDescriptionPostSlideMain"">{item.NoiDungMot}</p>
                                        <h2 class=""titlePostSlideMain"">{item.NoiDungHai}</h2>
                                        <div class=""wrapBtnItem""><a class=""btn1 btnLinkItem"" href=""{item.LienKetUrl}"" title=""Xem ngay"">
                                                Xem ngay<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                                    <path d=""M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z"" />
                                                </svg></a></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    ");
            }
            ltrShowSlide.Text = a;
        }

        private void BindLinhVucHoatDong()
        {
            List<BaiVietDto> listLinhVucHoatDong = listBaiBiet.Where(x => x.DanhMucId == 2 || x.DanhMucChaId == 4).ToList();

            string a = "";
            foreach (var item in listLinhVucHoatDong)
            {

                a += string.Format($@"
                    <div class=""col-sm-6 col-xl-4 colItem"">
                                <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                    <div class=""wrapImg""> <a class=""wrapImgResize img16And9"" href=""BaiVietPublish?id={item.Id}"" title=""{item.TieuDe}""><img src=""/Administration/UploadImage/{item.ThumbnailUrl}"" alt=""{item.TieuDe}"" /></a></div>
                                    <div class=""wrapOverTitle"">
                                        <h4 class=""wrapTitleItem""><a class=""titlItemMain"" href=""BaiVietPublish?id={item.Id}"" title=""{item.TieuDe}"">{item.TieuDe}</a></h4>
                                    </div>
                                </div>
                            </div>
                    ");
            }

            ltrLinhVucHoatDong.Text = string.Format($@"
                    <!-- activity-->
                    <div class=""wrapActivity bgColor1 wrapContent1"">
                        <div class=""container-xxl containerItem"">
                            <div class=""contentItem"">
                                <div class=""wrapTitle title1 center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                    <h2 class=""titleSub"">Lĩnh vực hoạt động</h2>

                                    <h3 class=""titleMain"">TẬP ĐOÀN HOÀN CẦU KHU VỰC KHÁNH HÒA</h3>
                                </div>
                                <div class=""showList"">
                                    <div class=""row rowList justify-content-center"">

                                            {a}

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end activity-->
                ");

        }

        private void BindDuAnTieuBieu()
        {
            List<BaiVietDto> listLinhVucHoatDong = listBaiBiet.Where(x => x.DanhMucId == 4 || x.DanhMucChaId == 4).ToList();

            string a = "";
            foreach (var item in listLinhVucHoatDong)
            {

                a += string.Format($@"
                   <div class=""itemSlide"">
                                <div class=""contentItem wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                    <div class=""wrapImg""><a class=""wrapImgResize img16And9"" href=""DuAnTieuBieuPublish?id={item.Id}"" title=""{item.TieuDe}""><img src=""/Administration/UploadImage/{item.ThumbnailUrl}"" alt=""{item.TieuDe}"" /></a></div>
                                    <div class=""wrapTextItem"">
                                        <h3 class=""wrapTitle""><a class=""linkTitle"" href=""DuAnTieuBieuPublish?id={item.Id}"" title=""{item.TieuDe}"">{item.TieuDe}</a></h3>
                                        <div class=""wrapDes"">{Regex.Replace(item.MoTaNgan, "<.*?>", string.Empty)}</div>
                                    </div>
                                </div>
                            </div>
                    ");
            }

            ltrDuAnTieuBieu.Text = string.Format($@"
                    <div class=""wrapProject bgColor2 wrapContent1"">
                        <div class=""contentItem"">
                            <div class=""wrapTitle title1 center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                <h2 class=""titleSub"">Dự án TIÊU BIỂU</h2>

                                <h3 class=""titleMain"">TẬP ĐOÀN HOÀN CẦU KHU VỰC KHÁNH HÒA</h3>
                            </div>
                            <div class=""wrapSlide"">
                                <div class=""contentSlide"">
                                    <div class=""showSlideProject slideDotsMb"">

                                            {a}

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end activity-->
                ");

        }

        private void BindCongTyThanhVien()
        {
            List<BaiVietDto> listLinhVucHoatDong = listBaiBiet.Where(x => x.DanhMucId == 3 || x.DanhMucChaId == 3).ToList();

            string a = "";
            foreach (var item in listLinhVucHoatDong)
            {

                a += string.Format($@"
                   <div class=""itemaSlide"">
                                <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                    <div class=""wrapOverImg"">
                                        <div class=""wrapImg""> <a class=""wrapImgResize imgSquare"" href=""BaiVietBigPublish?id={item.Id}"" title=""{item.TieuDe}""><img src=""/Administration/UploadImage/{item.ThumbnailUrl}"" alt=""{item.TieuDe}"" /></a></div>
                                    </div>
                                    <div class=""wrapOverTitle"">
                                        <h4 class=""wrapTitleItem""><a class=""titlItemMain"" href=""BaiVietBigPublish?id={item.Id}"" title=""{item.TieuDe}"">{item.TieuDe}</a></h4>
                                    </div>
                                </div>
                            </div>
                    ");
            }

            ltrCongTyThanhVien.Text = string.Format($@"
                    <div class=""wrapActivity bgColor1 wrapTeam wrapContent1"">
                        <div class=""container-xxl containerItem"">
                            <div class=""contentItem"">
                                <div class=""wrapTitle title1 center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                    <h2 class=""titleSub"">CÔNG TY THÀNH VIÊN</h2>

                                    <h3 class=""titleMain"">TẬP ĐOÀN HOÀN CẦU KHU VỰC KHÁNH HÒA</h3>
                                </div>
                                <div class=""wrapSlide"">
                                    <div class=""showSlideTeam slideDotsMb"">

                                            {a}

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end activity-->
                ");

        }

      
        private void BindDoiTac()
        {
            var doiTacs = DoiTacBLL.GetAll();

            string a = "";
            foreach (var item in doiTacs)
            {

                a += string.Format($@"
                   <div class=""itemSlide"">
                                <div class=""contentSlide wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                    <div class=""wrapImg""><a class=""wrapImgResize img16And9"" href=""{item.LienKetUrl}"" target=""_blank"" title=""{item.Ten}""><img src=""/Administration/UploadImage/{item.HinhAnhUrl}"" alt=""{item.Ten}""></a></div>
                                </div>
                            </div>
                    ");
            }

            ltrDoiTac.Text = string.Format($@"
                <div class=""wrapPartner wrapContent4 bgColor1"">
                    <div class=""container-xxl containerItem"">
                        <div class=""contentItem"">
                            <div class=""wrapTitle title1 center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                <h2 class=""titleSub"">KHÁCH HÀNG</h2>

                                <h3 class=""titleMain"">ĐỐI TÁC NỔI BẬT</h3>
                            </div>
                            <div class=""wrapSlideItem"">
                                <div class=""showSlidePartner slideDotsMb"">'
                                      {a}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end activity-->
                ");

        }

        private void BindCoTheBanThich()
        {
            List<BaiVietDto> listBanCoTheThich = listBaiBiet.Where(x => x.DanhMucId == 1013 || x.DanhMucChaId == 1013).Take(3).ToList();
            DanhSachBaiViet.GetAllPost(listBanCoTheThich);
        }

        #endregion

        #region Code cứng
        private void BindGioiThieu()
        {
            ltrAbout.Text = string.Format(@"
                                <!-- about-->
                            <div class=""wrapAbout bgColor1"">
                                <div class=""container-xxl containerItem"">
                                    <div class=""contentItem wrapContent1"">
                                        <div class=""wrapTitle title1 center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                            <h2 class=""titleSub"">Giới thiệu</h2>

                                            <h3 class=""titleMain"">Tập đoàn Đại Học Thái Bình Dương Khu vực Khánh Hòa</h3>
                                        </div>
                                        <div class=""row rowItem"">
                                            <div class=""col-lg-5 colImgItem"">
                                                <div class=""contentCol wow fadeInLeft"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                                    <div class=""wrapImgAnimation""><img src=""/Assets/images/about/about-bg.png"" alt=""Animation""></div>
                                                    <div class=""wrapImgItem""><img class=""imgContent"" src=""/Assets/images/about/roadmap-hoan-cau-1.png"" alt=""ĐẠI HỌC THÁI BÌNH DƯƠNG""></div>
                                                </div>
                                            </div>
                                            <div class=""col-lg-7 colTextItem"">
                                                <div class=""contentCol"">
                                                    <div class=""wrapTextItem wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                                        <p><b>Tập đoàn Đại Học Thái Bình Dương Khu vực Khánh Hòa (Group HC – KH)</b> ngày nay gồm các Công ty thành viên của Tập đoàn Đại Học Thái Bình Dương Khu vực Khánh Hòa được tách và thành lập từ Công ty TNHH Đại Học Thái Bình Dương thành lập năm 1993, do Bà Trần Thị Hường làm Chủ tịch Hội đồng Thành viên. Việc quản lý và điều hành theo mô hình Group HC - KH là chủ trương của Tập đoàn Đại Học Thái Bình Dương tập trung xây dựng và phát triển các dự án chất lượng cao tại Khu vực Khánh Hòa.</p>
                                                        <p><b style=""color:#ffcb0c;"">Với lịch sử hơn 30 năm xây dựng và phát triển,</b> Tập đoàn Đại Học Thái Bình Dương nói chung và Group HC-KH đã có những bước phát triển mạnh mẽ, trở thành một trong những Tập đoàn hàng đầu đáng tin cậy trên thị trường Việt Nam và cả Thị trường Quốc tế về các dự án đầu tư. Tập đoàn Đại Học Thái Bình Dương luôn kiên định với chiến lược phát triển của mình Đầu tư, cung cấp các sản phẩm dịch vụ uy tín, khơi dậy tiềm năng cho sự phát triển, tạo nên giá trị gia tăng cho xã hội.</p>
                                                        <div class=""wrapBoss"">
                                                            <div class=""media"">
                                                                <div class=""wrapImg"">
                                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/about/boss.jpg"" alt=""Bà TRẦN THỊ HƯỜNG""></div>
                                                                </div>
                                                                <div class=""media-body"">
                                                                    <h3 class=""titleName"">Bà TRẦN THỊ HƯỜNG</h3>
                                                                    <h4 class=""titlePostion"">NGƯỜI SÁNG LẬP</h4>
                                                                    <div class=""wrapTextItem"">Công ty TNHH Đại Học Thái Bình Dương được thành lập ngày 01/02/1993 với vốn điều lệ 193 tỷ đồng, do Bà Trần Thị Hường làm Chủ tịch Hội đồng Thành viên.</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end about-->
                        ");
        }
        private void BindVersionTamNhin()
        {
            ltrVersionTamTim.Text = string.Format(@"
                             <!-- vision-->
                             <div class=""wrapVision"" style=""background-image:url(/Assets/images/slide-main/1.jpg)"">
                                 <div class=""bgItem"">
                                     <div class=""container-xxl containerItem"">
                                         <div class=""contentItem wrapContent1"">
                                             <div class=""wrapTitle title1 white center wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                                 <h2 class=""titleSub"">Khám Phá</h2>

                                                 <h3 class=""titleMain"">Tầm nhìn - Sứ mệnh - Giá trị</h3>
                                             </div>
                                             <div class=""row rowList"">
                                                 <div class=""col-lg-12 colText"">
                                                     <div class=""wrapTextItem wow fadeInUp"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                                         <p>Với định hướng, mục tiêu rõ ràng trong hoạt động, kết hợp cùng nền tảng tài chính vững chắc và nguồn nhân lực chất lượng cao, Đại Học Thái Bình Dương đã và đang không ngừng tiến lên trong nhiều lĩnh vực. Có được thành quả đó chính là nhờ “chữ tín” và sự đầu tư đúng hướng, hiệu quả mà Đại Học Thái Bình Dương đã mang đến cho Quý Khách hàng. Ở mỗi lĩnh vực, Đại Học Thái Bình Dương đều đem đến những sản phẩm tốt nhất, đúng tiến độ nhất và giá cả cạnh tranh nhất.</p>
                                                         <p>Để đạt được “Giá trị cốt lõi”, công tác phát triển nguồn nhân lực của Tập đoàn Đại Học Thái Bình Dương Khu vực Khánh Hòa được quan tâm và đầu tư với nền tảng trọng tâm là tìm kiếm và phát triển những nhân sự có các tố chất và ưu điểm nổi bật: “Năng động – Sáng tạo – Nhiệt huyết”!.</p>
                                                     </div>
                                                 </div>
                                                 <div class=""col-lg-12 colList"">
                                                     <div class=""showList"">
                                                         <div class=""row rowList justify-content-center"">
                                                             <div class=""col-sm-6 col-xl-4 colItem"">
                                                                 <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                                                     <div class=""media"">
                                                                         <div class=""wrapImg"">
                                                                             <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/chart-line.svg"" alt=""Tầm nhìn"" /></div>
                                                                         </div>
                                                                         <div class=""media-body"">
                                                                             <h4 class=""titlItemMain"">Tầm nhìn</h4>
                                                                             <div class=""wrapTextItem"">Tập đoàn Đại Học Thái Bình Dương Khu vực Khánh Hòa Đầu tư, cung cấp các sản phẩm dịch vụ uy tín, khơi dậy tiềm năng cho sự phát triển, tạo nên giá trị gia tăng cho xã hội.</div>
                                                                         </div>
                                                                     </div>
                                                                 </div>
                                                             </div>
                                                             <div class=""col-sm-6 col-xl-4 colItem"">
                                                                 <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                                                     <div class=""media"">
                                                                         <div class=""wrapImg"">
                                                                             <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/ranking-star.svg"" alt=""Sứ mệnh"" /></div>
                                                                         </div>
                                                                         <div class=""media-body"">
                                                                             <h4 class=""titlItemMain"">Sứ mệnh</h4>
                                                                             <div class=""wrapTextItem"">Trở thành đơn vị chủ lực của Tập đoàn Đại Học Thái Bình Dương đầu tư đa ngành hàng đầu tại Khu vực Khánh Hòa, Việt Nam, thể hiện tầm vóc, trí tuệ và niềm tự hào Việt Nam trên trường quốc tế</div>
                                                                         </div>
                                                                     </div>
                                                                 </div>
                                                             </div>
                                                             <div class=""col-sm-6 col-xl-4 colItem"">
                                                                 <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.6s"">
                                                                     <div class=""media"">
                                                                         <div class=""wrapImg"">
                                                                             <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/star.svg"" alt=""Giá trị cốt lõi"" /></div>
                                                                         </div>
                                                                         <div class=""media-body"">
                                                                             <h4 class=""titlItemMain"">Giá trị cốt lõi</h4>
                                                                             <div class=""wrapTextItem"">“UY TÍN-CHẤT LƯỢNG-HIỆU QUẢ” trong tất cả các công trình, các sản phẩm và dịch vụ.</div>
                                                                         </div>
                                                                     </div>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                     </div>
                                                 </div>
                                             </div>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <!-- end vision-->

            ");
        }
        private void BindVersionQuyMo()
        {
            ltrVersionQuyMo.Text = string.Format(@"
                     <!-- vision-->
                    <div class=""wrapCount"" style=""background-image:url(/Assets/images/slide-main/2.jpg)"">
                        <div class=""bgItem"">
                            <div class=""container-xxl containerItem"">
                                <div class=""contentItem wrapContent1"">
                                    <div class=""row rowList justify-content-center row-cols-5"">
                                        <div class=""col-6 col-sm colItem"">
                                            <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.2s"">
                                                <div class=""wrapImg"">
                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/start.svg"" alt=""Năm thành lập"" /></div>
                                                </div>
                                                <h4 class=""titlNumber"">1993</h4>
                                                <div class=""wrapTextItem"">Năm thành lập</div>
                                            </div>
                                        </div>
                                        <div class=""col-6 col-sm colItem"">
                                            <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.4s"">
                                                <div class=""wrapImg"">
                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/dollar.svg"" alt=""Lĩnh vực hoạt động"" /></div>
                                                </div>
                                                <h4 class=""titlNumber"">5</h4>
                                                <div class=""wrapTextItem"">Lĩnh vực hoạt động</div>
                                            </div>
                                        </div>
                                        <div class=""col-6 col-sm colItem"">
                                            <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.6s"">
                                                <div class=""wrapImg"">
                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/team.svg"" alt=""Công ty thành viên"" /></div>
                                                </div>
                                                <h4 class=""titlNumber"">9</h4>
                                                <div class=""wrapTextItem"">Công ty thành viên</div>
                                            </div>
                                        </div>
                                        <div class=""col-6 col-sm colItem"">
                                            <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""0.8s"">
                                                <div class=""wrapImg"">
                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/hand.svg"" alt=""Đối tác chiến lược"" /></div>
                                                </div>
                                                <h4 class=""titlNumber"">5</h4>
                                                <div class=""wrapTextItem"">Đối tác chiến lược</div>
                                            </div>
                                        </div>
                                        <div class=""col-6 col-sm colItem"">
                                            <div class=""itemList wow zoomIn"" data-wow-duration=""1s"" data-wow-delay=""1s"">
                                                <div class=""wrapImg"">
                                                    <div class=""wrapImgResize imgSquare""><img src=""/Assets/images/icons/users.svg"" alt=""Cán bộ nhân viên"" /></div>
                                                </div>
                                                <h4 class=""titlNumber"">1000</h4>
                                                <div class=""wrapTextItem"">Cán bộ nhân viên</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end vision-->   

                ");
        }

        #endregion
    }
}