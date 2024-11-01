<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LienHePublish.aspx.cs" Inherits="CMS.WebUI.LienHePublish" %>


<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link href="Assets/validation-engine/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Assets/css/contact.css?v=f81a959662efae2fc3cc158351e6d90c" />

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js?v=f81a959662efae2fc3cc158351e6d90c#xfbml=1&version=v16.0" nonce="spknZUtO"></script>
    <uc1:SlideTop runat="server" ID="SlideTop" />
    <div class="wrapContact bgColor2 wrapContent4">
        <div class="container-xxl containerItem">
            <div class="contentItem">
                <div class="wrapMap">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3898.9965661360598!2d109.1850547!3d12.2485113!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31705d7fa1f282d9%3A0x481d3e6afcecb8f0!2zNjYgVGjDoWkgTmd1ecOqbiwgUGjGsOG7m2MgVMOibiwgTmhhIFRyYW5nLCBLaMOhbmggSMOyYSA2NTAwMDA!5e0!3m2!1svi!2s!4v1683885873791!5m2!1svi!2s" width="600" height="450" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
                <div class="infoAndForm">
                    <div class="row rowItem align-items-center">
                        <div class="col-lg-5 colInfo">
                            <div class="contentItem">
                                <div class="listInfo">
                                    <div class="row rowItem">
                                        <div class="col-6 colItem">
                                            <div class="contentItem">
                                                <div class="wrapIcon">
                                                    <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 512 512">
                                                        <path fill="currentColor" d="M164.9 24.6c-7.7-18.6-28-28.5-47.4-23.2l-88 24C12.1 30.2 0 46 0 64C0 311.4 200.6 512 448 512c18 0 33.8-12.1 38.6-29.5l24-88c5.3-19.4-4.6-39.7-23.2-47.4l-96-40c-16.3-6.8-35.2-2.1-46.3 11.6L304.7 368C234.3 334.7 177.3 277.7 144 207.3L193.3 167c13.7-11.2 18.4-30 11.6-46.3l-40-96z"></path>
                                                    </svg>
                                                </div>

                                                <div class="titleItem">Hotline</div>

                                                <div class="wrapText">
                                                    <a href="tel:0961273355" title="096 127 3355">096 127 3355</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 colItem">
                                            <div class="contentItem">
                                                <div class="wrapIcon">
                                                    <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 512 512">
                                                        <path fill="currentColor" d="M48 64C21.5 64 0 85.5 0 112c0 15.1 7.1 29.3 19.2 38.4L236.8 313.6c11.4 8.5 27 8.5 38.4 0L492.8 150.4c12.1-9.1 19.2-23.3 19.2-38.4c0-26.5-21.5-48-48-48H48zM0 176V384c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V176L294.4 339.2c-22.8 17.1-54 17.1-76.8 0L0 176z"></path>
                                                    </svg>
                                                </div>

                                                <div class="titleItem">Email</div>

                                                <div class="wrapText">
                                                    <a href="mailto:info@hoancaugroupkh.com" title="info@hoancaugroupkh.com">info@hoancaugroupkh.com</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 colItem">
                                            <div class="contentItem">
                                                <div class="wrapIcon">
                                                    <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 576 512">
                                                        <path fill="currentColor" d="M224 0H96V32 384v32h32 64 32V384 32 0zM64 32H0V512H576V32H256V416v32H224 96 64V416 32zM320 96H512v96H320V96zm64 160v64H320V256h64zm64 0h64v64H448V256zm64 128v64H448V384h64zm-192 0h64v64H320V384z"></path>
                                                    </svg>
                                                </div>

                                                <div class="titleItem">Điện Thoại</div>

                                                <div class="wrapText">
                                                    <a href="tel:02582245003" title="0258 2245 003">0258 2245 003</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 colItem">
                                            <div class="contentItem">
                                                <div class="wrapIcon">
                                                    <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 384 512">
                                                        <path fill="currentColor" d="M48 0C21.5 0 0 21.5 0 48V464c0 26.5 21.5 48 48 48h96V432c0-26.5 21.5-48 48-48s48 21.5 48 48v80h96c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48H48zM64 240c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H80c-8.8 0-16-7.2-16-16V240zm112-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H176c-8.8 0-16-7.2-16-16V240c0-8.8 7.2-16 16-16zm80 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H272c-8.8 0-16-7.2-16-16V240zM80 96h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H80c-8.8 0-16-7.2-16-16V112c0-8.8 7.2-16 16-16zm80 16c0-8.8 7.2-16 16-16h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H176c-8.8 0-16-7.2-16-16V112zM272 96h32c8.8 0 16 7.2 16 16v32c0 8.8-7.2 16-16 16H272c-8.8 0-16-7.2-16-16V112c0-8.8 7.2-16 16-16z"></path>
                                                    </svg>
                                                </div>

                                                <div class="titleItem">Địa chỉ</div>

                                                <div class="wrapText">
                                                    <a href="https://www.google.com/maps/place/66+Th%C3%A1i+Nguy%C3%AAn,+Ph%C6%B0%E1%BB%9Bc+T%C3%A2n,+Nha+Trang,+Kh%C3%A1nh+H%C3%B2a+650000,+Vi%E1%BB%87t+Nam/@12.2485165,109.1824798,17z/data=!3m1!4b1!4m5!3m4!1s0x31705d7fa1f282d9:0x481d3e6afcecb8f0!8m2!3d12.2485113!4d109.1850547" target="_blank" title="Xem bản đồ">Xem bản đồ</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="col-lg-7 colForm" id="contactForm">
                            <div class="bgItemMain"></div>
                            <div class="contentItem">

                                <asp:UpdatePanel ID="UpdatePanelContact" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="wrapTitleMain type1">
                                            <h1 class="titleMain">Gửi Tin Nhắn</h1>
                                            <asp:Label ID="lblAddErrorMessage" CssClass="text-light" runat="server" Text=""></asp:Label>
                                        </div>


                                        <div class="frmContact">
                                            <div class="row rowFrm">
                                                <div class="col-lg-6 colItem">
                                                    <div class="formContent">
                                                        <div class="inputGroupItem">
                                                            <div class="iconItem">
                                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                                                                    <path fill="currentColor" d="M224 256c70.7 0 128-57.31 128-128s-57.3-128-128-128C153.3 0 96 57.31 96 128S153.3 256 224 256zM274.7 304H173.3C77.61 304 0 381.6 0 477.3c0 19.14 15.52 34.67 34.66 34.67h378.7C432.5 512 448 496.5 448 477.3C448 381.6 370.4 304 274.7 304z" />
                                                                </svg>
                                                            </div>
                                                            <asp:TextBox ID="txtHoVaTen" CssClass="inputItem validate[required]" placeholder="Nhập họ và tên" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="formContent">
                                                        <div class="inputGroupItem">
                                                            <div class="iconItem">
                                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                                    <path fill="currentColor" d="M256 352c-16.53 0-33.06-5.422-47.16-16.41L0 173.2V400C0 426.5 21.49 448 48 448h416c26.51 0 48-21.49 48-48V173.2l-208.8 162.5C289.1 346.6 272.5 352 256 352zM16.29 145.3l212.2 165.1c16.19 12.6 38.87 12.6 55.06 0l212.2-165.1C505.1 137.3 512 125 512 112C512 85.49 490.5 64 464 64h-416C21.49 64 0 85.49 0 112C0 125 6.01 137.3 16.29 145.3z" />
                                                                </svg>
                                                            </div>
                                                            <asp:TextBox ID="txtEmail" CssClass="inputItem validate[required]" placeholder="Nhập email" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="formContent">
                                                        <div class="inputGroupItem">
                                                            <div class="iconItem">
                                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                                    <path fill="currentColor" d="M511.2 387l-23.25 100.8c-3.266 14.25-15.79 24.22-30.46 24.22C205.2 512 0 306.8 0 54.5c0-14.66 9.969-27.2 24.22-30.45l100.8-23.25C139.7-2.602 154.7 5.018 160.8 18.92l46.52 108.5c5.438 12.78 1.77 27.67-8.98 36.45L144.5 207.1c33.98 69.22 90.26 125.5 159.5 159.5l44.08-53.8c8.688-10.78 23.69-14.51 36.47-8.975l108.5 46.51C506.1 357.2 514.6 372.4 511.2 387z" />
                                                                </svg>
                                                            </div>
                                                            <asp:TextBox ID="txtSoDienThoai" CssClass="inputItem " placeholder="Nhập số điện thoại" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 colItem">
                                                    <div class="formContent">
                                                        <div class="inputGroupItem">
                                                            <div class="iconItem">
                                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                                                    <path fill="currentColor" d="M256 0v128h128L256 0zM224 128L224 0H48C21.49 0 0 21.49 0 48v416C0 490.5 21.49 512 48 512h288c26.51 0 48-21.49 48-48V160h-127.1C238.3 160 224 145.7 224 128zM272 416h-160C103.2 416 96 408.8 96 400C96 391.2 103.2 384 112 384h160c8.836 0 16 7.162 16 16C288 408.8 280.8 416 272 416zM272 352h-160C103.2 352 96 344.8 96 336C96 327.2 103.2 320 112 320h160c8.836 0 16 7.162 16 16C288 344.8 280.8 352 272 352zM288 272C288 280.8 280.8 288 272 288h-160C103.2 288 96 280.8 96 272C96 263.2 103.2 256 112 256h160C280.8 256 288 263.2 288 272z" />
                                                                </svg>
                                                            </div>
                                                            <asp:TextBox ID="txtChuDe" CssClass="inputItem " placeholder="Nhập chủ đề" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="formContent">
                                                        <asp:TextBox ID="txtTinNhan" TextMode="MultiLine" CssClass="inputItem validate[required]" placeholder="Nhập tin nhắn" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="wrapBtnSend">
                                                <div class="showBtnSend">
                                                    <a
                                                        class="btn1 btnSend" 
                                                        runat="server"
                                                        id="btnSend" 
                                                        onserverclick="btnSend_ServerClick"
                                                        onclick="return CheckValid();">
                                                        Gửi ngay
                                                         <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                                             <path fill="currentColor" d="M511.6 36.86l-64 415.1c-1.5 9.734-7.375 18.22-15.97 23.05c-4.844 2.719-10.27 4.097-15.68 4.097c-4.188 0-8.319-.8154-12.29-2.472l-122.6-51.1l-50.86 76.29C226.3 508.5 219.8 512 212.8 512C201.3 512 192 502.7 192 491.2v-96.18c0-7.115 2.372-14.03 6.742-19.64L416 96l-293.7 264.3L19.69 317.5C8.438 312.8 .8125 302.2 .0625 289.1s5.469-23.72 16.06-29.77l448-255.1c10.69-6.109 23.88-5.547 34 1.406S513.5 24.72 511.6 36.86z"></path>
                                                         </svg>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform" async="async"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js" type="text/javascript"></script>

    <script src="Assets/validation-engine/js/languages/jquery.validationEngine-vi.js" type="text/javascript" charset="utf-8"></script>
    <script src="Assets/validation-engine/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8"></script>

    <script>

        function CheckValid() {
            var validated = $("#contactForm").validationEngine('validate', { promptPosition: "topLeft", scroll: false });
            if (validated)
                return validated;
        };
        function DisableContentChanged() {
            window.onbeforeunload = null;
        };

    </script>

</asp:Content>