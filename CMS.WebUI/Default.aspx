<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMS.WebUI._Default" %>

<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>




<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/home.css?v=f81a959662efae2fc3cc158351e6d90c" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js?v=f81a959662efae2fc3cc158351e6d90c#xfbml=1&version=v16.0" nonce="spknZUtO"></script>

    <!-- end breadcrumb-->
    <div class="wrapSlideMain">
        <div class="contentSlideMain">
            <div class="showSlideMain slideDotsMb">
                <asp:Literal ID="ltrShowSlide" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    <!-- about-->

    <asp:Literal runat="server" ID="ltrGioiThieu" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateAbout">
        <div class="wrapAbout bgColor1">
            <div class="container-xxl containerItem">
                <div class="contentItem wrapContent1">
                    <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                        <h2 class="titleSub">Giới thiệu</h2>

                        <h3 class="titleMain">{0}</h3>
                    </div>
                    <div class="row rowItem">
                        <div class="col-lg-5 colImgItem">
                            <div class="contentCol wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.2s">
                                <div class="wrapImgAnimation">
                                    <img src="/Assets/images/about/about-bg.png" alt="Animation">
                                </div>
                                <div class="wrapImgItem">
                                    <img class="imgContent" src="{2}" alt="{0}">
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-7 colTextItem">
                            <div class="contentCol">
                                <div class="wrapTextItem wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.4s">
                                    {1}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end about-->

    <!-- vision-->
    <asp:Literal runat="server" ID="ltrKhamPha" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateKhamPha">
        <div class="wrapVision" style="background-image: url({5})">
            <div class="bgItem">
                <div class="container-xxl containerItem">
                    <div class="contentItem wrapContent1">
                        <div class="wrapTitle title1 white center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                            <h2 class="titleSub">Khám Phá</h2>

                            <h3 class="titleMain">{0}</h3>
                        </div>
                        <div class="row rowList">
                            <div class="col-lg-12 colText">
                                <div class="wrapTextItem wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.4s">
                                    {1}
                                </div>
                            </div>
                            <div class="col-lg-12 colList">
                                <div class="showList">
                                    <div class="row rowList justify-content-center">
                                        <div class="col-sm-6 col-xl-4 colItem">
                                            <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.2s">
                                                <div class="media">
                                                    <div class="wrapImg">
                                                        <div class="wrapImgResize imgSquare">
                                                            <img src="/Assets/images/icons/chart-line.svg" alt="Tầm nhìn" />
                                                        </div>
                                                    </div>
                                                    <div class="media-body">
                                                        <h4 class="titlItemMain">Tầm nhìn</h4>
                                                        <div class="wrapTextItem">{2}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xl-4 colItem">
                                            <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                                                <div class="media">
                                                    <div class="wrapImg">
                                                        <div class="wrapImgResize imgSquare">
                                                            <img src="/Assets/images/icons/ranking-star.svg" alt="Sứ mệnh" />
                                                        </div>
                                                    </div>
                                                    <div class="media-body">
                                                        <h4 class="titlItemMain">Sứ mệnh</h4>
                                                        <div class="wrapTextItem">{3}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xl-4 colItem">
                                            <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.6s">
                                                <div class="media">
                                                    <div class="wrapImg">
                                                        <div class="wrapImgResize imgSquare">
                                                            <img src="/Assets/images/icons/star.svg" alt="Giá trị cốt lõi" />
                                                        </div>
                                                    </div>
                                                    <div class="media-body">
                                                        <h4 class="titlItemMain">Giá trị cốt lõi</h4>
                                                        <div class="wrapTextItem">{4}</div>
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
    </div>
    <!-- end vision-->
    <!-- activity-->
    <asp:Literal runat="server" ID="ltlrNganhDaoTao" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateNganhDaoTao">
        <div class="wrapActivity bgColor1 wrapContent1">
            <div class="container-xxl containerItem">
                <div class="contentItem">
                    <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                        <h2 class="titleSub">Ngành đào tạo</h2>

                        <h3 class="titleMain">{0}</h3>
                    </div>
                    <div class="showList">
                        <div class="row rowList justify-content-center">
                            {1}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end activity-->


    <div runat="server" visible="false" enableviewstate="false" id="templateItemNganhDaoTao">
        <div class="col-sm-6 col-xl-4 colItem">
            <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                <div class="wrapImg">
                    <a class="wrapImgResize img16And9" href="{2}" title="{0}">
                        <img src="{1}" alt="{0}" /></a>
                </div>
                <div class="wrapOverTitle">
                    <h4 class="wrapTitleItem"><a class="titlItemMain" href="{2}" title="{0}">{0}</a></h4>
                </div>
            </div>
        </div>
    </div>



    <!-- Quy mô-->
    <asp:Literal runat="server" ID="ltrQuyMo" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateQuyMo">
        <div class="wrapCount" style="background-image: url({10})">
            <div class="bgItem">
                <div class="container-xxl containerItem">
                    <div class="contentItem wrapContent1">
                        <div class="row rowList justify-content-center row-cols-5">
                            <div class="col-6 col-sm colItem">
                                <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.2s">
                                    <div class="wrapImg">
                                        <div class="wrapImgResize imgSquare">
                                            <img src="/Assets/images/icons/start.svg" alt="{0}" />
                                        </div>
                                    </div>
                                    <h4 class="titlNumber">{1}</h4>
                                    <div class="wrapTextItem">{0}</div>
                                </div>
                            </div>
                            <div class="col-6 col-sm colItem">
                                <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                                    <div class="wrapImg">
                                        <div class="wrapImgResize imgSquare">
                                            <img src="/Assets/images/icons/dollar.svg" alt="{2}" />
                                        </div>
                                    </div>
                                    <h4 class="titlNumber">{3}</h4>
                                    <div class="wrapTextItem">{2}</div>
                                </div>
                            </div>
                            <div class="col-6 col-sm colItem">
                                <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.6s">
                                    <div class="wrapImg">
                                        <div class="wrapImgResize imgSquare">
                                            <img src="/Assets/images/icons/team.svg" alt="{4}" />
                                        </div>
                                    </div>
                                    <h4 class="titlNumber">{5}</h4>
                                    <div class="wrapTextItem">{4}</div>
                                </div>
                            </div>
                            <div class="col-6 col-sm colItem">
                                <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.8s">
                                    <div class="wrapImg">
                                        <div class="wrapImgResize imgSquare">
                                            <img src="/Assets/images/icons/hand.svg" alt="{6}" />
                                        </div>
                                    </div>
                                    <h4 class="titlNumber">{7}</h4>
                                    <div class="wrapTextItem">{6}</div>
                                </div>
                            </div>
                            <div class="col-6 col-sm colItem">
                                <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="1s">
                                    <div class="wrapImg">
                                        <div class="wrapImgResize imgSquare">
                                            <img src="/Assets/images/icons/users.svg" alt="{8}" />
                                        </div>
                                    </div>
                                    <h4 class="titlNumber">{9}</h4>
                                    <div class="wrapTextItem">{8}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end vision-->

    <%-- Sự Kiện --%>
    <asp:Literal runat="server" ID="ltrSuKien" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateSuKien">
        <div class="wrapProject bgColor2 wrapContent1">
            <div class="contentItem">
                <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                    <h2 class="titleSub">SỰ KIỆN NỔI BẬT</h2>

                    <h3 class="titleMain">{0}</h3>
                </div>
                <div class="wrapSlide">
                    <div class="contentSlide">
                        <div class="showSlideProject slideDotsMb">
                            {1}

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" visible="false" enableviewstate="false" id="templateItemSuKien">
        <div class="itemSlide">
            <div class="contentItem wow zoomIn">
                <div class="wrapImg">
                    <a class="wrapImgResize img16And9" href="{2}" title="{0}">
                        <img src="{1}" alt="{0}" /></a>
                </div>
                <div class="wrapTextItem">
                    <h3 class="wrapTitle"><a class="linkTitle" href="{2}" title="{0}">{0}</a></h3>
                    <div class="wrapDes">{3}</div>
                </div>
            </div>
        </div>
    </div>
    <%-- End sự kiện --%>

    <%-- Thành viên --%>
    <asp:Literal runat="server" ID="ltrThanhVien" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateThanhVien">
        <div class="wrapActivity bgColor1 wrapTeam wrapContent1">
            <div class="container-xxl containerItem">
                <div class="contentItem">
                    <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                        <h2 class="titleSub">Thành viên</h2>

                        <h3 class="titleMain">{0}</h3>
                    </div>
                    <div class="wrapSlide">
                        <div class="showSlideTeam slideDotsMb">
                            {1}

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" visible="false" enableviewstate="false" id="templateItemThanhVien">
        <div class="itemaSlide">
            <div class="itemList wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                <div class="wrapOverImg">
                    <div class="wrapImg">
                        <a class="wrapImgResize imgSquare" href="{2}" title="{0}">
                            <img src="{1}" alt="{0}" /></a>
                    </div>
                </div>
                <div class="wrapOverTitle">
                    <h4 class="wrapTitleItem"><a class="titlItemMain" href="{2}" title="{0}">{0}</a></h4>
                </div>
            </div>
        </div>
    </div>
    <%-- End thành viên --%>

    <!-- Tin tức-->
    <asp:Literal runat="server" ID="ltrTinTuc" EnableViewState="false"></asp:Literal>
    <div runat="server" visible="false" enableviewstate="false" id="templateTinTuc">
        <div class="wrapNews wrapContent1 bgColor2">
            <div class="container-xxl containerItem">
                <div class="contentItem">
                    <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                        <h2 class="titleSub">Tin tức</h2>

                        <h3 class="titleMain">{0}</h3>
                    </div>
                    <div class="row rowList justify-content-center">
                        {1}
                    
                    
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" visible="false" enableviewstate="false" id="templateItemTinTuc">
        <div class="col-sm-6 col-lg-4 colItem">
            <div class="contentCol wow zoomIn" data-wow-duration="1s" data-wow-delay="0.4s">
                <div class="contentImg">
                    <div class="wrapImg">
                        <a class="wrapImgResize img16And9" href="{2}" title="{0}">
                            <img src="{1}" alt="{0}" /></a>
                    </div>

                    <div class="time">
                        <div class="contentItem">
                            <p class="txtDate">{4}</p>
                            <p class="txtMonthYear">{5}</p>
                        </div>
                    </div>
                </div>

                <div class="wrapText">
                    <h4 class="titleMain"><a class="title5 linkTitle" href="{2}" title="{0}">{0}</a></h4>
                    <div class="wrapInfo clearfix">
                        <p class="author">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                <path fill="currentColor" d="M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z"></path>
                            </svg>TRƯỜNG ĐẠI HỌC THÁI BÌNH DƯƠNG
                        </p>
                    </div>
                    <p class="wrapSummary">{3}</p>
                </div>
            </div>
        </div>
    </div>
    <!-- end tin tức-->

    <asp:Literal ID="ltrDoiTac" runat="server"></asp:Literal>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script src="/Assets/js/home.js?v=f81a959662efae2fc3cc158351e6d90c"></script>

</asp:Content>
