<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="test.aspx.cs" Inherits="CMS.WebUI.test" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content runat="server" ID="content2" ContentPlaceHolderID="ContentHead">
    <link rel="stylesheet" type="text/css" href="Assets/css/news-detail.css?v=f81a959662efae2fc3cc158351e6d90c" />

</asp:Content>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="MainContent">

 <%--   <h2>This is XMLHttpRequest Object</h2>
    <input id="txtGetId" type="text" />

    <textarea id="NoiDung" style="min-height:200px" ></textarea>
    <script src="Administration/Style/plugins/ckeditor/ckeditor.js"></script>


        <script>

            CKEDITOR.replace("NoiDung");

        </script>--%>

     <div class="form-group">
     <label class="control-label">Mô tả ngắn</label>
     <textarea id="txtSummary" runat="server" placeholder="Mô tả ngắn" class="form-control" rows="4"></textarea>
     <label class="small">Nội dung mô tả ngắn không quá 1000 ký tự</label>
 </div>
 <div class="form-group">
     <label class="control-label">Nội dung</label>
     <CKEditor:CKEditorControl ID="txtDescription" Width="100%" CssClass="ck-editor"
         Toolbar="Full" BodyId="StaticPageContent" Language="en-US" AutoParagraph="false"
         BasePath="/Administration/Style/plugins/ckeditor/" runat="server" Height="300">
     </CKEditor:CKEditorControl>
 </div>

    <div id="mainContentHere"></div>


    <button id="btnrequest" type="button">Request Data</button>




    <div class="wrapContent4 bgColor2 pageNewsDetail">
        <div class="container-xxl containerItem">
            <div class="contentItem">
                <div class="row rowItem">
                    <div class="col-lg-8 colText" id="post">


                        </div>
                    <div class="col-lg-4 colRight">
                        <div class="contentItem">
                            <div class="titleNewsMain">Tin Tức</div>
                        </div>
                    </div>
                </div>
                <div class="otherPost">
                </div>
            </div>
        </div>
    </div>


        <script>
            $(document).ready(function () {
                $('#btnrequest').click(function () {
                var content = CKEDITOR.instances['<%= txtDescription.ClientID %>'].getData();
                //console.log(content);
                $.ajax({
                    url: 'test.aspx/BindInDecription',
                    data: JSON.stringify({ txtDecription: content }),
                    method: 'post',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        //var post = JSON.parse(response.d); // đây là ép chuổi về json
                        var post = response.d; // còn đây là một chuỗi không cần ép nữa!!
                        
                        $('#mainContentHere').html(post);
                    },
                    error: function (error) {
                        console.error("Error:", error);
                    }
                })
            })
        });



            //function loadDoc() {
            //    const xhttp = new XMLHttpRequest();
            //    xhttp.onload = function () {
            //        document.getElementById("demo").innerHTML = this.responseText;
            //    }
            //    // Gửi yêu cầu đến phương thức WebMethod với URL và tham số
            //    xhttp.open("POST", "test.aspx/GetGreeting", true);
            //    xhttp.setRequestHeader("Content-Type", "application/json");
            //    xhttp.send(JSON.stringify({ firstname: "Henry", lastname: "Ford" }));
            //}
        </script>



    <script>
        // test ajax get post by id
//        $(document).ready(function () {
//            $('#btnrequest').click(function () {
//                var id = $('#txtGetId').val();
//                $.ajax({
//                    url: 'test.aspx/GetPost',
//                    data: JSON.stringify({ id: id }),
//                    method: 'post',
//                    contentType: 'application/json; charset=utf-8',
//                    dataType: 'json',
//                    success: function (response) {
//                        var post = JSON.parse(response.d);
//                        var postBind = `  
//                          <div class="contentItem">
//                              <div class="wrapImg">
//                                  <img src="/Administration/UploadImage/${post.ThumbnailUrl}" alt="${post.TieuDe}">
//                              </div>

//                              <div class="contentText">
//                                  <p class="titleNewsMain">${post.TieuDe}</p>

//                                  <div class="infoNumberItem">
//                                      <span class="itemInfoNumber user">
//                                          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
//                                              <path fill="currentColor" d="M490.3 40.4C512.2 62.27 512.2 97.73 490.3 119.6L460.3 149.7L362.3 51.72L392.4 21.66C414.3-.2135 449.7-.2135 471.6 21.66L490.3 40.4zM172.4 241.7L339.7 74.34L437.7 172.3L270.3 339.6C264.2 345.8 256.7 350.4 248.4 353.2L159.6 382.8C150.1 385.6 141.5 383.4 135 376.1C128.6 370.5 126.4 361 129.2 352.4L158.8 263.6C161.6 255.3 166.2 247.8 172.4 241.7V241.7zM192 63.1C209.7 63.1 224 78.33 224 95.1C224 113.7 209.7 127.1 192 127.1H96C78.33 127.1 64 142.3 64 159.1V416C64 433.7 78.33 448 96 448H352C369.7 448 384 433.7 384 416V319.1C384 302.3 398.3 287.1 416 287.1C433.7 287.1 448 302.3 448 319.1V416C448 469 405 512 352 512H96C42.98 512 0 469 0 416V159.1C0 106.1 42.98 63.1 96 63.1H192z"></path>
//                                          </svg>TẬP ĐOÀN HOÀN CẦU KHU VỰC KHÁNH HOÀ</span><span class="itemInfoNumber view">
//                                              <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 576 512">
//                                                  <path fill="currentColor" d="M288 288a64 64 0 0 0 0-128c-1 0-1.88.24-2.85.29a47.5 47.5 0 0 1-60.86 60.86c0 1-.29 1.88-.29 2.85a64 64 0 0 0 64 64zm284.52-46.6C518.29 135.59 410.93 64 288 64S57.68 135.64 3.48 241.41a32.35 32.35 0 0 0 0 29.19C57.71 376.41 165.07 448 288 448s230.32-71.64 284.52-177.41a32.35 32.35 0 0 0 0-29.19zM288 96a128 128 0 1 1-128 128A128.14 128.14 0 0 1 288 96zm0 320c-107.36 0-205.46-61.31-256-160a294.78 294.78 0 0 1 129.78-129.33C140.91 153.69 128 187.17 128 224a160 160 0 0 0 320 0c0-36.83-12.91-70.31-33.78-97.33A294.78 294.78 0 0 1 544 256c-50.53 98.69-148.64 160-256 160z"></path>
//                                              </svg>${post.ViewCount} Lượt xem</span><span class="itemInfoNumber date">
//                                                  <svg aria-hidden="true" focusable="false" role="img" xmlns="http://www.w3.org/2000/svg" viewbox="0 0 448 512">
//                                                      <path fill="currentColor" d="M400 64h-48V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H128V12c0-6.6-5.4-12-12-12h-8c-6.6 0-12 5.4-12 12v52H48C21.5 64 0 85.5 0 112v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V112c0-26.5-21.5-48-48-48zM48 96h352c8.8 0 16 7.2 16 16v48H32v-48c0-8.8 7.2-16 16-16zm352 384H48c-8.8 0-16-7.2-16-16V192h384v272c0 8.8-7.2 16-16 16zM148 320h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 96h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm-96 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm192 0h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12z"></path>
//                                                  </svg>${post.NgayTao}</span>
//                                  </div>

//                                  <div class="wrapDesItem">
//                                      <div class="media">
//                                          <div class="wrapIcon">
//                                              <svg fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
//                                                  <path d="M264 224h-144C106.8 224 96 234.8 96 248S106.8 272 120 272h144C277.3 272 288 261.3 288 248S277.3 224 264 224zM168 320h-48C106.8 320 96 330.8 96 344s10.75 24 24 24h48c13.25 0 24-10.75 24-24S181.3 320 168 320zM264 128h-144C106.8 128 96 138.8 96 152S106.8 176 120 176h144C277.3 176 288 165.3 288 152S277.3 128 264 128zM320 0H64C28.65 0 0 28.65 0 64v384c0 35.35 28.65 64 64 64h256c35.35 0 64-28.65 64-64V64C384 28.65 355.3 0 320 0zM336 448c0 8.822-7.178 16-16 16H64c-8.822 0-16-7.178-16-16V64c0-8.822 7.178-16 16-16h256c8.822 0 16 7.178 16 16V448z" />
//                                              </svg>
//                                          </div>

//                                          <div class="media-body">
//                                              <p class="desItem">${post.TieuDe}.</p>
//                                          </div>
//                                      </div>
//                                  </div>

//                                  <div class="wrapText showTextDetail">
//                                      ${post.NoiDungChinh}
//                                  </div>

//                                  <div class="wrapShare">
//                                      <div class="titleShare">Chia sẻ:</div>

//                                      <div class="listBtnSharePost">
//                                          <div class="sharethis-inline-share-buttons"></div>
//                                      </div>
//                                  </div>
//                              </div>
//                          </div>
                      
//`
//                        //$('#post').html(postBind);
//                    },
//                    error: function (error) {
//                        console.error("Error:", error);
//                    }


//                })
//            })
//        });



        //function loadDoc() {
        //    const xhttp = new XMLHttpRequest();
        //    xhttp.onload = function () {
        //        document.getElementById("demo").innerHTML = this.responseText;
        //    }
        //    // Gửi yêu cầu đến phương thức WebMethod với URL và tham số
        //    xhttp.open("POST", "test.aspx/GetGreeting", true);
        //    xhttp.setRequestHeader("Content-Type", "application/json");
        //    xhttp.send(JSON.stringify({ firstname: "Henry", lastname: "Ford" }));
        //}
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentEnd" ID="content3">
    <script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform" async="async"></script>

</asp:Content>
