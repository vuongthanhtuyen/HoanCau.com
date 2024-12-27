<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileDinhKemControl.ascx.cs" Inherits="CMS.WebUI.Controls.ControlContentPage.FileDinhKemControl" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>

<uc1:SlideTop runat="server" ID="SlideTop" />
<!-- partner-->
<div class="wrapEvent bgColor2 wrapContent3 wrapPartner">
    <div class="container containerItem">
        <div class="contentItem">
            <div class="listItem">
                <div class="row rowList justify-content-center row-cols-1 row-cols-sm-2">
                    <asp:Literal runat="server" ID="ltrFiledinhKem" EnableViewState="false"></asp:Literal>

                    <div runat="server" visible="false" enableviewstate="false" id="templateDanhMuc">
                        <div class="col-md-12">
                            <div class="card card-outline {3}">
                                <div class="card-header">
                                    <h3 class="card-title"><strong>{0}</strong></h3>
                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="jzadnw">
                                            <i class="{5}"></i>
                                        </button>
                                    </div>
                                    <!-- /.card-tools -->
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body" style="display: {4};">
                                    <ul class="nav flex-column col-md-12">
                                        {1}
                                    </ul>
                                    {2}
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>


                </div>
            </div>

        </div>
    </div>
</div>
<!-- end partner-->

<div runat="server" visible="false" enableviewstate="false" id="templateItemfile">
    <li class="nav-item">
        <a href="{1}" class="nav-link">{0} 
            <span class="float-right">
                <svg width="21" height="24" viewBox="0 0 21 21" fill="#00bcef;" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" clip-rule="evenodd" d="M3 5C3 2.79086 4.79086 1 7 1H15.3431C16.404 1 17.4214 1.42143 18.1716 2.17157L19.8284 3.82843C20.5786 4.57857 21 5.59599 21 6.65685V19C21 21.2091 19.2091 23 17 23H7C4.79086 23 3 21.2091 3 19V5ZM19 8V19C19 20.1046 18.1046 21 17 21H7C5.89543 21 5 20.1046 5 19V5C5 3.89543 5.89543 3 7 3H14V5C14 6.65685 15.3431 8 17 8H19ZM18.8891 6C18.7909 5.7176 18.6296 5.45808 18.4142 5.24264L16.7574 3.58579C16.5419 3.37035 16.2824 3.20914 16 3.11094V5C16 5.55228 16.4477 6 17 6H18.8891Z" fill="currentColor"></path>
                    <path d="M9 12C8.44771 12 8 12.4477 8 13C8 13.5523 8.44771 14 9 14H15C15.5523 14 16 13.5523 16 13C16 12.4477 15.5523 12 15 12H9Z" fill="currentColor"></path><path d="M9 16C8.44771 16 8 16.4477 8 17C8 17.5523 8.44771 18 9 18H12C12.5523 18 13 17.5523 13 17C13 16.4477 12.5523 16 12 16H9Z" fill="currentColor"></path>
                </svg></span>
        </a>
    </li>
</div>
