var makeNewMediaSection = true;
function RemoveMediaSection(tag, event) {
    event.stopPropagation();
    $(tag).closest(".item").remove();
};
var selectedOffsetTop;
function ConfirmHotspotSection() {
    var imageTag = $("#upLoadHeaderTemplate2 .img-thumbnail img");
    if (imageTag.length === 0)
        return;
    //var image = new Image();
    //image.src = imageTag.attr("src");
    var radioHeight = imageTag[0].naturalHeight / imageTag.height();
    var radioWidth = imageTag[0].naturalWidth / imageTag.width();
    var spLength = $("#coords-map area").length;
    var defaultCoords = [];
    for (var i = 0; i < spLength; i++) {
        var newCoords = "";
        defaultCoords[i] = $($("#coords-map area")[i]).attr("coords").split(",");
        for (var j = 0; j < defaultCoords[i].length; j++) {
            if (j % 2 === 0)
                newCoords += parseInt(defaultCoords[i][j] * radioWidth) + ",";
            else
                newCoords += parseInt(defaultCoords[i][j] * radioHeight) + ",";
        }
        newCoords = newCoords.slice(0, newCoords.length - 1);
        $($("#coords-map area")[i]).attr("coords", newCoords);
    }
    var hotspotTag = String.format(htmlHotspot, $('[data-selector="imgTemplate2Thumb"]').attr("src"), $('#coords-map').html(), $('#hoverNotice').html()
            , $('#upLoadHeaderTemplate2 [data-selector="txtImageCaption"]').val(), $('#upLoadHeaderTemplate2 [data-selector="txtImageCredits"]').val());
    if (makeNewFeature2) {
        $('[data-seclector="txtSectionContent"]').val(htmlEncode(hotspotTag));
        $('[data-selector="btnAddMediaSection"]')[0].click();
    }
    else {
        var tagId = $(selectedFeatureElement2).closest(".section-body").find(".section-value").attr("id");
        CKEDITOR.instances[tagId].setData(hotspotTag);
        $('[data-selector="btnEditMediaSection"]')[0].click();
    }
    ManagerHotspotControl();
}
function ManagerHotspotControl() {
    $(".section-media .section-body .htmlHotspot-content").attr("onclick", "EditHotspotSection(this);");
}
//var selectedSectionHotspotElement;
function EditHotspotSection(tag) {
    var listItem = $(String.format(".{0}", $(tag).attr("class")));
    hotSpotIndex = listItem.index($(tag));
    makeNewFeature2 = false;
    selectedFeatureElement2 = tag;
    $('#upLoadHeaderTemplate2').modal("show");
}
function ConfirmMediaSection() {
    var sectionValue;
    var mediaType;
    switch ($("select[id*='ddlSectionMediaType']").val()) {
        case "0":
            mediaType = "Image";
            var imageTag = String.format(htmlImageTag, $('[data-selector="imgSectionThumb"]').attr("src"), $('[data-selector="txtSectionCaption"]').val()
            , $('[data-selector="txtSectionCredits"]').val(), $("select[id*='ddlSectionMediaType']").val(), imgSectionArea);
            sectionValue = imageTag;
            break;
        case "1":
            mediaType = "ImageFullWidth";
            var imageTag = String.format(htmlImageFullWidthTag, $('[data-selector="imgSectionThumb"]').attr("src"), $('[data-selector="txtSectionCaption"]').val()
            , $('[data-selector="txtSectionCredits"]').val(), $("select[id*='ddlSectionMediaType']").val());
            sectionValue = imageTag;
            break;
        case "3":
            mediaType = "ImageLeft";
            $('[data-selector="social-ou"] .oa:checked').attr("checked", "");
            var imageTag = String.format(htmlImageWithContent, "content-in-left", $('[data-selector="imgSectionThumb"]').attr("src")
                , $('[data-selector="txtMOCTitle"]').val(), $('[data-selector="txtSectionCredits"]').val(), $('[data-selector="txtMOCSubTitle"]').val()
                , $('[data-selector="social-ou"]').html(), $('[data-selector="txtMOCFootnote"]').val(), $("select[id*='ddlSectionMediaType']").val());
            sectionValue = imageTag;
            break;
        case "4":
            mediaType = "ImageRight";
            $('[data-selector="social-ou"] .oa:checked').attr("checked", "");
            var imageTag = String.format(htmlImageWithContent, "content-in-right", $('[data-selector="imgSectionThumb"]').attr("src")
                , $('[data-selector="txtMOCTitle"]').val(), $('[data-selector="txtSectionCredits"]').val(), $('[data-selector="txtMOCSubTitle"]').val()
                , $('[data-selector="social-ou"]').html(), $('[data-selector="txtMOCFootnote"]').val(), $("select[id*='ddlSectionMediaType']").val());
            sectionValue = imageTag;
            break;
        case "5":
            mediaType = "Video";
            var source = $('[data-selector="txtSectionVideoLink"]').val();
            var type = "youtube";
            if (source.indexOf("vimeo.com") !== -1)
                type = "vimeo";
            var videoTag = String.format(htmlVideoTag, source
                , $('[data-selector="imgSectionThumb"]').attr("src"), $('[data-selector="txtSectionCaption"]').val()
                , $('[data-selector="txtSectionCredits"]').val(), $("select[id*='ddlSectionMediaType']").val(), type);
            sectionValue = videoTag;
            break;
        case "6":
            mediaType = "VideoFullWidth";
            var source = $('[data-selector="txtSectionVideoLink"]').val();
            var type = "youtube";
            if (source.indexOf("vimeo.com") !== -1)
                type = "vimeo";
            var videoTag = String.format(htmlVideoFullWidthTag, source
                , $('[data-selector="imgSectionThumb"]').attr("src"), $('[data-selector="txtSectionCaption"]').val()
                , $('[data-selector="txtSectionCredits"]').val(), $("select[id*='ddlSectionMediaType']").val(), type);
            sectionValue = videoTag;
            break;
        case "7":
            mediaType = "VideoLeft";
            $('[data-selector="social-ou"] .oa:checked').attr("checked", "");
            var source = $('[data-selector="txtSectionVideoLink"]').val();
            var type = "youtube";
            if (source.indexOf("vimeo.com") !== -1)
                type = "vimeo";
            var videoTag = String.format(htmlVideoWithContent, "content-in-left", $('[data-selector="imgSectionThumb"]').attr("src")
                , $('[data-selector="txtMOCTitle"]').val(), $('[data-selector="txtSectionCredits"]').val(), $('[data-selector="txtMOCSubTitle"]').val()
                , $('[data-selector="social-ou"]').html(), $('[data-selector="txtMOCFootnote"]').val(), $("select[id*='ddlSectionMediaType']").val()
                , source, type);
            sectionValue = videoTag;
            break;
        case "8":
            mediaType = "VideoRight";
            $('[data-selector="social-ou"] .oa:checked').attr("checked", "");
            var source = $('[data-selector="txtSectionVideoLink"]').val();
            var type = "youtube";
            if (source.indexOf("vimeo.com") !== -1)
                type = "vimeo";
            var videoTag = String.format(htmlVideoWithContent, "content-in-right", $('[data-selector="imgSectionThumb"]').attr("src")
                , $('[data-selector="txtMOCTitle"]').val(), $('[data-selector="txtSectionCredits"]').val(), $('[data-selector="txtMOCSubTitle"]').val()
                , $('[data-selector="social-ou"]').html(), $('[data-selector="txtMOCFootnote"]').val(), $("select[id*='ddlSectionMediaType']").val()
                , source, type);
            sectionValue = videoTag;
            break;
    }
    if (selectedMediaSection === undefined) {
        $('[data-seclector="txtSectionContent"]').val(htmlEncode(sectionValue));
        $('[data-selector="btnAddMediaSection"]')[0].click();
    }
    else {
        var sectionList = $('[data-selector="txtTemplateContentId"]').val();
        var tagId = $(selectedMediaSection).closest(".section-body").find(".section-value").attr("id");
        var name = $(selectedMediaSection).closest(".section-body").find(".section-value").attr("name");
        var sectionListAr = sectionList.split(name);
        var repalceFo = String.format("{0}|{1}", name, sectionListAr[1].split("|")[1]);
        var repalceTo = String.format("{0}|{1}", name, mediaType);
        sectionList = sectionList.replace(repalceFo, repalceTo);
        $('[data-selector="txtTemplateContentId"]').val(sectionList);
        CKEDITOR.instances[tagId].setData(sectionValue);
        $('[data-selector="btnEditMediaSection"]')[0].click();
        selectedMediaSection = undefined;
    }
    ManagerSectionControl();
}
function ManagerSectionControl() {
    $(".section-media .section-body .item,.section-media .section-body .media-with-content").attr("onclick", "EditMediaSection(this);");
    $("video").removeAttr("controls");
}
function EditMediaSection(tag) {
    makeNewMediaSection = false;
    selectedMediaSection = tag;
    $('#upLoadSectionMedia').modal("show")
}
function OpenSelectSectionImage() {
    var uploadKey;
    switch ($("select[id*='ddlSectionMediaType']").val()) {
        case "0":
            uploadKey = uploadPhotoKey;
            break;
        case "1":
        case "6":
            uploadKey = uploadPhotoFullWidthKey;
            break;
        case "3":
        case "4":
        case "7":
        case "8":
            uploadKey = uploadMediaWithContentKey;
            break;
        case "5":
            uploadKey = uploadVideoPosterKey;
            break;
    }
    var txtid = $('[data-selector="txtSectionImage"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadKey
        + '&selectFun=setSectionImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setSectionImageUrl(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgSectionThumb"]').attr('src', url);
    if ($("select[id*='ddlSectionMediaType']").val() === "0")
        ReSelectImageArea();
    //call next function
};
function MakeNewSectionMedia() {
    MakeSectionTitle();
    $('[data-selector="chkUpHotspot"]').prop("checked", false);
    selectedMediaSection = undefined;
    makeNewMediaSection = true;
}
var selectedMediaSection;
$(document).ready(function () {
    MediaSectionModalShow();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MediaSectionModalShow);
    ManagerSectionControl();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerSectionControl);
    ManagerHotspotControl();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerHotspotControl);
});
function MediaSectionModalShow() {
    if (!$('#upLoadSectionMedia').hasClass("shown-binded")) {
        $("#upLoadSectionMedia").scroll(function () {
            if (ias !== undefined)
                ias.update();
        });
        $('#upLoadSectionMedia').addClass("shown-binded");
        $('#upLoadSectionMedia').on('shown.bs.modal', function () {
            if (selectedMediaSection !== undefined)
                selectedOffsetTop = $(selectedMediaSection).closest(".section-content").offset().top - $('.box-container > .row').offset().top;
            if (makeNewMediaSection) {
                $('[data-selector="txtSectionCredits"],[data-selector="txtSectionCaption"],[data-selector="txtSectionVideoLink"],[data-selector="txtMOCTitle"],[data-selector="txtMOCSubTitle"],[data-selector="txtMOCFootnote"]').val("");
                $('[data-selector="social-ou"] .oa').removeAttr("checked");
                $('[data-selector="txtSocialContent"]').val(htmlEncode($('[data-selector="social-ou"]').html()));
                $('[data-selector="txtSectionImage"]').val("/uploads/Article/no-image.jpg");
                $("[id*='ddlSectionMediaType']")[0].selectedIndex = 0;
                $("select[id*='ddlSectionMediaType']").trigger("change");
            }
            else {
                var mediaType = $(selectedMediaSection).attr("data-mediaType");
                var contentText = $(selectedMediaSection).find(".content-text");
                if (contentText !== undefined) {
                    var title = contentText.find(".title").text();
                    var subTitle = contentText.find(".subtitle").text();
                    var social = contentText.find(".social").html();
                    var footnote = contentText.find(".footnote").text();
                    $('[data-selector="txtMOCTitle"]').val(title);
                    $('[data-selector="txtMOCSubTitle"]').val(subTitle);
                    if (social !== undefined)
                        $('[data-selector="txtSocialContent"]').val(htmlEncode(social));
                    $('[data-selector="txtMOCFootnote"]').val(footnote);
                }
                var img = $(selectedMediaSection).find("img");
                if (img.length === 1) {
                    $('[data-selector="txtSectionCredits"]').val(img.attr("data-credits"));
                    $('[data-selector="txtSectionCaption"]').val(img.attr("alt"));
                    $('[data-selector="txtSectionImage"]').val(img.attr("src"));
                }
                else {
                    var video = $(selectedMediaSection).find("video");
                    $('[data-selector="txtSectionVideoLink"]').val(video.find("source").attr("src"));
                    $('[data-selector="txtSectionCredits"]').val(video.attr("data-credits"));
                    $('[data-selector="txtSectionCaption"]').val(video.attr("title"));
                    $('[data-selector="txtSectionImage"]').val(video.attr("poster"));
                }
                setTimeout(function () {
                    $("[id*='ddlSectionMediaType']").val(mediaType);
                    $("select[id*='ddlSectionMediaType']").trigger("change");
                }, 1);
            }
        });
    }
    if (!$('#upLoadSectionMedia').hasClass("hide-binded")) {
        $('#upLoadSectionMedia').addClass("hide-binded");
        $('#upLoadSectionMedia').on('hide.bs.modal', function () {
            if (ias !== undefined)
                ias.cancelSelection();
            ScrollToSelectedSection();
        })
    }
};
function MarkNewHotspotSection() {
    MakeSectionTitle();
    $('[data-selector="chkUpHotspot"]').prop("checked", true);
    MakeNewFeature2();
};
function AddFreeTextMakeSection() {
    MakeSectionTitle();
};
var ias;
var imgSectionArea = 0;
function ReSelectImageArea() {
    var outerWigth = $('[data-selector="imgSectionThumb"]').outerWidth();
    var outerHeight = $('[data-selector="imgSectionThumb"]').outerHeight();
    if ((outerWigth / 3) * 2 >= outerHeight)
        return;
    var x2 = outerWigth;
    var y2 = (outerWigth / 3) * 2;
    var x1 = 0;
    var y1 = 0;
    if (selectedMediaSection !== undefined) {
        var top = parseFloat($(selectedMediaSection).find("img").attr("data-top"));
        if (top !== undefined && !isNaN(top)) {
            y1 = ($('[data-selector="imgSectionThumb"]').outerHeight() / 100) * top;
            y2 += y1;
        }
    }
    ias = $('[data-selector="imgSectionThumb"]').imgAreaSelect({
        resizable: false,
        x1: 0,
        y1: y1,
        x2: x2,
        y2: y2,
        instance: true,
        handles: true,
        aspectRatio: "3:2",
        onSelectEnd: function (img, section) {
            imgSectionArea = (section.y1 * 100) / outerHeight;
            console.log(imgSectionArea);
        }
    });
};
function DisableSelectImageArea() {
    if (ias !== undefined)
        ias.cancelSelection();
};