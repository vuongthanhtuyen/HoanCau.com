<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaiVietPublish.ascx.cs" Inherits="CMS.WebUI.Controls.ControlContentPage.BaiVietPublish" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>




<script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js?v=f81a959662efae2fc3cc158351e6d90c#xfbml=1&version=v16.0" nonce="spknZUtO"></script>

<uc1:SlideTop runat="server" ID="SlideTop" />
<div class="wrapContent4 bgColor2 pageNewsDetail">
    <div class="container-xxl containerItem">
        <div class="contentItem">
            <div class="row rowItem">
                <div class="col-lg-8 colText">
                    <div class="contentItem">
                        <asp:Literal ID="ltlPostView" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="col-lg-4 colRight">
                    <div class="contentItem">
                        <div class="titleNewsMain">Tin Tức</div>
                        <div class="wrapNews">
                            <div class="row rowList justify-content-center">

                                <uc1:DanhSachBaiViet runat="server" ID="DanhSachBaiVietLienQuan" />

                                <%-- Tin tức --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="otherPost">
            <div class="titleNewsMain">Có thể bạn thích</div>
            <div class="wrapNews">
                <div class="row rowList justify-content-center">
                    <uc1:DanhSachBaiViet runat="server" ID="DanhSachBaiVietCoTheBanSeThich" />
                </div>
            </div>
        </div>
    </div>
</div>
<%--</div>--%>


