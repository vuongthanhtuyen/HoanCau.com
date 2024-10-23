<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DuAnTieuBieuPublish.aspx.cs" Inherits="CMS.WebUI.DuAnTieuBieuPublish" %>

<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/project-detail.css?v=f81a959662efae2fc3cc158351e6d90c" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:SlideTop runat="server" ID="SlideTop" />

    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js?v=f81a959662efae2fc3cc158351e6d90c#xfbml=1&version=v16.0" nonce="spknZUtO"></script>


    <div class="wrapContent4 bgColor2 pageProjectDetail">
        <div class="container-xxl containerItem">
            

            <asp:Literal ID="ltlPostView" runat="server"></asp:Literal>

        </div>
    </div>

     <div class="wrapProject otherPost wrapContent4">
     <div class="titleNewsMain center">Có thể bạn thích</div>
     <div class="wrapSlide">
         <div class="contentSlide">
             <div class="showSlideProject slideDotsMb">
                <asp:Literal ID="ltrCoTheBanThich" runat="server"></asp:Literal>

             </div>
         </div>
     </div>
 </div>
   
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">
    <script src="/Assets/js/project-detail.js?v=f81a959662efae2fc3cc158351e6d90c"></script>
    <!-- end JS-->
    <%--    <script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform" async="async"></script>--%>
</asp:Content>
