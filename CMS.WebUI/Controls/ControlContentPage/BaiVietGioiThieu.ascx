<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaiVietGioiThieu.ascx.cs" Inherits="CMS.WebUI.Controls.ControlContentPage.BaiVietGioiThieu" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>

<script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js?v=f81a959662efae2fc3cc158351e6d90c#xfbml=1&version=v16.0" nonce="spknZUtO"></script>


<uc1:SlideTop runat="server" ID="SlideTop" />
<div class="wrapContent4 bgColor2 pageNewsDetail">
    <div class="container-xxl containerItem">
        <div class="contentItem">
            <div class="row rowItem">
                <div class="col-lg-12 colText">
                    <div class="contentItem">
                        <asp:Literal ID="ltlPostView" runat="server"></asp:Literal>
                        <div class="wrapShare">
                            <div class="titleShare">Chia sẻ:</div>
                            <div class="listBtnSharePost">
                                <div class="sharethis-inline-share-buttons"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
