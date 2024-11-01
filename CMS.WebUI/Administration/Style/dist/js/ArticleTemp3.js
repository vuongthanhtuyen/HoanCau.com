var makeNewFeature3 = true;
function RemoveItem3(tag, event) {
    event.stopPropagation();
    $(tag).closest(".item").remove();
    var tagId = $('[data-selector="txtHeaderTemplate3"]').attr("id");
    CKEDITOR.instances[tagId].setData("");
    $("#btnMakeNewTemp3Feature").show();
};
function ConfirmHeaderTemplate3() {
    var socialButton = "";
    if ($('[data-selector="txtFacebook"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtFacebook"]').val(), "Facebook", "fa-facebook");
    if ($('[data-selector="txtYoutube"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtYoutube"]').val(), "Youtube", "fa-youtube");
    if ($('[data-selector="txtPinterest"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtPinterest"]').val(), "Pinterest", "fa-pinterest-p");
    if ($('[data-selector="txtTwitter"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtTwitter"]').val(), "Twitter", "fa-twitter");
    if ($('[data-selector="txtInstagram"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtInstagram"]').val(), "Instagram", "fa-instagram");
    if ($('[data-selector="txtEmail"]').val().length > 0)
        socialButton += String.format(htmlImageWithLogoSocialButton, $('[data-selector="txtEmail"]').val(), "Email", "fa-envelope-o");
    //
    if ($("select[id*='ddlFeatureMediaType']").val() === "0") {
        var imageTag = String.format(htmlImageWithLogoTag, $('[data-selector="imgTemplate3Thumb"]').attr("src"), $('[data-selector="txtImageCaption"]').val()
            , $('[data-selector="txtImageCredits"]').val(), $('[data-selector="imgTemplate3Logo"]').attr("src"), $('[data-selector="txtLogoCaption"]').val(),
            socialButton);
        var tagId = $('[data-selector="txtHeaderTemplate3"]').attr("id");
        CKEDITOR.instances[tagId].setData(imageTag);
        $('#header-template-3').html(imageTag);
    }
    else {
        var videoTag = String.format(htmlVideoWithLogoTag, $('[data-selector="txtVideoLink"]').val(), $('[data-selector="imgTemplate3Thumb"]').attr("src")
            , $('[data-selector="txtImageCaption"]').val(), $('[data-selector="txtImageCredits"]').val(), $('[data-selector="imgTemplate3Logo"]').attr("src")
            , $('[data-selector="txtLogoCaption"]').val(), socialButton);
        var tagId = $('[data-selector="txtHeaderTemplate3"]').attr("id");
        CKEDITOR.instances[tagId].setData(videoTag);
        $('#header-template-3').html(videoTag);
    }
    $("#btnMakeNewTemp3Feature").hide();
    ManagerControl3();
}
function ManagerControl3() {
    if ($('[data-selector="txtHeaderTemplate3"]').val() !== "")
        $("#btnMakeNewTemp3Feature").hide();
    //Only Manager
    $("#header-template-3 .item").attr("onclick", "EditFeature3(this);");
    $("#header-template-3 .item").each(function (index, tag) {
        if ($(tag).find(".remove-item").length === 0)
            $(tag).append("<div class='remove-item' onclick='RemoveItem3(this,event)'><i class='fa fa-times' aria-hidden='true'></i></div>");
    })
}
function OpenSelectTempalte3Image() {
    var txtid = $('[data-selector="txtTemplate3Image"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadPhotoFullWidthKey
        + '&selectFun=setTemplate3ImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setTemplate3ImageUrl(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgTemplate3Thumb"]').attr('src', url);
    //call next function
};
function OpenSelectTempalte3Logo() {
    var txtid = $('[data-selector="txtTemplate3Logo"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadThumbnailKey
        + '&selectFun=setTemplate3Logo',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setTemplate3Logo(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgTemplate3Logo"]').attr('src', url);
    //call next function
};
function MakeNewFeature3() {
    selectedFeatureElement3 = undefined;
    makeNewFeature3 = true;
}
var selectedFeatureElement3;
function EditFeature3(tag) {
    makeNewFeature3 = false;
    selectedFeatureElement3 = tag;
    $('#upLoadHeaderTemplate3').modal("show")
}
$(document).ready(function () {
    Template3ModalShow();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Template3ModalShow);
    ManagerControl3();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerControl3);
});
function Template3ModalShow() {
    if (!$('#upLoadHeaderTemplate3').hasClass("shown-binded")) {
        $('#upLoadHeaderTemplate3').addClass("shown-binded");
        $('#upLoadHeaderTemplate3').on('shown.bs.modal', function () {
            if (makeNewFeature3) {
                $('[data-selector="txtImageCredits"],[data-selector="txtImageCaption"],[data-selector="txtVideoLink"],[data-selector="txtLogoCaption"],[data-selector="txtFacebook"],[data-selector="txtYoutube"],[data-selector="txtPinterest"],[data-selector="txtTwitter"],[data-selector="txtInstagram"],[data-selector="txtEmail"]').val("");
                $('[data-selector="txtTemplate3Image"],[data-selector="txtTemplate3Logo"]').val("~/uploads/Article/no-image.jpg");
                $("[id*='ddlFeatureMediaType']").val(0);
                $("select[id*='ddlFeatureMediaType']").trigger("change");
            }
            else {
                var logo = $(selectedFeatureElement3).find(".logo");
                if (logo.length === 1) {
                    $('[data-selector="txtTemplate3Logo"]').val(logo.attr("src"));
                    $('[data-selector="txtLogoCaption"]').val(logo.attr("alt"));
                }
                $(selectedFeatureElement3).find(".social-media .fa").each(function (index, tag) {
                    if ($(tag).hasClass("fa-facebook"))
                        $('[data-selector="txtFacebook"]').val($(tag).parent().attr("href"));
                    else if ($(tag).hasClass("fa-youtube"))
                        $('[data-selector="txtYoutube"]').val($(tag).parent().attr("href"));
                    else if ($(tag).hasClass("fa-pinterest-p"))
                        $('[data-selector="txtPinterest"]').val($(tag).parent().attr("href"));
                    else if ($(tag).hasClass("fa-twitter"))
                        $('[data-selector="txtTwitter"]').val($(tag).parent().attr("href"));
                    else if ($(tag).hasClass("fa-instagram"))
                        $('[data-selector="txtInstagram"]').val($(tag).parent().attr("href"));
                    else if ($(tag).hasClass("fa-envelope-o"))
                        $('[data-selector="txtEmail"]').val($(tag).parent().attr("href"));
                })
                var img = $(selectedFeatureElement3).find(".image");
                if (img.length === 1) {
                    $('[data-selector="txtImageCredits"]').val(img.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(img.attr("alt"));
                    $('[data-selector="txtTemplate3Image"]').val(img.attr("src"));
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(0);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
                else {
                    var video = $(selectedFeatureElement3).find("video");
                    $('[data-selector="txtVideoLink"]').val(video.find("source").attr("src"));
                    $('[data-selector="txtImageCredits"]').val(video.attr("data-credits"));
                    $('[data-selector="txtImageCaption"]').val(video.attr("title"));
                    $('[data-selector="txtTemplate3Image"]').val(video.attr("poster"));
                    setTimeout(function () {
                        $("[id*='ddlFeatureMediaType']").val(1);
                        $("select[id*='ddlFeatureMediaType']").trigger("change");
                    }, 1);
                }
            }
        });
    }
}