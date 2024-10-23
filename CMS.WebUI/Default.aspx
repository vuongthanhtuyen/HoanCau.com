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
    <asp:Literal ID="ltrAbout" runat="server"></asp:Literal>
    <asp:Literal ID="ltrVersionTamTim" runat="server"></asp:Literal>



  


    <asp:Literal ID="ltrLinhVucHoatDong" runat="server"></asp:Literal>


    <asp:Literal ID="ltrVersionQuyMo" runat="server"></asp:Literal>

    <asp:Literal ID="ltrDuAnTieuBieu" runat="server"></asp:Literal>
    <asp:Literal ID="ltrCongTyThanhVien" runat="server"></asp:Literal>








       <!-- news list-->
   <div class="wrapNews wrapContent1 bgColor2">
       <div class="container-xxl containerItem">
           <div class="contentItem">
               <div class="wrapTitle title1 center wow fadeInUp" data-wow-duration="1s" data-wow-delay="0.2s">
                   <h2 class="titleSub">Tin tức</h2>

                   <h3 class="titleMain">Tin tức mới nhất</h3>
               </div>
               <div class="row rowList justify-content-center">

                    <uc1:DanhSachBaiViet runat="server" ID="DanhSachBaiViet" />
                 
               </div>
           </div>
       </div>
   </div>
   <!-- end news list-->

    <asp:Literal ID="ltrDoiTac" runat="server"></asp:Literal>



</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script src="/Assets/js/home.js?v=f81a959662efae2fc3cc158351e6d90c"></script>


</asp:Content>
