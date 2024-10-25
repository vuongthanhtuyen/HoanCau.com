<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuAnTieuBieu.ascx.cs" Inherits="CMS.WebUI.Controls.ControlContentPage.DuAnTieuBieu" %>

<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>



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
