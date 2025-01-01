<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuKienControl.ascx.cs" Inherits="CMS.WebUI.Controls.SuKienControl" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>

<div class="wrapContent4 bgColor2 pageProjectDetail">
    <div class="container-xl containerItem">
        <div class="contentItem">
            <asp:Literal runat="server" ID="ltrMain" EnableViewState="false"></asp:Literal>

            <div runat="server" visible="false" enableviewstate="false" id="templateDanhMuc">
                <div class="row rowItem rowSlideImg">
                    <div class="col-xl-9 colImg">
                        <div class="contentItem">
                            <div class="wrapSlideMainImg">
                                <div class="showSlideMainImg">
                                    {3}
                                </div>
                            </div>
                            <div class="wrapSlideCtrlImg">
                                <div class="showSlideCtrlImg">
                                    {4}

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xl-3 colText">
                        <div class="contentItem">
                            <p class="titleMain titleNewsMain">{0}</p>

                            <div class="wrapInfoText showTextDetail">
                                {1}
                            </div>
                        </div>
                    </div>
                </div>

                <div class="listVideos"></div>

                <div class="wrapContentDetail">
                    <div class="wrapMenuTab">
                        <div class="list-group" id="tabInfoProject" role="tablist"><a class="list-group-item list-group-item-action active" data-toggle="list" href="#project-text" role="tab" title="Thông tin chi tiết">Thông tin chi tiết</a><a class="list-group-item list-group-item-action" data-toggle="list" href="#project-video" role="tab" title="Xem videos">Xem videos</a></div>
                    </div>

                    <div class="tab-content tab1">
                        <div class="tab-pane active" role="tabpanel" id="project-text">
                            <div class="textDescription">
                                <div class="wrapText showTextDetail">
                                    {2}
                                </div>

                                <div class="wrapShare">
                                    <div class="titleShare">Chia sẻ:</div>

                                    <div class="listBtnSharePost">
                                        <div class="sharethis-inline-share-buttons"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" role="tabpanel" id="project-video">
                            <div class="listItemVideos">
                                <div class="rowList showGalleryVideosProject">
                                    {5}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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

<div runat="server" visible="false" enableviewstate="false" id="templateItemSlide">

    <div class="itemSlide" data-src="{1}">
        <div class="contentImg">
            <div class="wrapImgResize img16And9">
                <img src="{1}" alt="{0}">
            </div>
        </div>
    </div>
</div>

<div runat="server" visible="false" enableviewstate="false" id="templateItemSubSlide">

    <div class="itemSlide">
        <div class="contentImg">
            <div class="wrapImgResize img16And9">
                <img src="{1}" alt="{0}">
            </div>
        </div>
    </div>
</div>


<div runat="server" visible="false" enableviewstate="false" id="templateItemVideo">

    <div class="colItem" data-src="{2}">
        <div class="contentCol">
            <div class="wrapImg">
                <div class="wrapImgResize">
                    <img src="{1}" alt="{0}">
                </div>

                <div class="wrapIcon">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                        <path d="M361 215C375.3 223.8 384 239.3 384 256C384 272.7 375.3 288.2 361 296.1L73.03 472.1C58.21 482 39.66 482.4 24.52 473.9C9.377 465.4 0 449.4 0 432V80C0 62.64 9.377 46.63 24.52 38.13C39.66 29.64 58.21 29.99 73.03 39.04L361 215z"></path>
                    </svg>
                </div>
            </div>
        </div>
    </div>
</div>
