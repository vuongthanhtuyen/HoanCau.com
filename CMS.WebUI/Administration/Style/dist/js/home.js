/// <reference path="../../plugins/bootstrap-switch/js/bootstrap-switch.js" />
/// <reference path="modal-helper.js" />
/// <reference path="home-ui-custom.js" />
/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
/// <reference path="mustache.js" />

/// <reference path="/Administration/Style/plugins/select2/Dropdown.js" />
/// <reference path="/Administration/Style/plugins/datepicker/ExtraDateTimePicker.js" />
/// <reference path="../../plugins/moment/moment-with-locales.min.js" />

var templateSlide = '';
var templateSub = '';
var templateEditor = '';
var maindivSlide = [];
var maindivSub = [];
var maindivEditor = [];

var objAction = {
    '0': 'Select',
    '1': 'Change'
};


$(function () {

    /*#region init variable*/

    var scr = $('#templateslide');
    if (scr.length > 0) {
        templateSlide = scr.html();
        try {
            Mustache.parse(templateSlide);
        }
        catch (ex) {
        }
    }

    scr = $('#templatesub');
    if (scr.length > 0) {
        templateSub = scr.html();
        try {
            Mustache.parse(templateSub);
        }
        catch (ex) {
        }
    }

    scr = $('#templateeditor');
    if (scr.length > 0) {
        templateEditor = scr.html();
        try {
            Mustache.parse(templateEditor);
        }
        catch (ex) {
        }
    }

    //setting for modal
    if (typeof ModalHelper !== 'undefined') {
        ModalHelper.Setting.wdId = '#confirmdelete';
        ModalHelper.Setting.saveBtnId = '#mdAccept';
        ModalHelper.Setting.agreeBtnId = '#mdAgree';
        ModalHelper.Setting.closeBtnId = '#mdClose';
    }

    /*#endregion*/

    InitMain();

    InitCheckEnable();
    AddEndRequest('InitCheckEnable');
});

function GetMainSlide() {
    if (typeof maindivSlide === 'undefined' || maindivSlide.length === 0)
        maindivSlide = $('#mainslide');
    return maindivSlide;
}

function GetMainSub() {
    if (typeof maindivSub === 'undefined' || maindivSub.length === 0)
        maindivSub = $('#mainsub');
    return maindivSub;
}

function GetMainEditor() {
    if (typeof maindivEditor === 'undefined' || maindivEditor.length === 0)
        maindivEditor = $('#maineditor');
    return maindivEditor;
}

function InitCheckEnable() {
    var chkShowTM = $('input:checkbox[id$="chkShowTM"]');
    if (chkShowTM.length > 0) {
        chkShowTM.removeClass('ignore').bootstrapSwitch({
            size: 'mini',
            onSwitchChange: function (ev, state) {
                var pnl = $(this).closest('div[id$="upMain"]');
                if (pnl.length > 0) {
                    var inptmColl = pnl.find('.tm input:text');
                    if (inptmColl.length > 0) {
                        if (state === true)
                            inptmColl.removeAttr('disabled');
                        else
                            inptmColl.attr('disabled', 'disabled');
                    }

                    var cked = CKEDITOR.instances["cpMain_txtTMagazineContent"];
                    if (typeof cked !== 'undefined') {
                        if (state === true)
                            cked.setReadOnly(false);
                        else
                            cked.setReadOnly(true);
                    }
                }
            }
        });

        var isenable = chkShowTM.is(':checked');
        if (isenable === false)
            chkShowTM.trigger('switchChange');
    }

    var chkShowLI = $('input:checkbox[id$="chkShowLI"]');
    if (chkShowLI.length > 0) {
        chkShowLI.removeClass('ignore').bootstrapSwitch({
            size: 'mini',
            onSwitchChange: function (ev, state) {
                var pnl = $(this).closest('div[id$="upMain"]');
                if (pnl.length > 0) {
                    var inptmColl = pnl.find('.li input:text');
                    if (inptmColl.length > 0) {
                        if (state === true)
                            inptmColl.removeAttr('disabled');
                        else
                            inptmColl.attr('disabled', 'disabled');
                    }

                    var cked = CKEDITOR.instances["cpMain_txtLastestIssueContent"];
                    if (typeof cked !== 'undefined') {
                        if (state === true)
                            cked.setReadOnly(false);
                        else
                            cked.setReadOnly(true);
                    }
                }
            }
        });

        var isenable = chkShowLI.is(':checked');
        if (isenable === false)
            chkShowLI.trigger('switchChange');
    }
}

function InitMain() {

    /*#region Code block*/

    var btnaddslide = $('#btnaddslide');
    if (btnaddslide.length > 0) {
        btnaddslide.unbind('click').click(function () {
            AddSlide(1);
        });
    }

    var btnaddsub = $('#btnaddsub');
    if (btnaddsub.length > 0) {
        btnaddsub.unbind('click').click(function () {
            AddSub(1);
        });
    }

    var btnaddeditor = $('#btnaddeditor');
    if (btnaddeditor.length > 0) {
        btnaddeditor.unbind('click').click(function () {
            AddEditor(1);
        });
    }


    var mainslide = GetMainSlide();
    if (mainslide.length > 0) {
        mainslide.sortable({
            handle: '.holder',
            axis: 'y',
            containment: '#mainslide',
            update: function (event, ui) {
                //console.log(this, ui);
                var items = $(this).children('.item');
                if (items.length > 0) {
                    var t = '';
                    $.each(items, function (i, o) {
                        t = $.trim($(o).find('span.holder:eq(0)').attr('data-text'));
                        $(o).find('span.holder:eq(0)').text(t + ' ' + (i + 1));
                    });
                }
            },
            helper: function (event, ui) {
                var $clone = $(ui).clone();
                $clone.css({ 'background': '#ddd', height: $(ui).outerHeight() });
                return $clone.get(0);
            }
        });
    }

    var mainsub = GetMainSub();
    if (mainsub.length > 0) {
        mainsub.sortable({
            handle: '.holder',
            axis: 'y',
            update: function (event, ui) {
                //console.log(this, ui);
                var items = $(this).children('.item');
                if (items.length > 0) {
                    var t = '';
                    $.each(items, function (i, o) {
                        t = $.trim($(o).find('span.holder:eq(0)').attr('data-text'));
                        $(o).find('span.holder:eq(0)').text(t + ' ' + (i + 1));
                    });
                }
            },
            helper: function (event, ui) {
                var $clone = $(ui).clone();
                $clone.css({ 'background': '#ddd', height: $(ui).outerHeight() });
                return $clone.get(0);
            }
        });
    }

    var maineditor = GetMainEditor();
    if (maineditor.length > 0) {
        maineditor.sortable({
            handle: '.holder',
            axis: 'y',
            update: function (event, ui) {
                //console.log(this, ui);
                var items = $(this).children('.item');
                if (items.length > 0) {
                    var t = '';
                    $.each(items, function (i, o) {
                        t = $.trim($(o).find('span.holder:eq(0)').attr('data-text'));
                        $(o).find('span.holder:eq(0)').text(t + ' ' + (i + 1));
                    });
                }
            },
            helper: function (event, ui) {
                var $clone = $(ui).clone();
                $clone.css({ 'background': '#ddd', height: $(ui).outerHeight() });
                return $clone.get(0);
            }
        });
    }

    /*#endregion*/

    //bind change media type upload
    var inptype = $('#modalselect input[name="groupmediatype"]');
    if (inptype.length > 0) {
        inptype.bootstrapSwitch({
            size: 'mini',
            onSwitchChange: function (ev, state) {
                //console.log('state : ', state);
                var mdbody = $(this).closest('.modal-body');
                if (state === true) {
                    mdbody.find('.mediaimage input:text').val('');
                    mdbody.find('.mediaimage').show();
                    mdbody.find('.mediavideo').hide();
                }
                else {
                    mdbody.find('.mediavideo input:text').val('');
                    mdbody.find('.mediaimage').hide();
                    mdbody.find('.mediavideo').show();
                }
            }
        });
    }

    var btnsel = $('#btnchoose');
    if (btnsel.length > 0) {
        btnsel.click(function () {
            var act = $('div[id^="selmedia"] .sel.active');
            if (act.length > 0) {
                var div = act.closest('div[id^="selmedia"]');
                div.children('.img-holder').removeClass('hide');
                div.find('.del').removeClass('hide');
                act.attr('data-action', '1');
                act.text(objAction['1']);

                var img = div.find('.img-holder img');
                var issetimage = $('#modalselect input[name="groupmediatype"]').is(':checked');
                if (issetimage === true)
                    img.attr({
                        //'src': '/Administration/Style/dist/img/photo.png',
                        //'src': ConvertToSmall($('#txtmimage').val()),
                        'src': $('#txtmimage').val(),
                        'data-imgsrc': $('#txtmimage').val(),
                        'data-type': 'image'
                    });
                else
                    img.attr({
                        'src': '/Administration/Style/dist/img/video.png',
                        'data-videosrc': $('#txtmvideosrc').val(),
                        'data-videothumb': $('#txtmvideothumb').val(),
                        'data-type': 'video'
                    });
            }

            $('#modalselect').modal('hide');
        });
    }

    var btnsaveeditor = $('#btnsaveeditor');
    if (btnsaveeditor.length > 0) {
        btnsaveeditor.click(function () {
            var act = $('div[id^="edp"] .btnedit.active');
            if (act.length > 0) {
                var mds = $('#modaleditorpicker');
                var div = act.closest('div[id^="edp"]');

                //validate
                var valtitle = mds.find('#txtEditorTitle').val() || '';
                if (valtitle.length === 0) {
                    mds.find('#txtEditorTitle').focus();
                    mds.find('#txtEditorTitle').closest('.form-group').addClass('has-error');
                    return false;
                }
                else
                    mds.find('#txtEditorTitle').closest('.form-group').removeClass('has-error');

                var valimg = mds.find('#txtepimage').val() || '';
                if (valimg.length === 0) {
                    mds.find('#txtepimage').focus();
                    mds.find('#txtepimage').closest('.form-group').addClass('has-error');
                    return false;
                }
                else
                    mds.find('#txtepimage').closest('.form-group').removeClass('has-error');

                var aid = '';
                var atext = '';
                var url = '';
                if (mds.find('.enterurl').is(':visible') === true) {
                    url = mds.find('#txtexternal').val() || '';
                    if (url.length === 0) {
                        mds.find('#txtexternal').focus();
                        mds.find('#txtexternal').closest('.form-group').addClass('has-error');
                        return false;
                    }
                    else
                        mds.find('#txtexternal').closest('.form-group').removeClass('has-error');
                }
                else {
                    aid = mds.find('#dlarticle').val() || '';
                    atext = mds.find('#dlarticle option:selected').text() || '';
                    if (aid.length === 0) {
                        mds.find('#dlarticle').closest('.form-group').addClass('has-error');
                        return false;
                    }
                    else
                        mds.find('#dlarticle').closest('.form-group').removeClass('has-error');
                }

                //img
                //div.find('img').attr('src', ConvertToSmall(valimg));
                div.find('img').attr('src', valimg);

                var divtitle = div.find('.text');

                //title
                divtitle.text(valtitle);

                //target url

                divtitle.attr({
                    'data-id': aid,
                    'data-url': url,
                    'data-text': atext,
                    'data-target': mds.find('#ddloption').val()
                });


                div.closest('.item').removeClass('hide').addClass('changed');
                mds.modal('hide');
            }
        });
    }

    var mdpicker = $('#modaleditorpicker');
    if (mdpicker.length > 0) {
        mdpicker.on('hide.bs.modal', function () {
            var act = $('div[id^="edp"] .btnedit.active');
            if (act.length > 0) {
                var div = act.closest('.item');
                if (div.length > 0) {
                    if (div.hasClass('changed') === false && div.hasClass('hide') === true)
                        div.remove();
                }
            }

            $(this).find('.has-error').removeClass('has-error');
        });
    }


    //render 
    if (typeof dataSlide !== 'undefined' && $.isArray(dataSlide) === true && dataSlide.length > 0) {
        $.each(dataSlide, function (i, o) {

            CreateSlide(o.imgsrc || '', o.videosrc || '',
                o.videothumb || '', o.aid || '', o.atext || '',
                o.pd || '', o.black || '');
        });
    }

    if (typeof dataSub !== 'undefined' && $.isArray(dataSub) === true && dataSub.length > 0) {
        $.each(dataSub, function (i, o) {
            CreateSub(o.aid || '', o.atext || '');
        });
    }

    if (typeof dataEditor !== 'undefined' && $.isArray(dataEditor) === true && dataEditor.length > 0) {
        $.each(dataEditor, function (i, o) {
            CreateEditor(o.title || '', o.aid || '', o.atext || '',
                o.isactive === '1' ? true : false, o.imgsrc || '',
                o.target || '1', o.exturl || '');
        });
    }

    $('#mainform .lbmain > span').click(function () {
        var div = $(this).closest('.form-group').find('.ui-sortable');
        var isclose = div.is(':hidden');
        if (isclose === true) {
            $(this).parent().children('a').show();
            div.show();
        }
        else {
            $(this).parent().children('a').hide();
            div.hide();
        }
    });
}

/*#region Code block*/

function AddSlide(num) {
    if (typeof num === 'number') {
        for (var i = 0; i < num; i++) {
            CreateSlide();
        }
    }
}

function AddSub(num) {
    if (typeof num === 'number') {
        for (var i = 0; i < num; i++) {
            CreateSub();
        }
    }
}

function AddEditor(num) {
    if (typeof num === 'number') {
        for (var i = 0; i < num; i++) {
            CreateEditor();
        }
    }
}

function RefreshMainSlide() {
    var mainslide = GetMainSlide();
    if (mainslide.length > 0) {
        //refresh sortable
        var api = mainslide.data('ui-sortable');
        if (typeof api !== 'undefined')
            api.refresh();
    }
}

function RefreshMainSub() {
    var mainSub = GetMainSub();
    if (mainSub.length > 0) {
        //refresh sortable
        var api = mainSub.data('ui-sortable');
        if (typeof api !== 'undefined')
            api.refresh();
    }
}

function RefreshMainEditor() {
    var mainEditor = GetMainEditor();
    if (mainEditor.length > 0) {
        //refresh sortable
        var api = mainEditor.data('ui-sortable');
        if (typeof api !== 'undefined')
            api.refresh();
    }
}

/*#endregion*/

function CreateSlide(imgsrc, videosrc, videothumb, articleid,
    articletitle, publishdate, colorBlack) {
    if (typeof templateSlide !== 'undefined' && templateSlide.length > 0) {
        var mainslide = GetMainSlide();
        if (mainslide.length > 0) {
            var countChild = mainslide.children().length;
            var mediatype = '';
            var iconsrc = '';
            var actid = '0';
            var imgcls = ' hide';

            if (typeof imgsrc !== 'undefined' && imgsrc.length > 0) {
                mediatype = 'image';
                //iconsrc = '/Administration/Style/dist/img/photo.png';
                iconsrc = imgsrc;
                //iconsrc = ConvertToSmall(imgsrc);
                actid = '1';
                imgcls = '';
            }
            else if (typeof videosrc !== 'undefined' && videosrc.length > 0) {
                mediatype = 'video';
                actid = '1';
                imgcls = '';
                iconsrc = '/Administration/Style/dist/img/video.png';
            }

            var cpublish = '';
            var cimmedia = '';
            var colorblackChecked = '';
            if (colorBlack === '1')
                colorblackChecked = ' checked="checked"';


            var html = Mustache.render(templateSlide, {
                indx: countChild + 1, id: uniqueID(),
                src: iconsrc,
                type: mediatype,
                show: imgcls,
                videothumb: videothumb,
                videosrc: videosrc,
                imgsrc: imgsrc,
                checkpublish: cpublish,
                checkimmediately: cimmedia,
                articleid: articleid,
                articletitle: articletitle,
                action: actid,
                actionText: objAction[actid],
                publishdate: publishdate,
                colorblackchecked: colorblackChecked
            });
            mainslide.append(html);

            //refresh sortable
            RefreshMainSlide();

            //bind data
            var appended = mainslide.find(' > div:last');
            if (appended.length > 0) {
                //console.log('publishdate : ', publishdate);
                if (publishdate && publishdate.length > 0) {
                    appended.attr('data-rdo', '1').find('input:radio:not([id^="rdopublish"])').prop('checked', true);
                }
                else {
                    appended.attr('data-rdo', '0').find('input[id^="rdopublish"]').prop('checked', true);
                }
                //bind delete
                appended.find('.remove').click(function (e) {
                    e.preventDefault();
                    var t = $(this);
                    if (typeof ModalHelper !== 'undefined') {
                        ModalHelper.mainFunction.OpenModalWindow('', 'Are you sure to delete this slide ?',
                            ModalHelper.DataType.ConfirmDelete, function () {
                                t.closest('.item').remove();
                                //refresh sortable
                                RefreshMainSlide();
                                ModalHelper.mainFunction.CloseConfirmWindow();
                            });
                    }
                });

                //bind dropdown article
                var sel = appended.find('select.select2');
                if (sel.length > 0) {
                    sel.removeClass('ignore');
                    BootStrap_NET.ExtraDropdown.mainFunction.initForElement(sel);
                }

                //bind radio
                var rdoColl = appended.find("input:radio[data-toggle='toggle']");
                if (rdoColl.length > 0)
                    rdoColl.removeClass('ignore').bootstrapSwitch({
                        size: 'mini',
                        onSwitchChange: function () {
                            $(this).closest('div.item').attr('data-rdo', $(this).val());
                        }
                    });

                //bind datetime
                var txtdate = appended.find('[data-datetimepicker="true"]');
                if (txtdate.length > 0) {
                    txtdate.removeClass('ignore');
                    BootStrap_NET.ExtraDateTimePicker.mainFunction.initForElement(txtdate);
                }

                //bind select
                var btnsel = appended.find('.sel');
                if (btnsel.length > 0) {
                    btnsel.click(function () {
                        if ($(this).hasClass('active') === false) {
                            var act = $('div[id^="selmedia"] .sel.active');
                            if (act.length > 0)
                                act.removeClass('active');
                            $(this).addClass('active');
                        }

                        var mds = $('#modalselect');
                        mds.find('input:text').val('');
                        mds.find('input[name="groupmediatype"]').bootstrapSwitch('state', true, false);
                        mds.modal('show');
                    });
                }

                //bind remove select
                var btndel = appended.find('.del');
                if (btndel.length > 0) {
                    btndel.click(function () {
                        btndel.parent().children('.sel').attr('data-action', '0')
                        .text(objAction['0']);
                        $(this).addClass('hide');
                        $(this).closest('div[id^="selmedia"]')
                            .children('.img-holder').addClass('hide').find('img').attr('src', '');
                    });
                }

                var chkBlackColl = appended.find("input:checkbox[data-toggle='toggle']");
                if (chkBlackColl.length > 0)
                    chkBlackColl.removeClass('ignore').bootstrapSwitch({
                        size: 'mini'
                    });

            }
        }
    }
}

function CreateSub(articleid, articletitle) {
    if (typeof templateSub !== 'undefined' && templateSub.length > 0) {
        var mainsub = GetMainSub();
        if (mainsub.length > 0) {
            var countChild = mainsub.children().length;
            var html = Mustache.render(templateSub, {
                indx: countChild + 1, id: uniqueID(),
                articleid: articleid,
                articletitle: articletitle
            });
            mainsub.append(html);

            //refresh sortable
            RefreshMainSub();

            //bind data
            var appended = mainsub.find(' > div:last');
            if (appended.length > 0) {
                //bind delete
                appended.find('.remove').click(function (e) {
                    e.preventDefault();
                    var t = $(this);
                    if (typeof ModalHelper !== 'undefined') {
                        ModalHelper.mainFunction.OpenModalWindow('', 'Are you sure to delete this story ?',
                            ModalHelper.DataType.ConfirmDelete, function () {
                                t.closest('.item').remove();
                                //refresh sortable
                                RefreshMainSub();

                                //console.log('close');
                                ModalHelper.mainFunction.CloseConfirmWindow();
                            });
                    }
                });

                //bind dropdown article
                var sel = appended.find('select.select2');
                if (sel.length > 0) {
                    sel.removeClass('ignore');
                    BootStrap_NET.ExtraDropdown.mainFunction.initForElement(sel);
                }
            }
        }
    }
}

function CreateEditor(title, articleid, articletitle, active,
    imgsrc, linktarget, externalurl) {
    if (typeof templateEditor !== 'undefined' && templateEditor.length > 0) {
        var maineditor = GetMainEditor();
        if (maineditor.length > 0) {
            var countChild = maineditor.children().length;
            var chk = '';
            if (active) {
                if (active === true)
                    chk = ' checked="checked"';
            }
            else
                chk = ' checked="checked"';

            var iscreate = true;
            if ((articleid || externalurl) && title && imgsrc)
                iscreate = false;

            var html = Mustache.render(templateEditor, {
                indx: countChild + 1, id: uniqueID(),
                articleid: articleid,
                articletitle: articletitle,
                title: title,
                target: linktarget,
                url: externalurl,
                src: imgsrc,
                checked: chk
            });
            maineditor.append(html);

            //refresh sortable
            RefreshMainEditor();

            //bind data
            var appended = maineditor.find(' > div:last');
            if (appended.length > 0) {
                //bind delete
                appended.find('.remove').click(function (e) {
                    e.preventDefault();
                    var t = $(this);
                    if (typeof ModalHelper !== 'undefined') {
                        ModalHelper.mainFunction.OpenModalWindow('', 'Are you sure to delete this item ?',
                            ModalHelper.DataType.ConfirmDelete, function () {
                                t.closest('.item').remove();
                                //refresh sortable
                                RefreshMainEditor();

                                //console.log('close');
                                ModalHelper.mainFunction.CloseConfirmWindow();
                            });
                    }
                });

                //bind checkbox
                var chk = appended.find("input[data-toggle='toggle']");
                if (chk.length > 0)
                    chk.removeClass('ignore').bootstrapSwitch({
                        size: 'mini',
                        onSwitchChange: function (ev, state) {
                            $(this).closest('.item').addClass('changed');
                        }
                    });

                var btnedit = appended.find('.btnedit');
                if (btnedit.length > 0) {
                    btnedit.click(function () {
                        if ($(this).hasClass('active') === false) {
                            var act = $('div[id^="edp"] .btnedit.active');
                            if (act.length > 0)
                                act.removeClass('active');
                            $(this).addClass('active');
                        }

                        var mds = $('#modaleditorpicker');

                        //bind data
                        var div = $(this).closest('div[id^="edp"]');
                        if (div.length > 0) {
                            var imgsrc = div.find('img').attr('src') || '';
                            mds.find('#txtepimage').val(imgsrc);

                            //title
                            var divtitle = div.find('.text');
                            mds.find('#txtEditorTitle').val($.trim(divtitle.text() || ''));

                            var target = divtitle.attr('data-target');
                            if (target)
                                mds.find('#ddloption').val(target).trigger('change');
                            else
                                mds.find('#ddloption').val('0').trigger('change');

                            //select url
                            var url = divtitle.attr('data-url') || '';
                            if (url.length > 0) {
                                mds.find('#txtexternal').val(url);
                                mds.find('#dlarticle').val('').trigger('change');
                                mds.find('#ddltarget').val('1').trigger('change');
                            }
                            else {
                                mds.find('#ddltarget').val('0').trigger('change');
                                setTimeout(function () {
                                    var aid = divtitle.attr('data-id') || '';
                                    var dataText = $.trim(divtitle.attr('data-text') || '');

                                    mds.find('#dlarticle').empty()
                                        .append('<option value="' + aid + '">' +
                                              (dataText || 'undefined') + '</option>')
                                        .val(aid).trigger('change');

                                    mds.find('#txtexternal').val('');
                                }, 400);
                            }
                        }

                        mds.modal('show');
                    });

                    if (iscreate === true) {
                        var mds2 = $('#modaleditorpicker');
                        mds2.find('#dlarticle').val('').trigger('change');
                        btnedit[0].click();
                    }
                    else
                        appended.removeClass('hide');
                }
            }
        }
    }
}


var templateArticle = undefined;
$(function () {
    templateArticle = $.trim($("#templatearticle").html());
});

function subFormatResult(data) {
    //console.log('subFormatResult data : ', data);
    if (typeof data.loading !== 'undefined' && data.loading === true)
        return '<span class="select2-match"></span>' + 'Loading...';

    if (typeof data.text !== 'undefined') {
        //var obj = {
        //    src: data.image, title: data.text,
        //    price: data.price, priceold: data.priceold
        //};
        return Mustache.render(templateArticle, data);
    }
}

function subFormatSelection(data) {
    //return data.id + ' - ' + (data.text || '');
    return (data.text || '');
}

function escapeMarkup(markup) {
    //console.log(markup);
    return markup;
};

var uniqueID = function () {
    // Math.random should be unique because of its seeding algorithm.
    // Convert it to base 36 (numbers + letters), and grab the first 9 characters
    // after the decimal.
    return '_' + Math.random().toString(36).substr(2, 9);
};


var GetObjectSlideForSave = function () {
    var arr = [];

    var mainslide = GetMainSlide();
    if (mainslide.length > 0) {
        var iserror = false;
        var articleid = '';
        var immediatedate = false;
        var publish = '';
        var media = undefined;
        $.each(mainslide.children(), function () {
            if (iserror === true)
                return false;

            //check validate media and article
            media = $(this).find('div[id^="selmedia"] img');
            articleid = '';
            if (media.length > 0 && (media.attr('src') || '').length > 0) {
                articleid = $(this).find('select[id^="ddlslide"]').val() || '';

                if (articleid.length > 0) { }
                else {
                    iserror = true;
                    return false;
                }
            }
            else {
                articleid = $(this).find('select[id^="ddlslide"]').val() || '';

                if (articleid.length > 0) {
                    media = $(this).find('div[id^="selmedia"] img');

                    if (media.length > 0 && (media.attr('src') || '').length > 0) { }
                    else
                        iserror = true;
                }
                else
                    iserror = true;
            }

            //console.log(media, articleid, iserror);

            if (iserror === false) {
                publish = '';
                immediatedate = $(this).attr('data-rdo') || '0';

                if (immediatedate === '1') {
                    var da = BootStrap_NET.ExtraDateTimePicker.mainFunction.getHiddenDate($(this).find('input[id^="date"]'));
                    if (da) {
                        publish = da.toJSON();
                    }
                    else
                        iserror = true;
                }

                if (iserror === true) {
                    $(this).addClass('has-error');
                    return false;
                }
                else {
                    $(this).removeClass('has-error');
                    arr.push({
                        aid: articleid,
                        pd: publish,
                        black: $(this).find('div[id^="colortext"] input:checkbox').is(':checked') === true ? '1' : '0',
                        type: media.attr('data-type') || '',
                        imgsrc: media.attr('data-imgsrc') || '',
                        videosrc: media.attr('data-videosrc') || '',
                        videothumb: media.attr('data-videothumb') || '',
                    });
                }
            }
        });

        if (iserror === true)
            return undefined;
        else
            return arr;
    }
}

var GetObjectSubForSave = function () {
    var arr = [];

    var mainsub = GetMainSub();
    if (mainsub.length > 0) {
        var iserror = false;
        var articleid = '';
        $.each(mainsub.children(), function () {
            if (iserror === true)
                return false;

            //check validate article
            articleid = $(this).find('select[id^="ddlsub"]').val() || '';
            if (articleid.length > 0) { }
            else
                iserror = true;

            if (iserror === true) {
                $(this).addClass('has-error');
                return false;
            }
            else {
                $(this).removeClass('has-error');
                arr.push({
                    aid: articleid
                });
            }
        });

        if (iserror === true)
            return undefined;
        else
            return arr;
    }
}

var GetObjectEditorForSave = function () {
    var arr = [];

    var maineditor = GetMainEditor();
    if (maineditor.length > 0) {
        var iserror = false;
        var articleid = '';
        var url = '';
        var media = undefined;
        $.each(maineditor.children(), function () {
            if (iserror === true)
                return false;

            var divtext = $(this).find('div.text');
            if (divtext.length > 0) {

                //check validate media and article
                media = $(this).find('img').attr('src') || '';
                if (media.length > 0) {
                }
                else
                    iserror = true;

                url = '';
                articleid = divtext.attr('data-id') || '';
                if (articleid.length > 0) {
                }
                else {
                    url = divtext.attr('data-url') || '';
                    if (url.length === 0)
                        iserror = true;
                }
                //console.log(articleid,url,iserror);

                if (iserror === true) {
                    $(this).addClass('has-error');
                    return false;
                }
                else {
                    $(this).removeClass('has-error');
                    arr.push({
                        aid: articleid,
                        imgsrc: media,
                        title: $.trim(divtext.text()),
                        target: divtext.attr('data-target') || '1',
                        exturl: url,
                        isactive: $(this).find('input:checkbox').is(':checked') ? '1' : '0'
                    });
                }
            }
        });

        if (iserror === true)
            return undefined;
        else
            return arr;
    }
}

var setImageUrl = function (txtid, url) {
    //console.log(txtid, url);
    if (txtid.indexOf('txtmimage') >= 0)
        $('#' + txtid).val(ConvertToSmall(url));
    else
        $('#' + txtid).val(url);
};

function ChangeTarget() {
    //console.log(this);
    var val = $('#ddltarget').val();
    if (val === '0') {
        $('#divtarget .selart').removeClass('hide').find('select').val('').trigger('change');
        $('#divtarget .enterurl').addClass('hide');
    }
    else if (val === '1') {
        $('#divtarget .selart').addClass('hide');
        $('#divtarget .enterurl').removeClass('hide').val('');
    }
}

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

var resizeBox = function () {
    var obj = $.lightbox();
    if (typeof obj !== 'undefined') {
        //var ws = getWindowSize();
        if (obj.visible === true) {
            var iframe = $.lightbox().esqueleto.html.find('iframe');
            if (iframe.length > 0) {
                var ws = getWindowSize();
                obj.resize(ws.width - 30, ws.height - 30);
            }
        }
    }
}

$(window).resize(function () {
    delay(resizeBox, 500);
});

var getWindowSize = function () {
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
};

function ConvertToSmall(url) {
    if (url && url.length > 0) {
        if (url.indexOf('www') === 0 || url.indexOf('http') === 0)
            return url;
        if (url.toLowerCase().indexOf('/small') >= 0)
            return url;
        else {
            var folder = url.substring(0, url.lastIndexOf('/'));
            var file = url.substring(url.lastIndexOf('/'));
            return folder + '/small' + file;
        }
    }
}

function ConvertToLarge(url) {
    if (url && url.length > 0) {
        if (url.indexOf('www') === 0 || url.indexOf('http') === 0)
            return url;
        if (url.toLowerCase().indexOf('/small') < 0)
            return url;
        else {
            var folder = url.substring(0, url.lastIndexOf('/'));
            folder = folder.substring(0, folder.lastIndexOf('/'));
            var file = url.substring(url.lastIndexOf('/'));
            return folder + file;
        }
    }
}

function SaveObject() {
    var objslide = GetObjectSlideForSave();
    if (typeof objslide === 'undefined') {
        return false;
    }

    var objsub = GetObjectSubForSave();
    if (typeof objsub === 'undefined') {
        return false;
    }

    var objeditor = GetObjectEditorForSave();
    if (typeof objeditor === 'undefined') {
        return false;
    }

    var isvalid = $('#mainform .validationEngineContainer').validationEngine('validate');
    if (isvalid === true) {
        //save to hidden field
        $('input:hidden[id$="hdfSetting"]').val(
            JSON.stringify(objslide) + joinKey + JSON.stringify(objsub)
            + joinKey + JSON.stringify(objeditor)
            );

        var btn = $('input:submit[id$="btnSave"]');
        if (btn.length > 0)
            btn[0].click();
    }
}