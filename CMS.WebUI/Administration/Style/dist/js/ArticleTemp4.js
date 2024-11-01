$(document).ready(function () {
    Template4ModalShow();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Template4ModalShow);
    ManagerControl4();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerControl4);
    SortFeature4();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SortFeature4);
});
var makeNewFeature4 = true;
var selectedFeatureElement4;
function MakeNewFeature4() {
    selectedFeatureElement4 = undefined;
    makeNewFeature4 = true;
};
function RemoveItem4(tag, event) {
    event.stopPropagation();
    $(tag).closest(".item").remove();
    RemoveManagerControl4();
    var tagId = $('[data-selector="txtHeaderTemplate4"]').attr("id");
    CKEDITOR.instances[tagId].setData($("#header-template-4").html());
    ManagerControl4();
    SortFeature4();
    $("#btnMakeNewTemp4Feature").show();
};
function RemoveManagerControl4() {
    $("#header-template-4 .item").removeAttr("onclick");
    $("#header-template-4 .item .remove-item").remove();
}
function ConfirmHeaderTemplate4() {
    if ($("select[id*='ddlFeatureMediaType']").val() === "0") {
        if (selectedFeatureElement4 === undefined) {
            var imageTag = String.format(htmlFeatureImageWithContent, $('[data-selector="imgTemplate4Thumb"]').attr("src"), $('[data-selector="txtImageCaption"]').val()
                , $('[data-selector="txtImageCredits"]').val(), "", $('[data-selector="txtTitle"]').val(), $('[data-selector="txtContent"]').val());
            $("#header-template-4").append(imageTag);
        }
        else {
            $(selectedFeatureElement4).find("img").attr("src", $('[data-selector="imgTemplate4Thumb"]').attr("src")).attr("alt", $('[data-selector="txtImageCaption"]').val())
                .attr("data-credits", $('[data-selector="txtImageCredits"]').val());
            $(selectedFeatureElement4).find(".right .title").text($('[data-selector="txtTitle"]').val());
            $(selectedFeatureElement4).find(".right .text").text($('[data-selector="txtContent"]').val());
        }
    }
    else {
        if (selectedFeatureElement4 === undefined) {
            var source = $('[data-selector="txtVideoLink"]').val();
            var type = "youtube";
            if (source.indexOf("vimeo.com") !== -1)
                type = "vimeo";
            var videoTag = String.format(htmlFeatureVideoWithContent, source, $('[data-selector="imgTemplate4Thumb"]').attr("src")
                , $('[data-selector="txtImageCaption"]').val(), $('[data-selector="txtImageCredits"]').val(), "", type
                , $('[data-selector="txtTitle"]').val(), $('[data-selector="txtContent"]').val());
            $("#header-template-4").append(videoTag);
        }
        else {
            $(selectedFeatureElement4).find("video").attr("poster", $('[data-selector="imgTemplate4Thumb"]').attr("src"))
                .attr("title", $('[data-selector="txtImageCaption"]').val())
                .attr("data-credits", $('[data-selector="txtImageCredits"]').val());
            $(selectedFeatureElement4).find(".right .title").text($('[data-selector="txtTitle"]').val());
            $(selectedFeatureElement4).find(".right .text").text($('[data-selector="txtContent"]').val());
        }
        $(selectedFeatureElement4).find("video source").attr("src", $('[data-selector="txtVideoLink"]').val());
    }
    RemoveManagerControl4();
    $("#header-template-4 video").attr("controls", "");
    var tagId = $('[data-selector="txtHeaderTemplate4"]').attr("id");
    CKEDITOR.instances[tagId].setData($("#header-template-4").html());
    ManagerControl4();
    SortFeature4();
}
function OpenSelectTempalte4Image() {
    var txtid = $('[data-selector="txtTemplate4Image"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadThumbnailKey
        + '&selectFun=setTemplate4ImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setTemplate4ImageUrl(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgTemplate4Thumb"]').attr('src', url);
    //call next function
};
function EditFeature4(tag) {
    makeNewFeature4 = false;
    selectedFeatureElement4 = tag;
    $('#upLoadHeaderTemplate4').modal("show")
}
function Template4ModalShow() {
    if (!$('#upLoadHeaderTemplate4').hasClass("shown-binded")) {
        $('#upLoadHeaderTemplate4').addClass("shown-binded");
        $('#upLoadHeaderTemplate4').on('shown.bs.modal', function () {
            if (makeNewFeature4) {
                $('[data-selector="txtImageCredits"],[data-selector="txtImageCaption"],[data-selector="txtVideoLink"],[data-selector="txtTitle"],[data-selector="txtContent"]').val("");
                $('[data-selector="txtTemplate4Image"]').val("~/uploads/Article/no-image.jpg");
                $("[id*='ddlFeatureMediaType']").val(0);
                $("select[id*='ddlFeatureMediaType']").trigger("change");
            }
            else {
                var img = $(selectedFeatureElement4).find("img");
                if (img.length === 1) {
                    $('[data-selector="txtImageCredits"]').val(img.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(img.attr("alt"));
                    $('[data-selector="txtTemplate4Image"]').val(img.attr("src"));
                    $('[data-selector="txtTitle"]').val($(selectedFeatureElement4).find(".right .title").text());
                    $('[data-selector="txtContent"]').val($(selectedFeatureElement4).find(".right .text").text());
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(0);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
                else {
                    var video = $(selectedFeatureElement4).find("video");
                    $('[data-selector="txtVideoLink"]').val(video.find("source").attr("src"));
                    $('[data-selector="txtImageCredits"]').val(video.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(video.attr("title"));
                    $('[data-selector="txtTemplate4Image"]').val(video.attr("poster"));
                    $('[data-selector="txtTitle"]').val($(selectedFeatureElement4).find(".right .title").text());
                    $('[data-selector="txtContent"]').val($(selectedFeatureElement4).find(".right .text").text());
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(1);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
            }
        });
    }
};
function ManagerControl4() {
    //Only Manager
    $("#header-template-4 video").removeAttr("controls");
    $("#header-template-4 .item").attr("onclick", "EditFeature4(this);");
    $("#header-template-4 .item").each(function (index, tag) {
        if ($(tag).find(".remove-item").length === 0)
            $(tag).append("<div class='remove-item' onclick='RemoveItem4(this,event)'><i class='fa fa-times' aria-hidden='true'></i></div>");
    })
};
function SortFeature4()
{
    $("#header-template-4").sortable({
        items: ".item",
        stop: function (event, ui) {
            RemoveManagerControl4();
            $("#header-template-4 video").attr("controls","");
            var tagId = $('[data-selector="txtHeaderTemplate4"]').attr("id");
            CKEDITOR.instances[tagId].setData($("#header-template-4").html());
            ManagerControl4();
        }
    });
}