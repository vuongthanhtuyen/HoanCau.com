function ConfirmHeaderTemplate2() {
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
            , $('[data-selector="txtImageCaption"]').val(), $('[data-selector="txtImageCredits"]').val());
    $("#header-template-2").html(hotspotTag);
    var tagId = $('[data-selector="txtHeaderTemplate2"]').attr("id");
    CKEDITOR.instances[tagId].setData($("#header-template-2").html());
    ManagerControl2();
}
function ManagerControl2() {
    if ($("#header-template-2 .htmlHotspot-content").length === 1) {
        $("#btnMakeNewTemp2Feature").hide();
    }
    //Only Manager
    $("#header-template-2 .htmlHotspot-content").attr("onclick", "EditFeature2(this);");
    if ($("#header-template-2 .htmlHotspot-content .remove-item").length === 0)
        $("#header-template-2 .htmlHotspot-content").append("<div class='remove-item' onclick='RemoveItem2(this,event)'><i class='fa fa-times' aria-hidden='true'></i></div>");
}
function RemoveItem2(tag, event) {
    event.stopPropagation();
    $("#header-template-2").html("");
    var tagId = $('[data-selector="txtHeaderTemplate2"]').attr("id");
    CKEDITOR.instances[tagId].setData("");
    $("#btnMakeNewTemp2Feature").show();
};
var makeNewFeature2 = true;
var selectedFeatureElement2;
var makeNewHotspot = true;
var selectedHotspotElement;
function ConfirmHotspot() {
    if (makeNewHotspot) {
        $("#hoverNotice").append(String.format("<div class='hover-notice'>{0}</div>", $("#txtHotspotDetail").val()));
    }
    else if (selectedHotspotElement !== undefined) {
        var index = $("#coords-map area").index(selectedHotspotElement);
        var notice = $(String.format("#hoverNotice .hover-notice:eq({0})", index));
        if (notice !== undefined)
            notice.text($("#txtHotspotDetail").val());
    }
}
function EditHotspot(tag) {
    makeNewHotspot = false;
    selectedHotspotElement = $(tag);
    var index = $("#coords-map area").index(tag);
    var description = $(String.format("#hoverNotice .hover-notice:eq({0})", index)).text();
    $("#txtHotspotDetail").val(description);
    $("#hotspot-detail").modal("show");
}
function DeleteHotspot() {
    var index = $("#coords-map area").index(selectedHotspotElement);
    var notice = $(String.format("#hoverNotice .hover-notice:eq({0})", index));
    if (notice !== undefined)
        notice.remove();
    selectedHotspotElement.remove();
    defaultCoords[hotSpotIndex].splice(index, 1);
    ReDrawHotspot();
}
var defaultCoords = [];
var hotSpotIndex = 0;;
function ReDrawHotspot() {
    if (ctx === undefined)
        return;
    var outerWidth = $("#upLoadHeaderTemplate2 .img-thumbnail").outerWidth();
    var outerHeight = $("#upLoadHeaderTemplate2 .img-thumbnail").outerHeight();
    ctx.clearRect(0, 0, outerWidth, outerHeight);
    var imageTag = $("#upLoadHeaderTemplate2 .img-thumbnail img");
    if (imageTag.length === 0)
        return;
    //var image = new Image();
    //image.src = imageTag.attr("src");
    var radioHeight = imageTag.height() / imageTag[0].naturalHeight;
    var radioWidth = imageTag.width() / imageTag[0].naturalWidth;
    var spLength = $("#coords-map area").length;
    for (var i = 0; i < spLength; i++) {
        var newCoords = "";
        if (defaultCoords[hotSpotIndex] === undefined)
            defaultCoords[hotSpotIndex] = [];
        if (defaultCoords[hotSpotIndex][i] === undefined)
            defaultCoords[hotSpotIndex][i] = $($("#coords-map area")[i]).attr("coords").split(",");
        for (var j = 0; j < defaultCoords[hotSpotIndex][i].length; j++) {
            if (j % 2 === 0)
                newCoords += parseInt(defaultCoords[hotSpotIndex][i][j] * radioWidth) + ",";
            else
                newCoords += parseInt(defaultCoords[hotSpotIndex][i][j] * radioHeight) + ",";
        }
        newCoords = newCoords.slice(0, newCoords.length - 1);
        $($("#coords-map area")[i]).attr("coords", newCoords);
    }
    $("#coords-map area").each(function (index, tag) {
        var arrCoords = $(tag).attr("coords").split(",");
        for (var i = 2; i < arrCoords.length; i = i + 2)
            AddLine(arrCoords[i - 2], arrCoords[i - 1], arrCoords[i], arrCoords[i + 1]);
    })
}
function MakeNewFeature2() {
    selectedFeatureElement2 = undefined;
    makeNewFeature2 = true;
}
function EditFeature2(tag) {
    makeNewFeature2 = false;
    selectedFeatureElement2 = tag;
    $('#upLoadHeaderTemplate2').modal("show");
}
function OpenSelectTempalte2Image() {
    var txtid = $('[data-selector="txtTemplate2Image"]').attr('id');
    var ws = getWindowSize();
    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        + '&key=' + uploadHotspotKey
        + '&selectFun=setTemplate2ImageUrl',
        {
            iframe: true,
            width: ws.width - 60,
            height: ws.height - 40,
        });
};
function setTemplate2ImageUrl(txtid, url) {
    document.getElementById(txtid).value = url;
    $('[data-selector="imgTemplate2Thumb"]').attr('src', url);
    setTimeout(function () {
        myInit();
        ResetArea();
    }, 500);
};
function NewArea() {
    $("#up-map").css("z-index", "9");
}
function ResetArea() {
    //$("#canvas-map").css("z-index", "0");
    //$("#up-map").css("z-index", "9");
    $("#coords-map,#hoverNotice").empty();
    var outerWidth = $("#upLoadHeaderTemplate2 .img-thumbnail").outerWidth();
    var outerHeight = $("#upLoadHeaderTemplate2 .img-thumbnail").outerHeight();
    //$("#upLoadHeaderTemplate2 .img-thumbnail canvas").attr("width", outerWidth + "px").attr("height", outerHeight + "px");
    ctx.clearRect(0, 0, outerWidth, outerHeight);
}
var fX;
var fY;
var oldX;
var oldY;
var ctx;
//ctx.fillStyle="#373a3c";
function AddLine(oldX, oldY, newX, newY) {
    ctx.beginPath();
    ctx.moveTo(oldX, oldY);
    ctx.lineTo(newX, newY);
    ctx.stroke();
}
function CreateArea(coords) {
    $("#coords-map").append("<area onclick='EditHotspot(this);' onmouseover='myHover(this);' onmouseout='myLeave();' coords='" + coords + fX + "," + fY + "' shape='poly' alt='Sun' href='#'>");
}
function GetCoords() {
    $("#up-map").mousemove(function (event) {
        var asbX = oldX - event.offsetX;
        var asbY = oldY - event.offsetY;
        if (Math.sqrt((asbX * asbX + asbY * asbY)) > 1) {
            coords += oldX + "," + oldY + ",";
            AddLine(oldX, oldY, event.offsetX, event.offsetY);
            oldX = event.offsetX;
            oldY = event.offsetY;
        }
    })
}
function DestroyGetCoords() {
    //$("#canvas-map").css("z-index", "-1");
    $("#up-map").css("z-index", "-1");
    $("#up-map").off("mousemove");
}
var coords = "";
function BindPaint() {
    $("#up-map").mousedown(function (event) {
        fX = oldX = event.offsetX;
        fY = oldY = event.offsetY;
        GetCoords();
    })
    $("#up-map").mouseup(function () {
        if (coords.length > 0) {
            //ConvertCoords();
            DestroyGetCoords();
            CreateArea(coords);
            makeNewHotspot = true;
            selectedHotspotElement = $("#coords-map area:last");
            $("#hotspot-detail").modal("show");
            coords = "";
        }
    })
}
function ConvertCoords() {
    var coordsArray = coords.split(",");
    coords = "";
    for (var i = 0; i < coordsArray.length - 2; i = i + 2) {
        var asbX = Math.abs(coordsArray[i] - coordsArray[i + 2]);
        var asbY = Math.abs(coordsArray[i + 1] - coordsArray[i + 3]);
        if (asbY > asbX) {
            coordsArray[i + 2] = coordsArray[i];
        }
        else {
            coordsArray[i + 3] = coordsArray[i + 1];
        }
        coords += String.format("{0},{1},", coordsArray[i], coordsArray[i + 1]);
    }
    //var mid = parseInt(coordsArray.length / 2);
    //if (mid % 2 !== 0)
    //    mid--;
    //var noticeX1 = parseInt((parseInt(coordsArray[0]) + parseInt(coordsArray[mid])) / 2);
    //var noticeY1 = parseInt((parseInt(coordsArray[1]) + parseInt(coordsArray[mid + 1])) / 2);
    //var mid12 = parseInt(coordsArray.length / 4);
    //if (mid12 % 2 !== 0)
    //    mid12--;
    //var mid34 = parseInt(coordsArray.length * 3 / 4);
    //if (mid34 % 2 !== 0)
    //    mid34--;
    //var noticeX2 = parseInt((parseInt(coordsArray[mid12]) + parseInt(coordsArray[mid34])) / 2);
    //var noticeY2 = parseInt((parseInt(coordsArray[mid12 + 1]) + parseInt(coordsArray[mid34 + 1])) / 2);
    //var noticeX = parseInt((noticeX1 + noticeX2) / 2);
    //var noticeY = parseInt((noticeY1 + noticeY2) / 2);
    //var asbX = noticeX - coordsArray[0];
    //var asbY = noticeY - coordsArray[1];
    //if (!(isNaN(asbX) || isNaN(asbY)))
    //    coords = String.format("{0},{1},{2},{3},{4},{5},{6},{7},"
    //        , coordsArray[0], coordsArray[1]
    //        , coordsArray[0], parseInt(coordsArray[1]) + asbX * 2
    //        , parseInt(coordsArray[0]) + asbY * 2, parseInt(coordsArray[1]) + asbX * 2
    //        , parseInt(coordsArray[0]) + asbY * 2, coordsArray[1]);
}
var hdc;
function byId(e) { return document.getElementById(e); }
function drawPoly(coOrdStr) {
    var mCoords = coOrdStr.split(',');
    var i, n;
    n = mCoords.length;
    hdc.beginPath();
    hdc.moveTo(mCoords[0], mCoords[1]);
    for (i = 2; i < n; i += 2)
        hdc.lineTo(mCoords[i], mCoords[i + 1]);
    var sp = n / 2;
    if (sp % 2 === 1)
        sp--;
    for (i = sp; i < n; i += 2) {
        hdc.moveTo(mCoords[i], mCoords[i + 1]);
        hdc.lineTo(mCoords[i - sp], mCoords[i - sp + 1]);
    }
    hdc.stroke();
}
function drawRect(coOrdStr) {
    var mCoords = coOrdStr.split(',');
    var top, left, bot, right;
    left = mCoords[0];
    top = mCoords[1];
    right = mCoords[2];
    bot = mCoords[3];
    hdc.strokeRect(left, top, right - left, bot - top);
}
function myHover(element) {
    var hoveredElement = element;
    var coordStr = element.getAttribute('coords');
    var areaType = element.getAttribute('shape');
    switch (areaType) {
        case 'polygon':
        case 'poly':
            drawPoly(coordStr);
            break;
        case 'rect':
            drawRect(coordStr);
    }
}
function myLeave() {
    var canvas = byId('myCanvas');
    hdc.clearRect(0, 0, canvas.width, canvas.height);
}
function AddHoverNotice() {
    for (var i = 0; i < $("area").length; i++) {
        var coords = $($("area")[i]).attr("coords").split(",")
        var mid = coords.length / 2;
        if (mid % 2 !== 0)
            mid--;
        var noticeX = parseInt((parseInt(coords[0]) + parseInt(coords[mid])) / 2);
        var noticeY = parseInt((parseInt(coords[1]) + parseInt(coords[mid + 1])) / 2);
        $("#hoverNotice").append("<div style='top:" + noticeY + "px;left:" + noticeX + "px;' class='hover-notice'></div>");
    }
}
function myInit() {
    BindPaint();
    var img = byId("imgTemplate2Thumb");
    var x, y, w, h;
    x = img.offsetLeft;
    y = img.offsetTop;
    w = img.clientWidth;
    h = img.clientHeight;
    var imgParent = img.parentNode;
    var can = byId('myCanvas');
    imgParent.appendChild(can);
    can.style.zIndex = 1;
    can.style.left = x + 'px';
    can.style.top = y + 'px';
    can.setAttribute('width', w + 'px');
    can.setAttribute('height', h + 'px');
    $("#canvas-map, #up-map,#hoverNotice").css("left", x + 'px');
    $("#canvas-map, #up-map,#hoverNotice").css("top", y + 'px');
    $("#canvas-map, #up-map,#hoverNotice").attr('width', w + 'px');
    $("#canvas-map, #up-map,#hoverNotice").attr('height', h + 'px');
    $("#up-map,#hoverNotice").css('width', w + 'px').css('height', h + 'px');
    $("#up-map").css("z-index", "-1");
    hdc = can.getContext('2d');
    hdc.fillStyle = 'red';
    hdc.strokeStyle = 'rgba(0, 0, 0, 0.1)';
    hdc.lineWidth = 20;
    //var img=document.getElementById("map");
    //hdc.drawImage(img,0,0);
    hdc.shadowColor = "black";
    hdc.shadowBlur = 20;
    //AddHoverNotice();
}
$(document).ready(function () {
    Template2ModalShow();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Template2ModalShow);
    ManagerControl2();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ManagerControl2);
});
function Template2ModalShow() {
    if (!$('#upLoadHeaderTemplate2').hasClass("shown-binded")) {
        $('#upLoadHeaderTemplate2').addClass("shown-binded");
        $('#upLoadHeaderTemplate2').on('shown.bs.modal', function () {
            //if (selectedFeatureElement2 !== undefined)
            //    selectedOffsetTop = $(selectedFeatureElement2).closest(".section-content").offset().top - $('.box-container > .row').offset().top;
            setTimeout(function () {
                ctx = document.getElementById("canvas-map");
                if (ctx !== null)
                    ctx = ctx.getContext("2d");
                if (makeNewFeature2) {
                    $('#upLoadHeaderTemplate2 [data-selector="txtImageCredits"],#upLoadHeaderTemplate2 [data-selector="txtImageCaption"]').val("");
                    $('[data-selector="txtTemplate2Image"]').val("/uploads/Article/no-image.jpg");
                    $('[data-selector="imgTemplate2Thumb"]').attr("src", "/uploads/Article/no-image.jpg");
                    myInit();
                    ResetArea();
                }
                else {
                    var img = $(selectedFeatureElement2).find("img");
                    $('#upLoadHeaderTemplate2 [data-selector="txtImageCredits"]').val(img.attr("data-credits"));
                    $('#upLoadHeaderTemplate2 [data-selector="txtImageCaption"]').val(img.attr("alt"));
                    $('[data-selector="txtTemplate2Image"]').val(img.attr("src"));
                    $('[data-selector="imgTemplate2Thumb"]').attr("src", img.attr("src"));
                    var mapImage = $(selectedFeatureElement2).find('[name="planetmap-binded"]');
                    $("#coords-map").html(mapImage.html());
                    var hoverNotice = $(selectedFeatureElement2).find('[name="hoverNotice"]');
                    $("#hoverNotice").html(hoverNotice.html());
                    myInit();
                    ReDrawHotspot();
                }
            }, 500)
        });
    }
    if (!$('#hotspot-detail').hasClass("hide-binded")) {
        $('#hotspot-detail').addClass("hide-binded");
        $('#hotspot-detail').on('hide.bs.modal', function () {
            setTimeout(function () {
                $("body").addClass("modal-open");
            }, 500)
        });
    }
};
var resizeHotpot;
$(window).resize(function () {
    clearTimeout(resizeHotpot);
    resizeHotpot = setTimeout(function () {
        myInit();
        ReDrawHotspot();
    }, 500);
});