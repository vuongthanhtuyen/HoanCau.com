/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
/// <reference path="../../plugins/moment/moment.min.js" />
window['moment-range'].extendMoment(moment);

var dataChart = undefined;
var viewSelector = undefined;
var callQuery = 2;
var bannerText = '';

var chartImpression = undefined;
var chartClick = undefined;
var keyReport = '';
var pageSizeGet = 400;
var pageSizeUserGet = 200;
var pageSizeSave = 300;
var totalClientId = [];
var labels = []; var data1 = []; var data2 = [];
var arNumDate = [];
var countryArr = {};
var pageArr = {};
var arrClientId = [];
var genderRange = { Male: 0, Female: 0 };
var countDone = 0;
var ageRange = [
    { from: 0, to: 18, value: 0 },
    { from: 18, to: 23, value: 0 },
    { from: 24, to: 30, value: 0 },
    { from: 31, to: 35, value: 0 },
    { from: 36, to: 45, value: 0 },
    { from: 46, to: 55, value: 0 },
    { from: 56, to: 65, value: 0 },
    { from: 66, to: 1000, value: 0 }
];

$(function () {

    var yesterday = moment().add(-1, 'days');
    var pass7daysago = moment().add(-7, 'days');
    
    //console.log(pass7daysago, yesterday);
    $('input[id$="dpto"]').datetimepicker({
        showClose: true,
        format: 'MM-DD-YYYY',
        minDate: pass7daysago,
        defaultDate: yesterday,
        maxDate: moment().add(1, 'years')
    });

    $('input[id$="dpfrom"]').datetimepicker({
        showClose: true,
        minDate: moment().add(-1, 'years'),
        maxDate: yesterday,
        defaultDate: pass7daysago,
        format: 'MM-DD-YYYY',
        useCurrent: false //Important! See issue #1075
    });

    $('input[id$="dpfrom"]').on("dp.change", function (e) {
        var d = e.date;
        var min = moment(d).add(-1, 'years');
        $(this).closest('.input-group.date')
            .find('input[id$="dpto"]').data("DateTimePicker").minDate(d);

        var a = $(this).data("DateTimePicker");
        a.minDate(min);
        a.hide();

        //console.log(this, callQuery);
        if (callQuery >= 2) {
            if (moment(e.oldDate).format('YYYYMMDD') !== moment(e.date).format('YYYYMMDD'))
                UpdateChartType();
        }
        else {
            callQuery += 1;
            if (callQuery >= 2)
                UpdateChartType();
        }
    });

    $('input[id$="dpto"]').on("dp.change", function (e) {
        var d = e.date;
        var max = moment(d).add(1, 'years');
        $(this).closest('.input-group.date')
            .find('input[id$="dpfrom"]').data("DateTimePicker").maxDate(d);

        var b = $(this).data("DateTimePicker");
        b.maxDate(max);
        b.hide();

        //console.log(this, callQuery);
        if (callQuery >= 2) {
            if (moment(e.oldDate).format('YYYYMMDD') !== moment(e.date).format('YYYYMMDD'))
                UpdateChartType();
        }
        else {
            callQuery += 1;
            if (callQuery >= 2)
                UpdateChartType();
        }
    });


    $("#btnsignout").on('click', function () {

        if (dataChart && dataChart.jY)
            dataChart.jY.clearChart();

        gapi.analytics.auth.signOut();

        $('#btnsignout,#mainpnl').addClass('hide');
        $('#authmsg').html('');
        $('#view-selector-container').empty();
        viewSelector = undefined;

        return false;
    });


    setTimeout(function () {
        if ($('#btnsignout').hasClass('hide') === true)
            $('#lblmessage').text('There was an error when initializing the google analytics.');
    }, 6000);
});

function SetDateRange(id, value) {
    callQuery = 0;
    var elfrom = $('#' + id + 'dpfrom');
    var elto = $('#' + id + 'dpto');
    if (elfrom.length > 0 && elto.length > 0) {
        if (typeof value === 'undefined' || value === null || value.length === 0)
            value = 'week';

        if (value.toLowerCase() === '7daysago') {
            elfrom.data('DateTimePicker').date(moment().add(-7, 'days'));
            elto.data('DateTimePicker').date(moment().add(-1, 'days'));
        }
        else if (value.toLowerCase() === '15daysago') {
            elfrom.data('DateTimePicker').date(moment().add(-15, 'days'));
            elto.data('DateTimePicker').date(moment().add(-1, 'days'));
        }
        else if (value.toLowerCase() === 'thismonth') {
            elfrom.data('DateTimePicker').date(moment().startOf('month'));
            elto.data('DateTimePicker').date(moment().endOf('month'));
        }
        else if (value.toLowerCase() === 'pastmonth') {
            elfrom.data('DateTimePicker').date(moment().add('-1', 'months').startOf('month'));
            elto.data('DateTimePicker').date(moment().add('-1', 'months').endOf('month'));
        }
    }
};

function GetDateRange(id) {
    var arr = [];
    var mome = $('#' + id + 'dpfrom').data("DateTimePicker").date();
    arr.push(mome.format('YYYY-MM-DD'));
    mome = $('#' + id + 'dpto').data("DateTimePicker").date();
    arr.push(mome.format('YYYY-MM-DD'));
    return arr;
};

var getDataQuery = function (name, ss) {
    if (typeof ss !== 'undefined' && ss.length > 0) {
        if (ss.indexOf('?') !== 0)
            ss = '?' + ss;
    }
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)", 'i'),
        results = regex.exec(ss || location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};

google.load("visualization", "1", {
    //packages: ["corechart"],
    callback: function () {
        gapi.analytics.ready(function () {
            if (typeof specialId === 'undefined' || specialId === null || specialId.length === 0) {
                $('#authmsg').html("Please go to <b>Settings</b> -&gt; <b>System configuration</b>"
                           + "<br/>Under \"Back-end settings\" to set <b>\"GA Tracking code\"</b>.");

                return;
            }

            if (typeof CLIENT_ID === 'undefined' || CLIENT_ID === null || CLIENT_ID.length === 0) {
                $('#authmsg').html("Please go to <b>Settings</b> -&gt; <b>System configuration</b>"
                    + "<br/>Under \"Back-end settings\" to set <b>\"Google Client ID\"</b>.");
                return;
            }

            bannerText = getDataQuery('id');
            if (bannerText.length > 0) {

                // Set some global Chart.js defaults.
                Chart.defaults.global.animationSteps = 60;
                Chart.defaults.global.animationEasing = 'easeInOutQuart';
                Chart.defaults.global.responsive = true;
                Chart.defaults.global.maintainAspectRatio = false;

                gapi.analytics.auth.on('error', function (err) {
                    //console.log('error : ', err);
                }).on('needsAuthorization', function () {
                    //console.log('needsAuthorization !');
                }).on('signIn', function (resa) {
                    //console.log('signIn : ', gapi.analytics.auth.getUserProfile());
                    //console.log('response : ', gapi.analytics.auth.getAuthResponse());

                    $('#btnsignout').removeClass('hide');

                    var request = gapi.client.analytics.management.accountSummaries.list();
                    request.execute(function (res) {
                        if (res.error) {
                            $('#authmsg').html(res.error.message + "<br/>Current tracking code: <b>" + specialId + "</b>");
                        }
                        else {
                            var found = false;
                            $.each(res.items, function (i, o) {
                                if (found === true)
                                    return false;
                                if ($.isArray(o.webProperties) === true && o.webProperties.length > 0) {
                                    $.each(o.webProperties, function (ii, oo) {
                                        if (oo.id === specialId) {
                                            found = true;
                                            return false;
                                        }
                                    });
                                }
                                if (found === true)
                                    return false;
                            });

                            if (found === true) {
                                $('#mainpnl,section,#mainpnl .btn-ex').removeClass('hide');


                                /**
                                 * Create a new ViewSelector instance to be rendered inside of an
                                 * element with the id "view-selector-container".
                                 */

                                viewSelector = new gapi.analytics.ext.ViewSelector2({
                                    container: 'view-selector-container',
                                    propertyId: specialId
                                });


                                /**
                                 * Render the dataChart on the page whenever a new view is selected.
                                 */
                                viewSelector.on('change', function (ids) {
                                   
                                    UpdateChartType();
                                }).on('error', function (err) {
                                    //console.log('viewSelector : ', err);
                                    if (err.result.error) {
                                        $('#authmsg').html(err.result.error.message +
                                            "<br/>Current tracking code: <b>" + specialId + "</b>");
                                    }
                                    else
                                        $('#authmsg').html("You don't have permission to access this Google Analytics account" +
                                            "<br/>or this Google Analytics account does not exists." +
                                            "<br/>Current tracking code: <b>" + specialId + "</b>");
                                });

                               
                                // Render the view selector to the page.
                                viewSelector.execute();
                            }
                            else
                                $('#authmsg').html("You don't have permission to access this Google Analytics account" +
                                    "<br/>or this Google Analytics account does not exists." +
                                    "<br/>Current tracking code: <b>" + specialId + "</b>");
                        }
                    });
                });


                /**
                 * Authorize the user immediately if the user has already granted access.
                 * If no access has been created, render an authorize button inside the
                 * element with the ID "embed-api-auth-container".
                 */
                gapi.analytics.auth.authorize({
                    container: 'embed-api-auth-container',
                    clientid: CLIENT_ID
                });
            }

        });
    }
});


function UpdateChartType() {
    $('#table-page').addClass('loading');
    renderImpressionChart();
    pageArr = {};
};

var txt = getDataQuery('cn');
if (txt.length > 0) {
    $('[id$="lblCampaignName"]').text(decodeURIComponent(txt));
};

txt = getDataQuery('t');
if (txt.length > 0) {
    $('[id$="lblBannerType"]').text(decodeURIComponent(txt));
};

txt = getDataQuery('bn');
if (txt.length > 0) {
    $('[id$="lblBannerName"]').text(decodeURIComponent(txt));
};

function QueueDataGA(pageIndex, pageSize, dateArr, actionType, cb) {
    var totalImpression = query({
        'ids': viewSelector.ids,
        'dimensions': 'ga:date, ga:pagePath',
        'metrics': 'ga:totalEvents,ga:uniqueEvents',
        'start-date': dateArr[0],
        'end-date': dateArr[1],
        'start-index': ((pageIndex - 1) * pageSize) + 1,
        'max-results': pageSize,
        filters: 'ga:eventLabel==' + bannerText + ';ga:eventAction=~^' + actionType
    });

    Promise.all([totalImpression])
        .then(function (results) {
            //console.log('results : ', results[0].totalResults);
            if (results) {
                ProcessDataGA(results, dateArr);
                //console.log('loaded', (pageIndex * pageSize));

                if (results[0].totalResults > (pageIndex * pageSize))
                    QueueDataGA(++pageIndex, pageSize, dateArr, actionType, cb);
                else if (typeof cb === 'function')
                    cb();
            }
            else if (typeof cb === 'function')
                cb();
        }).catch(function (res) {
            console.log('QueueDataGA Error load data.', res);
            if (typeof cb === 'function')
                cb();
        });
};

function ProcessDataGA(results, arrDate) {
    if ($.isArray(results) && results[0].rows) {
        var range = moment.range(moment(arrDate[0], 'YYYY-MM-DD'), moment(arrDate[1], 'YYYY-MM-DD'));
        if (typeof range !== 'undefined') {
            var arr = Array.from(range.by('days', { step: 1 }));
            if (arr.length > 0) {
                var countTotal = 0;
                var countUnique = 0;
                var len = arr.length;
                var ds = '';
                $.each(arr, function (i, o) {
                    countTotal = 0;
                    countUnique = 0;

                    ds = o.format('YYYYMMDD');
                    if ($.inArray(ds, arNumDate) < 0) {
                        arNumDate.push(ds);
                        labels.push(o.format('MM/DD/YYYY'));
                    }

                    /*
                    $.each(results[0].rows, function (ii, oo) {
                        if (oo[0] === ds) {
                            countTotal += parseInt(oo[4]) || 0;
                            countUnique += parseInt(oo[5]) || 0;
                        }
                    });
                    */
                    //DEV: ignore clientid
                    $.each(results[0].rows, function (ii, oo) {
                        if (oo[0] === ds) {
                            countTotal += parseInt(oo[2]) || 0;
                            countUnique += parseInt(oo[3]) || 0;
                        }
                    });
                    //DEV: ignore clientid

                    if (data1.length < len)
                        data1.push(countTotal);
                    else
                        data1[i] += countTotal;

                    if (data2.length < len)
                        data2.push(countUnique);
                    else
                        data2[i] += countUnique;
                });
            }

            var isQueryImpression = results[0].query.filters.indexOf(';ga:eventAction=~^Impression') >= 0;
            $.each(results[0].rows, function (i, oo) {
                /*
                if ($.inArray(oo[1], totalClientId) < 0) {

                    if (typeof countryArr[oo[2]] === 'undefined')
                        countryArr[oo[2]] = 0;
                    countryArr[oo[2]] += 1;

                    arrClientId.push(oo[1]);
                    totalClientId.push(oo[1]);
                }
                

                if (typeof pageArr[oo[3]] === 'undefined')
                    pageArr[oo[3]] = { impression: 0, click: 0 };

                if (isQueryImpression === true)
                    pageArr[oo[3]]['impression'] += parseInt(oo[4]) || 0;
                else
                    pageArr[oo[3]]['click'] += parseInt(oo[4]) || 0;
                */

                //DEV: ignore clientid
                if (typeof pageArr[oo[1]] === 'undefined')
                    pageArr[oo[1]] = { impression: 0, click: 0 };

                if (isQueryImpression === true)
                    pageArr[oo[1]]['impression'] += parseInt(oo[2]) || 0;
                else
                    pageArr[oo[1]]['click'] += parseInt(oo[2]) || 0;
                //DEV: ignore clientid
            });

            //console.log(labels, data1, data2, countryArr, arrClientId);
        }
    }
};

function renderImpressionChart() {
    $('#chart-1-container').addClass('loading').html('');
  
    labels = [];
    arNumDate = [];
    data1 = [];
    data2 = [];

    var dateArr = GetDateRange('');
    QueueDataGA(1, pageSizeSave, dateArr, 'Impression', function () {
        RenderReportData('Impression');
        renderClickChart();
    });
};

function renderClickChart() {
    $('#chart-2-container').addClass('loading').html('');
    
    labels = [];
    arNumDate = [];
    data1 = [];
    data2 = [];

    var dateArr = GetDateRange('');
    QueueDataGA(1, pageSizeSave, dateArr, 'Click', function () {
        RenderReportData('Click');
        RenderTablePage();
    });
};

function RenderTablePage() {
    if (typeof pageArr !== 'undefined') {
        var spage = SortPages(pageArr);
        var str = '<table class="table table-bordered"><thead><tr><th>Page</th><th>Impression</th><th>Click</th></tr></thead><tbody>';
        for (var page in spage) {
            str += '<tr><td>' + page + '</td><td>' +
                spage[page].impression + '</td><td>' + spage[page].click + '</td>';
        }
        str += '</tbody></table>';
        $('#table-page').removeClass('loading').html(str);
    }

    renderUserImpression();
};

function renderUserImpression() {
    totalClientId = [];
    keyReport = UUID.generate() + '-' + Math.random().toString(36).substring(17);
    countryArr = {};
    for (var i = 0; i < ageRange.length; i++)
        ageRange[i].value = 0;

    genderRange.Male = 0;
    genderRange.Female = 0;

    var dateArr = GetDateRange('');
    QueueUser(1, pageSizeUserGet, dateArr, 'Impression', function () {
        RenderUserData('Impression');
        renderUserClick();
    });
}

function renderUserClick() {
    totalClientId = [];
    keyReport = UUID.generate() + '-' + Math.random().toString(36).substring(17);
    countryArr = {};
    for (var i = 0; i < ageRange.length; i++)
        ageRange[i].value = 0;

    genderRange.Male = 0;
    genderRange.Female = 0;

    var dateArr = GetDateRange('');
    QueueUser(1, pageSizeUserGet, dateArr, 'Click', function () {
        RenderUserData('Click');
    });
}

function QueueUser(pageIndex, pageSize, dateArr, actionType, cb) {

    var totalImpression = query({
        'ids': viewSelector.ids,
        'dimensions': 'ga:dimension1, ga:country',
        'metrics': 'ga:sessions',
        'start-date': dateArr[0],
        'end-date': dateArr[1],
        'start-index': ((pageIndex - 1) * pageSize) + 1,
        'max-results': pageSize,
        filters: 'ga:eventAction=~^' + actionType
    });


    Promise.all([totalImpression])
        .then(function (results) {

            //console.log('results : ', results[0].totalResults);
            if (results)
                ProcessUser(results, ++pageIndex, pageSize, dateArr, actionType, cb);
            else if (typeof cb === 'function')
                cb();

        }).catch(function (res) {
            console.log('Error load data.', res);
            if (typeof cb === 'function')
                cb();
        });
};

function ProcessUser(results, pageIndex, pageSize, dateArr, actionType, cb) {
    //console.log('ProcessUser results : ', results);
    arrClientId = [];
    
    $.each(results[0].rows, function (i, oo) {
        if ($.inArray(oo[0], totalClientId) < 0) {
            arrClientId.push(oo[0]);
            totalClientId.push(oo[0]);
        }

        if (typeof countryArr[oo[1]] === 'undefined')
            countryArr[oo[1]] = 0;
        countryArr[oo[1]] += 1;
    });
    

    //console.log('arrClientId : ', arrClientId, actionType);
    var isEnd = (results[0].totalResults > (pageIndex * pageSize)) ? 0 : 1;
    $.ajax({
        type: "POST",
        url: "/Administration/BannerTracking.aspx/ProcessReport",
        data: JSON.stringify({
            str: arrClientId.join('--'),
            kr: keyReport,
            e: isEnd,
            pss: pageSizeSave,
            psg: pageSizeGet,
            pig: 1
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            if (res && res.d && res.d.Age) {
                ProcessUserReport(res.d);
                QueueUserReport(2, pageSizeGet, cb);
            }
            else {
                if (results[0].totalResults > (pageIndex * pageSize))
                    QueueUser(++pageIndex, pageSize, dateArr, actionType, cb);
                else if (typeof cb === 'function')
                    cb();
            }
        },
        error: function (response) {
            console.log('QueueUser ajax error.', response);
            /*
            if (results[0].totalResults > (pageIndex * pageSize))
                QueueUser(++pageIndex, pageSize, dateArr, cb);
            else if (typeof cb === 'function')
                cb();
            */
        }
    });

}

function QueueUserReport(pageIndex, pageSize, cb) {
    $.ajax({
        type: "POST",
        url: "/Administration/BannerTracking.aspx/ProcessReport",
        data: JSON.stringify({
            str: '',
            kr: keyReport,
            e: 1,
            pss: pageSizeSave,
            psg: pageSize,
            pig: pageIndex
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            if (res && res.d && res.d.Age) {
                ProcessUserReport(res.d);
                QueueUserReport(++pageIndex, pageSizeGet, cb);
            }
            else {
                if (typeof cb === 'function')
                    cb();
            }
        },
        error: function (response) {
            console.log('QueueUserReport ajax error.', response);
        }
    });
};

function ProcessUserReport(data) {
    //console.log(data);
    if (data && data.Age) {
        var obj = JSON.parse(data.Age);
        var n = 0;
        for (var key in obj) {
            for (var i = 0; i < ageRange.length; i++) {
                n = parseInt(key.toString()) || 0;
                if (n >= ageRange[i].from && n <= ageRange[i].to) {
                    ageRange[i].value += parseInt(obj[key].toString()) || 0;
                    break;
                }
            }
        }
    }

    if (data && data.Gender) {
        var objGender = JSON.parse(data.Gender);
        //console.log('objGender : ', objGender);
        genderRange.Male += objGender.Male;
        genderRange.Female += objGender.Female;
    }

    //console.log(ageRange, genderRange);
};

function RenderReportData(type) {

    //console.log(data1, data2);

    var countTotalC = 0;
    var countTotalI = 0;
    $.each(data1, function (i, o) { countTotalI += parseInt(o) || 0 });
    $.each(data2, function (i, o) { countTotalC += parseInt(o) || 0; });

    var data = {
        labels: labels,
        datasets: [
          {
              label: 'Total ' + type.toLowerCase() + ' (' + countTotalI + ')',
              backgroundColor: 'rgba(150,94,15,0.5)',
              strokeColor: 'rgba(150,94,15,1)',
              pointColor: 'rgba(150,94,15,1)',
              pointStrokeColor: '#fff',
              data: data1
          },
          {
              label: 'Unique ' + type.toLowerCase() + ' (' + countTotalC + ')',
              backgroundColor: 'rgba(49,202,175,0.5)',
              strokeColor: 'rgba(49,202,175,1)',
              pointColor: 'rgba(49,202,175,1)',
              pointStrokeColor: '#fff',
              data: data2
          }
        ]
    };

    var panel = type === 'Impression' ? '1' : '2';
    $('#chart-' + panel + '-container').removeClass('loading');
    var chart = window['chart' + type];
    if (typeof chart !== 'undefined')
        chart.destroy();

    window['chart' + type] = new Chart(makeCanvas('chart-' + panel + '-container'), {
        type: 'line',
        data: data
    });

    //generateLegend('legend-1-container', data.datasets);

};

function RenderUserData(type) {
    var panel = type === 'Impression' ? '1' : '2';
    var objFm = GetFormatNumberSetting();
    //country and user
    var num = genderRange.Female + genderRange.Male;
    $('#rlogged' + panel).text(accounting.formatNumber(num, objFm));
    var s = '';
    for (var k in genderRange)
        s += k + ':' + accounting.formatNumber(genderRange[k], objFm) + ', ';
    if (s.length > 2)
        s = s.substring(0, s.length - 2);
    $('#rgender' + panel).text(s);

    s = '';
    $.each(ageRange, function (i, o) {
        if (o.from === 0)
            s += '<li><span>' + '< ' + o.to + ': </span><span>' + accounting.formatNumber(o.value, objFm) + '</span></li>';
        else if (o.to > 900)
            s += '<li><span>' + '> ' + o.from + ': </span><span>' + accounting.formatNumber(o.value, objFm) + '</span></li>';
        else
            s += '<li><span>' + o.from + ' - ' + o.to + ': </span><span>' + accounting.formatNumber(o.value, objFm) + '</span></li>';
    });
    $('#rage' + panel).html(s);

    //anonymous country
    var objFm = GetFormatNumberSetting();
    var scountry = SortCountries(countryArr);
    //console.log('scountry : ', scountry);
    var countCountry = 0;
    s = '';
    var indx = 0;
    var totalUser = 0;
    for (var ke in scountry) {
        countCountry += 1;
        totalUser += parseInt(scountry[ke].toString()) || 0;

        if (indx < 5)
            s += '<li><span>' + ke + ': </span><span>' + accounting.formatNumber(parseInt(scountry[ke].toString()) || 0, objFm) + '</span></li>';

        indx += 1;
    }

    $('#rtotal' + panel).text(accounting.formatNumber(totalUser, objFm));

    $('#rtcountry' + panel).text(accounting.formatNumber(countCountry, objFm));
    $('#rlcountry' + panel).html(s);
}

function SortCountries(objs) {

    var newObject = {};

    var sortedArray = sortProperties(objs, '', true, true);
    for (var i = 0; i < sortedArray.length; i++) {
        var key = sortedArray[i][0];
        var value = sortedArray[i][1];

        newObject[key] = value;

    }

    return newObject;

};

function SortPages(objs) {

    var newObject = {};

    var sortedArray = sortProperties(objs, '', true, true);
    for (var i = 0; i < sortedArray.length; i++) {
        var key = sortedArray[i][0];
        var value = sortedArray[i][1];

        newObject[key] = value;

    }

    return newObject;

};

/**
* Sort object properties (only own properties will be sorted).
* @param {object} obj object to sort properties
* @param {string|int} sortedBy 1 - sort object properties by specific value.
* @param {bool} isNumericSort true - sort object properties as numeric value, false - sort as string value.
* @param {bool} reverse false - reverse sorting.
* @returns {Array} array of items in [[key,value],[key,value],...] format.
*/
function sortProperties(obj, sortedBy, isNumericSort, reverse) {
    sortedBy = sortedBy || ''; // by default first key
    isNumericSort = isNumericSort || false; // by default text sort
    reverse = reverse || false; // by default no reverse

    var reversed = (reverse) ? -1 : 1;

    var sortable = [];
    for (var key in obj) {
        if (obj.hasOwnProperty(key)) {
            sortable.push([key, obj[key]]);
        }
    }

    if (sortedBy === '') {
        if (isNumericSort)
            sortable.sort(function (a, b) {
                return reversed * (a[1] - b[1]);
            });
        else
            sortable.sort(function (a, b) {
                var x = a[1].toLowerCase(),
                    y = b[1].toLowerCase();
                return x < y ? reversed * -1 : x > y ? reversed : 0;
            });
    }
    else {
        if (isNumericSort)
            sortable.sort(function (a, b) {
                return reversed * (a[1][sortedBy] - b[1][sortedBy]);
            });
        else
            sortable.sort(function (a, b) {
                var x = a[1][sortedBy].toLowerCase(),
                    y = b[1][sortedBy].toLowerCase();
                return x < y ? reversed * -1 : x > y ? reversed : 0;
            });
    }
    return sortable; // array in format [ [ key1, val1 ], [ key2, val2 ], ... ]
};

function GetFormatNumberSetting() {
    return {
        precision: 0,		// default precision on numbers is 0
        grouping: 3,		// digit grouping (not implemented yet)
        thousand: ",",
        decimal: "."
    };
};

/**
 * Create a new canvas inside the specified element. Set it to be the width
 * and height of its container.
 * @param {string} id The id attribute of the element to host the canvas.
 * @return {RenderingContext} The 2D canvas context.
 */
function makeCanvas(id) {
    var container = document.getElementById(id);
    var canvas = document.createElement('canvas');
    var ctx = canvas.getContext('2d');

    container.innerHTML = '';
    canvas.width = container.offsetWidth;
    canvas.height = container.offsetHeight;
    container.appendChild(canvas);

    return ctx;
};

/**
 * Create a visual legend inside the specified element based off of a
 * Chart.js dataset.
 * @param {string} id The id attribute of the element to host the legend.
 * @param {Array.<Object>} items A list of labels and colors for the legend.
 */
function generateLegend(id, items) {
    var legend = document.getElementById(id);
    legend.innerHTML = items.map(function (item) {
        var color = item.color || item.backgroundColor;
        var label = item.label;
        return '<li><i style="background:' + color + '"></i>' + label + '</li>';
    }).join('');
};

/**
* Extend the Embed APIs `gapi.analytics.report.Data` component to
* return a promise the is fulfilled with the value returned by the API.
* @param {Object} params The request parameters.
* @return {Promise} A promise.
*/
function query(params) {
    return new Promise(function (resolve, reject) {
        var data = new gapi.analytics.report.Data({
            query: params
        });
        data.once('success', function (response) { resolve(response); })
            .once('error', function (response) { reject(response); })
            .execute();
    });
};

function RenderImageForReport() {
    if (typeof chartImpression !== 'undefined') {
        $('#hiddenchartI,#hiddenchartC').empty();
        new Chart(makeCanvas('hiddenchartI'), {
            type: 'line',
            data: chartImpression.data,
            options: {
                animation: {
                    onComplete: function () {
                        countDone += 1;
                        if (countDone === 2)
                            StartExport();
                    }
                }
            }
        });
        new Chart(makeCanvas('hiddenchartC'), {
            type: 'line',
            data: chartClick.data,
            options: {
                animation: {
                    onComplete: function () {
                        countDone += 1;
                        if (countDone === 2)
                            StartExport();
                    }
                }
            }
        });
    }
};

function StartExport() {
    var url = '';
    var id = getDataQuery('id');
    if (id.length > 0 && typeof chartImpression !== 'undefined') {
        url = '/Administration/BannerExport.aspx?id=' + id;
        $.fileDownload(url + '&ran=' + Math.floor(Math.random() * 1000), {
            httpMethod: "POST",
            data: {
                i1: $('#hiddenchartI canvas')[0].toDataURL('image/png'),
                i2: $('#hiddenchartC canvas')[0].toDataURL('image/png'),
                cn: $('[id$="lblCampaignName"]').text(),
                t: $('[id$="lblBannerType"]').text(),
                bn: $('[id$="lblBannerName"]').text(),
                iu: encodeURIComponent($.trim($('#toggleleft .logged').html().replace(/>\s+</g, '><'))),
                ia: encodeURIComponent($.trim($('#toggleleft .anony').html().replace(/>\s+</g, '><'))),
                cu: encodeURIComponent($.trim($('#toggleright .logged').html().replace(/>\s+</g, '><'))),
                ca: encodeURIComponent($.trim($('#toggleright .anony').html().replace(/>\s+</g, '><'))),
                tb: encodeURIComponent($.trim($('#table-page').html()))
            },
            successCallback: function (urlLoad) {
                //alert('You just got a file download dialog or ribbon for this URL :' + urlLoad);
                $('#div-loading').hide();
            },
            failCallback: function (html, urlLoad) {
                console.log('Your file download just failed for this URL:' + urlLoad + '\r\n' +
                    'Here was the resulting error HTML: \r\n' + html);
            }
        });


        setTimeout(function () { $('#div-loading').hide(); }, 7000);
    }
}

function OpenExport() {
    var url = '';
    var id = '';
    countDone = 0;
    id = getDataQuery('id');
    if (id.length > 0 && typeof chartImpression !== 'undefined') {
        url = '/Administration/BannerExport.aspx?id=' + id;

        $('#div-loading').show();
        RenderImageForReport();

    }
    else {
        $.lightbox($('<div style="text-align:center;padding-top:10px;">' +
             '<h5 style="margin:0">No data to report !</h5>' +
             '</div>'), { width: '40p', height: 40 });
    }
};

function RemoveFrameExport() {
    var ifr = $('body > iframe:last');
    if (ifr.length > 0 && ifr.attr('src').length > 0) {
        ifr.remove();
        $('#div-loading').hide();
    }
};
