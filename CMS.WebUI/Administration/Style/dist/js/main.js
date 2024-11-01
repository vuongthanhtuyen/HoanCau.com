/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
/// <reference name="MicrosoftAjax.js" />
/// <reference name="MicrosoftAjaxWebForms.js" />
function setHeightContentTab($content, $fix) {
    var h = $(window).height();
    var hFooter = $('.main-footer').outerHeight();
    var hTopContent = $content.offset();
    if (typeof (hTopContent) != "undefined") {
        var hContent = h - hTopContent.top - hFooter - $fix;
        if ($content.hasClass('set-height') && $content.hasClass('box-body')) {
            if (hContent < 250)
                hContent = 250;
        }
        $content.css({
            height: hContent,
            overflow: 'auto',
        });
    };
};

var validateBeforePostback = false;
function DisableValidateBeforePostback() {
    validateBeforePostback = false;
}

function EnableValidateBeforePostback() {
    validateBeforePostback = true;
}
function CheckPageIsValid() {
    if (validateBeforePostback) {
        Page_IsValid = $("#aspnetForm").validationEngine('validate', { promptPosition: "topLeft", autoHidePrompt: false, scroll: false, showOneMessage: true });
        return Page_IsValid;
    }
    else {
        $("#aspnetForm").validationEngine('detach');
    }
    return true;
}

function HideAllValidatorPrompts() {
    $('#aspnetForm').validationEngine('hideAll')
}

function AttachValidation() {
    if (validateBeforePostback)
        $("#aspnetForm").validationEngine('attach', { promptPosition: "topLeft", autoHidePrompt: false, scroll: false, showOneMessage: true })
}

jQuery(document).ready(function ($) {

    //$('#listShowcase').hide();
    //$('#girdShowcase').show();

    $('#chooseList').click(function () {
        $('#listShowcase').show();
        $('#girdShowcase').hide();
        $('#chooseGird').removeClass('active');
        $('#chooseList').addClass('active');
    });

    $('#chooseGird').click(function () {
        $('#listShowcase').hide();
        $('#girdShowcase').show();
        $('#chooseList').removeClass('active');
        $('#chooseGird').addClass('active');
    });

    function setHeight() {
        var h = $(window).height();
        var hFooter = $('.main-footer').outerHeight();
        var hTopContent = $('section.content').offset();
        if (typeof hTopContent !== 'undefined') {
            var hTopSlider = $('#mainmenu').offset();
            if (hTopSlider) {
                var hContent = h - hTopContent.top - hFooter - 30;
                var hSlider = h - hTopSlider.top;
                $('section.content').height(hContent);
                $('#mainmenu').outerHeight(h - $('.user-panel').outerHeight() - $('header').outerHeight() - $('.sidebar-form').outerHeight() - 20);
            }
        }
    };

    // function readURL(input) {
    //    if (input.files && input.files[0]) {
    //       var reader = new FileReader();
    //       reader.onload = function (e) {
    //          $('#showImage').attr('src', e.target.result);
    //       };
    //       reader.readAsDataURL(input.files[0]);
    //    };
    // };

    $('#showCaseActive').click(function (event) {
        setTimeout(function () {
            setHeightContentTab($('.content-showcase'), 30);
        });
    });

    $('.treeview > a').click(function (event) {
        var h = $(window).height();
        var hTopSlider = $('.sidebar-menu').offset();
        var hSlider = h - hTopSlider.top;
        $('.sidebar-menu').height(hSlider);
    });

    $(window).load(function () {
        setHeight();
        setHeightContentTab($('#wrapDetail .nav-tabs-custom .tab-pane'), 25);
        setHeightContentTab($('.set-height'), 15);
    });

    $(window).resize(function (event) {
        setHeight();
        if ($('.tab-pane#profile').hasClass('active')) {
            setHeightContentTab($('#wrapDetail .nav-tabs-custom .tab-pane'), 25);
        }
        else {
            if ($(window).width() <= 991) {
                if ($(window).width() <= 768) {
                    setHeightContentTab($('#wrapDetail .nav-tabs-custom .tab-pane'), 280);
                }
                else {
                    setHeightContentTab($('#wrapDetail .nav-tabs-custom .tab-pane'), 230);
                }
            }
            else {
                setHeightContentTab($('#wrapDetail .nav-tabs-custom .tab-pane'), 190);
            }
        }
        setHeightContentTab($('.set-height'), 15);
        setTimeout(function () {
            setHeightContentTab($('.content-showcase'), 30);
        });
    });

    SetActiveMenu();
    CheckCloseActiveModal();

    if (typeof Sys !== 'undefined')
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHanlder);

    InitMainPage();
    if (typeof Sys !== 'undefined' && typeof Sys.WebForms.PageRequestManager !== 'undefined')
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MainEndRequest);

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        $(window).resize();
    });
    //setTimeout("if ($('#mainmenu').height() < 200) $._data(window,'events')['resize'][1].handler();", 1000);
});

function AddEndRequest(functionName) {
    if (typeof Sys !== 'undefined' && typeof Sys.WebForms.PageRequestManager !== 'undefined')
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(window[functionName]);
}

function AddBeginRequest(functionName) {
    if (typeof Sys !== 'undefined' && typeof Sys.WebForms.PageRequestManager !== 'undefined')
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(window[functionName]);
}

function SetActiveMenu() {
    $('#mainmenu-dashboard').removeClass('active');
    var fid = $('input:hidden[id$="hdfFunction"]').val() || '';
    if (fid && fid.length > 0) {
        var link = $('#mainmenu li[data-id="' + fid + '"]');

        if (link.length > 0) {
            var liColl = link.parentsUntil('.sidebar-menu').filter('li').add(link);
            if (liColl.length > 0)
                liColl.addClass('active');
        }
        else {
            $('#mainmenu-dashboard').addClass('active');
        }
    }
}

function CheckCloseActiveModal() {
    var btnsearch = $('[id*="btnSearchGeneral"]');
    if (btnsearch.length > 0) {
        btnsearch.click(function (e) {
            if ($('.modal:visible').length > 0) {
                e.preventDefault();
                $('.modal:visible').modal('hide');
                return false;
            }
        });
    }
}

var timeoutCloseModal = undefined;
function showMessageBox(id) {

    $(id).modal('show');

    if (id.indexOf('MessageModel') >= 0) {
        var hdf = $('input[type="hidden"][id$="hdfCancelClose"]');
        if (hdf.length > 0 &&
            hdf.val().length > 0 &&
            hdf.val() === '1') { }
        else {
            if (typeof timeoutCloseModal !== 'undefined') {
                window.clearTimeout(timeoutCloseModal);
                timeoutCloseModal = undefined;
            }

            timeoutCloseModal = setTimeout("$('" + id + "').modal('hide');", 5000);
        }
    }
}

var getDataQuery = function (name, ss) {
    if (typeof ss !== 'undefined' && ss.length > 0) {
        if (ss.indexOf('?') !== 0)
            ss = '?' + ss;
    }
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)", 'i'),
        results = regex.exec(ss || location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function closeMessageBox(id) {
    /*@cc_on @*/
    $(id).modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
    CheckIsCreated();
}

function CheckIsCreated() {
    var hdf = $('input[type="hidden"][id$="hdfIsCreated"]');
    if (hdf.length > 0) {
        if (hdf.val().length > 0) {
            var id = getDataQuery('id');
            if (id.length === 0)
                location.href = location.protocol + "//" + location.host +
                    location.pathname + '?id=' + hdf.val();
        }
    }
}

var activeTab = {};
var activeToggle = {};
function BeginRequestHanlder() {
    SaveActiveTab();
    SaveActiveToggle();
}

function SaveActiveTab() {
    activeTab = {};
    var activeColl = $('.nav-tabs-custom .nav-tabs li.active a');
    if (activeColl.length > 0) {
        $.each(activeColl, function (i, o) {
            var href = $(o).attr('href') || $(o).attr('data-target');
            if (href) {
                if (href.indexOf('#') >= 0) {
                    var el = $(href);
                    if (el.length > 0)
                        activeTab[el.attr('id')] = el.hasClass('active');
                }
                else {
                    var idt = href.substring(href.indexOf("'") + 1, href.lastIndexOf("'"))
                    var el = $('#' + idt);
                    if (el.length > 0)
                        activeTab[idt] = el.hasClass('active');
                }
            }
        });
    }
}

function SetActiveTab() {
    if (typeof activeTab !== 'undefined') {
        //remove active
        for (var key in activeTab) {
            var elTrigger = $('[href="#' + key + '"],[data-target="#' + key + '"]');
            //console.log(elTrigger);
            if (elTrigger.length === 0)
                elTrigger = $('.nav-tabs li a[href*="' + key + '"]');

            if (elTrigger.length > 0)
                elTrigger.closest('.nav-tabs').find('.active').removeClass('active').find('a').attr('aria-expanded', 'false');

            var el = $('#' + key);
            //console.log(el);

            if (el.length > 0)
                el.closest('.tab-content').find('.tab-pane.active').removeClass('active');
        }

        for (var key in activeTab) {
            var elTrigger = $('[href="#' + key + '"],[data-target="#' + key + '"]');
            //console.log(elTrigger);
            if (elTrigger.length === 0)
                elTrigger = $('.nav-tabs li a[href*="' + key + '"]');

            if (elTrigger.length > 0) {
                if (activeTab[key] === true)
                    elTrigger.attr('aria-expanded', 'true').parent().addClass('active');
                else
                    elTrigger.attr('aria-expanded', 'false').parent().addClass('active');
            }

            var el = $('#' + key);
            //console.log(el);

            if (el.length > 0) {
                if (activeTab[key] === true)
                    el.addClass('active');
                else
                    el.removeClass('active');

                var hasError = el.find('.formError');
                if (hasError.length > 0) {
                    if (typeof $.fn.validationEngine === 'function') {
                        setTimeout(function () {
                            var form = el.closest('form, .validationEngineContainer');
                            form.validationEngine("updatePromptsPosition");
                        }, 300);
                    }
                }
            }

        }
    }
}

function SaveActiveToggle() {
    activeToggle = {};

    var dataToggleColl = $('[data-toggle="collapse"]');
    if (dataToggleColl.length > 0) {
        $.each(dataToggleColl, function (i, o) {
            var href = $(o).attr('href') || $(o).attr('data-target');
            if (href) {
                var el = $(href);
                if (el.length > 0)
                    activeToggle[el.attr('id')] = el.hasClass('in');
            }
        });
    }
}

function SetActiveToggle() {
    if (typeof activeToggle !== 'undefined') {
        for (var key in activeToggle) {
            var elTrigger = $('[href="#' + key + '"],[data-target="#' + key + '"]');
            //console.log(elTrigger);

            if (elTrigger.length > 0) {
                if (activeToggle[key] === true)
                    elTrigger.removeClass('collapsed').attr('aria-expanded', 'true');
                else
                    elTrigger.addClass('collapsed').attr('aria-expanded', 'false');
            }

            var el = $('#' + key);
            //console.log(el);

            if (el.length > 0) {
                if (activeToggle[key] === true)
                    el.addClass('in').attr('aria-expanded', 'true');
                else
                    el.removeClass('in').attr('aria-expanded', 'false');
            }
        }
    }
}


function MainEndRequest() {
    SetActiveTab();
    SetActiveToggle();
    InitMainPage();
    CheckCloseActiveModal();
    renderInputMask();
    if (typeof (Sys) !== 'undefined') {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) prm.add_endRequest(renderInputMask);
    }
}

function InitMainPage() {

    if (typeof $.fn.bootstrapSwitch === 'function') {
        $("input[data-toggle='toggle']:not(.ignore)").bootstrapSwitch({ size: 'mini' });
    }

    $(".onlynumber").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    if (typeof $.fn.select2 !== 'undefined') {
        $('.select-tag').select2({ minimumResultsForSearch: $(this).attr('data-minimumResultsForSearch') || -1 });
        //$('[id*="DropDownListPageSize"]').select2({ width: 70, minimumResultsForSearch: -1 });
    }

    if (typeof $.fn.validationEngine === 'function')
        AttachValidation();
}

function ShowPromtMessage(id, message) {
    if (typeof $.fn.validationEngine === 'function')
        $('#' + id).validationEngine('showPrompt', message, 'error', 'topLeft', true);
}

function renderInputMask() {
    $(".mask-integer").inputmask("integer", { autoGroup: true, groupSeparator: ".", groupSize: 3, max: 999999999 });
    $(".mask-float").inputmask("decimal", { radixPoint: ",", autoGroup: true, groupSeparator: ".", groupSize: 3, max: 999999999 });
}