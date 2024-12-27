<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DanhSachThanhVien.ascx.cs" Inherits="CMS.WebUI.Controls.DanhSachThanhVien" %>

<style>
    .min-he {
        min-height: 75px;
    }

    .callout.callout-info {
        border-left-color: #ed1a3b;
            padding: 10px;
    }

    .callout {
        padding: 5px;
        margin: 20px -14px;
    }

    .wrapTeam .wrapSlide .colTeam {
        margin-top: 10px;
    }

   
    .wrapContent4 {
    padding: 3px 0;
}
</style>
<!-- activity-->
<div class="wrapActivity bgColor2 wrapTeam wrapContent4 danhsachthanhvien">
    <div class="container containerItem">
        <div class="contentItem">
            <asp:Literal runat="server" ID="ltrMain" EnableViewState="false"></asp:Literal>
            <div runat="server" visible="false" enableviewstate="false" id="templateList">
                <div class="callout callout-info">
                    <h2 style="display: inline;">{0}</h2>
                </div>
                <div class="wrapSlide">
                    <div class="justify-content-center row rowTeam row-cols-1 row-cols-xl-4 row-cols-sm-2">
                        {1}
                    </div>
                </div>
            </div>

            <div runat="server" visible="false" enableviewstate="false" id="templateSingleItem">
                <div class="col colTeam">
                    <div class="itemaSlide">
                        <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                            <div class="wrapOverImg">
                                <div class="wrapImg">
                                    <a class="wrapImgResize imgSquare" href="{2}" title="{0}">
                                        <img src="{1}" alt="{0}" /></a>
                                </div>
                            </div>
                            <div class="wrapOverTitle min-he">
                                <h4 class="wrapTitleItem"><a class="titlItemMain" href="{2}" title="{0}">{0}</a>
                                    <div class="titlItemMain">{3}</div>

                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="otherPost">
                <div class="titleNewsMain">Có thể bạn thích</div>
                <div class="wrapNews">
                    <div class="row rowList justify-content-center">
                        <div class="col-sm-6 col-lg-4 colItem">
                            <div class="contentCol wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                                <div class="contentImg">
                                    <div class="wrapImg">
                                        <a class="wrapImgResize img16And9" href="lich-su-phat-trien.html" title="Lịch sử phát triển">
                                            <img src="/Assets/images/slide-main/2.jpg" alt="Lịch sử phát triển" /></a>
                                    </div>

                                    <div class="time">
                                        <div class="contentItem">
                                            <p class="txtDate">07</p>
                                            <p class="txtMonthYear">05/2022</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="wrapText">
                                    <h4 class="titleMain"><a class="title5 linkTitle" href="lich-su-phat-trien.html" title="Lịch sử phát triển">Lịch sử phát triển</a></h4>
                                    <div class="wrapInfo clearfix">
                                        <p class="author">
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                <path fill="currentColor" d="M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z"></path>
                                            </svg>TẬP ĐOÀN HOÀN CẦU
                                           
                                        </p>
                                    </div>
                                    <p class="wrapSummary">Tập đoàn Hoàn Cầu Khu vực Khánh Hòa (Group HC – KH) ngày nay gồm các Công ty thành viên của Tập đoàn Hoàn Cầu Khu vực Khánh Hòa được tách và thành lập từ Công ty TNHH Hoàn Cầu thành lập năm 1993, do Bà Trần Thị Hường làm Chủ tịch Hội đồng Thành viên. Việc quản lý và điều hành theo mô hình Group HC - KH là chủ trương của Tập đoàn Hoàn Cầu tập trung xây dựng và phát triển các dự án chất lượng cao tại Khu vực Khánh Hòa.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-lg-4 colItem">
                            <div class="contentCol wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                                <div class="contentImg">
                                    <div class="wrapImg">
                                        <a class="wrapImgResize img16And9" href="so-do-to-chuc.html" title="Sơ đồ tổ chức">
                                            <img src="/Assets/images/slide-main/3.jpg" alt="Sơ đồ tổ chức" /></a>
                                    </div>

                                    <div class="time">
                                        <div class="contentItem">
                                            <p class="txtDate">07</p>
                                            <p class="txtMonthYear">05/2022</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="wrapText">
                                    <h4 class="titleMain"><a class="title5 linkTitle" href="so-do-to-chuc.html" title="Sơ đồ tổ chức">Sơ đồ tổ chức</a></h4>
                                    <div class="wrapInfo clearfix">
                                        <p class="author">
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                <path fill="currentColor" d="M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z"></path>
                                            </svg>TẬP ĐOÀN HOÀN CẦU
                                           
                                        </p>
                                    </div>
                                    <p class="wrapSummary">Tập đoàn Hoàn Cầu Khu vực Khánh Hòa (Group HC – KH) ngày nay gồm các Công ty thành viên của Tập đoàn Hoàn Cầu Khu vực Khánh Hòa được tách và thành lập từ Công ty TNHH Hoàn Cầu thành lập năm 1993, do Bà Trần Thị Hường làm Chủ tịch Hội đồng Thành viên. Việc quản lý và điều hành theo mô hình Group HC - KH là chủ trương của Tập đoàn Hoàn Cầu tập trung xây dựng và phát triển các dự án chất lượng cao tại Khu vực Khánh Hòa.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-lg-4 colItem">
                            <div class="contentCol wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                                <div class="contentImg">
                                    <div class="wrapImg">
                                        <a class="wrapImgResize img16And9" href="ban-lanh-dao.html" title="Ban lãnh đạo">
                                            <img src="/Assets/images/slide-main/3.jpg" alt="Ban lãnh đạo" /></a>
                                    </div>

                                    <div class="time">
                                        <div class="contentItem">
                                            <p class="txtDate">07</p>
                                            <p class="txtMonthYear">05/2022</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="wrapText">
                                    <h4 class="titleMain"><a class="title5 linkTitle" href="ban-lanh-dao.html" title="Ban lãnh đạo">Ban lãnh đạo</a></h4>
                                    <div class="wrapInfo clearfix">
                                        <p class="author">
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                <path fill="currentColor" d="M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z"></path>
                                            </svg>TẬP ĐOÀN HOÀN CẦU
                                           
                                        </p>
                                    </div>
                                    <p class="wrapSummary">Tập đoàn Hoàn Cầu Khu vực Khánh Hòa (Group HC – KH) ngày nay gồm các Công ty thành viên của Tập đoàn Hoàn Cầu Khu vực Khánh Hòa được tách và thành lập từ Công ty TNHH Hoàn Cầu thành lập năm 1993, do Bà Trần Thị Hường làm Chủ tịch Hội đồng Thành viên. Việc quản lý và điều hành theo mô hình Group HC - KH là chủ trương của Tập đoàn Hoàn Cầu tập trung xây dựng và phát triển các dự án chất lượng cao tại Khu vực Khánh Hòa.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- end activity-->
