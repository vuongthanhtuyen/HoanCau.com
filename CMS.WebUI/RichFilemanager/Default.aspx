<%@ Page Language="C#" AutoEventWireup="True"
    CodeBehind="Default.aspx.cs" Inherits="SweetCMS.WebUI.RichFilemanager.Default" %>

<!DOCTYPE html>
<html>

<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>File Manager</title>
    <link rel="stylesheet" type="text/css" href="styles/reset.css" />
    <link rel="stylesheet" type="text/css" href="/RichFilemanager/scripts/jquery.filetree/src/jQueryFileTree.css" />
    <link rel="stylesheet" type="text/css" href="/RichFilemanager/scripts/jquery.contextmenu/dist/jquery.contextMenu.min.css" />
    <link rel="stylesheet" type="text/css" href="/RichFilemanager/scripts/custom-scrollbar-plugin/jquery.mCustomScrollbar.min.css" />
    <link rel="stylesheet" type="text/css" href="/RichFilemanager/themes/default/styles/filemanager.css">

    <style type="text/css">
        .fm-container #loading-wrap {
            position: fixed;
            height: 100%;
            width: 100%;
            overflow: hidden;
            top: 0;
            left: 0;
            display: block;
            background: white url(./images/wait30trans.gif) no-repeat center center;
            z-index: 999;
        }
    </style>

    <link href="scripts/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <style type="text/css">
        .jquery-lightbox-button-close {
            top: 12px;
            right: 12px;
            z-index: 7001;
        }

        .jquery-lightbox-html iframe {
            height: 100% !important;
            width: 100% !important;
        }

        #ddlpaging {
            position: absolute;
            left: 10px;
            bottom: 16px;
        }

        .bgtran {
            background: transparent !important
        }
        .fm-container .context-menu-list{
            top: 0 !important;
            left: 40% !important;
        }
    </style>
    <!-- CSS dynamically added using 'config.options.theme' defined in config file -->
</head>

<body>
    <div class="fm-container" id="mainfm">
        <script type="text/javascript">
            if (location.search.length === 0)
                document.getElementById('mainfm').className = 'fm-container bgtran';
        </script>
        <div id="loading-wrap" style="display: none">
            <!-- loading wrapper / removed when loaded -->
        </div>
        <div>
            <form id="uploader" method="post" runat="server">
                <h1></h1>
                <button id="level-up" name="level-up" type="button" value="LevelUp">&nbsp;</button>
                <button id="home" name="home" type="button" value="Home">&nbsp;</button>
                <input id="mode" name="mode" type="hidden" value="add" />
                <input id="currentpath" name="currentpath" type="hidden" />
                <div id="file-input-container">
                    <div id="alt-fileinput">
                        <input id="filepath" name="filepath" type="text" />
                        <button id="browse" name="browse" type="button" value="Browse"></button>
                    </div>
                    <input id="newfile" name="newfile" type="file" />
                </div>
                <button id="upload" name="upload" type="button" value="Upload" class="em"></button>
                <button id="newfolder" name="newfolder" type="button" value="New Folder" class="em"></button>
                <button id="grid" class="ON" type="button">&nbsp;</button>
                <button id="list" type="button">&nbsp;</button>
            </form>

            <div id="splitter">
                <div id="filetree"></div>
                <div id="fileinfo">
                    <h1></h1>
                </div>
            </div>

            <div id="footer">
                <form name="search" id="search" method="get">
                    <div>
                        <input type="text" value="" name="q" id="q" />
                        <a id="reset" href="#" class="q-reset"></a>
                        <span class="q-inactive"></span>
                    </div>
                </form>
                <div class="right">
                    <div id="folder-info">
                        <span id="items-counter"></span>- <span id="items-size"></span>
                    </div>
                    <div id="summary"></div>
                    <!--<a href="" id="link-to-project"></a>-->
                </div>
                <div style="clear: both"></div>
                <select id="ddlpaging" style="display: none">
                    <option value="10">10</option>
                    <option selected="selected" value="20">20</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                    <option value="200">200</option>
                </select>
            </div>

            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-1.11.3.min.js"></script>

            <!-- build for drag&drop only -->
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/version.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/ie.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/data.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/plugin.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/safe-active-element.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/safe-blur.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/unique-id.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/scroll-parent.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/widget.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/mouse.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/position.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/draggable.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-ui/droppable.js"></script>

            <script type="text/javascript" src="/RichFilemanager/scripts/jquery-browser.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery.splitter/jquery.splitter-1.5.1.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery.filetree/src/jQueryFileTree.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery.contextmenu/dist/jquery.contextMenu.min.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery.impromptu/dist/jquery-impromptu.min.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/TinySort/dist/tinysort.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/TinySort/dist/jquery.tinysort.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/javascript-templates/js/tmpl.min.js"></script>
            <!-- Load jquery file upload library -->
            <!--<script type="text/javascript" src="scripts/jQuery-File-Upload/js/vendor/jquery.ui.widget.js"></script>-->
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/canvas-to-blob.min.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/load-image.all.min.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/jquery.iframe-transport.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/jquery.fileupload.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/jquery.fileupload-process.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/jquery.fileupload-image.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jQuery-File-Upload/js/jquery.fileupload-validate.js"></script>
            <script type="text/javascript" src="/RichFilemanager/scripts/jquery.single_double_click.js"></script>
            <script src="scripts/lightbox-evolution-1.8/js/jquery.lightbox.1.8.min.js"></script>

            <!-- Load filemanager script -->
            <!--<script type="text/javascript" src="scripts/filemanager.min.js"></script>-->
            <script type="text/javascript" src="scripts/filemanager.js?v=201710251418"></script>
            <script src="<%=ResolveClientUrl("~/Administration/Style/dist/js/clipboard.min.js?v=201706010424")%>"></script>
            <script>
                const _hostPath = '<%= SweetCMS.Core.Helper.CommonHelper.GetHostPath().TrimEnd('/') %>';
                function renderCopyRich() {
                    if ($('#copy-button').length > 0) {
                        var clipboard = new ClipboardJS('#copy-button');
                        clipboard.on('success', function (e) {
                        });
                        clipboard.on('error', function (e) {
                        });
                    }
                }
                $(document).ready(function () {
                    $('#list')[0].click();
                })
            </script>
        </div>
    </div>
</body>

</html>
