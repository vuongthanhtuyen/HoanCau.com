var ar = {};
function a(x, y, z, t, u, v, w, x2) {
    ar.x = x;
    ar.y = y;
    ar.z = z;
    ar.t = t;
    ar.u = u;
    ar.v = v;
    ar.w = w;
    ar.x2 = x2;
    switch (x) {
        case "1":
            $('[data-selector="txtTemplate"]').val(1);
            break;
        case "2":
            $('[data-selector="txtTemplate"]').val(2);
            break;
        case "3":
            $('[data-selector="txtTemplate"]').val(3);
            break;
        case "4":
            $('[data-selector="txtTemplate"]').val(4);
            break;
    }
    $('[data-selector="btnBindTemplate"]')[0].click();
};
function c1() {
    $('[data-ad="feature"]').html(ar.v);
    $('[data-ad="body"]').prepend(ar.w);
    RebindTemp1();
};
function c2() {
    $('[data-ad="feature"]').html(ar.v);
    $('[data-ad="body"]').prepend(ar.w);
    RebindTemp2();
};
function c3() {
    $('[data-ad="feature"]').html(ar.v);
    //$('[data-ad="body"]').prepend(ar.w);
    var body = "";
    for (var i = 0; i < ar.w.length - 1; i++) {
        body += String.format('<div class="scroll-section" data-section-title="{0}">\
            {1}\
        </div>', ar.w[i].Title, ar.w[i].Content);
    }
    $('[data-ad="body"]').after(body);
    $('[data-ad="footer"]').attr("data-section-title", ar.w[ar.w.length - 1].Title).find(".bottom-box-text").html(ar.w[ar.w.length - 1].Content);
    $('[data-ad="subTitle"]').text(ar.x2);
    $('[data-title="data-section-title"]').attr("data-section-title", ar.y);
    RebindTemp3();
};
function c4() {
    $('[data-ad="feature"]').html(ar.v);
    $('[data-ad="body"]').prepend(ar.w);
    RebindTemp4();
};
function b() {
    $('[data-ad="title"]').text(ar.y);
    $('[data-ad="author"]').text(String.format("By {0}", ar.z));
    $('[data-ad="category"]').text(ar.t);
    $('[data-ad="pDate"]').text(ar.u);
};

//Recall main bind temp1
var templateOne;
function RebindTemp1() {
    templateOne = $('#article-teamplate.template-one');
    if (templateOne.length === 0)
        return;
    bindVideo();
    slideBanner1();
    hideDotsSlide();
    bindCaption();
    hideTextWhenVideoPlay();
};
function bindCaption() {
    var caption = templateOne.find(".inner-slide .slick-active img").attr("alt");
    if (caption === undefined)
        caption = templateOne.find(".inner-slide .slick-active .video-js").attr("title");
    templateOne.find(".caption.slider-caption").text(caption);
    templateOne.find('.inner-slide').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
        templateOne.find(".caption.slider-caption").animate({ opacity: "0" }, 200);
    });
    templateOne.find('.inner-slide').on('afterChange', function (event, slick, currentSlide, nextSlide) {
        var caption = templateOne.find(".inner-slide .slick-active img").attr("alt");
        if (caption === undefined)
            caption = templateOne.find(".inner-slide .slick-active .video-js").attr("title");
        templateOne.find(".caption.slider-caption").text(caption);
        templateOne.find(".caption.slider-caption").animate({ opacity: "1" }, 200);
    });
};
function hideDotsSlide() {
    templateOne.find('.slide-show-teamplate-one .wrap-video .vjs-big-play-button').on('click', function () {
        templateOne.find('.slide-show-teamplate-one .slick-dots').css({
            'z-index': '-1'
        });
        templateOne.find('.slide-show-teamplate-one .inner-slide').slick("slickPause");
    });
};
function slideBanner1() {
    var innerSlide = templateOne.find('.slide-show-teamplate-one .inner-slide');
    //innerSlide.slick('unslick');
    innerSlide.slick({
        autoplay: true,
        arrows: false,
        dots: true,
        autoplaySpeed: 2000,
        pauseOnHover: true
    });
};
function hideTextWhenVideoPlay() {
    var button = $('#article-teamplate .video-fashion .vjs-big-play-button');
    if (button.length === 0) {
        setTimeout(function () {
            hideTextWhenVideoPlay();
        }, 500);
        return;
    }
    button.on('click', function () {
        $(this).closest('.video-fashion').find('.text-note').hide();
        templateOne.find('.slide-show-teamplate-one .inner-slide').slick("slickPause");
    });
};
//Recall main bind temp2
var templateTwo;
function RebindTemp2() {
    templateTwo = $('#article-teamplate.template-two');
    if (templateTwo.length === 0)
        return;
    bindVideo();
    var image = templateTwo.find(".htmlHotspot-content img").removeAttr("style");
    templateTwo.find(".htmlHotspot-content area").removeAttr("onclick").removeAttr("onmouseout").removeAttr("onmouseover");
    templateTwo.find(".img .credits").text(String.format("Credit: {0}", image.attr("data-credits")));
    pointFeatureImage();
    ReSelectAreaImage2();
};
var resizeFeatureHotpot;
function ReSelectAreaImage2() {
    $("#article-teamplate.template-two .wrap-content .content .img img").each(function (index, tag) {
        var templateTwo = $(tag);
        var top = templateTwo.attr("data-top");
        if (top !== undefined) {
            var topParse = parseFloat(top);
            if (!isNaN(topParse)) {
                var outerHeight = templateTwo.outerHeight();
                var topParse = (outerHeight / 100) * topParse;
                templateTwo.css("margin-top", topParse * -1);
            }
        }
    })
};
$(window).resize(function () {
    if (templateTwo === undefined || templateTwo.length === 0)
        return;
    clearTimeout(resizeFeatureHotpot);
    resizeFeatureHotpot = setTimeout(function () {
        pointFeatureImage();
    }, 500);
});
function pointFeatureImage() {
    ResponsiveFeatureArea();
    var area = templateTwo.find(".htmlHotspot-content area");
    var spotsArray = [];
    for (var i = 0; i < area.length; i++) {
        var spotsObj = new Object();
        var coords = $(area[i]).attr("coords").split(",")
        var mid = coords.length / 2;
        if (mid % 2 !== 0)
            mid--;
        var noticeX = parseInt((parseInt(coords[0]) + parseInt(coords[mid])) / 2);
        var noticeY = parseInt((parseInt(coords[1]) + parseInt(coords[mid + 1])) / 2);
        spotsObj.top = String.format("{0}px", noticeY);
        spotsObj.left = String.format("{0}px", noticeX);
        spotsObj.content = templateTwo.find(String.format('.htmlHotspot-content [name="hoverNotice"] .hover-notice:eq({0})', i)).text();
        spotsObj.tooltip = true;
        spotsObj.tooltipPosition = 'br';
        spotsArray.push(spotsObj);
    }
    templateTwo.find('.box-banner .img').pictip({
        spots: spotsArray,
        onShowToolTip: function (spot, tooltip) { },
        onCloseToolTip: function (spot, tooltip) {
        }
    });
}
var defaultCoords = [];
function ResponsiveFeatureArea() {
    var imageTag = templateTwo.find(".htmlHotspot-content img");
    if (imageTag.length === 0)
        return;
    var spLength = templateTwo.find(".htmlHotspot-content area").length;
    var radioHeight = imageTag.height() / imageTag[0].naturalHeight;
    var radioWidth = imageTag.width() / imageTag[0].naturalWidth;
    for (var i = 0; i < spLength; i++) {
        var newCoords = "";
        if (defaultCoords[i] === undefined)
            defaultCoords[i] = $(templateTwo.find(".htmlHotspot-content area")[i]).attr("coords").split(",");
        for (var j = 0; j < defaultCoords[i].length; j++) {
            if (j % 2 === 0)
                newCoords += parseInt(defaultCoords[i][j] * radioWidth) + ",";
            else
                newCoords += parseInt(defaultCoords[i][j] * radioHeight) + ",";
        }
        newCoords = newCoords.slice(0, newCoords.length - 1);
        $(templateTwo.find(".htmlHotspot-content area")[i]).attr("coords", newCoords);
    }
};
//Recall main bind temp3
var templateThree;
function RebindTemp3() {
    templateThree = $('#article-teamplate.template-three');
    if (templateThree.length === 0)
        return
    bindVideo();
    //Page post
    pinTopPagePost();
    $(window).resize(function () {
        pinTopPagePost();
    });
    //Content
    setFeatureImage();
    setFeatureHeight();
    scrollSection();
    convertColorDotScroll();

    $(window).resize(function () {
        setFeatureHeight();
        convertColorDotScroll();
    });
    $(window).scroll(function () {
        convertColorDotScroll();
    });
    $("#for-article-social").html($("#data-img .social-media").html());
    $("#data-img .social-media").remove();
    var logo = $("#data-img .logo").attr("src");
    if (logo === "../wp-content/Article/no-image.jpg")
        $("#name-post .name").remove();
    else
        $("#paid-post-logo").attr("src", logo);
    SetFullWidthImage();
    SetFullWidthVideo();
    SetSocialMediaWithContent();
    var image = templateThree.find(".htmlHotspot-content img").removeAttr("style");
    templateThree.find(".htmlHotspot-content area").removeAttr("onclick").removeAttr("onmouseout").removeAttr("onmouseover");
    templateThree.find(".img .credits").text(String.format("Credit: {0}", image.attr("data-credits")));
    RemakeUseMapImage();
    pointImage();
    ShareMediaSection();
    ReSelectAreaImage();
};
function ReSelectAreaImage() {
    $("#article-teamplate.template-three .container .img img").each(function (index, tag) {
        var templateThree = $(tag);
        var top = templateThree.attr("data-top");
        if (top !== undefined) {
            var topParse = parseFloat(top);
            if (!isNaN(topParse)) {
                var outerHeight = templateThree.outerHeight();
                var topParse = (outerHeight / 100) * topParse;
                templateThree.css("margin-top", topParse * -1);
            }
        }
    })
};
function ShareMediaSection() {
    var media = "http://tsin.local/wp-content/Article/MediaWithContent/img-article-tomford-video2.jpg";
    $(".media-with-content a.ou .fa-facebook").closest(".ou").click(function () {
        var templateThree = $(this).closest(".media-with-content");
        var media = location.origin + templateThree.find(".content-media.pic img").attr("src");
        if (templateThree.find(".content-media.pic img").attr("src") === undefined)
            media = location.origin + templateThree.find(".content-media.video .video-js").attr("poster");
        var description = templateThree.find(".content-text .subtitle").text();
        var title = templateThree.find(".content-text .title").text();
        var url = String.format("http://api.addthis.com/oexchange/0.8/forward/facebook/offer?url={0}&screenshot={1}&description={2}&title={3}"
            , location.href, media, description, title);
        var setting = String.format("height=375,width=675,top={0},left={1}", (screen.height - 375) / 2, (screen.width - 675) / 2);
        window.open(url, '_blank', setting);
    })
    $(".media-with-content a.ou .fa-pinterest-p").closest(".ou").click(function () {
        var templateThree = $(this).closest(".media-with-content");
        var media = location.origin + templateThree.find(".content-media.pic img").attr("src");
        if (templateThree.find(".content-media.pic img").attr("src") === undefined)
            media = location.origin + templateThree.find(".content-media.video .video-js").attr("poster");
        var description = templateThree.find(".content-text .subtitle").text();
        var url = String.format("http://api.addthis.com/oexchange/0.8/forward/pinterest_share/offer?url={0}&screenshot={1}&description={2}"
            , location.href, media, description);
        var setting = String.format("height=680,width=750,top={0},left={1}", (screen.height - 680) / 2, (screen.width - 750) / 2);
        window.open(url, '_blank', setting);
    })
    $(".media-with-content a.ou .fa-twitter").closest(".ou").click(function () {
        var templateThree = $(this).closest(".media-with-content");
        var title = templateThree.find(".content-text .subtitle").text();
        var url = String.format("http://api.addthis.com/oexchange/0.8/forward/twitter/offer?url={0}&title={1}"
            , location.href, title);
        var setting = String.format("height=450,width=550,top={0},left={1}", (screen.height - 450) / 2, (screen.width - 550) / 2);
        window.open(url, '_blank', setting);
    })
};
var resizeFullWidthImage;
var resizeHotpot;
$(window).resize(function () {
    clearTimeout(resizeFullWidthImage);
    resizeFullWidthImage = setTimeout(function () {
        SetFullWidthImage();
        SetFullWidthVideo();
    }, 500);
    clearTimeout(resizeHotpot);
    resizeHotpot = setTimeout(function () {
        pointImage();
    }, 500);
});
function SetSocialMediaWithContent() {
    $(".media-with-content .content-text .social input:checked").parent().remove();
};
function SetFullWidthImage() {
    var element = $(".item.pic.img.full-width").closest(".scroll-section");
    element.css("margin-left", "").css("margin-right", "");
    var margin = String.format("-{0}px", ($(window).width() - element.width()) / 2);
    element.css("margin-left", margin).css("margin-right", margin);
};
function SetFullWidthVideo() {
    var element = $(".media-big.item.video.full-width").closest(".scroll-section");
    element.css("margin-left", "").css("margin-right", "");
    var margin = String.format("-{0}px", ($(window).width() - element.width()) / 2);
    element.css("margin-left", margin).css("margin-right", margin);
};
function pointImage() {
    ResponsiveArea();
    templateThree.find(".htmlHotspot-content").each(function (index, tag) {
        var area = $(tag).find("area");
        var spotsArray = [];
        for (var i = 0; i < area.length; i++) {
            var spotsObj = new Object();
            var coords = $(area[i]).attr("coords").split(",")
            var mid = coords.length / 2;
            if (mid % 2 !== 0)
                mid--;
            var noticeX = parseInt((parseInt(coords[0]) + parseInt(coords[mid])) / 2);
            var noticeY = parseInt((parseInt(coords[1]) + parseInt(coords[mid + 1])) / 2);
            spotsObj.top = String.format("{0}px", noticeY);
            spotsObj.left = String.format("{0}px", noticeX);
            spotsObj.content = templateThree.find(String.format('.htmlHotspot-content [name="hoverNotice"] .hover-notice:eq({0})', i)).text();
            spotsObj.tooltip = true;
            spotsObj.tooltipPosition = 'br';
            spotsArray.push(spotsObj);
        }
        $(tag).closest('.img.hotspot').pictip({
            spots: spotsArray,
            onShowToolTip: function (spot, tooltip) { },
            onCloseToolTip: function (spot, tooltip) {
            }
        });
    });
};
function RemakeUseMapImage() {
    templateThree.find(".htmlHotspot-content").each(function (index, tag) {
        var imageTag = $(tag).find("img");
        imageTag.attr("usemap", imageTag.attr("usemap") + index);
        var useMap = $(tag).find("[name='planetmap-binded']");
        useMap.attr("name", useMap.attr("name") + index);
    });
};
var defaultCoords = [];
function ResponsiveArea() {
    templateThree.find(".htmlHotspot-content img").each(function (index, tag) {
        var imageTag = $(tag);
        if (imageTag.length === 0)
            return;
        //var image = new Image();
        //image.src = imageTag.attr("src");
        var spLength = $(tag).closest(".htmlHotspot-content").find("area").length;
        var radioHeight = imageTag.height() / imageTag[0].naturalHeight;
        var radioWidth = imageTag.width() / imageTag[0].naturalWidth;
        var area = $(tag).closest(".htmlHotspot-content").find("area");
        for (var i = 0; i < spLength; i++) {
            var newCoords = "";
            if (defaultCoords[index] === undefined)
                defaultCoords[index] = [];
            if (defaultCoords[index][i] === undefined)
                defaultCoords[index][i] = $(area[i]).attr("coords").split(",");
            for (var j = 0; j < defaultCoords[index][i].length; j++) {
                if (j % 2 === 0)
                    newCoords += parseInt(defaultCoords[index][i][j] * radioWidth) + ",";
                else
                    newCoords += parseInt(defaultCoords[index][i][j] * radioHeight) + ",";
            }
            newCoords = newCoords.slice(0, newCoords.length - 1);
            $(area[i]).attr("coords", newCoords);
        }
    })
};
function convertColorDotScroll() {
    var height = templateThree.find('.banner').height();
    var wrapDot = $('.section-bullets');
    if ($(window).scrollTop() > (height / 2)) {
        wrapDot.find('li a').addClass('after');
        wrapDot.find('li a span').css({
            'background': '#000',
            'color': '#fff'
        });
        wrapDot.find('li a span').addClass('after');
    } else {
        wrapDot.find('li a').removeClass('after');
        wrapDot.find('li a span').css({
            'background': '#fff',
            'color': '#000'
        });
        wrapDot.find('li a span').removeClass('after');
    }
};
function scrollSection() {
    $('.bullets-container').remove();
    $('body').sectionScroll({
        sectionsClass: 'scroll-section'
    });
};
function setFeatureHeight() {
    var heightWin = $(window).innerHeight();
    if ($(window).width() > 991) {
        templateThree.find('.banner').height(heightWin);
        templateThree.find('.media-big').height(heightWin - 78);
    }
    else {
        templateThree.find('.banner').height('');
        templateThree.find('.media-big').height('');
    }
};
function setFeatureImage() {
    var dataImg = templateThree.find('.banner');
    var img = dataImg.find('.img');
    js_fw.setImage(dataImg, img);
};
function pinTopPagePost() {
    var heightTotal = 0;
    var mdl = $('#top-page-post');
    var topHeight = mdl.outerHeight();
    if ($(window).width() > 768) {
        if ($(this).scrollTop() > heightTotal) {
            mdl.find('.element-pin').addClass('fixed');
        } else {
            mdl.find('.element-pin').removeClass('fixed');
        }
    }
};
function ActiveBullets() {
    $(".bullets-container").addClass("active");
};
function DeactiveBullets() {
    setTimeout(function () {
        $(".bullets-container.active").removeClass("active");
    }, 2000);
};
//Recall main bind temp4
var templateFour;
function RebindTemp4() {
    templateFour = $('#article-teamplate.template-four');
    if (templateFour.length === 0)
        return
    var $gallery = templateFour.find('.silde-story');
    var slideCount = null;
    bindVideo();
    $gallery.on('init', function (event, slick) {
        slideCount = slick.slideCount;
        setSlideCount();
        setCurrentSlideNumber(slick.currentSlide);
    });
    $gallery.slick({
        autoplaySpeed: 2000,
        fade: true,
        cssEase: 'linear',
        swipe: true,
        swipeToSlide: true,
        infinite: true,
        touchThreshold: 10,
        pauseOnHover: true,
        slide: ".item"
    });
    $gallery.find('.slick-arrow').empty();
    setPosArrow();
    $(window).resize(function () {
        setPosArrow();
    });
    $gallery.on('beforeChange', function (event, slick, currentSlide, nextSlide) {
        setCurrentSlideNumber(nextSlide);
    });
    function setSlideCount() {
        var $el = $('.slide-count-wrap').find('.total');
        $el.text(slideCount);
    };
    setHeigtStory();
    setHeightTextStory();
    setImage();
};
function setHeigtStory() {
    var leftHeight = templateFour.find('.silde-story .left').outerHeight();
    if ($(window) > 768) {
        templateFour.find('.silde-story .right').height(leftHeight - 40);
    }
    else {
        templateFour.find('.silde-story .right').height('auto');
    }
};
function setCurrentSlideNumber(currentSlide) {
    var $el = $('.slide-count-wrap').find('.current');
    $el.text(currentSlide + 1);
};
function setHeightTextStory() {
    var leftHeight = templateFour.find('.silde-story .left').outerHeight();

    if ($(window).width() < 1025) {
        templateFour.find('.silde-story .right .text').height(leftHeight / 2.5).mCustomScrollbar();
    }
    else {
        templateFour.find('.silde-story .right .text').height('auto').mCustomScrollbar("destroy");
    }

    if ($(window).width() < 768) {
        templateFour.find('.silde-story .right .text').height('auto').mCustomScrollbar("destroy");
    }
}
function setPosArrow() {
    var leftHeight = templateFour.find('.silde-story .left').outerHeight();
    var offset = leftHeight + 20;

    if ($(window).width() < 768) {
        templateFour.find('.box-banner .silde-story .slick-arrow').css({
            top: "",
            bottom: 'auto'
        });
        templateFour.find('.box-banner .silde-story .slide-count-wrap').css({
            top: offset - 5,
            bottom: 'auto'
        });
    } else {
        templateFour.find('.box-banner .silde-story .slick-arrow').css({
            top: '',
            bottom: 'auto'
        });
        templateFour.find('.box-banner .silde-story .slide-count-wrap').css({
            top: '',
            bottom: 'auto'
        });
    }
}
function setImage() {
    var dataImg = templateFour.find('.silde-story .left');
    var img = dataImg.find('.img');
    js_fw.setImage(dataImg, img);
};
//General
function bindVideo() {
    $(".video-js").each(function (index, tag) {
        tag;
        videojs(tag);
    });
    $(".item.pic.img:not(.full-width)").append('<span class="btn-zoom" onclick="ZoomImage(this)">\
                    <img src="/images/ic-zoom.png" alt="Zoom">\
                </span>');
};
function ZoomImage(tag) {
    $('.inner-slide').slick("slickPause");
    var image = $(tag).closest(".img").find("img");
    $("#img-zoom img").attr("src", image.attr("src")).attr("alt", image.attr("alt"));
    js_fw.open_popup({ rel: '#img-zoom', width: 1720, effect: 'zoom', close: function () { $('.inner-slide').slick("slickPlay"); } })
};