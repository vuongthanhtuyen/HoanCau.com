var ModalHelper = {
    Setting: {
        agreeBtnId: '',
        closeBtnId: '',
        saveBtnId: '',
        wdId: '#wfMessage'
    },
    ResourceText: {
        btnApply: 'Apply',
        btnCancel: 'Cancel',
        btnClose: 'Close',
        defTitle: 'Notice'
    },
    DataType: {
        Alert: 'alert',
        ConfirmDelete: 'confirmDelete',
        ConfirmCancelSave: 'confirmCancelSave',
    },
    commonFunction: {
        initLoad: function () {

            var agreeBtn = $(ModalHelper.Setting.agreeBtnId);
            if (agreeBtn.length > 0) {
                agreeBtn.removeAttr('onclick');
                agreeBtn.bind('click', function (evt) {
                    evt.preventDefault();
                    ModalHelper.mainFunction.CloseConfirmWindow();
                    return false;
                });
            }

            var saveBtn = $(ModalHelper.Setting.saveBtnId);
            if (saveBtn.length > 0) {
                saveBtn.removeAttr('onclick');
                saveBtn.bind('click', function (evt) {
                    evt.preventDefault();
                    ModalHelper.mainFunction.CloseConfirmWindow();
                    return false;
                });
            }

            var closeBtn = $(ModalHelper.Setting.closeBtnId);
            if (closeBtn.length > 0) {
                closeBtn.removeAttr('onclick');
                closeBtn.bind('click', function (evt) {
                    evt.preventDefault();
                    ModalHelper.mainFunction.CloseConfirmWindow();
                    return false;
                });
            }
        },
        include: function (url) {
            document.write('<script src="' + url + '" type="text/javascript"></script>');
        },
        getIEVersion: function () {
            var rv = -1;
            if (navigator.appName === 'Microsoft Internet Explorer') {
                var ua = navigator.userAgent;
                var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
                if (re.exec(ua) !== null)
                    rv = parseFloat(RegExp.$1);
            }
            else if (navigator.appName === 'Netscape') {
                var ua = navigator.userAgent;
                var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
                if (re.exec(ua) !== null)
                    rv = parseFloat(RegExp.$1);
            }
            return rv;
        },
        includeToHead: function (url) {
            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script'); script.type = 'text/javascript';
            script.src = url;
            head.appendChild(script);
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
        sortObjectProperties: function (obj) {
            var arr = [];
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop)) {
                    arr.push({
                        'key': prop,
                        'value': obj[prop]
                    });
                }
            }
            arr.sort(function (a, b) {
                return (a.value.localeCompare(b.value));
            });
            return arr;
        }
    },
    mainFunction: {
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
        OpenModalWindow: function (title, message, type, dataAction1, dataAction2,
            beforeOpenFunc) {
            var wd = $(ModalHelper.Setting.wdId);
            var btnCancel = null;
            if (wd.length > 0) {
                //show or hide process button
                var btn1 = $(ModalHelper.Setting.agreeBtnId);
                var btn2 = $(ModalHelper.Setting.saveBtnId);
                if (btn1.length > 0) {
                    if (typeof type !== 'undefined') {
                        switch (type) {
                            case ModalHelper.DataType.Alert:
                                btn1.hide();
                                btn2.hide();
                                btnCancel = $(ModalHelper.Setting.closeBtnId);
                                if (btnCancel.length > 0)
                                    btnCancel.val(ModalHelper.ResourceText.btnCancel);
                                break;
                            case ModalHelper.DataType.ConfirmDelete:
                                btn1.css('display', 'inline-block');
                                btn2.hide();
                                btn1.unbind().bind('click', function (evt) {
                                    evt.preventDefault();
                                    if (typeof dataAction1 !== 'undefined')
                                        dataAction1();
                                    else
                                        ModalHelper.mainFunction.CloseConfirmWindow();

                                });

                                btnCancel = $(ModalHelper.Setting.closeBtnId);
                                if (btnCancel.length > 0)
                                    btnCancel.val(ModalHelper.ResourceText.btnClose);

                                btnCancel.unbind().bind('click', function (evt) {
                                    evt.preventDefault();
                                    if (typeof dataAction2 !== 'undefined')
                                        dataAction2();
                                    else
                                        ModalHelper.mainFunction.CloseConfirmWindow();
                                });

                                break;
                            case ModalHelper.DataType.ConfirmCancelSave:
                                btn1.css('display', 'inline-block');
                                btn2.css('display', 'inline-block');
                                btn1.val('Yes');
                                btn2.val('No');

                                btn1.unbind().bind('click', function (evt) {
                                    evt.preventDefault();
                                    if (typeof dataAction1 !== 'undefined')
                                        dataAction1();
                                    else
                                        ModalHelper.mainFunction.CloseConfirmWindow();
                                });

                                btn2.unbind().bind('click', function (evt) {
                                    evt.preventDefault();
                                    if (typeof dataAction2 !== 'undefined')
                                        dataAction2();
                                    else
                                        ModalHelper.mainFunction.CloseConfirmWindow();
                                });

                                btnCancel = $(ModalHelper.Setting.closeBtnId);
                                if (btnCancel.length > 0)
                                    btnCancel.val(ModalHelper.ResourceText.btnClose);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (message.length > 0) {
                    wd.find('.modal-body').text(message);
                }
                else {
                    wd.find('.modal-body').text('');
                }

                if (typeof beforeOpenFunc === 'function') {
                    beforeOpenFunc();
                    setTimeout(function () {
                        wd.modal('show');
                    }, 500);
                }
                else
                    wd.modal('show');

                //title of window
                if (typeof title !== 'undefined' && title.length > 0)
                    wd.find('.modal-title').text(title);
                else
                    wd.find('.modal-title').text(ModalHelper.ResourceText.defTitle);

                setTimeout(function () {
                    //console.log(btn1, btn2, btnCancel);
                    if (btn1.length > 0 && btn1.is(':hidden') === false)
                        btn1.focus();
                    else if (btnCancel !== null && btnCancel.is(':hidden') === false)
                        btnCancel.focus();
                }, 500);
            }
        },
        CloseConfirmWindow: function () {
            var radWindowConfirm = $(ModalHelper.Setting.wdId);
            if (radWindowConfirm.length > 0) {
                radWindowConfirm.modal('hide');
            }
            return false;
        }
    }
}