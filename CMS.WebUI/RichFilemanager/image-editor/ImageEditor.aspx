<%@ Page Language="C#" AutoEventWireup="true" EnableTheming="false" Theme=""
    Inherits="SweetCMS.WebUI.RichFilemanager.imageeditor.ImageEditor" 
    Codebehind="ImageEditor.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- START Head -->
<head id="Head1" runat="server">
    <!-- START META SECTION -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Image Editor</title>
    <meta name="author" content="">
    <meta name="description" content="">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <!--/ END META SECTION -->

    <!-- START STYLESHEETS -->
    <!-- Plugins stylesheet : optional -->
    <link href="css/bootstrap-slider.css" rel="stylesheet" />
    <link href="css/jquery.Jcrop.css" rel="stylesheet" />
    <link href="css/jquery.qtip.css" rel="stylesheet" />
    <link href="css/thumb.css" rel="stylesheet" />
    <!--/ Plugins stylesheet : optional -->

    <!-- Application stylesheet : mandatory -->
    <link rel="stylesheet" href="css/bootstrap.css">
    <%--<link rel="stylesheet" href="css/layout.css">--%>
    <link rel="stylesheet" href="css/uielement.css">
    <link href="css/bootstrap-colorpicker.css" rel="stylesheet" />
    <!--/ Application stylesheet -->
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <!-- Theme stylesheet : optional -->
    <link href="css/custom.css" rel="stylesheet" />
    <!--/ Theme stylesheet : optional -->
    <!-- END STYLESHEETS -->
    <script src="js/jquery-1.11.2.min.js"></script>

</head>
<!--/ END Head -->
<body>
    <form id="form1" runat="server">
        <!-- START Template Main -->
        <section role="main">
            <!-- START Template Container -->
            <div class="container-fluid">
                <div class="panel panel-success">
                    <!-- panel heading/header -->
                    <div class="panel-heading">
                        <h3 class="panel-title"><span class="panel-icon mr5"><i class="ico-table22"></i></span>Editor window</h3>
                        <!-- panel toolbar -->
                        <%--<div class="panel-toolbar text-right">
                            <!-- option -->
                            <div class="option">
                                <button class="btn up" data-toggle="panelcollapse"><i class="arrow"></i></button>
                                <button class="btn" data-toggle="panelremove" data-parent=".col-md-12"><i class="remove"></i></button>
                            </div>
                            <!--/ option -->
                        </div>--%>
                        <!--/ panel toolbar -->
                    </div>
                    <!--/ panel heading/header -->                           

                    <!-- panel body with collapse capabale -->
                    <div class="panel-collapse pull out pa15" id="main">
                        <div class="row">
                            <div class="col-sm-3">
                                <!-- panel group -->
                                <div class="panel-group" id="accpanel"></div>
                                <!--/ panel group -->
                            </div>
                            <div class="col-sm-9 text-center">
                                <div class="img-thumbnail bg-info">
                                    <div class="indicator"><img src="img/spinner2x.gif" /><span class="helper"></span></div>
                                </div>

                                <span class="hidden" id="qtip"></span>
                                <div class="col-xs-12" style="display: none" id="alertbox"></div>
                                <div class="col-xs-12 mt20">
                                    <!-- wrapper for the whole component -->
                                    <div id="componentWrapper">
                                        <div class="thumbContainer">
                                            <div class="thumbInnerContainer"></div>
                                        </div>
                                        <div class="thumbBackward thumb_hidden">
                                            <img src="img/thumb_backward.png" alt="" width="21" height="31" />
                                        </div>
                                        <div class="thumbForward thumb_hidden">
                                            <img src="img/thumb_forward.png" alt="" width="21" height="31" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/ panel body with collapse capabale -->
                </div>
            </div>
            <!--/ END Template Container -->

            <!-- START To Top Scroller -->
            <%--<a href="javascript:void(0);" class="totop" id="gototop"><i class="hi-icon ico-angle-up"></i>Go top</a>--%>
            <!--/ END To Top Scroller -->
        </section>
        <!--/ END Template Main -->
    </form>

    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.transit.js"></script>
    <script src="js/underscore-min.js"></script>
    <script src="js/bootstrap-slider.js"></script>
    <script src="js/studio/jquery.Jcrop.js" type="text/javascript"></script> 
    <script src="js/studio/jquery.jcrop.preview.js" type="text/javascript"></script> 
    <script src="js/studio/ImageResizer.js" type="text/javascript"></script>
    <script src="js/studio/jquery.overdraw.js" type="text/javascript"></script>
    <script src="js/studio/jquery.imagestudio.js" type="text/javascript"></script>
    <%--<script src="js/bootstrap-colorpicker.min.js"></script>--%>
    <script src="js/bootstrap-colorpicker.js"></script>

    <script src="js/jquery.easing.1.3.js"></script>
    <script src="js/jquery.mousewheel.js"></script>
    <script src="js/jquery.qtip.min.js"></script>
    <script src="js/jquery.thumbGallery.js"></script>
    <script src="js/bootstrap-multiselect.js"></script>
    <%--register variable--%>
    <asp:Literal ID="ltData" EnableViewState="false" runat="server"></asp:Literal>

    <script type="text/javascript" src="js/editor.js"></script>

    <script type="text/javascript">
        $(function () {
            $(document).on('click', '#gototop', function (e) {
                $('html, body').animate({
                    scrollTop: 0
                }, 200);

                e.preventDefault();
            });


            var toggler = '[data-toggle~=panelcollapse]';
            // clicker
            $(document).on('click', toggler, function (e) {
                // find panel element
                var panel = $(this).parents('.panel'),
                    target = panel.children('.panel-collapse'),
                    height = target.height();

                // error handling
                if (target.length === 0) {
                    alert('collapsable element need to be wrap inside ".panel-collapse"');

                    $.error('collapsable element need to be wrap inside ".panel-collapse"');
                }

                var open = function (toggler) {
                    $(toggler).removeClass('down').addClass('up');
                    $(target)
                        .removeClass('pull').addClass('pulling')
                        .css('height', '0px')
                        .transition({ height: height }, function () {
                            $(this).removeClass('pulling').addClass('pull out');
                            $(this).css({ 'height': '' });
                        });

                    // publish event
                    $(document).trigger('fa' + '.panelcollapse.open', { 'element': $(panel) });
                };

                var close = function (toggler) {
                    $(toggler).removeClass('up').addClass('down');
                    $(target)
                        .removeClass('pull out').addClass('pulling')
                        .css('height', height)
                        .transition({ height: '0px' }, function () {
                            $(this).removeClass('pulling').addClass('pull');
                            $(this).css({ 'height': '' });
                        });

                    // publish event
                    $(document).trigger('fa' + '.panelcollapse.close', { 'element': $(panel) });
                };

                // collapse the element
                if ($(target).hasClass('out')) {
                    close(this);
                } else {
                    open(this);
                }

                // prevent default
                e.preventDefault();
            });

            var handler = '[data-toggle~=panelremove]';
            $(document).on('click', handler, function (e) {
                // find panel element
                var panel = $(this).parents('.panel');
                var parent = $(this).data('parent');

                // remove panel
                panel.transition({ scale: 0 }, function () {
                    //remove
                    if (parent) {
                        $(this).parents(parent).remove();
                    } else {
                        $(this).remove();
                    }

                    // publish event
                    $(document).trigger('fa' + '.panelcollapse.remove', { 'element': $(panel) });
                });

                // prevent default
                e.preventDefault();
            });
        });
    </script>
</body>
</html>