/// <reference path="jquery-1.11.2.min.js" />
/// <reference path="bootstrap.min.js" />
/// <reference path="bootstrap-colorpicker.js" />
/// <reference path="jquery.lightbox.1.8.min.js" />
/// <reference path="jquery.qtip.min.js" />
/// <reference path="jquery.thumbGallery.js" />
/// <reference path="underscore-min.js" />
if (typeof SweetSoftScript === 'undefined')
    SweetSoftScript = {};

SweetSoftScript.Editor = {
    Data: {
        ajRequest: undefined,
        editor: undefined,
        needRefresh: false,
        dataWin: undefined,
        ImageManager: undefined,
        errorCount: 0
    },
    commonFunction: {
        getDataQuery: function (name, ss) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)", 'i'),
                results = regex.exec(ss || location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },
        preloadLoadImage: function (imgSrc, callback) {
            var imgPreload = new Image();
            imgPreload.src = imgSrc;
            if (imgPreload.complete) {
                callback(imgPreload);
            }
            else {
                imgPreload.onload = function () {
                    callback(imgPreload);
                }
                imgPreload.onerror = function () {
                    callback(undefined);
                }
            }
        },
        ajaxRequest: function (urlGet, dataValue, callback) {
            /// <summary>Get data from server by jQuery.</summary>
            if (!$('form').hasClass('progressClass')) {
                SweetSoftScript.Editor.Data.ajRequest = $.ajax({
                    type: "post",
                    async: true,
                    url: urlGet,
                    //traditional: false,
                    //dataType: "json",
                    data: dataValue,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    beforeSend: function () {
                        $('form').addClass('progressClass');
                    },
                    success: function (data) {
                        $('form').removeClass('progressClass');
                        if (typeof callback === 'function')
                            callback(data);
                    },
                    error: function () {
                        $('form').removeClass('progressClass');
                        if (typeof callback === 'function')
                            callback(undefined);
                    }
                });
            }
            else {
            }
        },
        getWindowSize: function () {
            var w = 0; var h = 0;
            //IE
            if (!window.innerWidth) {
                if (!(document.documentElement.clientWidth === 0)) {
                    //strict mode
                    w = document.documentElement.clientWidth;
                    h = document.documentElement.clientHeight;
                } else {
                    //quirks mode
                    w = document.body.clientWidth; h = document.body.clientHeight;
                }
            } else {
                //w3c
                w = window.innerWidth; h = window.innerHeight;
            }
            return { width: w, height: h };
        },
        delay: (function () {
            var timer = 0;
            return function (callback, ms) {
                clearTimeout(timer);
                timer = setTimeout(callback, ms);
            };
        })()
    },
    mainFunction: {
        OnClientLoad: function (sender, arg) {
            SweetSoftScript.Editor.Data.editor = sender;
            //console.log(sender, arg);
            SweetSoftScript.Editor.Data.editor.get_dialogOpener().add_close(SweetSoftScript.Editor.mainFunction.dialogClosed);
            SweetSoftScript.Editor.Data.editor.add_commandExecuted(SweetSoftScript.Editor.mainFunction.OnClientCommandExecuted);
            SweetSoftScript.Editor.Data.editor.get_dialogOpener().get_container().add_pageLoad(function (newsender, newargs) {
                if (newsender.get_navigateUrl().indexOf("DialogName=ImageManager") >= 0) {
                    //console.log('ImageManager load ', sender, newargs);
                    SweetSoftScript.Editor.mainFunction.RecruitLoad();
                }
            });
        },
        dialogClosed: function (sender, args) {
            //console.log(sender, args);
            if (args.get_navigateUrl().indexOf("DialogName=ImageManager") >= 0) {
                //console.log(sender, args);
            }
        },
        OpenImageEditor: function (files) {
            if (files && files.length > 0) {
                var ws = SweetSoftScript.Editor.commonFunction.getWindowSize();
                var url = 'editor/ImageEditor.aspx?d=' + encodeURIComponent(files);

                $.lightbox(url, {
                    width: ws.width - 120, height: ws.height - 30,
                    'iframe': true,
                    onOpen: function (ex) {
                        $('body').css('overflow', 'hidden');
                        //console.log(ex, this);
                    },
                    onClose: function () {
                        //console.log('needRefresh : ', needRefresh);
                        $('body').removeAttr('style');
                        if (SweetSoftScript.Editor.Data.needRefresh === true) {
                            SweetSoftScript.Editor.Data.needRefresh = false;
                            SweetSoftScript.Editor.Data.ImageManager.refresh();
                        }
                    }
                });
            }
        },
        OnClientItemSelected: function (sender, args) {
            var imageSrc = args.get_item().get_url();
            //console.log(imageSrc,sender, args);
        },
        OnClientCommandExecuted: function (editor, args) {
            //console.log('args : ', args);
            var command = args.get_commandName();
            if (command === "ImageManager") { // You should check for all desired file browser dialogs
                setTimeout(function () {

                    SweetSoftScript.Editor.Data.errorCount = 0;
                    //RecruitLoad();
                }, 1000);
            }
        },
        RecruitLoad: function () {
            //console.log('call RecruitLoad');
            if (typeof SweetSoftScript.Editor.Data.dataWin === 'undefined') {
                var content = SweetSoftScript.Editor.Data.editor.get_dialogOpener()._dialogContainers['ImageManager'].get_contentFrame();
                var cdoc = content.contentDocument;
                var cwin = content.contentWindow;
                if (typeof cdoc === 'undefined' || typeof cwin === 'undefined' ||
                    typeof cwin.$find === 'undefined')
                    setTimeout('SweetSoftScript.Editor.mainFunction.RecruitLoad()', 400);
                else {
                    SweetSoftScript.Editor.Data.dataWin = cwin;

                    SweetSoftScript.Editor.mainFunction.RecruitLoad();
                }
            }
            else {
                //console.log(SweetSoftScript.Editor.Data.dataWin);
                try {
                    SweetSoftScript.Editor.Data.ImageManager = SweetSoftScript.Editor.Data.dataWin.$find('RadFileExplorer1');

                    if (typeof SweetSoftScript.Editor.Data.ImageManager === 'undefined'
                        || SweetSoftScript.Editor.Data.ImageManager === null)
                        setTimeout('SweetSoftScript.Editor.mainFunction.RecruitLoad()', 400);
                    else {

                        var head = SweetSoftScript.Editor.Data.dataWin.document.getElementsByTagName('head')[0];
                        if (head !== null) {
                            $(head).append('<style type="text/css">' +
                        '.GridDraggedRows_Default .rgMasterTable { table-layout:auto!important;' +
                        '} .GridDraggedRows_Default .jpg,.GridDraggedRows_Default .jpeg,' +
                        '.GridDraggedRows_Default .jpe { background-position: -5px -584px !important;' +
                        '} .GridDraggedRows_Default .bmp {' +
                        ' background-position: -5px -840px !important; }' +
                        '.RadGrid .rgClipCells .rgRow > td + td {' +
                        'width:5%!important; white-space:nowrap!important;' +
                        '} .GridDraggedRows_Default .png{background-position: -5px -615px !important;}</style>');
                            //console.log(head);
                        }
                        var previewWindow = SweetSoftScript.Editor.Data.dataWin.$find("ImagePreviewToolBar");
                        //console.log(testPreview);
                        var oldPreviewClick = previewWindow._events._list.buttonClicked[0];

                        previewWindow._events._list.buttonClicked.splice(0, 0, function (sender, args) {
                            //console.log(sender, args);

                            if (args.get_item()._properties._data.value === 'ImageEditor') {

                                var files = SweetSoftScript.Editor.Data.ImageManager.get_selectedItems();
                                if (files.length > 0) {
                                    var sss = '';
                                    $.each(files, function (i, o) {
                                        if (o._type === 0)
                                            sss += o.get_path() + joinKey;
                                    });
                                    if (sss.length > joinKey.length)
                                        sss = sss.substring(0, sss.length - joinKey.length);

                                    if (sss.length > joinKey.length)
                                        SweetSoftScript.Editor.mainFunction.OpenImageEditor(sss);
                                }

                                return false;
                            }
                            else
                                oldPreviewClick(sender, args);
                        });

                        previewWindow._events._list.buttonClicked.splice(1, 1);
                        //testPreview._events._list.buttonClicked = [];
                        //console.log(testPreview._events._list.buttonClicked);

                        SweetSoftScript.Editor.Data.ImageManager._events._list.itemSelected.push(SweetSoftScript.Editor.mainFunction.OnClientItemSelected);


                        var toolbar = SweetSoftScript.Editor.Data.ImageManager.get_toolbar();
                        //var toolbar = SweetSoftScript.Editor.Data.dataWin.$find('RadFileExplorer1_toolbar');
                        //console.log('toolbar : ', toolbar);
                        var oldTBClick = toolbar._events._list.buttonClicked[0];

                        toolbar._events._list.buttonClicked.splice(0, 0, function (sender, args) {
                            //console.log(sender, args);

                            if (args.get_item()._properties._data.value === 'Upload') {
                                //console.log(sender, args);
                                if (SweetSoftScript.Editor.Data.dataWin.$telerik.$('#RadFileExplorer1_chkOpenEditor').length === 0) {
                                    var uploadContainer = SweetSoftScript.Editor.Data.dataWin.$telerik.$('#RadFileExplorer1_uploadContainer').get(0);

                                    var label = SweetSoftScript.Editor.Data.dataWin.$telerik.$("label[for='RadFileExplorer1_chkOverwrite']", uploadContainer);
                                    //console.log('label : ', label);
                                    $('<label for="RadFileExplorer1_chkOpenEditor">Open image editor after uploaded to edit files.</label>').insertAfter(label);
                                    $('<input id="RadFileExplorer1_chkOpenEditor" type="checkbox" name="RadFileExplorer1$chkOpenEditor" tabindex="-1">').insertAfter(label);
                                    $('<br/>').insertAfter(label);

                                    setTimeout(function () {
                                        var uploadwindow = SweetSoftScript.Editor.Data.dataWin.$find('RadFileExplorer1_windowManagerfileExplorerUpload');
                                        if (typeof uploadwindow !== 'undefined') {
                                            uploadwindow._events._list.close.push(function () {
                                                //console.log(SweetSoftScript.Editor.Data.dataWin.$telerik.$('#RadFileExplorer1_chkOpenEditor').is(':checked'));
                                            });
                                        }
                                    }, 500);
                                }

                                oldTBClick(sender, args);
                            }
                            else
                                oldTBClick(sender, args);
                        });

                        toolbar._events._list.buttonClicked.splice(1, 1);
                    }
                }
                catch (ex) {
                    //console.log('error : ', ex.message);
                    SweetSoftScript.Editor.Data.errorCount += 1;
                    if (SweetSoftScript.Editor.Data.errorCount === 5) { }
                    else
                        SweetSoftScript.Editor.mainFunction.RecruitLoad();
                }
            }
        }
    }
}

$(function () {
    $(window).resize(function () {
        SweetSoftScript.Editor.commonFunction.delay(function () {
            var obj = $.lightbox();
            if (typeof obj !== 'undefined') {
                var ws = SweetSoftScript.Editor.commonFunction.getWindowSize();
                //console.log(ws);
                if (obj.visible === true)
                    obj.resize(ws.width - 120, ws.height - 30);
            }
        }, 500);
    });

    
});






function LoadFileUploaded(files) {
    //console.log('LoadFileUploaded', files);
    SweetSoftScript.Editor.mainFunction.OpenImageEditor(files);
}
