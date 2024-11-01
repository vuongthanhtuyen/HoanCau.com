/// <reference path="../../plugins/moment/moment.min.js" />
/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />

function ReportBanner(bid, bna) {
    var link = $('div[id$="upDelete"] a[data-id="' + bid + '"]');
    if (link.length > 0) {
        var cn = $('h1').text();
        var t = link.closest('ul').find('li:eq(1)').text();
        var bplacement = link.attr('data-pl') || '';
        if (bplacement.length > 0) {
            var ws = getWindowSize();
            $.lightbox('/Administration/BannerTracking.aspx?id=' + (bid + '---' + bplacement)
                + '&cn=' + encodeURIComponent($.trim(cn))
                + '&t=' + encodeURIComponent($.trim(t))
                + '&bn=' + encodeURIComponent($.trim(bna)),
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                    onOpen: function () {
                        $(this).parent().find('iframe').addClass('ifr-res');
                    }
                });
        }
    };
};

var ObjBannerReport = {};
var timeToProcess = 5 * 60;//second
var viewSelector = undefined;

var objHour = {};
var yesterday = moment().add(-1, 'days');
var pass7daysago = moment().add(-7, 'days');
var callQuery = 2;

var countryArr = {};
var eventAction = { click: 0, impression: 0 };
var ObjCampaignReport = [];
var totalClientID = [];
var viewSelector = undefined;
var htmlloc = '';

//moment('22','HH').format('HH A')
var pageSizeGet = 400;

function CreateHourLabel() {
    if (typeof objHour === 'undefined')
        objHour = {};
    if ($.isEmptyObject(objHour) === true) {
        var range = moment.range(moment('00', 'HH'), moment('23', 'HH'));
        if (typeof range !== 'undefined') {
            var arr = Array.from(range.by('hours', {
                step: 1
            }));
            //console.log(arr);
            if ($.isArray(arr) === true && arr.length > 0) {
                var s = '';
                $.each(arr, function (i, o) {
                    s = o.format('H') + o.format('a');
                    objHour[s] = 0;
                });
            }
            //console.log(objHour);
        }
    }
};

$(function () {
    if (typeof specialId !== 'undefined' && specialId !== null && specialId.length > 0) {
        gapi.analytics.ready(function () {
            htmlloc = $('#loctemplate').html();

            InitPage();

            LoginServerGA();
        });


        if (typeof Sys !== 'undefined' && typeof Sys.WebForms.PageRequestManager !== 'undefined')
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ProcessGAClickThroughRate);
    }
});

function InitPage() {

    window['moment-range'].extendMoment(moment);
    CreateHourLabel();

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
                ProcessGACampaign();
        }
        else {
            callQuery += 1;
            if (callQuery >= 2)
                ProcessGACampaign();
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
                ProcessGACampaign();
        }
        else {
            callQuery += 1;
            if (callQuery >= 2)
                ProcessGACampaign();
        }
    });

    $('#mainloc .btnmore').click(function () {
        var isHide = $('#mainloc .moreloc').is(':hidden');
        if (isHide === true) {
            $(this).text('Collapse');
            $('#mainloc .moreloc').removeClass('hide');
        }
        else {
            $('#mainloc .moreloc').addClass('hide');
            $(this).text('Show more');
        }
    });
}

function LoginServerGA() {
    $.getJSON('/Administration/GAAccessToken.aspx', function (data) {
        //console.log(data);
        if (typeof data !== 'undefined') {
            gapi.analytics.auth.on('error', function (err) {
                console.log('error : ', err);
            }).on('needsAuthorization', function () {
                //console.log('needsAuthorization !');
            }).on('signIn', function () {
                //console.log('signIn');
                var user = gapi.analytics.auth.getUserProfile();
                //console.log('user : ', user);

                var response = gapi.analytics.auth.getAuthResponse();
                //console.log('response : ', response);



                viewSelector = new gapi.analytics.ext.ViewSelector2({
                    container: 'view-selector-container',
                    propertyId: specialId
                });


                viewSelector.on('change', function (ids) {
                    ProcessGACampaign();
                }).on('error', function (err) {
                    console.log('viewSelector : ', err);
                });

                viewSelector.execute();
            });

            gapi.analytics.auth.on('error', function () {
                console.log('cannot authorize this token.');
            });

            gapi.analytics.auth.authorize({
                'serverAuth': {
                    'access_token': data['access_token']
                }
            });

        }
        else {
            console.log('Nothing to process.');
        }
    })
    .fail(function (d, textStatus, error) {
        console.log("getJSON failed, status: " + textStatus + ", error: " + error)
    });

}

function ProcessGAClickThroughRate() {
    var liColl = $('div[id$="upDelete"] li.gabanner');
    if (liColl.length > 0) {
        var isProcess = false;
        var arrProcess = [];
        if ($.isEmptyObject(ObjBannerReport) === true) {
            isProcess = true;
            $.each(liColl, function (i, o) {
                var link = $(o).parent().find('li:last > a[data-id]');
                if (link.length > 0) {
                    bid = link.attr('data-id') || '';
                    plid = link.attr('data-pl') || '';
                    cr = link.attr('data-cr') || '';
                    if (bid.length > 0 && plid.length > 0) {
                        ObjBannerReport[bid + '---' + plid] = {
                            click: 0,
                            impression: 0,
                            lastReportTime: moment().toDate(),
                            CreateDate: cr.length === 0 ? moment().add(-1, 'days') : moment(cr, 'YYYY-MM-DD').add(-1, 'days')
                        };
                    }
                }
            });
        }
        else {
            var bid = '';
            var plid = '';
            var cr = '';
            var count = 0;
            for (var key in ObjBannerReport)
                count += 1;
            if (count < liColl.length) {
                $.each(liColl, function (i, o) {
                    var link = $(o).parent().find('li:last > a[data-id]');
                    if (link.length > 0) {
                        bid = link.attr('data-id') || '';
                        plid = link.attr('data-pl') || '';
                        cr = link.attr('data-cr') || '';
                        if (bid.length > 0 && plid.length > 0) {
                            if (typeof ObjBannerReport[bid + '---' + plid] === 'undefined')
                                ObjBannerReport[bid + '---' + plid] = {
                                    click: 0,
                                    impression: 0,
                                    lastReportTime: moment().toDate(),
                                    CreateDate: cr.length === 0 ? moment().add(-1, 'days') : moment(cr, 'YYYY-MM-DD').add(-1, 'days')
                                };
                            else {

                            }
                        }
                    }
                });
            }
            else if (count === liColl.length) {

            }
            else if (count > liColl.length) {
                var arrDel = [];
                var arrsplit = [];
                for (var k in ObjBannerReport) {
                    arrsplit = k.split('---');
                    if (arrsplit.length === 2) {
                        bid = arrsplit[0];
                        plid = arrsplit[1];
                    }
                    var linka = $('div[id$="upDelete"] li.gabanner a[data-id="' + bid + '"][data-pl="' + plid + '"]');
                    if (linka.length === 0)
                        arrDel.push(k);
                }
                if (arrDel.length > 0) {
                    $.each(arrDel, function (ii, oo) {
                        delete ObjBannerReport[oo];
                    });
                }
            }
        }

        if (isProcess === true) {
            for (var k in ObjBannerReport)
                arrProcess.push({ id: k, cr: ObjBannerReport[k].CreateDate });
        }
        else {
            var d = undefined;
            for (var k in ObjBannerReport) {
                if (moment(ObjBannerReport[k].lastReportTime).add(timeToProcess, 'seconds') <= moment())
                    arrProcess.push({ id: k, cr: ObjBannerReport[k].CreateDate });
            }
        }

        if (arrProcess.length > 0)
            QueueDataGAClickThroughRate(arrProcess);
        else
            DisplayClickThroughRate();
    }
}

function QueueDataGAClickThroughRate(arr) {
    if (typeof viewSelector === 'undefined')
        return;
    if ($.isArray(arr) === false || arr.length === 0)
        return;

    var arrQuery = [];
    $.each(arr, function (i, o) {
        arrQuery.push(query({
            'ids': viewSelector.ids,
            'dimensions': 'ga:eventAction',
            'metrics': 'ga:totalEvents',
            'start-date': moment(o.cr).add('-1', 'days').format('YYYY-MM-DD'),
            'end-date': moment().add('1', 'days').format('YYYY-MM-DD'),
            filters: 'ga:eventLabel==' + o.id
        }));
    });

    var countDone = 0;
    $.each(arrQuery, function (i, objQuery) {

        Promise.all([objQuery])
            .then(function (results) {

                //console.log('results : ', results[0]);
                if (results && results[0] && results[0].rows) {
                    var ke = results[0].query.filters.replace('ga:eventLabel==', '');
                    $.each(results[0].rows, function (iii, ooo) {
                        if (ooo[0].indexOf('Impression') === 0)
                            ObjBannerReport[ke].impression = parseInt(ooo[1]) || 0;
                        else if (ooo[0].indexOf('Click') === 0)
                            ObjBannerReport[ke].click = parseInt(ooo[1]) || 0;
                    });

                    ObjBannerReport[ke].lastReportTime = moment().toDate();

                    countDone += 1;
                    if (countDone === arrQuery.length)
                        DisplayClickThroughRate();
                }
            })
            .catch(function (res) {
                console.log('QueueDataGAClickThroughRate Error load data.', res);
                countDone += 1;
                if (res.error && res.error.code && res.error.code === 401)//Invalid Credentials
                {
                    if (countDone === arrQuery.length)
                        LoginServerGA();
                }
                else {
                    if (countDone === arrQuery.length)
                        DisplayClickThroughRate();
                }
            });
    });

};

function DisplayClickThroughRate() {
    //console.log('call DisplayClickThroughRate');
    var arrKey = [];
    var percentCTR = 0;
    if (typeof ObjBannerReport !== 'undefined' && $.isEmptyObject(ObjBannerReport) === false) {
        for (var ke in ObjBannerReport) {
            arrKey = ke.split('---');
            var linka = $('div[id$="upDelete"] li a[data-id="' + arrKey[0]
                + '"][data-pl="' + arrKey[1] + '"]');

            if (linka.length > 0) {
                var li = linka.closest('ul').find('li.gabanner');
                if (li.length > 0) {
                    percentCTR = 0;
                    if (ObjBannerReport[ke].click > 0 && ObjBannerReport[ke].impression > 0)
                        percentCTR = (ObjBannerReport[ke].click * 100) / ObjBannerReport[ke].impression;
                    //console.log('percentCTR : ', percentCTR, ObjBannerReport[ke]);
                    li.text(ObjBannerReport[ke].click + ' / ' + ObjBannerReport[ke].impression
                        + ' / ' + percentCTR.toFixed(2) + '%');
                }
            }
        }
    }
}

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

function ResetHourLabel() {
    CreateHourLabel();
    for (var kk in objHour)
        objHour[kk] = 0;
}

function GetFormatNumberSetting() {
    return {
        precision: 0,		// default precision on numbers is 0
        grouping: 3,		// digit grouping (not implemented yet)
        thousand: ",",
        decimal: "."
    };
};

function ProcessGACampaign() {

    $('#analytic .loading').show();

    var liColl = $('div[id$="upDelete"] li.gabanner');
    if (liColl.length > 0) {

        //reset data
        ObjCampaignReport = [];
        totalClientID = [];
        countryArr = {};
        eventAction.click = 0;
        eventAction.impression = 0;
        ResetHourLabel();

        $('#chart-1-container').addClass('loading');

        $.each(liColl, function (i, o) {
            var link = $(o).parent().find('li:last > a[data-id]');
            if (link.length > 0) {
                bid = link.attr('data-id') || '';
                plid = link.attr('data-pl') || '';
                cr = link.attr('data-cr') || '';
                if (bid.length > 0 && plid.length > 0)
                    ObjCampaignReport.push(bid + '---' + plid);
            }
        });

        QueueDataGACampaignCTRAndCountry(ObjCampaignReport, 1, pageSizeGet, RenderCampaignResult);
    }
}

function RenderCampaignResult() {
    var objFm = GetFormatNumberSetting();
    $('#clickcount').text(accounting.formatNumber(eventAction.click, objFm));
    $('#impressioncount').text(accounting.formatNumber(eventAction.impression, objFm));
    var perc = eventAction.click > 0 && eventAction.impression > 0 ? (((eventAction.click * 100) / eventAction.impression).toFixed(2)) : 0;
    $('#clickperimpression').text(perc + '%');


    //render location
    $('#mainloc .topfirst,#mainloc .moreloc').empty();
    $('#mainloc .btnmore,#mainloc .moreloc').addClass('hide');

    if ($.isEmptyObject(countryArr) == false) {
        var scountry = SortCountries(countryArr);
        var maxnum = 0;
        for (var k in scountry) {
            if (maxnum < scountry[k])
                maxnum = scountry[k];
        }

        maxnum += 50;
        var htmlColl = '';
        var perc = 0;
        var indx = 0;
        var topfirst = 5;
        for (var k in scountry) {
            if (indx === topfirst) {
                $('#mainloc .topfirst').html(htmlColl);
                htmlColl = '';
            }

            indx += 1;
            perc = 0;
            if (maxnum > 0 && scountry[k] > 0)
                perc = (scountry[k] * 100) / maxnum;

            htmlColl += Mustache.render(htmlloc, {
                country: k,
                valcur: scountry[k],
                valmax: maxnum,
                valper: perc.toFixed(2)
            });
        }

        if (indx > topfirst) {
            $('#mainloc .moreloc').html(htmlColl);
            $('#mainloc .btnmore').removeClass('hide');
        }
        else {
            if (indx > 0 && indx <= topfirst) {
                $('#mainloc .topfirst').html(htmlColl);
            }
            else if (indx < 1)
                $('#mainloc .topfirst').html('<div class="alert bg-info">No data.</div>');

            $('#mainloc .moreloc,#mainloc .btnmore').addClass('hide');
        }
    }
    else
        $('#mainloc .topfirst').html('<div class="alert bg-info">No data.</div>');


    $('#analytic .loading').hide();

    var data1 = [];
    var labels = [];
    for (var ke in objHour) {
        labels.push(ke);
        data1.push(objHour[ke]);
    }

    var data = {
        labels: labels,
        datasets: [
          {
              label: 'Click',
              backgroundColor: 'rgba(150,94,15,0.5)',
              strokeColor: 'rgba(150,94,15,1)',
              pointColor: 'rgba(150,94,15,1)',
              pointStrokeColor: '#fff',
              data: data1
          }
        ]
    };

    $('#chart-1-container').removeClass('loading');
    var chart = window['chartdata'];
    if (typeof chart !== 'undefined')
        chart.destroy();

    window['chartdata'] = new Chart(makeCanvas('chart-1-container'), {
        type: 'line',
        data: data
    });

    ProcessGAClickThroughRate();
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

function QueueDataGACampaignCTRAndCountry(arr, pageIndex, pageSize, cb) {
    if (typeof viewSelector === 'undefined')
        return;
    if ($.isArray(arr) === false || arr.length === 0)
        return;

    var filterColl = [];
    $.each(arr, function (i, o) {
        filterColl.push('ga:eventLabel==' + o);
    });

    var dateArr = GetDateRange('');
    var callCampaign = query({
        'ids': viewSelector.ids,
        'dimensions': 'ga:eventAction, ga:country, ga:dimension1, ga:hour',
        'metrics': 'ga:totalEvents',
        'start-date': dateArr[0],
        'end-date': dateArr[1],
        filters: filterColl.join(','),
        'start-index': ((pageIndex - 1) * pageSize) + 1,
        'max-results': pageSize
    });

    Promise.all([callCampaign])
        .then(function (results) {

            //console.log('results : ', results[0]);

            if (results && results[0]) {
                //process data
                if ($.isArray(results[0].rows) === true && results[0].rows.length > 0) {
                    var hourMM = undefined;
                    var hourText = '';
                    $.each(results[0].rows, function (i, o) {
                        if ($.inArray(o[2], totalClientID) < 0) {
                            totalClientID.push(o[2]);
                            if (typeof countryArr[o[1]] === 'undefined')
                                countryArr[o[1]] = 0;
                            countryArr[o[1]] += 1;
                        }

                        if (o[0].indexOf('Click---') === 0) {
                            eventAction.click += parseInt(o[4]) || 0;

                            hourMM = moment(o[3], 'HH');
                            hourText = hourMM.format('H') + hourMM.format('a');
                            objHour[hourText] += parseInt(o[4]) || 0;

                        }
                        else
                            eventAction.impression += parseInt(o[4]) || 0;
                    });
                }
                //process data

                if (results[0].totalResults > (pageIndex * pageSize))
                    QueueDataGACampaignCTRAndCountry(arr, ++pageIndex, pageSize, cb);
                else if (typeof cb === 'function')
                    cb();
            }
            else if (typeof cb === 'function')
                cb();
        })
        .catch(function (res) {
            console.log('QueueDataGACampaignCTRAndCountry Error load data.', res);
            if (res.error && res.error.code && res.error.code === 401)//Invalid Credentials
            {
                LoginServerGA();
            }
            else if (typeof cb === 'function')
                cb();
        });
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

