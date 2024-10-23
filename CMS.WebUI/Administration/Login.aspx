<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CMS.WebUI.Administration.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <asp:PlaceHolder runat="server"><%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Example Dev - Login</title>
    <link href="/Administration/Assets/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <link href="/Administration/Assets/css/sb-admin-2.min.css" rel="stylesheet" />
</head>
<body class="bg-gradient-primary d-flex align-items-center justify-content-center" style="min-height: 100vh;">
    <form id="form1" runat="server">
        <div class="container">
            <!-- Outer Row -->
            <div class="row justify-content-center">
                <div class="col-xl-10 col-lg-12 col-md-9">
                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row">
                                <div class="col-lg-6 d-none d-lg-block bg-login-image">
                                    <div class="text-center mt-4">
                                        <img src="UploadImage/ion_Login.png" width="68%" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Trang quản trị!</h1>
                                        </div>
                                         <asp:Label ID="lblErrorMessage"  runat="server" CssClass="text-danger mt-3" Text=""></asp:Label>

                                        <div class="user">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtUsername" CssClass="form-control form-control-user" placeholder="Username" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtPassword" CssClass="form-control form-control-user" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <div class="custom-control custom-checkbox small">
                                                    <input type="checkbox" class="custom-control-input" id="customCheck">
                                                    <label class="custom-control-label" for="customCheck">
                                                        Ghi nhớ</label>
                                                </div>
                                            </div>
                                            <hr>
                                            <div class="justify-content-end d-flex">
                                            <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-user btn-block" style="max-width:50%" runat="server" Text="Đăng nhập" OnClick="btnLogin_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <asp:PlaceHolder runat = "server" ><%: Scripts.Render("/Administration/Assets/vendor/jquery/jquery.min.js") %>
        <%: Scripts.Render("/Administration/Assets/vendor/bootstrap/js/bootstrap.bundle.min.js") %>
        <%: Scripts.Render("/Administration/Assets/vendor/jquery-easing/jquery.easing.min.js") %>
        <%: Scripts.Render("/Administration/Assets/js/sb-admin-2.min.js") %>

    </asp:PlaceHolder>
</body>
</html>
