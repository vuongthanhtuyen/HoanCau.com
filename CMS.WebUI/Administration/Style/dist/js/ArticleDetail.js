$(document).ready(function () {
    $.validationEngine.defaults.validateNonVisibleFields = true;
    SectionCollapse();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SectionCollapse);
    setTimeout(function () {
        SortSection();
    }, 500);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SortSection);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(DeletedSection);
    BindSectionTitle();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(BindSectionTitle);
});
function BindSectionTitle() {
    var sectionTitle = $('[data-selector="txtSectionTitle"]').val();
    if (sectionTitle !== "") {
        sectionTitle = sectionTitle.split("|");
        for (var i = 0; i < sectionTitle.length; i++) {
            $($(".section-title.get-title")[i]).val(sectionTitle[i]);
        }
    }
}
function MakeSaveSectionTitle() {
    var sectionTitle = "";
    $(".section-title.get-title").each(function (index, tag) {
        if (index === 0)
            sectionTitle = $(tag).val();
        else
            sectionTitle = String.format("{0}|{1}", sectionTitle, $(tag).val());
    })
    $('[data-selector="txtSectionTitle"]').val(sectionTitle);
}
function MakeSectionTitle() {
    var sectionTitle = "";
    switch ($("select[id*='ddlTemplate']").val()) {
        case "1":
        case "4":
            sectionTitle = $(".section-title.get-title").val();
            break;
        case "2":
            $(".section-title.get-title").each(function (index, tag) {
                if (index === 0)
                    sectionTitle = $(tag).val();
                else
                    sectionTitle = String.format("{0}|{1}", sectionTitle, $(tag).val());
            })
            break;
        case "3":
            $(".section-title.get-title").each(function (index, tag) {
                if (index === 0)
                    sectionTitle = $(tag).val();
                if (index === $(".section-title.get-title").length - 1) {
                    if (index === 0)
                        sectionTitle = String.format("|{0}", sectionTitle);
                    else
                        sectionTitle = String.format("{0}|", sectionTitle);
                }
                if (index !== 0)
                    sectionTitle = String.format("{0}|{1}", sectionTitle, $(tag).val());
            })
            break;
    }
    $('[data-selector="txtSectionTitle"]').val(sectionTitle);
}
function DeletedSection() {
    var selectorStr;
    $('[data-selector="txtDeletedTemplateContentId"]').val().split("|").forEach(function (val) {
        selectorStr = String.format("[name='{0}']", val);
        $(selectorStr).closest(".section-content").addClass("deleted");
    })
}
function ScrollToLastSection() {
    setTimeout(function () {
        $('.box-container').animate({
            scrollTop: $('.section-content:not(.footer-section):last').offset().top - $('.box-container > .row').offset().top
        }, 'slow');
    }, 500)
}
function ScrollToSelectedSection() {
    setTimeout(function () {
        $('.box-container').animate({
            scrollTop: selectedOffsetTop
        }, 'slow');
    }, 500)
}
function RemoveSection(tag) {
    if ($(tag).closest(".section-content").hasClass("deleted")) {
        $(tag).closest(".section-content").removeClass("deleted");
        $(tag).find("i").removeClass("fa-undo").addClass("fa-trash");
        $(tag).attr("title", "Remove this section");
    }
    else {
        $(tag).closest(".section-content").addClass("deleted");
        $(tag).find("i").removeClass("fa-trash").addClass("fa-undo");
        $(tag).attr("title", "Undo");
    }
    var deletedTemplateContentId = "";
    $(".section-content.deleted").each(function (index, tag) {
        if (deletedTemplateContentId === "")
            deletedTemplateContentId = $(tag).find("textarea").attr("name");
        else
            deletedTemplateContentId += String.format("|{0}", $(tag).find("textarea").attr("name"));
    })
    $('[data-selector="txtDeletedTemplateContentId"]').val(deletedTemplateContentId);
}
function CancelSort() {
    $(".section-content .section-body").collapse("show");
    $(".sort-parent").removeClass("sorting");
}
function SortSection() {
    if ($("select[id*='ddlTemplate']").val() === "1") {
        $(".section-content .control").hide();
        return;
    }
    var tempCKData;
    if (!$(".sort-parent > div").hasClass("ui-sortable")) {
        var ckeConfigs = [];
        var ckId = [];
        var type = [];
        var sectionId = [];
        var order = [];
        var showNav = [];
        var startOrder;
        Array.prototype.swapItems = function (a, b) {
            this[a] = this.splice(b, 1, this[a])[0];
            return this;
        }
        $(".sort-parent > div").sortable({
            items: '.section-content:not(.deleted)',
            handle: ".handle",
            create: function (event, ui) {
                var data = $('[data-selector="txtTemplateContentId"]').val().split("|");
                for (var i = 0; i < data.length; i = i + 4) {
                    ckId[i / 4] = data[i];
                    type[i / 4] = data[i + 1];
                    sectionId[i / 4] = data[i + 2];
                    showNav[i / 4] = data[i + 3];
                    order[i / 4] = i / 4;
                }
            },
            start: function (event, ui) {
                startOrder = ui.item.index();
                if ($(".section-content .section-body.collapse.in").length > 0) {
                    $(".section-content .section-body").collapse("hide");
                    $(".sort-parent").addClass("sorting");
                    $(ui.item.context).trigger('mouseup');
                    setTimeout(function () {
                        $(".handle.ui-sortable-handle").trigger("mousedown");
                    }, 1)
                }
                $('textarea.ck-editor', ui.item).each(function () {
                    var tagId = $(this).attr('id');
                    var ckeClone = $(this).next('.cke').clone().addClass('cloned');
                    ckeConfigs[tagId] = CKEDITOR.instances[tagId].config;
                    CKEDITOR.instances[tagId].destroy();
                    $(this).hide().after(ckeClone);
                });
            },
            stop: function (event, ui) {
                var stopOrder = ui.item.index();
                if (startOrder < stopOrder)
                    for (var i = stopOrder; i > startOrder; i--)
                        order = order.swapItems(i, startOrder);
                else
                    for (var i = stopOrder; i < startOrder; i++)
                        order = order.swapItems(i, startOrder);
                $('[data-selector="txtTemplateContentId"]').val("");
                var newOrder = "";
                newOrder = String.format("{0}|{1}|{2}|{3}", ckId[order[0]], type[order[0]], sectionId[order[0]], showNav[order[0]]);
                for (var i = 1; i < order.length; i++)
                    newOrder += String.format("|{0}|{1}|{2}|{3}", ckId[order[i]], type[order[i]], sectionId[order[i]], showNav[order[i]]);
                $('[data-selector="txtTemplateContentId"]').val(newOrder);
                $('textarea.ck-editor', ui.item).each(function () {
                    var tagId = $(this).attr('id');
                    CKEDITOR.replace(tagId, ckeConfigs[tagId]);
                    $(this).next('.cloned').remove();
                    var data = CKEDITOR.instances[tagId].getData();
                    CKEDITOR.instances[tagId].setData(htmlDecode(data));
                });
            }
        });
    }
}
function SectionCollapse() {
    $(".section-body").each(function (index, tag) {
        if (!$(tag).hasClass("collapse"))
            $(tag).collapse("toggle");
    })
}
function Collapse(tag) {
    $(".section-content .section-body").collapse("hide");
    $(".sort-parent").addClass("sorting");
    //var collapseElement = $(tag).closest(".section-content").find(".section-body");
    //if (collapseElement.hasClass("in"))
    //    collapseElement.collapse("hide");
    //else
    //    collapseElement.collapse("show");
}
function RemoveThumbnail() {
    $('[data-selector="imgThumb"]').attr("src", "/uploads/Article/no-image.jpg");
    $('[data-selector="txtImage"]').val("/uploads/Article/no-image.jpg");
};
function Publish() {
    MakeSaveSectionTitle();
    $('[data-selector="chkPusblish"]').prop("checked", true);
    $('[data-selector="btnSave"]')[0].click();
};
function SaveDraft() {
    MakeSaveSectionTitle();
    $('[data-selector="chkPusblish"]').prop("checked", false);
    $('[data-selector="btnSave"]')[0].click();
};
function CheckValid() {
    var validated = $(".validationEngineContainer").validationEngine('validate');
    if (validated)
        DisableContentChanged();
    else {
        $('.collapsed-box [data-widget="collapse"]').click();
        setTimeout(function () {
            $(".validationEngineContainer").validationEngine('validate');
        }, 500);
    }
    return validated;
};
function DisableContentChanged() {
    window.onbeforeunload = null;
};
function CreateFriendlyUrl(tag) {
    var str = $(tag).val().latinise();
    str = str.replace(/[^\w\s]+/g, '').replace(/\s+/g, "-").toLowerCase();
    $('[data-selector="txtFriendlyURL"]').val(str);
    //if (arId !== "") {
    //    $('[data-selector="btnPreview2"]').attr("href", "/review-article/" + str);
    //}
};

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
};
$(window).resize(function () {
    delay(resizeBox, 500);
});
var OpenSelectFeatureBlock = function () {
    var txtid = $('[data-selector="txtFeatureBlock"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadThumbnailKey
        + '&selectFun=setFeatureBlockUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
var setFeatureBlockUrl = function (txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgFeatureBlock"]').attr('src', url);
    //call next function
};
function RemoveFeatureBlock() {
    $('[data-selector="imgFeatureBlock"]').attr("src", "/uploads/Article/no-image.jpg");
    $('[data-selector="txtFeatureBlock"]').val("/uploads/Article/no-image.jpg");
};
var OpenSelectImage = function () {
    var txtid = $('[data-selector="txtImage"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadThumbnailKey
        + '&selectFun=setImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
var setImageUrl = function (txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgThumb"]').attr('src', url);
    //call next function
};
var calculateLightbox = function (wpercent, hpercent) {
    var ret = [];
    var ws = getWindowSize();
    if (typeof wpercent !== 'undefined') {
        ret.push((ws.width * wpercent) / 100);
    }
    if (typeof hpercent !== 'undefined') {
        ret.push((ws.height * hpercent) / 100);
    }
    return ret;
};
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
    return {
        width: w, height: h
    };
};
var htmlEncode = function (str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            //.replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
};
var htmlDecode = function (value) {
    return String(value)
        .replace(/&quot;/g, '"')
        //.replace(/&#39;/g, "'")
        .replace(/&lt;/g, '<')
        .replace(/&gt;/g, '>')
        .replace(/&amp;/g, '&');
};
function NewGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
};
function Review() {
    $("#popReview:not(binded)").on('shown.bs.modal', function () {
        $("#popReview").addClass("binded");
        var iframe = $("#popReview").find("iframe");
        iframe[0].onload = function () {
            var category;
            if ($("select[id*='ddlCategory']").select2('data').length > 0) {
                var text = $("select[id*='ddlCategory']").select2('data')[0].text.split("','");
                for (var i = 0; i < text.length; i++) {
                    if (category === undefined)
                        category = text[i];
                    else
                        category = String.format("{0}, {1}", category, text[i]);
                }
            }
            else
                category = "";
            var date = new Date($($("input[id*='txtPublishTime']")[0]).val());
            var options = { year: 'numeric', month: 'long', day: 'numeric' };
            var pusblishDate = date.toLocaleDateString('en-GB', options);
            var feature;
            var body;
            switch ($("select[id*='ddlTemplate']").val()) {
                case "1":
                    var tagId = $('[data-selector="txtHeaderTemplate1"]').attr("id");
                    feature = CKEDITOR.instances[tagId].getData();
                    tagId = $("textarea[id*='txtTemplateContent']").attr("id");
                    body = CKEDITOR.instances[tagId].getData();
                    break;
                case "2":
                    var tagId = $('[data-selector="txtHeaderTemplate2"]').attr("id");
                    feature = CKEDITOR.instances[tagId].getData();
                    body = "";
                    $("textarea[id*='txtTemplateContent'],textarea[id*='txtSectionContent']").each(function (index, tag) {
                        tagId = $(tag).attr("id");
                        body += CKEDITOR.instances[tagId].getData();
                    })
                    break;
                case "3":
                    var tagId = $('[data-selector="txtHeaderTemplate3"]').attr("id");
                    feature = CKEDITOR.instances[tagId].getData();
                    body = [];
                    $("textarea[id*='txtTemplateContent'],textarea[id*='txtSectionContent']").each(function (index, tag) {
                        var section = {};
                        tagId = $(tag).attr("id");
                        section.Title = $(tag).closest(".section-content").find(".control .section-title.get-title").val();
                        if (section.Title === "" && index === $("textarea[id*='txtTemplateContent'],textarea[id*='txtSectionContent']").length - 1)
                            section.Title = "Footer";
                        section.Content = CKEDITOR.instances[tagId].getData();
                        body[index] = section;
                    })
                    break;
                case "4":
                    var tagId = $('[data-selector="txtHeaderTemplate4"]').attr("id");
                    feature = CKEDITOR.instances[tagId].getData();
                    tagId = $("textarea[id*='txtTemplateContent']").attr("id");
                    body = CKEDITOR.instances[tagId].getData();
                    break;
            }
            iframe[0].contentWindow.a(
                $("select[id*='ddlTemplate']").val(),
                $("input[id*='txtTitle']").val(),
                $("input[id*='txtAuthorName']").val(),
                category,
                pusblishDate,
                feature,
                body,
                $("textarea[id*='txtSummary']").val()
            );
        };
        iframe.attr("src", "/Administration/ReviewArticle.aspx");
    });
    $('#popReview').modal({ show: true })
};
function ChangePreviewUrl(url) {
    $('[data-selector="btnPreview2"]').attr("href", url);
};