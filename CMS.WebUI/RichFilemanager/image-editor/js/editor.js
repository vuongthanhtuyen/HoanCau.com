/// <reference path="jquery-1.11.2.js" />
/// <reference path="jquery.qtip.js" />
/// <reference path="underscore-min.js" />
/// <reference path="jquery.easing.1.3.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.thumbGallery.js" />
if (typeof SweetSoftScript === 'undefined')
    SweetSoftScript = {};

SweetSoftScript.Editor = {
    Data: {
        sessionFile: '',
        ajRequest: undefined,
        arrImage: [],
        isPreload: true,
        tip: undefined,
        mainDiv: undefined,
        thumbElement: undefined,
        thumbsetting: '&maxwidth=100&maxheight=100'
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
        buildAlert: function (str, type) {
            var alertDiv = $('<div class="alert alert-' + type + ' alert-dismissible mt-md" role="alert">\
  <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>'+
            str + '</div>');
            alertDiv.appendTo($('#alertbox'));

            if ($('#alertbox').is(':hidden'))
                $('#alertbox').slideDown('normal');

            alertDiv.on('closed.bs.alert', function () {
                if ($('#alertbox').children().length === 0)
                    $('#alertbox').slideUp();
            });
            setTimeout(function () {
                alertDiv.find('[data-dismiss]').trigger('click');
            }, 5000);
        }
    },
    mainFunction: {
        callSlider: function () {
            $('#componentWrapper').thumbGallery({
                /* GENERAL */

                ic_thumb_forward: 'img/thumb_forward.png',
                ic_thumb_forward_on: 'img/thumb_forward_on.png',
                ic_thumb_backward: 'img/thumb_backward.png',
                ic_thumb_backward_on: 'img/thumb_backward_on.png',

                /*layoutType: grid/line */
                layoutType: 'line',
                /*thumbOrientation: horizontal/vertical */
                thumbOrientation: 'horizontal',
                /*moveType: scroll/buttons */
                moveType: 'buttons',
                /*scrollOffset: how much to move scrollbar and scrolltrack off the content (enter 0 or above) */
                scrollOffset: 0,

                /* GRID SETTINGS */
                /*verticalSpacing:  */
                verticalSpacing: 10,
                /*horizontalSpacing:  */
                horizontalSpacing: 10,
                /*buttonSpacing: button spacing from the grid itself */
                buttonSpacing: 10,
                /*direction: left2right/top2bottom (direction in which boxes are listed) */
                direction: 'left2right',

                /* INNER SLIDESHOW */
                /*innerSlideshowDelay: slideshow delay for inner items in seconds, random value between: 'min, max', 
                enter both number the same for equal time delay like for example 2 seconds: '2,2' */
                innerSlideshowDelay: [2, 4],
                /*innerSlideshowOn: autoplay inner slideshow, true/false */
                innerSlideshowOn: false
            });
        },
        processAddItem: function (url) {
            var title = SweetSoftScript.Editor.commonFunction.getDataQuery('f', url);
            /*just get filename
            if (title != null && title.length > 0)
            {
                if (title.lastIndexOf('/') >= 0)
                    title = title.substring(title.lastIndexOf('/') + 1);
            }
            */
            if (typeof customTitle !== 'undefined')
                title = customTitle;

            var div = $('<div class="thumbHolder">'
                    + '<a class="pp_content" data-img="' + url.replace(SweetSoftScript.Editor.Data.thumbsetting, '')
                    + '" href="javascript:void(0);" title="' + title + '">'
                    + '<span class="helper"></span><img class="thumb_hidden" src="img/picture.png" alt="' + title + '"></a>'
                    + '</div>');

            SweetSoftScript.Editor.mainFunction.initElement(div.find('a.pp_content'));
            var api = $('#componentWrapper').data('thumbGallery');
            if (typeof api !== 'undefined') {
                //var num = Math.floor(Math.random() * 12) + 1;                
                $('#componentWrapper').data('thumbGallery').addHorizontalItem(div);
            }
            else {
                $('#componentWrapper .thumbInnerContainer').append(div);
                SweetSoftScript.Editor.mainFunction.callSlider();
            }
        },
        viewImage: function (elem) {
            SweetSoftScript.Editor.Data.thumbElement = elem;
            var src = elem.attr('data-img') + '&numrandom=' + (Math.floor(Math.random() * 40 + 1))
            SweetSoftScript.Editor.Data.mainDiv.ImageStudio({
                url: src.replace(SweetSoftScript.Editor.Data.thumbsetting, '')
            });
        },
        initElement: function (elem) {
            $(elem)
                .attr('data-original-title', $(elem).attr('title'))
                .removeAttr('title')
                .mouseenter(function () {
                    SweetSoftScript.Editor.Data.tip
                        .qtip('option', 'position.target', this)
                        .qtip('option', 'position.my', 'bottom center')
                        .qtip('option', 'position.at', 'top center')
                        .qtip('option', 'hide.target', $('#componentWrapper .thumbContainer'))
                        .qtip('option', 'content.text', $(this).attr('data-original-title'))
                        .qtip('show');
                })
                .click(function () {
                    $('#fileupload').parent().slideUp();
                    if (SweetSoftScript.Editor.Data.isPreload === false) {
                        if ($(this).hasClass('active'))
                            return false;

                        if (typeof $(this).attr('data-img') !== 'undefined') {
                            var act = $('#componentWrapper .thumbInnerContainer .pp_content.active');
                            if (act.length > 0) {
                                act.removeClass('active');
                                act.find('img').attr('src', act.attr('data-img') + SweetSoftScript.Editor.Data.thumbsetting);
                            }
                            $(this).addClass('active');

                            SweetSoftScript.Editor.mainFunction.viewImage($(this));
                        }
                    }
                    return false;
                });
        },
        queueLoad: function (indx) {
            if (typeof indx === 'number' && indx < SweetSoftScript.Editor.Data.arrImage.length) {
                var src = 'ImageHandler.aspx?f=' + SweetSoftScript.Editor.Data.arrImage[indx] + SweetSoftScript.Editor.Data.thumbsetting;
                SweetSoftScript.Editor.mainFunction.processAddItem(src);

                SweetSoftScript.Editor.commonFunction.preloadLoadImage(src,
                    function (img) {
                        if (typeof img !== 'undefined' && img !== null) {
                            var cur = $('#componentWrapper .thumbInnerContainer .pp_content img:last');
                            if (cur.length > 0)
                                cur.attr('src', src);

                            setTimeout(function () {
                                SweetSoftScript.Editor.mainFunction.queueLoad(++indx);
                            }, indx == SweetSoftScript.Editor.Data.arrImage.length - 2 ? 800 : 10);
                        }
                        else {
                            setTimeout(function () {
                                SweetSoftScript.Editor.mainFunction.queueLoad(++indx);
                            }, indx == SweetSoftScript.Editor.Data.arrImage.length - 2 ? 800 : 10);
                        }
                    });
            }
            else {
                SweetSoftScript.Editor.Data.isPreload = false;
                if (SweetSoftScript.Editor.Data.arrImage.length === 1) {
                    $('#componentWrapper .thumbInnerContainer .pp_content:eq(0)').click();

                    /*good
                   setTimeout(function () {
                       if (typeof ISDataResize !== 'undefined' && ISDataResize.length === 1)
                           $('#ResizePane select')[0].selectedIndex = 0;
                       if (typeof ISDataCrop !== 'undefined' && ISDataCrop.length === 1)
                           $('#CropPane select')[0].selectedIndex = 0;
                   }, 600);
                   */


                    setTimeout(function () {
                        if (typeof nofile === 'undefined' || SweetSoftScript.Editor.Data.arrImage[0] !== 'session') {
                            if (typeof ISDataCrop !== 'undefined' && ISDataCrop.length === 1) {
                                $('#CropPane select')[0].selectedIndex = 0;
                                $('#CropPane .button_crop_crop').click();
                            }
                        }
                        else
                            $('#CropPane .button_crop_crop,#SavePane button').attr('disabled', 'disabled');
                    }, 1000);

                }
            }
        },
        initToolbar: function () {
            SweetSoftScript.Editor.Data.tip = $("#qtip").qtip({
                position: {
                    target: [0, 0],
                    my: 'right center',
                    at: 'left center',
                    adjust: {
                        //x: -10
                    }
                },
                prerender: true,
                //hide: { event: 'mouseleave', target: $('#sidebar > li') },
                content: {
                    text: ' '
                },
                style: { classes: 'qtip-green qtip-shadow qtip-rounded' }
            });

            /*
            $('#sidebar > li').each(function () {
                $(this).mouseenter(function () {
                    SweetSoftScript.Editor.Data.tip
                        .qtip('option', 'position.target', this)
                        .qtip('option', 'position.my', 'right center')
                        .qtip('option', 'position.at', 'left center')
                        .qtip('option', 'hide.target', $('#sidebar'))
                        .qtip('option', 'content.text', $(this).attr('data-original-title'))
                        .qtip('show');
                });
            });
            */
        },
        init: function () {

            SweetSoftScript.Editor.mainFunction.initToolbar();
            SweetSoftScript.Editor.Data.mainDiv = $('#main');

            var data = SweetSoftScript.Editor.commonFunction.getDataQuery('d');
            if (data.length > 0) {
                try {
                    SweetSoftScript.Editor.Data.arrImage = data.split(joinKey);
                }
                catch (ex) {
                    SweetSoftScript.Editor.Data.arrImage = undefined;
                }

                if (typeof SweetSoftScript.Editor.Data.arrImage !== 'undefined'
                    && SweetSoftScript.Editor.Data.arrImage.length > 0) {

                    var defcropratios = [[0, "Custom"], ["current", "Current"], [4 / 3, "4:3"], [16 / 9, "16:9 (Widescreen)"], [3 / 2, "3:2"]];
                    if (typeof ISDataCrop !== 'undefined' && ISDataCrop.length > 0) {
                        defcropratios = [];
                        $.each(ISDataCrop, function (i, o) {
                            defcropratios.push([parseInt(o[1]) === 0 ? 0 : (parseInt(o[0]) / parseInt(o[1]) || 0),
                                o.length > 2 ? o[2] : 'Untitled',
                                (o[0] !== '0' && o[1] !== '0') ? (o[0] + ' x ' + o[1]) : '']);
                        });
                    }

                    var panesCustom = ['crop', 'resize', 'effects', 'rotateflip', 'adjust', 'trimwhitespace'];
                    var key = SweetSoftScript.Editor.commonFunction.getDataQuery('editorKey');
                    if (key.length > 0)
                        panesCustom.splice(1);

                    var mergeOpt = {
                        url: '',
                        cropratios: defcropratios,
                        defaultImg: 'img/file-picture.png',
                        panelId: '#accpanel',
                        panes: panesCustom,
                        loadingElement: '#main .col-sm-9 > .img-thumbnail > .indicator',
                        imageElement: '#main .col-sm-9 > .img-thumbnail',
                        panelActiveIndex: 0,
                        onchange: function (api) {
                            var url = api.getStatus().url;
                            //var obj = ir.Utils.parseQuery($('<span></span>').html(url).text());
                            if (typeof SweetSoftScript.Editor.Data.thumbElement !== 'undefined')
                                SweetSoftScript.Editor.Data.thumbElement.find('img').attr('src', url + SweetSoftScript.Editor.Data.thumbsetting);
                        },
                        onsave: function () {
                            if (typeof parent.SweetSoftScript !== 'undefined'
                                && typeof parent.SweetSoftScript.Editor !== 'undefined'
                                && typeof parent.SweetSoftScript.Editor.Data.needRefresh !== 'undefined')
                                parent.SweetSoftScript.Editor.Data.needRefresh = true;
                            else
                                parent.needRefresh = true;
                        }
                    }

                    if (typeof ISText !== 'undefined')
                        $.extend(mergeOpt, ISText);

                    if (typeof ISDataResize !== 'undefined') {
                        var arrResize = [];
                        $.each(ISDataResize, function (i, o) {
                            arrResize.push([o[0], o[1], (ISDataCrop[i][0] !== '0' && ISDataCrop[i][1] !== '0') ? (ISDataCrop[i][0] + ' x ' + ISDataCrop[i][1]) : '']);
                        });
                        mergeOpt.resizeSetting = arrResize;
                    }

                    if (typeof custompanes !== 'undefined')
                        mergeOpt.panes = custompanes;

                    SweetSoftScript.Editor.Data.mainDiv.ImageStudio(mergeOpt);


                    SweetSoftScript.Editor.Data.isPreload = true;
                    SweetSoftScript.Editor.mainFunction.queueLoad(0);
                }
            }
        }
    }
}

jQuery(function ($) {
    SweetSoftScript.Editor.mainFunction.init();
});
