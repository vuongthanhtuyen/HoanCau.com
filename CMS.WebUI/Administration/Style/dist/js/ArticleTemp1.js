var makeNewFeature1 = true;
function RemoveItem1(tag, event) {
    event.stopPropagation();
    $(tag).closest(".item").remove();
    RemoveManagerControl1();
    var tagId = $('[data-selector="txtHeaderTemplate1"]').attr("id");
    CKEDITOR.instances[tagId].setData($("#header-template-1").html());
    ManagerControl1();
    $("#btnMakeNewTemp1Feature").show();
};
function RemoveManagerControl1() {
    $("#header-template-1 video").attr("controls", "");
    $("#header-template-1 .item").removeAttr("onclick");
    $("#header-template-1 .item .remove-item").remove();
}
function ConfirmHeaderTemplate1() {
    if ($("select[id*='ddlFeatureMediaType']").val() === "0") {
        if (selectedFeatureElement1 === undefined) {
            var imageTag = String.format(htmlImageFeatureTag, $('[data-selector="imgTemplate1Thumb"]').attr("src"), $('[data-selector="txtImageCaption"]').val()
                , $('[data-selector="txtImageCredits"]').val());
            $("#header-template-1").append(imageTag);
        }
        else
            $(selectedFeatureElement1).find("img").attr("src", $('[data-selector="imgTemplate1Thumb"]').attr("src")).attr("alt", $('[data-selector="txtImageCaption"]').val())
                .attr("data-credits", $('[data-selector="txtImageCredits"]').val());
    }
    else {
        //$("select[id*='ddlFeatureMediaType']").val("video");
        //$("select[id*='ddlFeatureMediaType']").trigger("change");
        var source = $('[data-selector="txtVideoLink"]').val();
        var type = "youtube";
        if (source.indexOf("vimeo.com") !== -1)
            type = "vimeo";
        if (selectedFeatureElement1 === undefined) {
            var videoTag = String.format(htmlVideoFeatureTag, source, $('[data-selector="imgTemplate1Thumb"]').attr("src")
                , $('[data-selector="txtImageCaption"]').val(), $('[data-selector="txtImageCredits"]').val(), "", type);
            $("#header-template-1").append(videoTag);
        }
        else {
            $(selectedFeatureElement1).find("video").attr("poster", $('[data-selector="imgTemplate1Thumb"]').attr("src"))
                .attr("title", $('[data-selector="txtImageCaption"]').val())
                .attr("data-credits", $('[data-selector="txtImageCredits"]').val());
            $(selectedFeatureElement1).find("video source").attr("src", $('[data-selector="txtVideoLink"]').val()).attr("type", String.format("video/{0}", type));
        }
    }
    RemoveManagerControl1();
    var tagId = $('[data-selector="txtHeaderTemplate1"]').attr("id");
    CKEDITOR.instances[tagId].setData($("#header-template-1").html());
    ManagerControl1();
}
function ManagerControl1() {
    if ($("#header-template-1 .item").length === 3) {
        $("#btnMakeNewTemp1Feature").hide();
    }
    //Only Manager
    $("#header-template-1 video").removeAttr("controls");
    $("#header-template-1 .item").attr("onclick", "EditFeature1(this);");
    $("#header-template-1 .item").each(function (index, tag) {
        if ($(tag).find(".remove-item").length === 0)
            $(tag).append("<div class='remove-item' onclick='RemoveItem1(this,event)'><i class='fa fa-times' aria-hidden='true'></i></div>");
    })
}
function OpenSelectTempalte1Image() {
    var txtid = $('[data-selector="txtTemplate1Image"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadThumbnailKey
        + '&selectFun=setTemplate1ImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setTemplate1ImageUrl(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgTemplate1Thumb"]').attr('src', url);
    //call next function
};
function MakeNewFeature1() {
    selectedFeatureElement1 = undefined;
    makeNewFeature1 = true;
}
var selectedFeatureElement1;
function EditFeature1(tag) {
    makeNewFeature1 = false;
    selectedFeatureElement1 = tag;
    $('#upLoadHeaderTemplate1').modal("show")
}
$(document).ready(function () {
    Template1ModalShow();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Template1ModalShow);
    ManagerControl1();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerControl1);
});
function Template1ModalShow() {
    if (!$('#upLoadHeaderTemplate1').hasClass("shown-binded")) {
        $('#upLoadHeaderTemplate1').addClass("shown-binded");
        $('#upLoadHeaderTemplate1').on('shown.bs.modal', function () {
            if (makeNewFeature1) {
                $('[data-selector="txtImageCredits"],[data-selector="txtImageCaption"],[data-selector="txtVideoLink"]').val("");
                $('[data-selector="txtTemplate1Image"]').val("~/uploads/Article/no-image.jpg");
                $("[id*='ddlFeatureMediaType']").val(0);
                $("select[id*='ddlFeatureMediaType']").trigger("change");
            }
            else {
                var img = $(selectedFeatureElement1).find("img");
                if (img.length === 1) {
                    $('[data-selector="txtImageCredits"]').val(img.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(img.attr("alt"));
                    $('[data-selector="txtTemplate1Image"]').val(img.attr("src"));
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(0);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
                else {
                    var video = $(selectedFeatureElement1).find("video");
                    $('[data-selector="txtVideoLink"]').val(video.find("source").attr("src"));
                    $('[data-selector="txtImageCredits"]').val(video.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(video.attr("title"));
                    $('[data-selector="txtTemplate1Image"]').val(video.attr("poster"));
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(1);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
            }
        });
    }
}