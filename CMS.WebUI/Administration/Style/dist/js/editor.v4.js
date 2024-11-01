/// <reference path="../js/jquery-1.11.2.js" />
/// <reference name="MicrosoftAjax.js" />
/// <reference name="MicrosoftAjaxWebForms.js" />

if (typeof TinyMCETemplate === 'undefined') {
    TinyMCETemplate = {
        data: {
            init: false,
            tableHtml: '<table class="table-icons"><tbody></tbody></table>',
            qtipOtherSize: ['<div class="form-group">',
                            '<label>{OtherSize}</label>',
                            '<div class="btn-group btnothersize btn-group-justified">',
                                '<div class="btn-group">',
                                    '<input type="button" class="btn-default btn btn-sm" value="2x" />',
                                '</div>',
                                '<div class="btn-group">',
                                    '<input type="button" class="btn-default btn btn-sm" value="3x" />',
                                '</div>',
                                '<div class="btn-group">',
                                    '<input type="button" class="btn-default btn btn-sm" value="4x" />',
                                '</div>',
                                '<div class="btn-group">',
                                    '<input type="button" class="btn-default btn btn-sm" value="5x" />',
                                '</div>',
                            '</div>',
                        '</div>'].join(''),
            qtipIconHtml: ['<div class="mainset">',
                            '{Guide}',
                            '<div class="row">',
                                '<div class="col-xs-6">',
                                    '<div class="form-group outerhtml">',
                                        '<label>{OuterHtml}</label>',
                                        '<div class="well"></div>',
                                    '</div>',
                                    '<div class="form-group htmlcode">',
                                        '<label>{HtmlCode}</label>',
                                        '<div class="well text-center"></div>',
                                    '</div>',
                                    '<div class="form-group csscontent">',
                                        '<label>{CssContent}</label>',
                                        '<div class="well text-center"></div>',
                                    '</div>',
                                '</div>',
                                '<div class="col-xs-6">',
                                    '<div class="form-group">',
                                        '<label for="txtsize{id}">{IconSize}</label>',
                                        '<div class="input-group">',
                                            '<input type="text" id="txtsize{id}" class="txtsize form-control" />',
                                            '<span class="input-group-addon">px</span>',
                                        '</div>',
                                    '</div>',
                                    '{OtherSizeHtml}',
                                    '<div class="form-group">',
                                        '<label>{Preview}</label>',
                                        '<div class="preview text-center">',
                                        '</div>',
                                    '</div>',
                                    '<div class="form-group"><input type="button" class="btn-block btn btn-info btnsel" value="{Select}"></div>',
                                '</div>',
                            '</div>',
                        '</div>'].join(''),
            modalHtml: ['<div id="tinymce-modal-param" class="modal fade" role="dialog">',
            '<div class="modal-dialog">',
                '<div class="modal-content">',
                    '<div class="modal-header">',
                        '<button type="button" class="close" data-dismiss="modal">&times;</button>',
                        '<h4 class="modal-title"></h4>',
                    '</div>',
                    '<div class="modal-body main-param">',
                        '<div class="row">',
                            '<div class="col-xs-12 search">',
                                '<label for="search-input"><span class="fa fa-search"></span></label>',
                                '<input id="search-input" class="search-input form-control input-sm" placeholder="Search..."',
                                    'autocomplete="off" spellcheck="false" autocorrect="off" tabindex="1" />',
                                '<a href="javascript:void(0);" class="search-clear fa fa-times-circle hide"></a>',
                            '</div>',
                        '</div>',
                        '<div class="search-results hide">',
                            '<h4 class="page-header"></h4>',
                            '<div class="row box-param">',
                                '<div class="loading-search">',
                                    '<span class="fa fa-spin fa-cogs fa-2x"></span>',
                                '</div>',
                            '</div>',
                        '</div>',
                        '<div class="main-item">',
                        '</div>',
                    '</div>',
                    '<div class="modal-footer">',
                        '<input type="button" class="btn btn-danger" data-delete="modal" onclick="TinyMCETemplate.mainFunction.DeleteSelectedParam(true); return false;" value="Delete" />',
                        '<input type="button" class="btn btn-default" data-dismiss="modal" value="Close" />',
                    '</div>',
                '</div>',
            '</div>',
        '</div>'].join(''),
            mapData: [],
            found: [],
            highlightkey: 'khuyến mãi ,test ,giảm giá ,ưu đãi % ,ưu đãi% ,khuyen mai ,test ,giam gia ,uu dai % ,uu dai%',
            addedKey: {}
        },
        text: {
            toolbarButton: 'Insert',
            Header: 'Select item',
            CloseButton: 'Close',
            DeleteButton: 'Delete',
            SearchPlaceHolder: 'Search...',
            SearchProcess: 'Search for',
            SearchNoResult: 'No result for key',

            OtherSize: 'Other size:',
            OuterHtml: 'Outer html:',
            HtmlCode: 'Html code:',
            CssContent: 'Css content:',
            IconSize: 'Icon size:',
            Preview: 'Preview:',
            Setting: 'Setting',
            Guide: '',
            SelectButton: 'Select'
        },
        commonFunction: {
            encode_utf8: function (s) {
                return unescape(encodeURIComponent(s));
            },
            decode_utf8: function (s) {
                return decodeURIComponent(escape(s));
            },
            htmlEncode: function (html) {
                return document.createElement('a').appendChild(
                    document.createTextNode(html)).parentNode.innerHTML;
            },
            htmlDecode: function (html) {
                var a = document.createElement('a'); a.innerHTML = html;
                return a.textContent;
            },

            //function converting Unicode text to HTML entities
            symbolsToEntities: function (sText) {
                var sNewText = "";
                var iLen = sText.length;
                for (i = 0; i < iLen; i++) {
                    iCode = sText.charCodeAt(i);
                    sNewText += (iCode > 256 ? "&#" + iCode + ";" : sText.charAt(i));
                }
                return sNewText;
            },
            //toggle() is a function for toggling between Unicode text and HTML entities
            toggle: function () {
                var objSomeText = document.getElementById("someText");
                if (objSomeText.value) {
                    oDiv = document.createElement("DIV");
                    if (objSomeText.value.match(/&.+?;/gim)) {
                        oDiv.innerHTML = objSomeText.value;
                        objSomeText.value = oDiv.innerText || oDiv.firstChild.nodeValue;
                    } else {
                        objSomeText.value = symbolsToEntities(objSomeText.value);
                    }
                }
            },
            encodedStr: function (rawStr) {
                return rawStr.replace(/[\u00A0-\u9999<>\&]/gim, function (i) {
                    return '&#' + i.charCodeAt(0) + ';';
                });
            },
            encodedStr2: function (text) {
                return text.replace(/[\u00A0-\u2666]/g, function (c) {
                    return '&#' + c.charCodeAt(0) + ';';
                });
            },
            html_entity_decode: function (s) {
                var t = document.createElement('textarea');
                t.innerHTML = s;
                var v = t.value;
                t.parentNode.removeChild(t);
                return v;
            },
            escapeHTML: function (str) {
                var div = document.createElement('div');
                var text = document.createTextNode(str);
                div.appendChild(text);
                return div.innerHTML;
            },
            htmlEscape: function (str) {
                return String(str)
                        .replace(/&/g, '&amp;')
                        .replace(/"/g, '&quot;')
                        .replace(/'/g, '&#39;')
                        .replace(/</g, '&lt;')
                        .replace(/>/g, '&gt;');
            },
            htmlUnescape: function (value) {
                return String(value)
                    .replace(/&quot;/g, '"')
                    .replace(/&#39;/g, "'")
                    .replace(/&lt;/g, '<')
                    .replace(/&gt;/g, '>')
                    .replace(/&amp;/g, '&');
            },
            encodeHTML2: function (str) {
                var aStr = str.split(''),
                       i = aStr.length,
                       aRet = [];

                while (i--) {
                    var iC = aStr[i].charCodeAt();
                    if (iC < 65 || iC > 127 || (iC > 90 && iC < 97)) {
                        aRet.push('&#' + iC + ';');
                    } else {
                        aRet.push(aStr[i]);
                    }
                }
                return aRet.reverse().join('');
            },
            replaceAll: function (main, findwhat, replacewith, ignoreCase) {
                return main.replace(new RegExp(findwhat.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, "\\$&"),
                    (ignoreCase ? "gi" : "g")), (typeof (replacewith) == "string") ? replacewith.replace(/\$/g, "$$$$") : replacewith);
            },
            extendJquery: function () {
                if (typeof $.fn.attrs === 'undefined') {
                    // Attrs
                    $.fn.attrs = function (attrs) {
                        var t = $(this);
                        if (attrs) {
                            // Set attributes
                            t.each(function (i, e) {
                                var j = $(e);
                                for (var attr in attrs) {
                                    j.attr(attr, attrs[attr]);
                                }
                            });
                            return t;
                        } else {
                            // Get attributes
                            var a = {},
                                r = t.get(0);
                            if (r) {
                                r = r.attributes;
                                for (var i in r) {
                                    var p = r[i];
                                    if (typeof p.nodeValue !== 'undefined') a[p.nodeName] = p.nodeValue;
                                }
                            }
                            return a;
                        }
                    };
                }
            }
        },
        mainFunction: {
            getDataParam: function (id) {
                var dataParam = [];
                if (TinyMCETemplate.data.mapData.length > 0) {
                    $.each(TinyMCETemplate.data.mapData, function (i, o) {
                        if (o.editorId === id) {
                            dataParam = window[o.editorParam];
                            return false;
                        }
                    });
                }
                if (dataParam.length === 0) {
                    var keys = Object.keys(window);
                    if ($.isArray(keys) === true && keys.length > 0) {
                        $.each(keys, function (i, o) {
                            if (o.indexOf('dataParam') >= 0) {
                                if (o.indexOf(id) >= 0) {
                                    dataParam = window[o];
                                    TinyMCETemplate.data.mapData.push({ editorId: id, editorParam: o });
                                    return false;
                                }
                                else {
                                    var idsplit = o.substring('dataParam'.length);
                                    if (id.indexOf(idsplit) >= 0) {
                                        dataParam = window[o];
                                        TinyMCETemplate.data.mapData.push({ editorId: id, editorParam: o });
                                        return false;
                                    }
                                }
                            }
                        });
                    }
                }
                return dataParam;
            },
            addToFound: function (obj) {
                if (obj) {
                    var isExist = false;
                    var fo = TinyMCETemplate.data.found;
                    if (fo.length > 0) {
                        $.each(fo, function (i, o) {
                            if (o.id === obj.id) {
                                isExist = true;
                                return false;
                            }
                        });
                    }
                    if (isExist === false)
                        fo.push(obj);
                    TinyMCETemplate.data.found = fo;
                }
            },
            searchIcon: function () {
                var id = tinymce.activeEditor.id;
                if (typeof $.fn.qtip === 'function') {
                    var api = $('#qtip-' + id).qtip('api');
                    if (api !== null)
                        api.hide();
                }

                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length > 0) {
                    var key = modal.find('.search-input').val() || '';
                    if (key.length > 0) {

                        modal.find('.search-results .page-header span').text(key);
                        modal.find('.search-results').removeClass('hide');
                        modal.find('.main-item').addClass('hide');
                        modal.find('.search-clear').removeClass('hide');

                        if (modal.find('.search-results .box-param .loading-search').length === 0)
                            modal.find('.search-results .box-param').html('<div class="loading-search">' +
                            '<span class="fa fa-spin fa-cogs fa-2x"></span>' +
                        '</div>');

                        setTimeout('TinyMCETemplate.mainFunction.processSearch("' + id.replace(/"/g, "\"") +
                            '","' + key.replace(/"/g, "\"") + '");', 100);
                    }
                    else
                        TinyMCETemplate.mainFunction.clearSearch();
                }
            },
            processSearch: function (id, key) {
                //var id = tinymce.activeEditor.id;
                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length > 0) {

                    TinyMCETemplate.data.found = [];
                    var dataParam = TinyMCETemplate.mainFunction.getDataParam(id);
                    if (typeof dataParam !== 'undefined') {
                        var keylower = key.toLowerCase();
                        $.each(dataParam, function (indx, obj) {
                            if ($.isArray(obj.content) === true && obj.content.length > 0) {
                                $.each(obj.content, function (i, o) {
                                    //if (o.name.toLowerCase().indexOf(key) >= 0)
                                    if (o.text.toLowerCase().indexOf(keylower) >= 0)
                                        TinyMCETemplate.mainFunction.addToFound(o);
                                });
                            }
                        });
                    }

                    var elmain = $('#' + id);
                    var renderType = elmain.attr('data-renderResultAsIcon');
                    if (renderType && renderType.toLowerCase() === 'true')
                        TinyMCETemplate.mainFunction.renderResultIcon(key);
                    else
                        TinyMCETemplate.mainFunction.renderResult(key);
                }
            },
            getDataParamObject: function (key) {
                var found = undefined;
                var id = tinymce.activeEditor.id;
                var dataParam = TinyMCETemplate.mainFunction.getDataParam(id);
                if (typeof dataParam !== 'undefined') {
                    var keylower = key.toLowerCase();
                    $.each(dataParam, function (indx, obj) {
                        if ($.isArray(obj.content) === true && obj.content.length > 0) {
                            $.each(obj.content, function (ii, oo) {
                                if (oo.code.toLowerCase() === keylower) {
                                    found = oo;
                                    return false;
                                }
                            });
                        }
                    });
                }
                return found;
            },
            showInfoIcon: function (elem) {
                if (typeof elem !== 'undefined') {
                    var code = $(elem).attr('data-param-code');
                    if (code && code.length > 0) {
                        var obj = TinyMCETemplate.mainFunction.getDataParamObject(code);
                        if (typeof obj !== 'undefined') {

                            //set info
                            var id = tinymce.activeEditor.id;
                            if (typeof $.fn.qtip === 'function') {
                                var api = $('#qtip-' + id).qtip('api');
                                if (api !== null) {
                                    var qtipcontent = api.elements.content;
                                    if (qtipcontent.length > 0) {
                                        var outerhtml = obj.outerhtml || $(elem).html();
                                        qtipcontent.find('.mainset .outerhtml .well').text(outerhtml);

                                        var htmlcode = obj.htmlcode;
                                        if (htmlcode && htmlcode.length > 0) { }
                                        else {
                                            if (obj.csscontent && obj.csscontent.length > 0)
                                                htmlcode = ('&#x' + obj.csscontent + ';');
                                        };

                                        if (typeof htmlcode !== 'undefined' && htmlcode.length > 0)
                                        { }
                                        else
                                            htmlcode = '';
                                        qtipcontent.find('.mainset .htmlcode .well').text(htmlcode);

                                        var csscontent = obj.csscontent;
                                        if (csscontent && csscontent.length > 0) { }
                                        else
                                            csscontent = '';
                                        qtipcontent.find('.mainset .csscontent .well').text(csscontent);

                                        qtipcontent.find('.mainset .preview').html(outerhtml);

                                        api.set('position.container', $('div[id^="tinymce-modal-param"]:visible'));
                                        api.set('position.target', $(elem));
                                        api.reposition(null, false);

                                        var inputsize = api.elements.content.find('#txtsize' + id);
                                        if (inputsize.length > 0) {
                                            if (inputsize.val().length > 0)
                                                TinyMCETemplate.mainFunction.setSizeIcon(inputsize);
                                        }

                                        api.show();
                                    }
                                }
                            }
                        }
                    }
                }
            },
            renderResult: function (key) {

                var id = tinymce.activeEditor.id;
                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length > 0) {
                    modal.find('.search-results .box-param').empty();

                    var lst1 = '', lst2 = '';
                    var keylower = key.toLowerCase();

                    if (TinyMCETemplate.data.found.length > 0) {
                        var indxKey = -1;
                        var posfirst, keymatch, newname, text;
                        $.each(TinyMCETemplate.data.found, function (i, o) {
                            text = '';
                            indxKey = o.text.toLowerCase().indexOf(keylower);
                            if (indxKey >= 0) {
                                posfirst = o.text.substring(0, indxKey);
                                keymatch = o.text.substring(indxKey, indxKey + key.length);
                                newname = o.text.substring(indxKey + key.length);

                                text = (posfirst + ('<em>' + keymatch + '</em>') + newname)
                            }
                            else
                                text = o.text;

                            if (i % 2 === 0)
                                lst1 += '<li onclick="TinyMCETemplate.mainFunction.InsertParamToEditor(\''
                                                    + o.id.replace(/'/g, "\\'")
                                                    + '\',\'' + o.text.replace(/'/g, "\\'") + '\');" data-param-code="' +
                                                    o.id.replace(/"/g, "\"") + '">' + text + '</li>';
                            else
                                lst2 += '<li onclick="TinyMCETemplate.mainFunction.InsertParamToEditor(\''
                                                    + o.id.replace(/'/g, "\\'") +
                                                    '\',\'' + o.text.replace(/'/g, "\\'") + '\');" data-param-code="' +
                                                    o.id.replace(/"/g, "\"") + '">' + text + '</li>';
                        });

                        var html = '<div class="col-xs-6"><ul>' + lst1 + '</ul></div>' +
                            '<div class="col-xs-6"><ul>' + lst2 + '</ul></div>';

                        modal.find('.search-results .box-param').html(html);

                    }
                    else {
                        var elmain = $('#' + id);
                        var txt = TinyMCETemplate.text.SearchNoResult;
                        var txtNoResult = elmain.attr('data-searchNoResultText');
                        if (txtNoResult && txtNoResult.length > 0)
                            txt = txtNoResult;
                        modal.find('.search-results .box-param').html('<div class="col-xs-12">' +
                            txt + ' "' + key.replace(/"/g, "\"") + '".');
                    }
                }
            },
            renderResultIcon: function (key) {

                var id = tinymce.activeEditor.id;
                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length > 0) {

                    modal.find('.search-results .box-param').empty();

                    //var lst1 = '', lst2 = '';
                    //var keylower = key.toLowerCase();

                    if (TinyMCETemplate.data.found.length > 0) {

                        var elmain = $('#' + id);
                        var clmain = elmain.attr('data-iconMainClass') || '';
                        var clPrefix = elmain.attr('data-iconPrefix') || '';
                        var textAttr = elmain.attr('data-dataParamIsIcon');
                        var isIcon = textAttr && textAttr.toLowerCase() === 'true';

                        //var indxKey = -1;
                        //var posfirst, keymatch, newname, text;
                        var html = '<div class="col-xs-12">', ul = '', onclick = '';
                        var selected = modal.find('.main-item ul.list-icons li.selected');
                        var idselected = selected.length > 0 ? selected.attr('data-param-code') : '';

                        ul = '<ul class="list-icons">';
                        $.each(TinyMCETemplate.data.found, function (ii, oo) {
                            if (typeof isIcon !== 'undefined' && isIcon === true)
                                onclick = '';
                            else
                                onclick = ' onclick="TinyMCETemplate.mainFunction.InsertParamToEditor(\''
                                       + oo.id.replace(/'/g, "\\'") + '\',\'' + oo.code.replace(/'/g, "\\'") +
                                       '\',\'' + oo.text.replace(/'/g, "\\'") +
                                       '\');"';

                            ul += '<li' + onclick + ' data-param-code="' + oo.code.replace(/"/g, "\"") + '" title="' +
                                    oo.text.replace(/"/g, "\"") + '" class="btn btn-sm btn-default' +
                                    (idselected.length > 0 && oo.code === idselected ? ' selected' : '')
                                    + '"><span class="' + clmain.replace(/"/g, "\"") + (clmain.length > 0 ? ' ' : '')
                                    + (clPrefix + oo.code).replace(/"/g, "\"") + '"></span></li>';
                        });

                        ul += '</ul>';
                        ul += '<div class="clearfix"></div>';

                        html += ul;
                        html += '</div>';
                        modal.find('.search-results .box-param').html(html);

                        if (typeof isIcon !== 'undefined' && isIcon === true) {
                            modal.find('ul.list-icons li').click(function () {
                                if ($(this).hasClass('selected') === false) {
                                    var selected = modal.find('.search-results ul.list-icons li.selected');
                                    if (selected.length > 0)
                                        selected.removeClass('selected');
                                    $(this).addClass('selected');
                                    TinyMCETemplate.mainFunction.showInfoIcon(this);
                                }
                            });
                        }
                    }
                    else {
                        var elmain = $('#' + id);
                        var txt = TinyMCETemplate.text.SearchNoResult;
                        var txtNoResult = elmain.attr('data-searchNoResultText');
                        if (txtNoResult && txtNoResult.length > 0)
                            txt = txtNoResult;
                        modal.find('.search-results .box-param').html('<div class="col-xs-12">' +
                            txt + ' "' + key.replace(/"/g, "\"") + '".');
                    }
                }
            },
            renderItem: function (id, holder, isIcon) {
                if (typeof holder !== 'undefined' && typeof id !== 'undefined') {
                    //create param
                    var dataParam = TinyMCETemplate.mainFunction.getDataParam(id);

                    if ($.isArray(dataParam) === true && dataParam.length > 0) {
                        var divRow, block, titleBlock, ul, liColl, onclick;
                        for (var i = 0, j = dataParam.length; i < j; i += 2) {
                            divRow = $('<div class="row box-param"></div>');
                            for (var k = i; k < i + 2; k++) {
                                if (k < j) {
                                    block = $('<div class="col-xs-6"></div>');
                                    titleBlock = $('<h3>' + (dataParam[k].name || 'untitled') + '</h3>');
                                    ul = $('<ul></ul>');
                                    liColl = '';
                                    if ($.isArray(dataParam[k].content) === true && dataParam[k].content.length > 0) {
                                        $.each(dataParam[k].content, function (ii, oo) {
                                            if (typeof isIcon !== 'undefined' && isIcon === true)
                                                onclick = '';
                                            else
                                                onclick = ' onclick="TinyMCETemplate.mainFunction.InsertParamToEditor(\''
                                                + oo.id.replace(/'/g, "\\'") + '\',\'' +
                                                oo.text.replace(/'/g, "\\'") + '\');"';

                                            liColl += '<li' + onclick + ' data-param-code="' + oo.id.replace(/"/g, "\"") + '">' + oo.text + '</li>';
                                        });
                                        ul.append(liColl);
                                    }

                                    block.append(titleBlock);
                                    block.append(ul);
                                    divRow.append(block);
                                }
                            }
                            holder.append(divRow);
                        }
                    }
                }
            },
            renderTableItem: function (id, holder, isIcon) {
                if (typeof holder !== 'undefined' && typeof id !== 'undefined') {
                    //create param
                    var dataParam = TinyMCETemplate.mainFunction.getDataParam(id);

                    if ($.isArray(dataParam) === true && dataParam.length > 0) {
                        var elmain = $('#' + id);
                        var num = elmain.attr('data-renderColumn');
                        var numTemp = parseFloat(num);
                        var columns = (isNaN(numTemp) || numTemp < 1) ? 6 : numTemp;
                        var clmain = elmain.attr('data-iconMainClass') || '';
                        var clPrefix = elmain.attr('data-iconPrefix') || '';

                        var total = 0, rows = 0, modNum = 0;
                        var html = '', trColl = '';
                        var tb = $(TinyMCETemplate.data.tableHtml);
                        $.each(dataParam, function (i, o) {
                            html += '<div class="col-xs-12"><h3>' + (o.name || 'untitled') + '</h3></div>';

                            trColl = '';
                            if ($.isArray(o.content) === true && o.content.length > 0) {
                                var count = o.content.length;
                                total = count / num;
                                rows = parseInt(total.toString());
                                modNum = count % num;
                                if (modNum > 0)
                                    rows += 1;
                                var indx = 0;
                                for (var i = 0; i < rows; i++) {
                                    trColl += '<tr>';
                                    for (var n = 0; n < columns; n++) {
                                        indx = i * columns + n;
                                        if (indx < count) {

                                            trColl += '<td>';
                                            trColl += '<a href="javascript:void(0);" class="btn btn-default btn-icon" title="'
                                                + clPrefix + o.content[indx].code + '">';
                                            trColl += '<span class="' + clmain + (clmain.length > 0 ? ' ' : '') +
                                                clPrefix + o.content[indx].code + '"></span>';
                                            trColl += '</a>';
                                            trColl += '</td>';
                                        }
                                        else {
                                            trColl += '<td></td>';
                                        }
                                    }
                                    trColl += '</tr>';
                                }
                            }


                            tb.find('tbody').html(trColl);
                            html += tb.wrap('<div></div>').parent().html();
                        });

                        holder.html(html);
                    }
                }
            },
            renderListItem: function (id, holder, isIcon) {
                if (typeof holder !== 'undefined' && typeof id !== 'undefined') {
                    //create param
                    var dataParam = TinyMCETemplate.mainFunction.getDataParam(id);

                    if ($.isArray(dataParam) === true && dataParam.length > 0) {
                        var elmain = $('#' + id);
                        var clmain = elmain.attr('data-iconMainClass') || '';
                        var clPrefix = elmain.attr('data-iconPrefix') || '';

                        var html = '', ul = '', onclick = '';

                        $.each(dataParam, function (i, o) {
                            html += '<div class="cat-header alert bg-info"><h4>' + (o.name || 'untitled') + '</h4></div>';

                            ul = '<ul class="list-icons">';
                            if ($.isArray(o.content) === true && o.content.length > 0) {
                                $.each(o.content, function (ii, oo) {

                                    if (typeof isIcon !== 'undefined' && isIcon === true)
                                        onclick = '';
                                    else
                                        onclick = ' onclick="TinyMCETemplate.mainFunction.InsertParamToEditor(\''
                                                + oo.id.replace(/'/g, "\\'") + '\',\'' + oo.code.replace(/'/g, "\\'") + '\',\'' + oo.text.replace(/'/g, "\\'") +
                                                '\');"';

                                    ul += '<li' + onclick + ' data-param-code="' + oo.code.replace(/"/g, "\"") + '" title="' +
                                                  oo.text.replace(/"/g, "\"") + '" class="btn btn-sm btn-default'
                                                  + '"><span class="' + clmain.replace(/"/g, "\"") + (clmain.length > 0 ? ' ' : '')
                                                  + (clPrefix + oo.code).replace(/"/g, "\"") + '"></span></li>';
                                });
                            }

                            ul += '</ul>';
                            ul += '<div class="clearfix"></div>';

                            html += ul;
                        });

                        holder.html(html);

                        holder.find('.cat-header').click(function () {
                            $(this).next().stop().slideToggle();
                        });
                    }
                }
            },
            clearSearch: function () {
                var id = tinymce.activeEditor.id;

                if (typeof $.fn.qtip === 'function') {
                    var api = $('#qtip-' + id).qtip('api');
                    if (api !== null)
                        api.hide();
                }

                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length > 0) {
                    modal.find('.search-input').val('');
                    modal.find('.search-clear').addClass('hide');
                    modal.find('.search-results').addClass('hide');
                    modal.find('.main-item').removeClass('hide');
                }
            },
            setSizeIcon: function (elem) {
                if (typeof elem !== 'undefined') {
                    var isinputtext = $(elem).hasClass('txtsize');
                    var id = tinymce.activeEditor.id;
                    if (typeof $.fn.qtip === 'function') {
                        var api = $('#qtip-' + id).qtip('api');

                        if (api !== null) {
                            var qtipcontent = api.elements.content;
                            if (qtipcontent.length > 0) {
                                var previewelem = qtipcontent.find('.mainset .preview').children().eq(0);

                                if (isinputtext === true) {
                                    previewelem.removeClass('fa-2x fa-3x fa-4x fa-5x');
                                    previewelem.css('font-size', $(elem).val() + 'px');
                                }
                                else {
                                    previewelem.css('font-size', '').removeClass('fa-2x fa-3x fa-4x fa-5x');
                                    previewelem.addClass('fa-' + $(elem).val());
                                }
                            }
                        }
                    }
                }
            },
            createModal: function (id) {
                var modal = $('#tinymce-modal-param-' + id);
                if (modal.length === 0) {
                    var md = $(TinyMCETemplate.data.modalHtml);
                    var elmain = $('#' + id);

                    /*#region text*/

                    var str = TinyMCETemplate.text.Header;
                    var textAttr = elmain.attr('data-headerTitle');
                    if (textAttr && textAttr.length > 0)
                        str = textAttr;
                    md.find('h4.modal-title').text(str);

                    str = TinyMCETemplate.text.CloseButton;
                    textAttr = elmain.attr('data-closeBtnText');
                    if (textAttr && textAttr.length > 0)
                        str = textAttr;
                    md.find('.modal-footer input[data-dismiss]').val(str);

                    str = TinyMCETemplate.text.DeleteButton;
                    textAttr = elmain.attr('data-deleteBtnText');
                    if (textAttr && textAttr.length > 0)
                        str = textAttr;
                    md.find('.modal-footer input[data-delete]').val(str);

                    str = TinyMCETemplate.text.SearchPlaceHolder;
                    textAttr = elmain.attr('data-searchPlaceHolder');
                    if (textAttr && textAttr.length > 0)
                        str = textAttr;
                    md.find('.main-param .search-input').attr('placeholder', str);

                    str = TinyMCETemplate.text.SearchProcess;
                    textAttr = elmain.attr('data-searchProcessText');
                    if (textAttr && textAttr.length > 0)
                        str = textAttr;
                    md.find('.search-results .page-header').html(str + ' \'<span class="text-color-default"></span>\'');

                    /*#endregion*/

                    md.attr('id', 'tinymce-modal-param-' + id);

                    textAttr = elmain.attr('data-dataParamIsIcon');
                    var isIcon = textAttr && textAttr.toLowerCase() === 'true';

                    var renderType = elmain.attr('data-renderAs');
                    if (renderType && renderType.toLowerCase() === 'table') {
                        TinyMCETemplate.mainFunction.renderTableItem(id, md.find('.main-item'), isIcon);
                    }
                    else if (renderType && renderType.toLowerCase() === 'listicon') {
                        TinyMCETemplate.mainFunction.renderListItem(id, md.find('.main-item'), isIcon);
                    }
                    else
                        TinyMCETemplate.mainFunction.renderItem(id, md.find('.main-item'), isIcon);

                    var inp = md.find('#search-input');
                    inp.attr('id', 'search-input-' + id);
                    inp.keyup(function () {
                        TinyMCETemplate.data.delay(TinyMCETemplate.mainFunction.searchIcon, 500);
                    });

                    md.find('[for="search-input"]').attr('for', 'search-input-' + id);

                    md.find('.search-clear').click(function () {
                        TinyMCETemplate.mainFunction.clearSearch();
                        return false;
                    });

                    $('body').append(md);

                    if (typeof $.fn.draggable === 'function') {
                        md.find('.modal-content').draggable({
                            handle: ".modal-header"
                        });
                    }

                    //create qtip setting
                    textAttr = elmain.attr('data-dataParamIsIcon');
                    if (textAttr && textAttr === 'true') {

                        /*#region text*/

                        var arrText = [];
                        str = TinyMCETemplate.text.OuterHtml;
                        textAttr = elmain.attr('data-iconTextOuterHtml');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{OuterHtml}', val: str });

                        str = TinyMCETemplate.text.HtmlCode;
                        textAttr = elmain.attr('data-iconTextHtmlCode');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{HtmlCode}', val: str });

                        str = TinyMCETemplate.text.CssContent;
                        textAttr = elmain.attr('data-iconTextContentCss');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{CssContent}', val: str });

                        str = TinyMCETemplate.text.IconSize;
                        textAttr = elmain.attr('data-iconTextIconSize');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{IconSize}', val: str });

                        str = TinyMCETemplate.text.Preview;
                        textAttr = elmain.attr('data-iconTextPreview');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{Preview}', val: str });

                        str = TinyMCETemplate.text.SelectButton;
                        textAttr = elmain.attr('data-iconTextSelectBtn');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{Select}', val: str });

                        str = TinyMCETemplate.text.Guide;
                        textAttr = elmain.attr('data-iconTextGuide');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;
                        arrText.push({ key: '{Guide}', val: str.length > 0 ? ('<p>' + str + '</p>') : '' });

                        arrText.push({ key: '{id}', val: id });

                        str = '';
                        var clmain = elmain.attr('data-iconMainClass') || '';
                        var clPrefix = elmain.attr('data-iconPrefix') || '';
                        if (clmain.toLowerCase() === 'fa')//fontawesome, it support other define size
                        {
                            var othersizehtml = TinyMCETemplate.data.qtipOtherSize;

                            var strother = TinyMCETemplate.text.OtherSize;
                            textAttr = elmain.attr('data-iconTextOtherSize');
                            if (textAttr && textAttr.length > 0)
                                strother = textAttr;

                            othersizehtml = TinyMCETemplate.commonFunction.replaceAll(othersizehtml, '{OtherSize}', strother);
                            str = othersizehtml;
                        }
                        arrText.push({ key: '{OtherSizeHtml}', val: str });

                        /*#endregion*/

                        var fullhtml = TinyMCETemplate.data.qtipIconHtml;
                        $.each(arrText, function (i, o) {
                            fullhtml = TinyMCETemplate.commonFunction.replaceAll(fullhtml, o.key, o.val);
                        });

                        $('body').append('<span class="qtiphidden hide" id="qtip-' + id + '"></span>');

                        str = TinyMCETemplate.text.Setting;
                        textAttr = elmain.attr('data-iconSettingTitle');
                        if (textAttr && textAttr.length > 0)
                            str = textAttr;

                        $('#qtip-' + id).qtip({
                            content: {
                                title: str,
                                text: fullhtml
                            },
                            show: {
                                effect: false
                            },
                            hide: {
                                event: false
                            },
                            position: {
                                my: 'bottom center',
                                at: 'top center',
                                viewport: $(window),
                                adjust: {
                                    //scroll: false,
                                    method: 'flipinvert',
                                    screen: true
                                }
                            },
                            style: {
                                def: false, classes: 'tipcontent qtip-bootstrap',
                                tip: {
                                    width: 10,
                                    height: 5
                                }
                            },
                            prerender: true,
                            events: {
                                render: function (ev, api) {

                                    //bind input size
                                    var inputsize = api.elements.content.find('#txtsize' + id);
                                    if (inputsize.length > 0) {
                                        inputsize.keyup(function () {
                                            TinyMCETemplate.data.delay(function () {
                                                TinyMCETemplate.mainFunction.setSizeIcon(inputsize);
                                            }, 500);
                                        }).blur(function () {
                                            TinyMCETemplate.mainFunction.setSizeIcon(this);
                                        });
                                    }

                                    var btnothersize = api.elements.content.find('.mainset .btnothersize .btn');
                                    if (btnothersize.length > 0) {
                                        btnothersize.click(function () {
                                            TinyMCETemplate.mainFunction.setSizeIcon(this);
                                            return false;
                                        });
                                    }

                                    var btnselect = api.elements.content.find('.mainset .btnsel');
                                    if (btnselect.length > 0) {
                                        btnselect.click(function () {
                                            var selected = [];
                                            selected = md.find('.search-results ul.list-icons li.selected');
                                            if (selected.length === 0)
                                                selected = md.find('.main-item ul.list-icons li.selected');

                                            if (selected.length > 0) {
                                                var code = $(selected).attr('data-param-code');
                                                if (code && code.length > 0) {
                                                    var obj = TinyMCETemplate.mainFunction.getDataParamObject(code);
                                                    if (typeof obj !== 'undefined') {
                                                        var cl = '';
                                                        if (typeof $.fn.qtip === 'function') {
                                                            var api = $('#qtip-' + id).qtip('api');
                                                            if (api !== null) {
                                                                var qtipcontent = api.elements.content;
                                                                if (qtipcontent.length > 0) {
                                                                    var previewelem = qtipcontent.find('.mainset .preview').children().eq(0);
                                                                    var st = '', cl = '';
                                                                    var arrcls = 'fa-2x fa-3x fa-4x fa-5x'.split(' ');
                                                                    $.each(arrcls, function (i, o) {
                                                                        if (previewelem.hasClass(o)) {
                                                                            cl = o;
                                                                            return false;
                                                                        }
                                                                    });

                                                                    if (cl.length === 0) {
                                                                        var attr = previewelem.attr('style');
                                                                        if (attr && attr.toLowerCase().indexOf('font-size') >= 0)
                                                                            st = 'style="font-size:' + previewelem.css('font-size') + '"';
                                                                    }

                                                                    api.hide();

                                                                    var inpsize = api.elements.content.find('#txtsize' + id);
                                                                    inpsize.val('');
                                                                    TinyMCETemplate.mainFunction.InsertParamToEditor(obj.id, obj.code, obj.text, st, cl);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            return false;
                                        });
                                    }
                                }
                            }
                        });

                        md.find('ul.list-icons li').click(function () {
                            if ($(this).hasClass('selected') === false) {
                                var selected = md.find('.main-item ul.list-icons li.selected');
                                if (selected.length > 0)
                                    selected.removeClass('selected');
                                $(this).addClass('selected');
                                TinyMCETemplate.mainFunction.showInfoIcon(this);
                            }
                        });

                    }


                    md.on('hide.bs.modal', function () {
                        var el = tinymce.activeEditor.dom.select('span[data-param-code].sp-param.tmp-sel');
                        if ($(el).length > 0)
                            $(el).removeClass('tmp-sel');

                        TinyMCETemplate.mainFunction.clearSearch();
                        tinymce.activeEditor.focus();
                        //tinymce.activeEditor.selection.collapse(false);
                    });
                }
            },
            saveTokenKey: function (id) {

                var editor = undefined;
                if (typeof id === 'undefined' || id === null)
                    editor = tinymce.activeEditor;
                else
                    editor = tinymce.get(id);
                if (typeof editor === 'undefined')
                    editor = tinymce.activeEditor;
                var arrkey = [];

                if (editor) {
                    var paramColl = editor.dom.select('span[data-param-code].sp-param');
                    if (paramColl.length > 0) {
                        var attr = '';
                        $.each(paramColl, function (i, o) {
                            attr = $(o).attr('data-param-code');
                            if (attr && attr.length > 0) {
                                if ($.inArray(attr, arrkey) >= 0)
                                { }
                                else
                                    arrkey.push(attr);
                            }
                        });
                    }

                    TinyMCETemplate.data.addedKey[editor.id] = arrkey;
                }

                return arrkey;
            },
            getTokenKey: function (id) {

                var editor = undefined;
                if (typeof id === 'undefined' || id === null)
                    editor = tinymce.activeEditor;
                else
                    editor = tinymce.get(id);
                if (typeof editor === 'undefined')
                    editor = tinymce.activeEditor;

                return TinyMCETemplate.data.addedKey[editor.id];
            },
            removeUnicode: function (str) {
                str = str.toLowerCase();
                str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
                str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
                str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
                str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
                str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
                str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
                str = str.replace(/đ/g, "d");

                //str= str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-"); 
                //str= str.replace(/-+-/g,"-"); //thay thế 2- thành 1- 
                //str= str.replace(/^\-+|\-+$/g,"");  

                return str;
            },
            makeHightlightIfMatch: function (id) {
                var editor = undefined;
                if (typeof id === 'undefined' || id === null)
                    editor = tinymce.activeEditor;
                else
                    editor = tinymce.get(id);
                if (typeof editor === 'undefined')
                    editor = tinymce.activeEditor;

                //create anchor
                editor.execCommand('mceInsertContent', false, '<span data-anchor-position="1">_</span>');

                var content = editor.getContent();
                //var bm = editor.selection.getBookmark(0);

                //remove all highlight
                var divFinal = $('<div>' + content + '</div>');
                var spColl = divFinal.find('span[data-htmatch]');
                if (spColl.length > 0) {
                    $.each(spColl, function (i, o) {
                        $(o).replaceWith($(o).html());
                    });
                    content = divFinal.html();
                }

                /*#region remove unicode*/

                var tempToken = [];
                divFinal = $('<div>' + content + '</div>');
                var tokenColl = divFinal.find('span[data-param-code].sp-param');
                if (tokenColl.length > 0) {
                    tempToken = tokenColl.clone();
                    $.each(tokenColl, function (i, o) {
                        $(o).replaceWith('<span data-token-param-temp="' + i + '"><span>');
                    });
                }

                content = TinyMCETemplate.mainFunction.removeUnicode(divFinal.html());

                if (tempToken.length > 0) {
                    divFinal = $('<div>' + content + '</div>');
                    var tempPosition = divFinal.find('span[data-token-param-temp]');
                    if (tempPosition.length > 0) {
                        $.each(tempPosition, function (i, o) {
                            $(o).replaceWith($(tempToken).eq(i));
                        });
                    }
                    content = divFinal.html();
                }

                /*#endregion*/

                var arr = editor.getParam('highlightkey');
                if (arr.length > 0) {
                    var reg = undefined;
                    $.each(arr, function (i, o) {
                        reg = new RegExp('(' + RegExp.escape(o) + ')', 'gi');
                        content = content.replace(reg, function replace(match) {
                            return '<span data-htmatch="1" style="color:red">' + match + '</span>';
                        });
                    });

                    /*add space
                    var hasAdd = false;
                    divFinal = $('<div>' + content + '</div>');
                    var last = divFinal.find('span[data-htmatch]:last');
                    if (last.length > 0) {
                        var ch = last[0].parentNode.childNodes;
                        if (ch !== null && ch.length > 0) {
                            var ln = ch[ch.length - 1];
                            if (ln.nodeType === 3) {
                            }
                            else {
                                hasAdd = true;
                                var padRight = document.createTextNode("\u00A0");
                                last[0].parentNode.appendChild(padRight);
    
                            }
                        }
                        content = divFinal.html();
                    }
                    */

                    editor.setContent(content);

                    //move cursor back to anchor
                    var el = editor.dom.select('span[data-anchor-position="1"]');

                    if (el.length > 0) {
                        editor.selection.select(el[el.length - 1], false);
                        editor.selection.collapse(false);
                        var patemp = el[el.length - 1].parentNode;
                        if (patemp.childNodes.length === 1)
                            editor.selection.setContent('<br data-mce-bogus="1">');
                        editor.dom.remove(el);
                    }
                }
            },
            test: function () {
                var editor = tinymce.activeEditor;
                editor.focus();

                //create anchor
                editor.execCommand('mceInsertContent', false, '<span style="color:blue" data-anchor-position="1">\'</span>');

                //test replace content
                var content = editor.getContent();
                content = content.replace('test', '****OK**** ');
                editor.setContent(content);

                //move cursor back to anchor
                var el = editor.dom.select('span[data-anchor-position="1"]');
                if (el.length > 0) {
                    editor.selection.select(el[el.length - 1], false);
                    editor.selection.collapse(false);
                    var patemp = el[el.length - 1].parentNode;
                    if (patemp.childNodes.length === 1)
                        editor.selection.setContent('<br data-mce-bogus="1">');
                    editor.dom.remove(el);
                }
            },
            initSMS: function (id) {

                var editor = undefined;
                if (typeof id === 'undefined' || id === null)
                    editor = tinymce.activeEditor;
                else
                    editor = tinymce.get(id);
                if (typeof editor === 'undefined')
                    editor = tinymce.activeEditor;

                editor.on('keyup', function (e) {
                    var evt = e || window.event // IE support
                    var kc = e.which || evt.keyCode;
                    var ctrlDown = evt.ctrlKey || evt.metaKey; // Mac support

                    // Check for Alt+Gr (http://en.wikipedia.org/wiki/AltGr_key)
                    if (ctrlDown && evt.altKey) return true;

                    if (ctrlDown) {
                        if ((kc === 65 || kc === 97) /* 'A' or 'a'*/ || (kc === 67 || kc === 99)/* 'C' or 'c'*/) {
                            e.preventDefault();
                            return false;
                        }
                    }

                    if (kc === 32/*white space*/) {
                        var nodeSel = editor.selection.getNode();
                        if ($(nodeSel).is('[data-htmatch]')) {
                            var ht = $(nodeSel).text().trim();
                            var cl = $(nodeSel).clone();
                            cl.text(ht);
                            //editor.selection.setNode(cl.wrap('<div></div>').parent().html() + '&nbsp;');
                            editor.selection.select(nodeSel);
                            editor.execCommand('mceReplaceContent', false, cl.wrap('<div></div>').parent().html() + '&nbsp;');
                        }
                    }
                    else if ([37, 38, 39, 40, 16, 17, 18, 35, 36, 34, 33].indexOf(kc) != -1) { }
                    else
                        TinyMCETemplate.data.delay(TinyMCETemplate.mainFunction.makeHightlightIfMatch, 500);
                    //TinyMCETemplate.data.delay(TinyMCETemplate.mainFunction.test, 500);

                });

                editor.settings.charLimit = { 1: 160, 2: 306, 3: 459 };

                var arrhighlightkey = [];
                var datahightlight = $(editor.getElement()).attr('data-highlightkey');
                if (typeof datahightlight === 'undefined')
                    datahightlight = TinyMCETemplate.data.highlightkey;

                if (typeof datahightlight !== 'undefined') {
                    var splitkey = datahightlight.split(',');
                    if (splitkey.length > 0) {
                        $.each(splitkey, function (i, o) {
                            if ($.trim(o).length > 0) {
                                if ($.inArray($.trim(o), arrhighlightkey) === -1)
                                    arrhighlightkey.push($.trim(o));
                            }
                        });
                    }
                }
                editor.settings.highlightkey = arrhighlightkey || [];

                /*#region plugin charactercount*/

                var plugins = editor.settings.plugins;
                if (plugins !== null && plugins.length > 0)
                    plugins += ', ';
                else
                    plugins = ' ';
                plugins += 'charactercount';
                editor.settings.plugins = plugins;


                var funCharacterCount = tinymce.PluginManager.get('charactercount');
                if (typeof funCharacterCount === 'undefined') {
                    tinymce.PluginManager.add('charactercount', function (edi) {
                        var self = this;
                        var maxChar = 0;
                        var pageCount = 1;

                        function GetCharacterSetting() {
                            var countSet = edi.getParam('charLimit');
                            if (typeof countSet !== 'undefined' && $.isEmptyObject(countSet) === false) {
                                var maxPage = 1;
                                var maxCh = 1;
                                for (var key in countSet) {
                                    if (maxPage < key)
                                        maxPage = key;

                                    if (maxCh < countSet[key])
                                        maxCh = countSet[key];
                                }
                                pageCount = maxPage;
                                maxChar = maxCh;
                            }
                        }

                        GetCharacterSetting();

                        self.update = function () {
                            var count = edi.plugins["charactercount"].getCount();

                            /*#region chars count*/

                            var coll = edi.theme.panel.find('#statusbar')[0].find('.charactercount');

                            edi.theme.panel.find('#statusbar')[0].find('#charactercount')
                                .text(['Số kí tự: {0}', count]);

                            if (count > maxChar) {
                                $.each(coll, function () {
                                    this.classes.add('cmax');
                                });
                            }
                            else {
                                $.each(coll, function () {
                                    this.classes.remove('cmax');
                                });
                            }

                            /*#endregion*/

                            /*#region page count*/

                            var countSet = edi.getParam('charLimit');

                            var pcel = edi.theme.panel.find('#statusbar')[0].find('#pagecount');
                            var page = 1;
                            if (typeof countSet !== 'undefined' && $.isEmptyObject(countSet) === false) {
                                var pt = 1;
                                for (var k in countSet) {
                                    if (count > countSet[k])
                                        pt += 1;
                                    else
                                        break;
                                }
                                page = pt;
                            }

                            if (page > pageCount)
                                pcel[0].classes.add('cmax');
                            else
                                pcel[0].classes.remove('cmax');
                            pcel.text(['Số trang: {0}/{1},', page, pageCount]);

                            /*#endregion*/

                        }

                        var statusbar = edi.theme.panel && edi.theme.panel.find('#statusbar')[0];

                        if (statusbar) {
                            window.setTimeout(function () {
                                statusbar.insert({
                                    type: 'label',
                                    name: 'maxchartext',
                                    text: ['/ ' + maxChar],
                                    classes: 'charactercount',
                                    disabled: false,
                                    style: 'padding-left:0'
                                }, 0);
                                statusbar.insert({
                                    type: 'label',
                                    name: 'charactercount',
                                    text: ['Số kí tự: {0}', self.getCount()],
                                    classes: 'charactercount',
                                    disabled: false,
                                    style: 'padding-right:2px'
                                }, 0);
                                statusbar.insert({
                                    type: 'label',
                                    name: 'pagecount',
                                    text: ['Số trang: {0}/{1},', 1, pageCount],
                                    classes: 'pagecount',
                                    disabled: false,
                                    style: 'padding-right:2px'
                                }, 0);
                            }, 0);

                            window.setTimeout(function () {
                                //callback
                                edi.on('keyup NodeChange beforeaddundo', self.update);

                            }, 150);

                        };

                        self.getCount = function () {
                            //var tx = edi.getContent({ format: 'raw' });
                            var tx = edi.getContent({ format: 'html' });
                            var decoded = decodeHtml(tx);
                            var decodedStripped = decoded.replace(/(<([^>]+)>)/ig, "");
                            var tc = decodedStripped.length;
                            //count double chars
                            var re = /([\^\{\}\[\]\~\\|\n])/gi;
                            var m;
                            var countDouble = 0;
                            while ((m = re.exec(decoded)) !== null) {
                                if (m.index === re.lastIndex) {
                                    re.lastIndex++;
                                }
                                // View your result using the m-variable.
                                // eg m[0] etc.
                                countDouble += 1;
                            }
                            tc += countDouble;
                            return tc;
                        };

                        function decodeHtml(html) {
                            var txt = document.createElement("textarea");
                            txt.innerHTML = html;
                            return txt.value;
                        }
                    });
                    funCharacterCount = tinymce.PluginManager.get('charactercount');
                }

                editor.plugins['charactercount'] = new funCharacterCount(editor);

                /*#endregion*/

                setTimeout(function () {
                    editor.plugins['charactercount'].update();
                    TinyMCETemplate.mainFunction.makeHightlightIfMatch(id);
                }, 100);
            },
            initForElement: function (txt) {
                if (txt) {
                    var id = $(txt).attr('id');
                    TinyMCETemplate.mainFunction.createModal(id);

                    var content = $(txt).val();
                    if (content.indexOf('<') >= 0 && content.indexOf('>') >= 0) { }
                    else {
                        content = TinyMCETemplate.commonFunction.htmlDecode(content);
                        $(txt).val(content);
                    }

                    var tbar = $(txt).attr('data-toolbar');
                    if (tbar && tbar.length > 0) { }
                    else
                        tbar = 'insertfile undo redo | styleselect | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent' +
                                    //' | link image | print preview media | forecolor backcolor emoticons';
                                    ' | link image | media | forecolor backcolor emoticons';

                    var plug = $(txt).attr('data-plugins');
                    if (plug && plug.length > 0) { }
                    else
                        plug = [
                            //'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                            'advlist autolink lists link image charmap hr anchor pagebreak',
                            'searchreplace wordcount visualblocks visualchars code fullscreen',
                            'insertdatetime media nonbreaking save table contextmenu directionality',
                            //'emoticons template paste textcolor colorpicker textpattern imagetools'
                            'emoticons paste textcolor colorpicker textpattern'
                        ];

                    var menuObj = {
                        file: { title: 'File', items: 'newdocument' },
                        edit: { title: 'Edit', items: 'undo redo | cut copy paste pastetext | selectall' },
                        //insert: { title: 'Insert', items: 'link media | template hr' },
                        insert: { title: 'Insert', items: 'link media | template hr' },
                        view: { title: 'View', items: 'visualaid | fullscreen' },
                        format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' },
                        table: { title: 'Table', items: 'inserttable tableprops deletetable | cell row column' },
                        //tools: { title: 'Tools', items: 'spellchecker code' }
                        tools: { title: 'Tools', items: 'code' }
                    };

                    var issms = $(txt).attr('data-smsconfig');
                    if (issms === '1' || issms === 'true') {
                        plug = 'code, preview, paste, contextmenu, searchreplace';
                        tbar = 'code | preview | searchreplace';
                        menuObj = {
                            file: { title: 'File', items: 'newdocument' },
                            edit: { title: 'Edit', items: 'undo redo | cut copy paste pastetext | selectall' },
                            insert: { title: 'Insert', items: 'link media | template hr' },
                            view: { title: 'View', items: 'visualaid | fullscreen' }
                            //format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' },
                            //table: { title: 'Table', items: 'inserttable tableprops deletetable | cell row column' },
                            //tools: { title: 'Tools', items: 'spellchecker code' }
                        };
                    }

                    var txth = txt.style.height || '100%';
                    var txtw = txt.style.width || '100%';

                    tinymce.init({
                        selector: 'textarea#' + id + '.tinymce-template',
                        mode: 'exact',
                        height: (txth === '100%' ? '' : txth),
                        width: (txtw === '100%' ? '' : txtw),
                        convert_urls: false,
                        plugins: [
                            plug.join(',') + (plug.length > 0 ? "," : "") + "codemirror,noneditable,fullscreen"
                        ],
                        codemirror: {
                            indentOnInit: true,
                            path: 'codeMirror-5.10.1',
                            config: {
                                lineNumbers: true,
                                mode: "htmlmixed"
                            },
                            fullscreen: true
                        },

                        extended_valid_elements: 'pre[*],script[*],style[*]',
                        valid_children: "+body[style|script],pre[script|div|p|br|span|img|style|h1|h2|h3|h4|h5],*[*]",
                        valid_elements: '*[*]',
                        force_p_newlines: false,
                        cleanup: false,
                        forced_root_block: false,
                        force_br_newlines: true,
                        /*
                        spellchecker_callback: function (method, text, success, failure) {
                            tinymce.util.JSONRequest.sendRPC({
                                url: "/tinymce/spellchecker.php",
                                method: "spellcheck",
                                params: {
                                    lang: this.getLanguage(),
                                    words: text.match(this.getWordCharPattern())
                                },
                                success: function (result) {
                                    success(result);
                                },
                                error: function (error, xhr) {
                                    failure("Spellcheck error:" + xhr.status);
                                }
                            })
                        },*/
                        menu: menuObj,
                        encoding: "xml",
                        style_formats: [
                            {
                                title: 'Headers', items: [
                                { title: 'Header 1', format: 'h1' },
                                { title: 'Header 2', format: 'h2' },
                                { title: 'Header 3', format: 'h3' },
                                { title: 'Header 4', format: 'h4' },
                                { title: 'Header 5', format: 'h5' },
                                { title: 'Header 6', format: 'h6' }
                                ]
                            },
                            {
                                title: 'Inline', items: [
                                { title: 'Bold', icon: 'bold', format: 'bold' },
                                { title: 'Italic', icon: 'italic', format: 'italic' },
                                { title: 'Underline', icon: 'underline', format: 'underline' },
                                { title: 'Strikethrough', icon: 'strikethrough', format: 'strikethrough' },
                                { title: 'Superscript', icon: 'superscript', format: 'superscript' },
                                { title: 'Subscript', icon: 'subscript', format: 'subscript' },
                                { title: 'Code', icon: 'code', format: 'code' }
                                ]
                            },
                            {
                                title: 'Blocks', items: [
                                { title: 'Paragraph', format: 'p' },
                                { title: 'Blockquote', format: 'blockquote' },
                                { title: 'Div', format: 'div' },
                                { title: 'Pre', format: 'pre' }
                                ]
                            },
                            {
                                title: 'Alignment', items: [
                                { title: 'Left', icon: 'alignleft', format: 'alignleft' },
                                { title: 'Center', icon: 'aligncenter', format: 'aligncenter' },
                                { title: 'Right', icon: 'alignright', format: 'alignright' },
                                { title: 'Justify', icon: 'alignjustify', format: 'alignjustify' }
                                ]
                            }/*,
                            {
                                title: 'Custom button style', items: [
                                    { title: 'Button 1', selector: 'a,input[button]', classes: 'button1' },
                                    { title: 'Button 2', selector: 'a,input[button]', classes: 'button2' },
                                    { title: 'Badge', inline: 'span', styles: { display: 'inline-block', border: '1px solid #2276d2', 'border-radius': '5px', padding: '2px 5px', margin: '0 2px', color: '#2276d2' } },
                                ]
                            }*/
                        ],
                        fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt",
                        extended_valid_elements: 'span[class|style]',
                        toolbar1: tbar + (tbar.length > 0 ? " | " : "") + "fullscreen | customEmElementMenuItem",
                        toolbar2: "sizeselect | bold italic | fontselect | fontsizeselect | code",
                        contextmenu: "bold italic | cut copy paste | undo redo",
                        //content_css: "editor.css",
                        content_style: '.sp-param{display: inline-block;padding: 2px 3px;\
background-color: #49128A;color: white;border-radius: 3px;font-weight: bold;cursor: pointer;font-size: 10px}',
                        setup: function (editor) {
                            /* ok1
                            editor.on('submit', function (e) {
                                editor.save();
                            });
            
                            editor.on('SaveContent', function (e) {
            
                                e.content = e.content.replace(/</gi, "&lt;").replace(/\>/gi, "&gt;");
                            });
                            */

                            editor.on('keydown', function (evt) {
                                var el = tinymce.activeEditor.dom.select('span[data-param-code].sp-param.tmp-sel');
                                if (el.length > 0) {
                                    evt.preventDefault();
                                    evt.stopPropagation();
                                    return false;
                                }

                                if ((evt.which === 8 || evt.keyCode === 8) || (evt.which === 46 || evt.keyCode === 46)
                                    && editor.selection) {

                                }
                            });

                            editor.on('init', function () {

                                TinyMCETemplate.mainFunction.saveTokenKey(editor.id);
                                var doc = this.contentDocument;

                                $(doc).find('.tmp-sel').removeClass('tmp-sel');

                                $(doc).bind('click', function (e) {
                                    var tg = e.target;
                                    if (tg) {
                                        if ($(tg).is('span[data-param-code].sp-param') === true) {
                                            e.preventDefault();
                                            e.stopPropagation();
                                            var code = $(tg).addClass('tmp-sel').attr('data-param-code');
                                            TinyMCETemplate.mainFunction.OpenModalSelectParam(code);
                                        }
                                    }
                                });

                                var elmain = $(editor.getElement());
                                var onloadFunc = elmain.attr('data-onLoadCallback');
                                if (onloadFunc && onloadFunc.length > 0) {
                                    if ($.isFunction(window[onloadFunc]) === true)
                                        window[onloadFunc](editor);
                                }



                                if (issms === '1' || issms === 'true') {
                                    setTimeout(function () {
                                        TinyMCETemplate.mainFunction.initSMS(id);
                                    }, 100);
                                }
                            });

                            editor.on('SaveContent', function (e) {
                                var hdfid = $(editor.getElement()).attr('data-hdf');
                                if (hdfid && hdfid.length > 0) {
                                    var hdf = $('#' + hdfid);
                                    var data = TinyMCETemplate.mainFunction.saveTokenKey(editor.id);
                                    if (data.length > 0)
                                        hdf.val(data.join(','));
                                    else
                                        hdf.val('');
                                    //editor.save();
                                }

                                //$(e.target.targetElm).val(e.content);
                            });

                            var textButton = TinyMCETemplate.text.toolbarButton;
                            var attrText = $(txt).attr('data-toolbarBtnText');
                            if (attrText && attrText.length > 0)
                                textButton = attrText;
                            editor.addButton('customEmElementMenuItem', {
                                text: textButton,
                                'classes': 'btncust',
                                //context: 'tools',
                                onclick: function () {
                                    tinymce.setActive(editor);
                                    // Open windotew
                                    TinyMCETemplate.mainFunction.OpenModalSelectParam();
                                }
                            });

                            var idtemp = '';
                            var tokentext = '';
                            var datacode = '';
                            editor.on('BeforeExecCommand', function (x) {
                                idtemp = '';
                                tokentext = '';
                                datacode = '';
                                if (x.command !== 'selectAll') {

                                    var els = editor.selection.getStart();
                                    var ele = editor.selection.getEnd();
                                    if (els === ele) {
                                        if ($(els).hasClass('sp-param') && $(els).is(':empty') === false) {
                                            idtemp = 'mce-temp-' + (Math.floor(Math.random() * 10));
                                            $(els).attr('data-tid', idtemp);

                                            if (x.command === 'RemoveFormat') {
                                                tokentext = $(els).text();
                                                datacode = $(els).attr('data-param-code') || '';
                                            }
                                        }
                                    }

                                    var tagColl = editor.dom.select('span[data-param-code].sp-param');
                                    $.each(tagColl, function () {
                                        $(this).removeAttr('contenteditable').removeClass('mceNonEditable');
                                    });
                                }

                            });

                            editor.on('ExecCommand', function (x) {
                                if (x.command != 'selectAll') {
                                    var tagColl = editor.dom.select('span[data-param-code].sp-param');
                                    $.each(tagColl, function () {
                                        $(this).attr('contenteditable', 'false').addClass('mceNonEditable');
                                    });
                                    var act = tinymce.activeEditor.dom.select('span[data-param-code].sp-param.tmp-sel');
                                    if ($(act).length > 0)
                                        $(act).removeClass('tmp-sel');

                                    if (x.command === 'RemoveFormat') {
                                        if (tokentext.length > 0) {
                                            if (tokentext === tinymce.activeEditor.selection.getContent()) {
                                                var newel = '<span class="sp-param tmp-sel" data-mce-temp="1"' +
                                                    ' data-param-code="' + datacode +
                                                    '" contenteditable="false">' + tokentext + '</span>';
                                                tinymce.activeEditor.selection.setContent(newel);
                                                var elnew = tinymce.activeEditor.dom.select('[data-mce-temp]');
                                                if (elnew.length > 0) {
                                                    $(elnew).removeAttr('data-mce-temp');
                                                    tinymce.activeEditor.selection.select(elnew[0]);
                                                }
                                            }
                                        }
                                    }
                                    else if (idtemp.length > 0) {
                                        //    ', idtemp: ', idtemp);
                                        var elfind = tinymce.activeEditor.dom.select('[data-tid="' + idtemp + '"]');
                                        if (elfind.length > 0) {
                                            $(elfind).removeAttr('data-tid');
                                            tinymce.activeEditor.selection.select(elfind[0]);
                                        }
                                    }
                                }
                            });
                        }
                    });
                }
            },

            callInit: function () {
                tinymce.remove();

                // polyfill for RegExp.escape
                if (!RegExp.escape) {
                    RegExp.escape = function (s) {
                        return String(s).replace(/[\\^$*+?.()|[\]{}]/g, '\\$&');
                    };
                }

                if (typeof TinyMCETemplate.data.delay === 'undefined') {
                    TinyMCETemplate.data.delay = (function () {
                        var timer = 0;
                        return function (callback, ms) {
                            clearTimeout(timer);
                            timer = setTimeout(callback, ms);
                        };
                    })();
                }

                TinyMCETemplate.commonFunction.extendJquery();

                var txt = $('textarea.tinymce-template');
                if (txt.length > 0) {
                    $.each(txt, function (i, o) {
                        TinyMCETemplate.mainFunction.initForElement(o);
                    });
                }
            },
            OpenModalSelectParam: function (id) {
                var editorid = tinymce.activeEditor.id;
                if (typeof id !== 'undefined' && id.length > 0) {
                    var ele = $('#tinymce-modal-param-' + editorid + ' .main-param li[data-param-code="'
                             + id.replace(/"/g, "\"") + '"]');

                    if (ele.length > 0) {
                        if (ele.hasClass('selected') === false) {
                            var active = $('#tinymce-modal-param-' + editorid + ' .main-param li[data-param-code].selected');
                            if (active.length > 0)
                                active.removeClass('selected');
                            ele.addClass('selected');
                        }
                    }
                    else {
                        var active = $('#tinymce-modal-param-' + editorid + ' .main-param li[data-param-code].selected');
                        if (active.length > 0)
                            active.removeClass('selected');
                    }
                }
                else {
                    var active = $('#tinymce-modal-param-' + editorid + ' .main-param li[data-param-code].selected');
                    if (active.length > 0)
                        active.removeClass('selected');
                }
                $('#tinymce-modal-param-' + editorid).modal('show');
            },
            InsertParamToEditor: function (id, text, style, classes) {

                var editorid = tinymce.activeEditor.id;

                TinyMCETemplate.mainFunction.processRemove(function () {

                    var elmain = $('#' + editorid);
                    var selectType = elmain.attr('data-selectAsIcon');
                    var html = '';
                    if (selectType && selectType.toLowerCase() === 'true') {
                        var clmain = elmain.attr('data-iconMainClass') || '';
                        var clPrefix = elmain.attr('data-iconPrefix') || '';

                        html = '<span class="sp-param mceNonEditable ' + clmain.replace(/"/g, "\"") + (clmain.length > 0 ? ' ' : '') +
                         (clPrefix) + ((typeof classes !== 'undefined' && classes.length > 0 ? ' ' : '') + (classes || '')) +
                         '"' + ((typeof style !== 'undefined' && style.length > 0 ? ' ' : '') + (style || '')) +
                         ' contenteditable="false" data-param-code="' + (id).replace(/"/g, "\"") + '"></span>&nbsp;';
                    }
                    else
                        html = '<span class="sp-param mceNonEditable" contenteditable="false" data-param-code="' +
                            (id).replace(/"/g, "\"") + '">' + text + '</span>&nbsp;';

                    tinymce.activeEditor.focus();

                    tinymce.activeEditor.execCommand('mceInsertContent', false, html);

                    //tinymce.activeEditor.windowManager.close();

                    $('#tinymce-modal-param-' + editorid).modal('hide');

                });
            },
            processRemove: function (callback) {
                var editorid = tinymce.activeEditor.id;

                var el = tinymce.activeEditor.dom.select('span[data-param-code].sp-param.tmp-sel');
                if (el.length === 0) {
                    el = tinymce.activeEditor.selection.getNode();
                    if ($(el).is('span[data-param-code].sp-param') === false)
                        el = [];
                    else
                        el = [el];
                }

                if (el.length > 0) {
                    var txt = el[0].nextSibling;
                    if (txt !== null && txt.nodeType === 3)//text node
                    {
                        var data = $(txt).text();
                        if (data.charCodeAt(0) === 160) {

                            data = data.substring(1);
                            $(txt).replaceWith(data);
                        }
                    }

                    var bm = tinymce.activeEditor.selection.getBookmark();
                    setTimeout(function () {
                        tinymce.activeEditor.dom.remove(el);
                        tinymce.activeEditor.selection.moveToBookmark(bm);

                        if (typeof callback === 'function')
                            callback();
                    }, 10);
                }
                else {
                    if (typeof callback === 'function')
                        callback();
                }
            },
            DeleteSelectedParam: function (isHideModal) {
                TinyMCETemplate.mainFunction.processRemove();
                var editorid = tinymce.activeEditor.id;

                if (typeof isHideModal !== 'undefined' && isHideModal === true)
                    $('#tinymce-modal-param-' + editorid).modal('hide');
            }
        }
    }
}

$(function () {
    CallTinyMCE();
    if (typeof Sys !== 'undefined') {
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            if (TinyMCETemplate.data.init === true) {
                TinyMCETemplate.data.init = false;
            }
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CallTinyMCE);
    }
});

function CallTinyMCE() {
    if (typeof TinyMCETemplate !== 'undefined') {
        if (TinyMCETemplate.data.init === false) {
            TinyMCETemplate.data.init = true;
            TinyMCETemplate.mainFunction.callInit();
        }
    }
}