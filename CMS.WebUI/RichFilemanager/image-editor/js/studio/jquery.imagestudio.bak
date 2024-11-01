/// <reference path="../bootstrap.js" />
/// <reference path="../bootstrap-colorpicker.js" />
/// <reference path="../jquery-1.11.2.js" />

/*This software is MIT licensed.

Copyright (c) 2012 Imazen LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

(function ($) {

    //Polling methods
    //$('obj').ImageStudio('api').getStatus({'restoreSuspendedCommands':true, 'removeEditingConstraints':true, 'useEditingServer':false} ) returns { url, path, query };
    //$('obj').ImageStudio('api').setOptions({height:600});
    //$('obj').ImageStudio('api').setOptions({url:'newimageurl.jpg'}); //Yes, you can switch images like this.. as long as you're not in the middle of cropping. That's not supported yet.
    //$('obj').ImageStudio('api').getOptions();
    //$('obj').ImageStudio('api').destroy();
    //labels and icon values cannot be updated after initialization. 
    var defaults = {
        url: null, //The image URL to load for editing. 
        width: null, //To set the width of the area
        accordionWidth: null,
        panelId: null,
        defaultImg: null,
        height: 560, //To constrain the height of the area.
        panes: ['rotateflip', 'crop', 'adjust', 'effects', 'resize', 'advanced', 'trimwhitespace'/*, 'carve', 'redeye', 'faces'*/], //A list of panes to display, in order. 
        panelActiveIndex: 0,
        loadingElement: null,
        editingServer: null, //If set, an alternate server will be used during editing. For example, using cloudfront during editing is counter productive
        editWithSemicolons: null, //If true, semicolon notation will be used with the editing server. 
        finalWithSemicolons: null, //If true, semicolons will be used in the final URLs. Defaults to true if the input URL uses semicolons.
        //A list of commands to temporarily remove from the URL during editing so that position-dependent operations aren't affected.
        //Any commands used by the editor should be in here also, such as 'cache', 'memcache', 'maxwidth',and 'maxheight'
        suspendKeys: ['width', 'height', 'maxwidth', 'maxheight',
                       'scale', 'rotate', 'flip', 'anchor',
                       /*'paddingwidth', 'paddingcolor', 'borderwidth', 'bordercolor', 'margin',*/
                       'cache', 'scache', 'process',/*'shadowwidth', 'shadowcolor',*/ 'shadowoffset', 'mode'],
        editingCommands: { cache: 'no', scache: 'mem' },
        onchange: null, //The callback to fire whenever an edit occurs.
        onsave: null, //The callback to fire whenever save new file.
        cropratios: [[0, "Custom"], ["current", "Current"], [4 / 3, "4:3"], [16 / 9, "16:9 (Widescreen)"], [3 / 2, "3:2"]],
        cropPreview: { width: '100%', height: '175px'/*, 'margin-left': '-15px'*/ },
        resizeSetting: []
    };
    /* Coding style notes:
    Within each pane, a closure object name 'cl' is used to track state within the pane.
    Each pane function is passed a reference to the instance-wide options object, which can be used to modify the image URL, lock/unlock the accordion, etc.
    */

    $.fn.ImageStudio = function (options) {
        var processOptions = function (options) {
            var defs = $.extend(true, {}, defaults);
            var defData = $.fn.ImageStudio.prototype.getDefaultResource();
            //console.log('defData : ', defData);
            //defs = $.extend(true, defs, defData);

            defs.labels = $.extend(true, defData.labels, options.labels);
            defs.icons = $.extend(true, defData.icons, options.icons);
            if (options.panes)
                defs.panes = options.panes;
            if (options.cropratios)
                defs.cropratios = null;

            return $.extend(true, defs, options);
        };

        var result = this;

        this.each(function () {
            var div = $(this);

            if (div.data('ImageStudio')) {
                // The API can be requested this way (undocumented)
                if (options === 'api') {
                    result = div.data('ImageStudio');
                    return;
                }
                    // Otherwise, we just reset the options...
                else div.data('ImageStudio').setOptions(options);
            } else {
                div.data('ImageStudio', init(div, processOptions(options)));
            }
        });

        return result;
    };

    $.fn.ImageStudio.prototype.getDefaultResource = function () {
        //console.log('getDefaultResource');
        return {
            icons: {
                rotateleft: 'ico-rotate',
                rotateright: 'ico-rotate2',
                flipvertical: 'ico-flip',
                fliphorizontal: 'ico-flip2',
                reset: ' ico-refresh',
                autofix: 'ico-wrench4',
                autowhite: ' ico-wrench',
                blackwhite: 'ico-chess',
                sepia: 'ico-image3',
                negative: 'ico-image'
            },
            labels: {
                //rotate
                pane_rotateflip: 'Rotate &amp; Flip',
                rotateleft: 'Rotate Left',
                rotateright: 'Rotate Right',
                flipvertical: 'Flip Vertical',
                fliphorizontal: 'Flip Horizontal',
                reset: 'Reset',
                //crop
                pane_crop: 'Crop',
                aspectratio: 'Aspect Ratio',
                crop_crop: 'Crop',
                crop_modify: 'Modify Crop',
                crop_cancel: 'Cancel',
                crop_done: 'Done',
                //Adjust
                pane_adjust: 'Adjust Image',
                autofix: 'Auto-Fix',
                autowhite: 'Auto-Balance',
                contrast: 'Contrast',
                saturation: 'Saturation',
                brightness: 'Brightness',
                pane_effects: 'Effects &amp; Filters',
                blackwhite: 'Black & White',
                sepia: 'Sepia',
                negative: 'Negative',
                sharpen: 'Smart Sharpen',
                noiseremoval: 'Noise Removal',
                oilpainting: 'Oil Painting',
                posterize: 'Posterize',
                blur: 'Gaussian Blur',
                pane_redeye: 'Red-Eye Removal',
                redeye_auto: 'Auto-detect Eyes',
                redeye_start: 'Fix Red-Eye',
                redeye_preview: 'Toggle Preview',
                redeye_clear: 'Clear',
                pane_faces: "Face Selection",
                faces_auto: "Auto-detect Faces",
                faces_start: "Select Faces",
                faces_clear: "Clear",
                cancel: 'Cancel',
                done: 'Done',
                pane_carve: 'Object Removal',
                carve_start: 'Remove objects',
                carve_preview: 'Preview result',
                //save pane
                pane_save: 'Save',
                save: 'Save',
                saveas: 'Save as...',
                save_Tilte: 'Save new file',
                save_Label: 'Save with a name:',
                save_close_btn: 'Close',
                save_btn: 'Save',
                //resize pane
                pane_resize: 'Resize',

                pane_advanced: 'Advanced',
                border_width: 'Border width',
                border_color: 'Border color',
                margin: 'Margin',
                padding_width: 'Padding width',
                padding_color: 'Padding color',
                bg_color: 'Background color',
                //whitespace trim
                pane_trimwhitespace: 'Trim whitespace',
                //whitespacetrim_title: 'Trim whitespace',
                whitespacetrim_threshold: 'Threshold',
                whitespacetrim_percent: 'Percent padding',
            }
        }
    }

    function init(div, opts) {

        var api = {
            getOptions: function () { return opts; },
            setOptions: function (newOpts) {
                if (typeof newOpts === 'string') {
                    if (newOpts === 'setLoading') {
                        api.setloading(opts, true);
                    }
                }
                else
                    updateOptions(newOpts);
            },
            setMarginTop: function () {
                var he = opts.img.height();
                var mh = opts.img.parent().height();
                //console.log('opts.img.height : ', he);
                //console.log('opts.img.parent().height : ', mh);
                var dif = mh - he;
                opts.img.css('margin-top', (dif / 2) + 'px');
            },
            getStatus: function (params) {
                params = $.extend(params, { 'restoreSuspendedCommands': true, 'removeEditingConstraints': true, 'useEditingServer': false });
                var path = params.useEditingServer ? opts.editPath : opts.original.path;

                var q = new ImageResizer.Instructions(opts.editQuery);
                if (params.removeEditingConstraints) {
                    q.remove(opts.suspendKeys);
                }
                if (params.restoreSuspendedCommands) q.mergeWith(opts.suspendedItems);

                var url = params.useEditingServer ? q.toQueryString(opts.editWithSemicolons) : q.toQueryString(opts.finalWithSemicolons);
                return { url: path + url, query: q, path: path };
            },
            destroy: function () {
                div.data('ImageStudio', null);
                div.removeClass("imagestudio");
                div.empty();
            },
            lockAccordion: function (opts, currentPaneDiv) {
                opts.accordion.addClass("disabled");
                currentPaneDiv.closest('.panel').addClass("panel-disabled");
            },
            unlockAccordion: function (opts, currentPaneDiv) {
                opts.accordion.removeClass("disabled");
                currentPaneDiv.closest('.panel').removeClass("panel-disabled");
            },
            setloading: function (opts, loading, stopOnImageLoad) {
                if (!opts.imageLoadedHandler) {
                    opts.imageLoadedHandler = function () {

                        if (opts.loadingElement !== null)
                            $(opts.loadingElement).removeClass('show');
                        else
                            opts.container.removeClass('imagestudio-loading');

                        if ($('#ResizePane').length > 0) {
                            if ($('#ResizePane').hasClass('in')) {
                                if ($('#ResizePane select').length > 0) {
                                    $('#ResizePane select').removeAttr('disabled').val('').multiselect('enable');
                                    //console.log(opts.url);
                                    $('#ResizePane .button_reset').attr('disabled', 'disabled');
                                    //if ($('#ResizePane select').val().length > 0)
                                    //    $('#ResizePane select').trigger('change');
                                }
                            }
                        }
                        if ($('#AdvancedPane').length > 0) {
                            if ($('#AdvancedPane input[type="text"].colorpicker-element').length > 0)
                                $('#AdvancedPane input[type="text"].colorpicker-element').colorpicker('enable');
                        }

                        api.unlockAccordion(opts, opts.accordion.find('.panel-collapse.in'));
                        api.setMarginTop();

                        opts.img.unbind('load', opts.imageLoadedHandler);
                    };
                }

                if (opts.loadingElement !== null)
                    $(opts.loadingElement).removeClass('show');
                else
                    opts.container.removeClass('imagestudio-loading');

                api.unlockAccordion(opts, opts.accordion.find('.panel-collapse.in'));

                opts.img.unbind('load', opts.imageLoadedHandler);
                if (loading) {

                    if (opts.loadingElement !== null)
                        $(opts.loadingElement).addClass('show');
                    else
                        opts.container.addClass('imagestudio-loading');

                    //console.log('lock');

                    api.lockAccordion(opts, opts.accordion.find('.panel-collapse.in'));

                    if (stopOnImageLoad) opts.img.bind('load', opts.imageLoadedHandler);
                }
            },
            resetToolbox: function () {
                opts.accordion.find('input[type="checkbox"]').prop('checked', false);
                $('#CropPane .button_crop_crop').text(opts.labels.crop_crop);
                $('#CropPane .button_reset').attr('disabled', 'disabled');
                if ($('#ResizePane select').length > 0)
                    $('#ResizePane select').val('').multiselect('refresh');
            }
        };
        opts.api = api;


        div = $(div);
        //div.empty();

        //div.removeClass("imagestudio");
        div.addClass("imagestudio");

        //Add accordion
        var a = $(opts.panelId).addClass("controls").width(opts.accordionWidth);

        //Add image
        var img = $('<img class="img-responsive" />').addClass("is-img").appendTo($(opts.imageElement));

        opts.img = img; //Save a reference to the image object in options
        opts.imgDiv = $(opts.imageElement);
        opts.container = div;
        opts.accordion = a;

        a.on('hide.bs.collapse', function (e) {
            if ($(this).hasClass('disabled')) {
                e.preventDefault();
                return false;
            }/*
            else {
                var active = $(this).find('.panel-collapse.in');
                if (active.attr('id') === 'ResizePane')
                    active.find('select').val('');
            }*/
        });
        a.on('show.bs.collapse', function (e) {
            if ($(this).hasClass('disabled')) {
                e.preventDefault();
                return false;
            }
        });

        a.on('shown.bs.collapse', function (e) {
            updateTooltip();
        });

        var updateTooltip = function () {
            var cur = $(opts.accordion).find('.panel > .in .hid-slider');
            if (cur.length > 0) {
                //refresh layout position tooltip
                if (typeof cur.data('slider') !== 'undefined') {
                    cur.slider('relayout');
                }
            }
        }

        var updateOptions = function (changedOpts) {
            //Called by both init and setOptions.
            var o = changedOpts;
            //When init calls, 'changedOpts' and 'opts' reference the same object
            var isUpdate = (o != opts);
            //See if we can skip the URL update.
            var skipUrlUpdate = isUpdate && (!o.url || o.url == opts.url);

            //If we're updating the URL, see if we are using semicolons, and set appropriate properties
            if (!skipUrlUpdate && (o.url && o.url.indexOf('?') < 0) && (o.url && o.url.indexOf(';') > -1) && (o.finalWithSemicolons === undefined || o.finalWithSemicolons == null)) o.finalWithSemicolons = true;
            if (o.finalWithSemicolons && _.all([o.editWithSemicolons, o.editingServer, opts.editWithSemicolons, opts.editingServer], function (v) { return v === null || v === undefined; }))
                o.editWithSemicolons = true;

            //If this is an update, not an init, override old values with new ones.
            if (isUpdate)
                $.extend(opts, o);

            //if (o.width) { div.width(o.width); }
            //if (o.height) div.height(o.height);
            //if (o.height) a.height(o.height);

            if (o.height) opts.imgDiv.height(o.height);
            opts.imgDiv.css('width', (o.width !== null && typeof o.width !== 'undefined') ? (o.width + (o.width.toString().indexOf('px') >= 0 ? '' : 'px')) : '100%');

            if (o.accordionWidth) {
                a.width(o.accordionWidth);
            }

            //console.log('skipUrlUpdate : ', skipUrlUpdate);

            if (!skipUrlUpdate) {
                opts.original = ImageResizer.Utils.parseUrl(opts.url);
                opts.editPath = (typeof opts !== 'undefined' && typeof opts.original !== 'undefined') ? opts.original.path : '';
                if (opts.editingServer) opts.editPath = ImageResizer.Utils.changeServer(opts.editPath, opts.editingServer);

                opts.originalQuery = (typeof opts !== 'undefined' && typeof opts.original !== 'undefined') ? opts.original.obj : null;
                opts.filteredQuery = new ImageResizer.Instructions(opts.originalQuery);
                opts.suspendedItems = opts.filteredQuery.remove(opts.suspendKeys);
                var withConstraints = new ImageResizer.Instructions(opts.filteredQuery);
                withConstraints.maxwidth = div.width() - (opts.accordionWidth || a.width()) - 30;
                withConstraints.maxheight = (opts.height !== null ? (opts.height - 28) : '');
                withConstraints.mergeWith(opts.editingCommands, true);
                opts.editQuery = withConstraints;
                opts.editUrl = opts.editPath + withConstraints.toQueryString(opts.editWithSemicolons);

                api.resetToolbox();

                if (opts.url) {
                    api.setloading(opts, true, true);
                    $('#ResizePane select').removeAttr('disabled').multiselect('enable');
                    //console.log('opts.editUrl : ', opts);
                    preloadLoadImage(opts.editUrl, function (imgdata) {
                        if (typeof imgdata !== 'undefined')
                            img.attr('src', opts.editUrl);
                    });
                }
                else {
                    var def = opts.defaultImg || '';
                    if (def.length > 0) {
                        //console.log('def : ', def);
                        preloadLoadImage(def, function (imgdata) {
                            if (typeof imgdata !== 'undefined')
                                opts.img.attr('src', def);

                            api.setMarginTop();
                        });
                    }
                    api.lockAccordion(opts, opts.accordion.find('.panel-collapse.in'));
                }
                //This event lets 'involved' panes like crop, object removal, faces, red-eye, etc. exit when we change the source image.
                img.triggerHandler('sourceImageChanged', [opts.url]);
                //This event keeps all sliders, toggles, etc in sync
                img.triggerHandler('query', [new ImageResizer.Instructions(opts.editUrl)]);
            }

        };
        updateOptions(opts);

        //Add requested panes
        var panes = {
            'rotateflip': addRotateFlipPane, 'crop': addCropPane,
            'adjust': addAdjustPane, 'redeye': addRedEyePane, 'carve': addCarvePane,
            'effects': addEffectsPane, 'faces': addFacesPane,
            'resize': addResizePane, 'advanced': addAdvancedPane, 'trimwhitespace': addTrimWhitespacePane
        };

        for (var i = 0; i < opts.panes.length; i++) {
            //console.log(opts.panes[i], opts.labels['pane_' + opts.panes[i]]);
            a.append(panes[opts.panes[i]](opts, opts.labels['pane_' + opts.panes[i]] || 'pane_' + opts.panes[i]));
        }
        if (opts.panes.length > 0)
            a.append(addSavePane(opts, opts.labels['pane_save']));

        //Activate accordion
        //Animated can be true, false or a string
        var animated = opts.animated && ((opts.animated.toLowerCase() === 'true') ? true : (opts.animated.toLowerCase() === 'false') ? false : opts.animated);
        //a.accordion({ fillSpace: true, animated: animated });

        if (opts.panelActiveIndex !== null) {
            a.find('.panel:eq(' + opts.panelActiveIndex + ') > .panel-collapse')
                .addClass('in').attr('aria-expanded', 'true');
            a.find('.panel:eq(' + opts.panelActiveIndex + ') .panel-title a')
            .removeClass('collapsed');
        }
        updateTooltip();

        //console.log(opts);
        return api;
    }

    //Because jquery's stupid ctor won't accept element arrays
    var $a = function (array) {
        var x = $();
        $.each(array, function (i, o) { x = x.add(o) });
        return x;
    };


    var preloadLoadImage = function (imgSrc, callback) {
        var imgPreload = new Image();
        imgPreload.src = imgSrc;
        //console.log(imgSrc);
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
    }

    //Provides a callback to edit the querystring inside
    var edit = function (opts, callback) {
        opts.editQuery = new ImageResizer.Instructions(opts.editQuery);
        callback(opts.editQuery);
        opts.editUrl = opts.editPath + opts.editQuery.toQueryString(opts.editWithSemicolons);
        opts.api.setloading(opts, true, true);
        //console.log('edit opts.editUrl: ', opts.editUrl);
        opts.img.attr('src', opts.editUrl);
        if (opts.img.prop('complete')) {
            //console.log('complete loaded');
            opts.api.setloading(opts, false);
            opts.api.setMarginTop();
        }
        opts.img.triggerHandler('query', [opts.editQuery]);
        if (opts.onchange != null) opts.onchange(opts.api);
    };

    var setUrl = function (opts, url, silent) {
        opts.editQuery = new ImageResizer.Instructions(url);
        opts.editUrl = url;
        opts.api.setloading(opts, true, true);
        //console.log('seturl opts.editUrl: ', opts.editUrl);

        preloadLoadImage(url, function (dataimg) {
            opts.img.attr('src', url);

            if (typeof dataimg !== 'undefined') {
                //console.log('opts.img loaded : ', opts.img);
                opts.api.setloading(opts, false);
                opts.api.setMarginTop();
            }
        });

        if (!silent) {
            opts.img.triggerHandler('query', [new ImageResizer.Instructions(opts.editQuery)]);
            if (opts.onchange != null) opts.onchange(opts.api);
        }
    }
    //Makes a button that edits the image's querystring.
    var button = function (opts, id, editCallback, clickCallback) {
        var icon = opts.icons[id];
        var ss = '<button type="button" role="button" aria-disabled="false">';

        if (icon != null && typeof icon !== 'undefined')
            ss += ('<i class="' + icon + '"></i> ');

        ss += opts.labels[id] || id;

        ss += '</button>';

        var b = $(ss).addClass('button_' + id + ' btn btn-default');

        if (editCallback) b.click(function (e) {
            if (opts.accordion.hasClass('disabled') && $(this).hasClass('is-ignorelock') === false) {
                e.stopPropagation();
                return false;
            }

            edit(opts, function (obj) {
                editCallback(obj);
            });
        });

        if (clickCallback) b.click(function (e) {
            if (opts.accordion.hasClass('disabled') && $(this).hasClass('is-ignorelock') === false) {
                e.stopPropagation();
                return false;
            }

            clickCallback();
        });
        return b;
    };
    var toggle = function (container, id, querystringKey, opts) {
        if (!window.uniqueId) window.uniqueId = (new Date()).getTime();
        window.uniqueId++;
        var ss = $('<span class="checkbox custom-checkbox">' +
                '<input type="checkbox" name="' + window.uniqueId + '" id="' + window.uniqueId + '">' +
                '<label for="' + window.uniqueId + '">&nbsp;&nbsp;' +
                opts.labels[id] + '</label>' + '</span>');

        var chk = ss.find('input[type="checkbox"]');
        chk.prop("checked", opts.editQuery.getBool(querystringKey));

        chk.change(function () {
            edit(opts, function (obj) {
                obj.toggle(querystringKey);
            });
        });

        opts.img.bind('query', function (e, obj) {
            /*
            var b = obj.getBool(querystringKey);
            if (chk.prop("checked") != b) chk.prop("checked", b);
            chk.button('refresh');
            */
        });

        ss.appendTo(container);
        return ss;
    };
    var slider = function (id, container, opts, min, max, step, key) {
        var supress = {};
        var startingValue = opts.editQuery[key];

        if (startingValue == null) startingValue = 0;

        var s = $("<div id='" + id + "' class='hid-slider'></div>")
            .appendTo(container).slider({
                min: min, max: max,
                step: step, value: startingValue
                //,change: function (event, ui) {
                //}
            }).on('slideStop', function (e) {
                if (opts.url.length > 0) {
                    $(this).parent().find('.button_reset').removeAttr('disabled');
                    supress[key] = true;
                    edit(opts, function (obj) {
                        //obj[key] = ui.value;
                        obj[key] = e.value;
                        if (key.charAt(0) === 'a') obj['a.radiusunits'] = 1000;
                        if (obj[key] === 0) delete obj[key];
                    });
                    supress[key] = false;
                }
            });
        opts.img.bind('query', function (e, obj) {
            if (supress[key]) return;
            var v = obj[key]; if (v == null) v = 0;
            if (v != s.slider('getValue')) {
                s.slider('setValue', v);
            }
        });

        return s;
    };
    var h3 = function (opts, id, container) {
        return $("<h4 />").text(opts.labels[id] || id).addClass(id).appendTo(container);
    };

    var freezeImage = function (opts) {
        //opts.imgDiv.css('padding-left', (opts.imgDiv.width() - opts.img.width()) / 2 + 1);
        //opts.imgDiv.css('text-align', 'left');
        //opts.imgDiv.height(opts.img.height());
        //opts.img.css('position', 'absolute');
    };
    var unFreezeImage = function (opts) {
        //opts.img.attr('style', ''); //Remove the position-abosolute stuff.
        //opts.imgDiv.css('padding-left', 0); //undo horizontal align fix
        //opts.imgDiv.css('text-align', 'center');
        //opts.imgDiv.css('height', 'auto');
    };

    var getFileName = function (opts) {
        if (typeof opts !== 'undefined' && typeof opts.editQuery !== 'undefined')
            return opts.editQuery.f.substring(opts.editQuery.f.lastIndexOf('/') + 1);
        else
            return '';
    }

    var getFileNameAndExtension = function (opts) {
        var name = getFileName(opts);
        if (typeof customTitle !== 'undefined')
            name = customTitle;
        if (name.length > 0) {
            return name.split('.');
        }
        else
            return [];
    }

    var createPanel = function (parentid, name, id) {
        var ss = '<div class="panel panel-default">' +
            //title
            '<div class="panel-heading"><h4 class="panel-title">' +
           '<a data-toggle="collapse" data-parent="' + parentid + '" href="#' + id + '" class="collapsed">' +
            '<span class="arrow mr5"></span>' + name + '</a></h4></div>';

        //body
        ss += '<div id="' + id + '" class="panel-collapse collapse"><div class="panel-body"></div></div>';

        ss += '</div>';
        return $(ss);
    }

    //Adds a pane for rotating and flipping the source image
    var addRotateFlipPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'RotateFlipPane');
        var body = $('<div class="btn-group-vertical col-md-12"></div>').appendTo(c.find('.panel-collapse > .panel-body'));

        button(opts, 'rotateleft', function (obj) { obj.increment("srotate", -90, 360); }).appendTo(body);
        button(opts, 'rotateright', function (obj) { obj.increment("srotate", 90, 360); }).appendTo(body);
        button(opts, 'flipvertical', function (obj) { obj.toggle("sflip.y"); }).appendTo(body);
        button(opts, 'fliphorizontal', function (obj) { obj.toggle("sflip.x"); }).appendTo(body);
        button(opts, 'reset', function (obj) { obj.resetSourceRotateFlip() }).appendTo(body);

        return c;
    };

    //contrast/saturation/brightness adjustment
    var addAdjustPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'AdjustPane');
        var body = c.find('.panel-collapse > .panel-body');
        var balancewhite = toggle(body, 'autowhite', "a.balancewhite", opts);
        h3(opts, 'contrast', body);
        slider('s-contrast', body, opts, -1, 1, 0.001, "s.contrast");
        h3(opts, 'saturation', body);
        slider('s-saturation', body, opts, -1, 1, 0.001, "s.saturation");
        h3(opts, 'brightness', body);
        slider('s-brightness', body, opts, -1, 1, 0.001, "s.brightness");
        button(opts, 'reset', function (obj) {
            obj.remove("s.contrast", "s.saturation", "s.brightness", "a.balancewhite");
            $a([balancewhite]).find('input[type="checkbox"]')
              .prop('checked', false).removeAttr('checked');
        }).css('margin-top', '15px').appendTo(body);
        return c;
    };

    //Effects and noise removal
    var addEffectsPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'EffectsPane');
        var body = $('<div class="form-horizontal"></div>').appendTo(c.find('.panel-collapse > .panel-body'));

        var grayscale = toggle(body, 'blackwhite', "s.grayscale", opts);
        var sepia = toggle(body, "sepia", "s.sepia", opts);
        var invert = toggle(body, "negative", "s.invert", opts);
        h3(opts, 'sharpen', body);
        slider('a-sharpen', body, opts, 0, 15, 1, "a.sharpen");
        h3(opts, 'noiseremoval', body);
        slider('a-removenoise', body, opts, 0, 100, 1, "a.removenoise");
        h3(opts, 'oilpainting', body);
        slider('a-oilpainting', body, opts, 0, 25, 1, "a.oilpainting");
        h3(opts, 'posterize', body);
        slider('a-posterize', body, opts, 0, 255, 1, "a.posterize");
        h3(opts, 'blur', body);
        slider('a-blur', body, opts, 0, 40, 1, "a.blur");
        button(opts, 'reset', function (obj) {
            obj.remove("a.sharpen", "a.removenoise", "a.oilpainting",
                "a.posterize", "s.grayscale", "s.sepia", "s.invert", "a.blur", "a.radiusunits");

            $a([grayscale, sepia, invert]).find('input[type="checkbox"]')
                .prop('checked', false).removeAttr('checked');

        }).css('margin-top', '15px').appendTo(body);
        return c;
    };

    //Object-remvoal (seam carving) pane
    var addCarvePane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'CarvePane');
        var body = c.find('.panel-collapse > .panel-body');

        var cl = {};
        cl.img = opts.img;
        cl.opts = opts;

        var start = button(opts, 'carve_start', null, function () {
            reset.hide(); start.hide();
            opts.api.lockAccordion(opts, body);
            var o = opts;
            var q = new ImageResizer.Instructions(o.editQuery);
            cl.packedData = o.editQuery["carve.data"];
            q.remove("carve.data");
            cl.baseUrl = o.editPath + q.toQueryString(o.editWithSemicolons);
            opts.img.attr('src', cl.baseUrl); //Undo current seam carving
            freezeImage(opts);
            var image = new Image();
            image.onload = function () {
                cl.w = image.width;
                cl.h = image.height;
                startCarve();
                cl.active = true;
            };
            image.src = cl.baseUrl;

        }).appendTo(body);

        var startCarve = function () {
            done.show();
            cancel.show();
            cl.img.canvasDraw({ C: 1000, controlParent: body });
            if (cl.packedData) cl.img.canvasDraw('unpack', cl.packedData);
        };
        var getFixedUrl = function () {
            var o = cl.opts;
            var q = new ImageResizer.Instructions(o.editQuery);
            q["carve.data"] = cl.packedData;
            return o.editPath + q.toQueryString(o.editWithSemicolons);
        }
        //Used for cancel, done buttons, and sourceImageChanged event.
        var stopDrawing = function (save, norestore) {
            cl.packedData = cl.img.canvasDraw('pack');
            cl.img.canvasDraw('unload');
            if (save)
                setUrl(opts, getFixedUrl(), false);
            else if (!norestore)
                cl.img.attr('src', opts.editUrl);
            done.hide();
            cancel.hide();
            start.show();
            reset.show();

            unFreezeImage(opts);
            opts.api.unlockAccordion(opts, body);
            cl.active = false;
        };

        var cancel = button(opts, 'cancel', null, function () { stopDrawing(false); }).appendTo(body).hide();
        var done = button(opts, 'done', null, function () { stopDrawing(true); }).appendTo(body).hide();
        //Just remove carve.data to reset everything!
        var reset = button(opts, 'reset', function (obj) { obj.remove("carve.data"); }).appendTo(body);
        //Handle source image changes by exiting
        opts.img.bind('sourceImageChanged', function () { if (cl.active) stopDrawing(false, true); });
        return c;
    };

    //Adds a pane for cropping
    var addCropPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'CropPane');
        var body = c.find('.panel-collapse > .panel-body');

        //Pane-local closure
        var cl = {
            img: opts.img,
            cropping: false,
            jcrop_reference: null,
            previousUrl: null,
            opts: opts
        };

        //Called once the 'uncropped' image has been loaded and its dimensions determined
        var startCrop = function (uncroppedWidth, uncroppedHeight, uncroppedUrl, oldCrop) {
            //Use existing coords if present
            var coords = null;
            var cropObj = oldCrop;
            //Adjust for xunits/yunits
            if (cropObj && cropObj.allPresent()) {
                coords = cropObj
                    .stretchTo(uncroppedWidth, uncroppedHeight)
                    .toCoordsArray();
            }

            //Handle preview init/update
            if (cl.opts.cropPreview)
                preview.JcropPreview({ jcropImg: cl.img });
            preview.hide();

            var update = function (coords) {
                if (cl.opts.cropPreview) {
                    preview.show();
                    preview.JcropPreviewUpdate(coords);
                }
            };

            //cl.opts.imgDiv.css('padding-left', (cl.opts.imgDiv.width() - cl.img.width()) / 2 + 1);
            //cl.opts.imgDiv.css('text-align', 'left');
            //Start up jCrop
            cl.img.Jcrop({
                onChange: update,
                onSelect: update,
                aspectRatio: getRatio(),
                bgColor: 'black',
                addClass: 'imagestudio',
                bgOpacity: 0.6
            }, function () {
                //Called when jCrop finishes loading
                cl.jcrop_reference = this;
                cl.opts.jcrop_reference = this;
                if (cl.opts.cropPreview) preview.JcropPreviewUpdate({
                    x: 0, y: 0, x2: uncroppedWidth, y2: uncroppedHeight,
                    w: uncroppedWidth, h: uncroppedHeight
                });


                if (coords != null) this.setSelect(coords);
                else {
                    //console.log('no data', getRatio());
                }

                //Show buttons
                $a([btnCancel, btnDone, label]).show();
                ratio.data('multiselect').$button.removeClass('hide');
                cl.cropping = true;


                var he = cl.jcrop_reference.ui.holder.height();
                var mh = cl.jcrop_reference.ui.holder.parent().height();
                var dif = mh - he;
                cl.jcrop_reference.ui.holder.css('margin-top', (dif / 2) + 'px');


                opts.api.setloading(opts, false);

                ratio.trigger('change');
                ratio.multiselect('refresh');
            });

        }

        var stopCrop = function (save, norestore) {
            //console.log('cl.cropping : ', cl.cropping);
            if (!cl.cropping) return;
            cl.cropping = false;

            if (save) {
                //console.log('seturl', ISDataCrop, cl.opts, cl.previousUrl);

                setUrl(cl.opts, cl.previousUrl, true);
                var coords = cl.jcrop_reference.tellSelect();

                if (typeof ImageResizer.Utils.parseQuery(location.search)['editorKey'] !== 'undefined')
                    cl.opts.editQuery.mergeWith(ir.Utils.parseQuery(cl.opts.resizeSetting[0][0]));

                edit(cl.opts, function (obj) {
                    obj.setCrop({
                        x1: coords.x, y1: coords.y,
                        x2: coords.x2, y2: coords.y2,
                        xunits: cl.img.width(), yunits: cl.img.height()
                    });
                });
            } else if (!norestore) {
                setUrl(cl.opts, cl.previousUrl);
            }
            if (cl.jcrop_reference) {
                cl.jcrop_reference.destroy();
                delete cl.opts.jcrop_reference;
            }
            cl.img.attr('style', ''); //Needed to fix all the junk JCrop added.
            //cl.opts.imgDiv.css('padding-left', 0); //undo horizontal align fix
            //cl.opts.imgDiv.css('text-align', 'center');
            $a([btnCancel, btnDone, label, preview]).hide();
            ratio.data('multiselect').$button.addClass('hide');

            $a([btnCrop, btnReset]).show();

            opts.api.setMarginTop();
            //opts.api.unlockAccordion(opts, body);
        }

        var btnCrop = button(opts, 'crop_crop', null, function () {

            opts.api.setloading(opts, true, false);

            //Hide the reset and crop button, lock the accordion
            $a([btnReset, btnCrop]).hide();
            //$a([ratio]).show();

            //Save the original crop values and URL
            var oldCrop = cl.opts.editQuery.getCrop();
            cl.previousUrl = opts.editUrl;

            //Create an uncropped URL
            var q = new ImageResizer.Instructions(cl.opts.editQuery);
            //console.log('q : ', q, cl.previousUrl, opts.editUrl);
            q.remove("crop", "cropxunits", "cropyunits", "width", "height", "mode");

            var uncroppedUrl = cl.opts.editPath + q.toQueryString(cl.opts.editWithSemicolons);
            //console.log('uncroppedUrl : ', uncroppedUrl);
            preloadLoadImage(uncroppedUrl, function (newimg) {
                if (typeof newimg !== 'undefined') {
                    cl.img.attr('src', uncroppedUrl);
                    startCrop(newimg.width, newimg.height, uncroppedUrl, oldCrop);
                    opts.api.lockAccordion(opts, body);
                }
            });


        }).addClass('btn-info').appendTo(body);

        //Set up aspect ratio checkbox
        var label = h3(opts, 'aspectratio', body).hide();
        var ratio = $("<select class='form-control mb10'></select>");
        var getRatio = function () {
            var vl = ratio.val();
            //console.log('vl : ', vl);
            var ret = null;
            if (vl === 'current')
                ret = cl.img.width() / cl.img.height();
            else if (vl === 0) { }
            else {
                var arr = vl.split('*');
                ret = parseFloat(arr[0]);
            }
            return ret;
        };
        var ratios = opts.cropratios;
        //console.log(ratios);
        for (var i = 0; i < ratios.length; i++)
            $('<option value="' + ratios[i][0].toString() + '*' + ratios[i][2] + '" data-sub="' + (ratios[i][2] || '') + '">' + ratios[i][1] + '</option>').appendTo(ratio);

        ratio[0].selectedIndex = 0;

        ratio.appendTo(body).addClass('hide').multiselect({
            enableFiltering: typeof ImageResizer.Utils.parseQuery(location.search)['editorKey'] === 'undefined',
            buttonWidth: '100%',
            buttonClass: 'hide btn',
            maxHeight: 300,
            enableHTML: true,
            optionLabel: function (element) {
                return $(element).html() + ($(element).attr('data-sub') ? ('<br/><span class="small-sub">(' + $(element).attr('data-sub') + ')</span>') : '');
            },
            onChange: function () {
            }
        });
        ratio.change(function () {
            var r = getRatio();
            var coords = cl.jcrop_reference.tellSelect();
            if (r !== null)
                cl.jcrop_reference.setOptions({ aspectRatio: r });

            var areAllEmpty = function (obj, keys) {
                for (var k in keys)
                    if (!isNaN(obj[keys[k]]) && obj[keys[k]] != 0) return false;
                return true;
            };
            if (areAllEmpty(coords, ['x', 'y', 'x2', 'y2'])) {
                if (r !== null) {
                    if (r != 0 && r != cl.img.width() / cl.img.height()) {
                        cl.jcrop_reference.setSelect(ImageResizer.Utils.getRectOfRatio(r, cl.img.width(), cl.img.height()));
                    }
                    else
                        cl.jcrop_reference.release();
                }
            }
            cl.jcrop_reference.focus();
        });

        var grouper = $('<div></div>').addClass('crop-active-buttons').appendTo(body);
        var btnCancel = button(opts, 'crop_cancel', null, function () {
            stopCrop(false);
        }).addClass('is-ignorelock').appendTo(grouper).hide();
        //Handle source image changes by exiting
        opts.img.bind('sourceImageChanged', function () { if (cl.cropping) stopCrop(false, true); });

        var btnDone = button(opts, 'crop_done', null, function () {
            var dataCrop = cl.jcrop_reference.tellSelect();
            //console.log('dataCrop : ', dataCrop);
            if (typeof dataCrop !== 'undefined' && dataCrop.w > 0 && dataCrop.h > 0)
                stopCrop(true);
            else
                return false;
        }).addClass('is-ignorelock').addClass('ml10 btn-danger').appendTo(grouper).hide();
        var preview = $("<div></div>").addClass('cropPreview').appendTo(body).hide();

        if (opts.cropPreview) preview.css(opts.cropPreview);
        var btnReset = button(opts, 'reset', function (obj) {
            stopCrop(false, true);
            obj.remove("crop", "cropxunits", "cropyunits", "width", "height", "mode");
        }).addClass('ml10').insertAfter(btnCrop);


        //Update button label and 'undo' visib
        btnCrop.text(opts.editQuery.crop ? opts.labels.crop_modify : opts.labels.crop_crop);
        btnReset.attr({ disabled: !opts.editQuery.crop });
        cl.img.bind('query', function (e, obj) {
            btnCrop.text(obj["crop"] ? opts.labels.crop_modify : opts.labels.crop_crop);
            btnReset.attr({ disabled: !obj["crop"] });
        });
        return c;
    };

    var getCachedJson = function (url, done, fail) {
        if (window.cachedJson == null) window.cachedJson = {};
        var result = window.cachedJson[url];
        if (result != null) {
            done(result); return;
        } else {
            $.ajax({
                url: url,
                dataType: 'jsonp',
                success: function (data) {
                    window.cachedJson[url] = data;
                    done(data);
                },
                fail: function () {
                    fail();
                }
            });
        }
    };

    var addResizePane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'ResizePane');
        var body = c.find('.panel-collapse > .panel-body');

        var ratioResize = $("<select class='form-control mb10'></select>");

        var ratios = opts.resizeSetting;
        for (var i = 0; i < ratios.length; i++)
            $('<option value="' + ratios[i][0].toString() + '*' + ratios[i][2] + '" data-sub="' + (ratios[i][2] || '') + '">' + ratios[i][1] + '</option>').appendTo(ratioResize);

        ratioResize.appendTo(body).attr('disabled', 'disabled').multiselect({
            enableFiltering: typeof ImageResizer.Utils.parseQuery(location.search)['editorKey'] === 'undefined',
            buttonWidth: '100%',
            //buttonClass: 'hide btn',
            maxHeight: 300,
            nonSelectedText: typeof customTextNoSelected !== 'undefined' ? customTextNoSelected : 'None selected',
            enableHTML: true,
            optionLabel: function (element) {
                return $(element).html() + ($(element).attr('data-sub') ? ('<br/><span class="small-sub">(' + $(element).attr('data-sub') + ')</span>') : '');
            },
            onChange: function () {
            }
        });

        ratioResize.change(function () {
            var vl = $(this).val();
            var data = vl.split('*')[0];

            var q = new ImageResizer.Instructions(opts.editQuery);
            q.remove('w', 'width', 'h', 'height', 'mode', 'scale', 'quality', 'numrandom');
            var newurl;
            var ss = q.toQueryString(opts.editWithSemicolons);
            if (data && data.length > 0) {
                newurl = opts.editPath + ss + '&' + data + '&numrandom=' + (Math.floor(Math.random() * 40 + 1));
                btn.removeAttr('disabled');
            }
            else {
                newurl = opts.editPath + ss + '&numrandom=' + (Math.floor(Math.random() * 40 + 1));
                btn.attr('disabled', 'disabled');
            }
            setUrl(opts, newurl, false);
        });

        var btn = button(opts, 'reset', function (obj) {
            obj.remove('w', 'width', 'h', 'height', 'mode', 'scale', 'quality', 'numrandom');
            btn.attr('disabled', 'disabled');
            $('#ResizePane select').val('').multiselect('refresh');
        }).attr('disabled', 'disabled').appendTo(body);
        return c;
    }

    var addAdvancedPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'AdvancedPane');
        var body = c.find('.panel-collapse > .panel-body');


        var setColor = function (ev, key) {
            var data = ev.color.toHex();
            if (data.length > 1) {
                data = data.substring(1);
                var oldcolor = opts.editQuery.getValue(key);
                if (oldcolor === data)
                    return;

                var q = new ImageResizer.Instructions(opts.editQuery);
                //console.log(q, opts);
                //console.log(q.getValue('bordercolor'));
                var newcolor = {};
                newcolor[key] = data;
                q.remove(key, 'numrandom');
                q.mergeWith(newcolor);

                var newurl;
                var ss = q.toQueryString(opts.editWithSemicolons);
                newurl = opts.editPath + ss + '&numrandom=' + (Math.floor(Math.random() * 40 + 1));
                //console.log(newurl);
                setUrl(opts, newurl, false);
            }
        }

        h3(opts, 'border_width', body).addClass('mt0');
        slider('s-border-width', body, opts, 0, 20, 1, "borderwidth");

        h3(opts, 'border_color', body);
        var divbordercolor = $('<div class="input-group colorpicker-element" id="is-color-border">' +
                            //'<input type="text" value="#fff" class="form-control" />' +
                            '<input type="text" value="white" class="form-control" />' +
                            '<span class="input-group-addon"><i></i></span>' +
                        '</div>');

        divbordercolor.css('margin-top', '15px').appendTo(body).find('input[type="text"]')
            .colorpicker({ format: 'hex' }).on('changeColor', function (ev) {
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('create', function (e) {
                var ev = $(e.target).data('colorpicker');
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('hidePicker', function (ev) {
                setColor(ev, 'bordercolor');
                btn.removeAttr('disabled');
            }).colorpicker('disable');



        h3(opts, 'padding_width', body).addClass('mt20');
        slider('s-padding-width', body, opts, 0, 20, 1, "paddingwidth");

        h3(opts, 'padding_color', body);
        var divpaddingcolor = $('<div class="input-group colorpicker-element" id="is-color-padding">' +
                            //'<input type="text" value="#fff" class="form-control" />' +
                            '<input type="text" value="white" class="form-control" />' +
                            '<span class="input-group-addon"><i></i></span>' +
                        '</div>');

        divpaddingcolor.css('margin-top', '15px').appendTo(body).find('input[type="text"]')
            .colorpicker({ format: 'hex' }).on('changeColor', function (ev) {
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('create', function (e) {
                var ev = $(e.target).data('colorpicker');
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('hidePicker', function (ev) {
                setColor(ev, 'paddingcolor');
                btn.removeAttr('disabled');
            }).colorpicker('disable');




        h3(opts, 'margin', body).addClass('mt20');
        slider('s-margin-width', body, opts, 0, 20, 1, "margin");

        h3(opts, 'bg_color', body);
        var divbgcolor = $('<div class="input-group colorpicker-element" id="is-color-bg">' +
                            //'<input type="text" value="#fff" class="form-control" />' +
                            '<input type="text" value="white" class="form-control" />' +
                            '<span class="input-group-addon"><i></i></span>' +
                        '</div>');

        divbgcolor.css('margin-top', '15px').appendTo(body).find('input[type="text"]')
            .colorpicker({ format: 'hex' }).on('changeColor', function (ev) {
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('create', function (e) {
                var ev = $(e.target).data('colorpicker');
                $(this).parent().find('.input-group-addon > i').css('backgroundColor', ev.color.toHex());
            }).on('hidePicker', function (ev) {
                setColor(ev, 'bgcolor');
                btn.removeAttr('disabled');
            }).colorpicker('disable');


        var btn = button(opts, 'reset', function (obj) {
            obj.remove('bgcolor', 'margin', 'paddingwidth', 'paddingcolor', 'borderwidth', 'bordercolor');
            $('#AdvancedPane input[type="text"].colorpicker-element').colorpicker('setValue', 'white');
            btn.attr('disabled', 'disabled');
        }).css('margin-top', '15px').attr('disabled', 'disabled').appendTo(body);
        return c;
    }

    var addTrimWhitespacePane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'TrimWhitespacePane');
        var body = c.find('.panel-collapse > .panel-body');

        //h3(opts, 'whitespacetrim_title', body).addClass('mt20');
        h3(opts, 'whitespacetrim_threshold', body).addClass('mt0');
        slider('s-trim-threshold', body, opts, 0, 200, 10, "trim.threshold");

        h3(opts, 'whitespacetrim_percent', body).addClass('mt10');
        slider('s-trim-percent-padding', body, opts, 0, 2, 0.1, "trim.percentpadding");

        var btn = button(opts, 'reset', function (obj) {
            obj.remove('trim.percentpadding', 'trim.threshold');
            btn.attr('disabled', 'disabled');
        }).css('margin-top', '15px').attr('disabled', 'disabled').appendTo(body);
        return c;
    }

    var saveFile = function (opts, savepath, template) {
        try {
            $.ajax({
                url: savepath,
                type: 'GET',
                success: function (data) {
                    //console.log(opts,data);
                    if (data && data.length > 0) {
                        var arrMess = data.split('||');

                        SweetSoftScript.Editor.Data.isSaveAs = false;

                        if (arrMess[0] === '1') {
                            alert('successful save!');
                            if (arrMess.length === 3) {
                                if (typeof SweetSoftScript.Editor.Data !== 'undefined')
                                    SweetSoftScript.Editor.Data.sessionFile = arrMess[2];
                            }

                            if (savepath.indexOf('newname') >= 0) {
                                if (opts.onsave !== null) opts.onsave(opts.api);
                            }
                            else {
                                opts.editUrl = opts.url;
                                opts.editQuery = new ImageResizer.Instructions(opts.url);
                                opts.api.resetToolbox();
                            }
                        }
                        else
                            alert(arrMess[1]);

                        if (typeof template !== 'undefined') {
                            SweetSoftScript.Editor.Data.isSaveAs = true;
                            template.modal('hide');
                        }
                    }
                },
                error: function (data) {
                    alert('save error');
                }
            });
        }
        catch (ex) {
            alert('save error');
        }
    }

    var addFacesPane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'FacesPane');
        var body = c.find('.panel-collapse > .panel-body');
        var mgr = new RectOverlayMgr(opts, 'f.rects', 'ow', 'oh');
        //Occurs after rect overlay system has exited.
        mgr.onExitComplete = function () {
            $a([done, cancel, clear, auto]).hide();
            $a([start, reset]).show();
            opts.api.unlockAccordion(opts, body);
        };
        //Occurs when data has been loaded and system is active
        mgr.onEnterComplete = function () {
            $a([done, cancel, clear, auto]).show();
        };
        mgr.onEnterFail = function () {
            $a([start, reset]).show();
            opts.api.unlockAccordion(opts, body);
        };
        mgr.getJsonUrl = function (basePath, baseQuery) {
            baseQuery['f.detect'] = true;
            return basePath + baseQuery.toQueryString(this.opts.editWithSemicolons);
        };

        var start = button(opts, 'faces_start', null, function () {
            $a([start, reset]).hide();
            opts.api.lockAccordion(opts, body);
            mgr.beginEnter();
        }).appendTo(body);

        var auto = button(opts, 'faces_auto', null, function () { mgr.addAuto(); }).appendTo(body).hide();
        var clear = button(opts, 'faces_clear', null, function () { mgr.clear(); }).appendTo(body).hide();
        $('<br />').appendTo(body);

        var cancel = button(opts, 'cancel', null, function () { mgr.cancel(); }).appendTo(body).hide();
        var done = button(opts, 'done', null, function () { mgr.saveAndClose(); }).appendTo(body).hide();
        var reset = button(opts, 'reset', null, function () { mgr.reset(); }).appendTo(body);
        return c;
    };

    var addRedEyePane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'RedEyePane');
        var body = c.find('.panel-collapse > .panel-body');
        var mgr = new RectOverlayMgr(opts, 'r.eyes', 'ow', 'oh');
        //Occurs after rect overlay system has exited.
        mgr.onExitComplete = function () {
            $a([done, cancel, preview, clear, auto]).hide();
            $a([start, reset]).show();
            opts.api.unlockAccordion(opts, body);
        };
        //Occurs when data has been loaded and system is active
        mgr.onEnterComplete = function () {
            $a([done, cancel, preview, clear, auto]).show();
        };
        mgr.onEnterFail = function () {
            $a([start, reset]).show();
            opts.api.unlockAccordion(opts, body);
        };
        mgr.getJsonUrl = function (basePath, baseQuery) {
            baseQuery['r.detecteyes'] = true;
            return basePath + baseQuery.toQueryString(this.opts.editWithSemicolons);
        };
        mgr.filterRects = function (rects) {
            return _.reject(rects, function (e) { return e.Feature !== 0 });
        }

        var start = button(opts, 'redeye_start', null, function () {
            $a([start, reset]).hide();
            opts.api.lockAccordion(opts, body);
            mgr.beginEnter();
        }).appendTo(body);

        var auto = button(opts, 'redeye_auto', null, function () { mgr.addAuto(); }).appendTo(body).hide();
        var clear = button(opts, 'redeye_clear', null, function () { mgr.clear(); }).appendTo(body).hide();
        $('<br />').appendTo(body);

        var preview = button(opts, 'redeye_preview', null, function () { $a([auto, clear, cancel, done]).toggle(); mgr.togglePreview(); }).appendTo(body).hide();
        var cancel = button(opts, 'cancel', null, function () { mgr.cancel(); }).appendTo(body).hide();
        var done = button(opts, 'done', null, function () { mgr.saveAndClose(); }).appendTo(body).hide();
        var reset = button(opts, 'reset', null, function () { mgr.reset(); }).appendTo(body);
        return c;
    };

    var addSavePane = function (opts, name) {
        var c = createPanel(opts.panelId, name || ' ', 'SavePane');
        var body = c.find('.panel-collapse > .panel-body');


        var template = $('<div id="imagestudio-modal-save" class="modal fade">\
                            <div class="modal-dialog">\
                                <div class="modal-content">\
                                    <div class="modal-header text-center">\
                                        <button type="button" class="close" data-dismiss="modal">×</button>\
                                        <h3 id="is-save-title" class="semibold modal-title text-danger">Save new file</h3>\
                                    </div>\
                                    <div class="modal-body">\
                                        <h4 id="is-save-label" class="semibold mt0">Save with a name:</h4>\
                                        <input class="form-control" autocomplete="off" type="text" id="is-txtNewName" />\
                                    </div>\
                                    <div class="modal-footer">\
                                        <button type="button" class="btn btn-default" id="is-btnClose" data-dismiss="modal"></button>\
                                        <button type="button" class="btn btn-primary" id="is-btnSave"></button>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>');
        template.find('#is-save-title').text(opts.labels['save_Tilte']);
        template.find('#is-save-label').text(opts.labels['save_Label']);
        template.find('#is-btnClose').text(opts.labels['save_close_btn']);
        template.find('#is-btnSave').text(opts.labels['save_btn']);

        template.appendTo($(body));

        template.on('shown.bs.modal', function () {
            //console.log(opts);
            var nameAndExt = getFileNameAndExtension(opts);
            //console.log(nameAndExt);
            var name = '';
            if (nameAndExt.length > 0) {
                name = nameAndExt[0] + '_' + (new Date()).getTime();
                name += '.' + nameAndExt[1];
            }
            template.find('#is-txtNewName').focus().val(name);
        });

        template.find('#is-txtNewName').keypress(function (e) {
            var theEvent = e || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);

            if (theEvent.keyCode === 13) {
                e.preventDefault();
                var btnok = template.find('#is-btnSave');
                if (btnok.length > 0)
                    btnok.click();
                return false;
            }
        });

        var btnSave = template.find('#is-btnSave');
        if (btnSave.length > 0) {
            btnSave.click(function () {
                var val = template.find('#is-txtNewName').val();
                if (val && val.length > 0) {
                    //console.log('opts : ', opts);

                    var savepath = opts.editUrl.replace(opts.editPath, 'savehandler.aspx') +
                        '&newname=' + val;

                    saveFile(opts, savepath, template);
                }
                return false;
            });
        }


        button(opts, 'save', function (obj) {
            var savepath = opts.editUrl.replace(opts.editPath, 'savehandler.aspx');
            saveFile(opts, savepath);
        }).appendTo(body);


        button(opts, 'saveas', function (obj) {
            template.modal({
                backdrop: 'static',
                keyboard: false
            });
        }).addClass('ml10 btn-warning').appendTo(body);

        return c;
    }



    //Used for red-eye and face rectangle overlay management
    var RectOverlayMgr = function (opts, key, origWidthKey, origHeightKey) {
        this.opts = opts; this.img = opts.img; this.key = key; this.origWidthKey = origWidthKey; this.origHeightKey = origHeightKey;
        //Handle source image changes by exiting
        var cl = this;
        opts.img.bind('sourceImageChanged', function () { if (cl.active) cl.cancel(); });
    };
    var rp = RectOverlayMgr.prototype;
    rp.hide = function () {
        $(this.img).show();
        this.container.remove();
        this.enabled = false;
    };
    rp.addAuto = function () {
        this.addRects(this.info.features);
        this.hide();
        this.show();
    };
    rp.clear = function () {
        this.rects = [];
        this.hide();
        this.show();
    };
    rp.togglePreview = function () {
        if (this.enabled) {
            //Apply or remove 
            this.img.attr('src', this.getFixedUrl());
            this.hide();
        } else {
            this.img.attr('src', this.baseUrl);
            this.show();
        }
        return this.enabled;
    };
    rp.cancel = function () { this.exit(false); };
    rp.saveAndClose = function () { this.exit(true); };
    rp.reset = function () {
        var k = this.key;
        edit(this.opts, function (obj) {
            obj.remove(k);
        });
    };
    rp.getFixedUrl = function () {
        var q = new ImageResizer.Instructions(this.opts.editQuery);
        q.setRectArray(this.key, this.rects);
        q[this.origWidthKey] = this.info.ow;
        q[this.origHeightKey] = this.info.oh;
        return this.opts.editPath + q.toQueryString(this.opts.editWithSemicolons);
    };
    rp.exit = function (save) {
        this.hide();
        if (save)
            setUrl(this.opts, this.getFixedUrl(), false);
        else
            this.img.attr('src', this.opts.editUrl);
        unFreezeImage(this.opts);
        this.active = false;
        if (this.onExitComplete) this.onExitComplete(save);
    };
    rp.beginEnter = function () {

        var o = this.opts;
        opts.api.setloading(o, true, false);
        var q = new ImageResizer.Instructions(o.editQuery);
        q.remove(this.key);
        this.baseUrl = o.editPath + q.toQueryString(o.editWithSemicolons);
        o.img.attr('src', this.baseUrl); //Undo current red-eye fixes
        this.jsonUrl = this.getJsonUrl(o.editPath, q);

        var cl = this;
        cl.loading = true;
        getCachedJson(this.jsonUrl, function (data) {
            if (cl.onEnterComplete) cl.onEnterComplete();
            cl.info = data;

            cl.rects = o.editQuery.getRectArray(cl.key);
            //Unless we already have rects, default to the automatic ones
            if (cl.rects.length == 0) cl.addRects(data.features);

            freezeImage(cl.opts);
            cl.show();
            opts.api.setloading(cl.opts, false);
            cl.active = true;
        }, function () {
            opts.api.setloading(cl.opts, false);
            cl.loading = false;
            if (cl.onEnterFail) cl.onEnterFail();
            cl.img.attr('src', o.editUrl);
        });
    };
    rp.hashrect = function (e) { return e.X + e.Y * 1000 + e.X2 * 100000 * e.Y2 * 1000000 };
    rp.addRects = function (rects) {
        if (rects == null || rects.length == 0) return;
        var cl = this;
        //merge with cl.rects, eliminating duplicates
        this.rects = _.uniq((this.rects ? this.rects : []).concat(cl.filterRects ? cl.filterRects(rects) : rects), false, cl.hashrect);
    };
    rp.addRect = function (rect, clientrect) {
        var cl = this;
        var d = cl.info;
        var cr = clientrect;
        var r = rect;
        if (cr == null) {
            cr = {
                x: (r.X - d.cropx) * (d.dw / d.cropw) - 1,
                y: (r.Y - d.cropy) * (d.dh / d.croph) - 1,
                w: (r.X2 - r.X) * (d.dw / d.cropw),
                h: (r.Y2 - r.Y) * (d.dh / d.croph)
            };
        } 2
        if (r == null) {
            var x = cr.x / (d.dw / d.cropw) + d.cropx;
            var y = cr.y / (d.dh / d.croph) + d.cropy;
            var w = cr.w / (d.dw / d.cropw);
            var h = cr.h / (d.dh / d.croph);
            r = { X: x, Y: y, X2: x + w, Y2: y + h, Accuracy: cr.accuracy };
            cl.rects.push(r);
        }
        //Don't add rectangle if it's out of bounds. silently keep it, in case we change the crop, though.
        if (cr.x < 0 || cr.y < 0 || cr.x + cr.w > cl.container.width() || cr.y + cr.h > cl.container.height()) return;

        var rect = $('<div></div>').addClass('red-eye-rect').width(cr.w).height(cr.h).css({ 'position': 'absolute', 'z-order': 2000 }).appendTo(cl.container).show().position({ my: 'left top', at: 'left top', collision: 'none', of: cl.container, offset: cr.x.toString() + ' ' + cr.y.toString() });
        rect.css('border', '1px solid green');
        rect.data('rect', r);
        var onClickRect = function () {
            var r = $(this).data('rect');
            $(this).remove();
            cl.rects = _.reject(cl.rects, function (val) { return cl.hashrect(val) == cl.hashrect(r) });
            cl.container.data('down', null);
        };

        rect.mouseup(onClickRect);
    };
    rp.show = function () {
        var cl = this;
        var d = cl.info;
        cl.enabled = true;

        cl.container = $('<div></div>').addClass('red-eye-container').css({ 'position': 'absolute', 'z-order': 1000 }).insertAfter(cl.img).show().position({ my: 'left top', at: 'left top', collision: 'none', of: cl.img, offset: '0 ' + d.dy }).width(d.dw).height(d.dh);
        $(cl.img).hide();
        $(cl.container).css({ 'backgroundImage': 'url(' + $(cl.img).attr('src') + ')' });
        for (var i = 0; i < cl.rects.length; i++) {
            cl.addRect(cl.rects[i]);
        }
        cl.container.mousedown(function (evt) {
            if (evt.which == 2) {
            }
            if (evt.which == 1) {
                if (typeof evt.offsetX === "undefined" || typeof evt.offsetY === "undefined") {
                    var targetOffset = $(evt.target).offset();
                    evt.offsetX = evt.pageX - targetOffset.left;
                    evt.offsetY = evt.pageY - targetOffset.top;
                }
                //var offset = $(this).offset();
                cl.container.data('down', { x: evt.offsetX, y: evt.offsetY });
                evt.preventDefault();
            }
        });

        cl.container.mouseup(function (evt) {
            if (typeof evt.offsetX === "undefined" || typeof evt.offsetY === "undefined") {
                var targetOffset = $(evt.target).offset();
                evt.offsetX = evt.pageX - targetOffset.left;
                evt.offsetY = evt.pageY - targetOffset.top;
            }

            if (cl.container[0] != this) return; //No bubbled events
            if (evt.which == 1) {
                var down = cl.container.data('down');
                if (down == null) return;
                cl.container.data('down', null);
                var cx = down.x;
                var cy = down.y;
                var cw = (evt.offsetX - cx);
                var ch = (evt.offsetY - cy);

                var accuracy = 9;
                if (cw < 0) { cx += cw; cw *= -1 };
                if (ch < 0) { cy += ch; ch *= -1 };
                if (cw + ch < 6) {
                    cx -= 12;
                    cy -= 12;
                    cw += 24;
                    ch += 24;
                    accuracy = 5;
                }
                cl.addRect(null, { x: cx, y: cy, w: cw, h: ch, accuracy: accuracy });
                $(document.body).focus();
            }
        });
    };
})(jQuery);
