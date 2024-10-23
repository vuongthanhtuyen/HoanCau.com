<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LichSuPhatTrienPublish.aspx.cs" Inherits="CMS.WebUI.LichSuPhatTrienPublish" %>


<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>



<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/history.css?v=f81a959662efae2fc3cc158351e6d90c" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:SlideTop runat="server" ID="SlideTop" />

    <!-- history-->
    <div class="wrapAbout bgColor2 wrapHistory wrapContent4">
        <div class="wrapSlideYear">
            <div class="container-xxl containerItem">
                <div class="contentItem">

                    <div class="showSlideYear">
                        <asp:Literal ID="ltrTopShow" runat="server"></asp:Literal>

                    </div>
                </div>
            </div>
        </div>
        <div class="wrapSlideText">
            <div class="container-xxl containerItem">
                <div class="contentItem">
                    <div class="showSlideHistory">
                        <asp:Literal ID="ltrBelowShow" runat="server"></asp:Literal>

                    </div>
                </div>
            </div>
        </div>





        <div class="otherPost">
            <div class="container-xxl containerItem">
                <div class="contentItem">
                    <div class="titleNewsMain">Có thể bạn thích</div>
                    <div class="wrapNews">
                        <div class="row rowList justify-content-center">

                            <uc1:DanhSachBaiViet runat="server" ID="DanhSachBaiVietCoTheBanThich" />
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end history-->


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script src="/Assets/js/history.js?v=f81a959662efae2fc3cc158351e6d90c"></script>

</asp:Content>
