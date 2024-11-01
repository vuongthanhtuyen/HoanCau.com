/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
/// <reference path="../../plugins/moment/moment.min.js" />

var callQuery = 2;
$(function () {

    var yesterday = moment().add(-1, 'days');
    var pass7daysago = moment().add(-7, 'days');
    var isload = true;
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

        if (dataChartTable && dataChartTable.jY)
            dataChartTable.jY.clearChart();

        if (breakdownChart && breakdownChart.jY)
            breakdownChart.jY.clearChart();

        if (dataChartGeomap && dataChartGeomap.jY)
            dataChartGeomap.jY.clearChart();

        if (dataChartDonut && dataChartDonut.jY)
            dataChartDonut.jY.clearChart();

        gapi.analytics.auth.signOut();

        $('#btnsignout,#mainpnl').addClass('hide');
        $('#authmsg').html('');
        $('#view-selector-container').empty();
        viewSelector = undefined;

        return false;
    });

    $('div[id$="dimensions"] input.btn').each(function () {
        $(this).click(function () {
            var t = $(this);
            if (t.hasClass('active'))
                return false;

            var act = t.closest('div[id$="dimensions"]').find('input.btn.active');
            if (act.length > 0)
                act.removeClass('active');
            t.addClass('active');
            var ty = t.attr('data-type');
            UpdateChartType();
            return false;
        });
    });

    $('div[id$="metrics"] input:button.btn').each(function () {
        $(this).click(function () {
            var t = $(this);
            if (t.hasClass('active'))
                return false;

            var act = t.closest('div[id$="metrics"]').find('input.btn.active');
            if (act.length > 0)
                act.removeClass('active');
            t.addClass('active');
            var ty = t.attr('data-type');
            UpdateChartType();
            return false;
        });
    });

    $('div[id$="metrics"] input:radio').change(function () {
        UpdateChartType();
    });

    $('div[id$="metrics"] input:checkbox').change(function () {
        UpdateChartType();
    });

    $('#mainpnl > .nav-tabs li a').on('shown.bs.tab', function () {
        UpdateChartType();
    });

    $('#charttype input.btn').each(function () {
        $(this).click(function () {
            var t = $(this);
            if (t.hasClass('active'))
                return false;

            var act = $('#charttype input.btn.active');
            if (act.length > 0)
                act.removeClass('active');
            t.addClass('active');
            var ty = t.attr('data-type');
            UpdateChartType();
            return false;
        });
    });


    $('#tab-container').on('click', '.tbmain tbody tr', function () {
        //console.log(this);
        if ($(this).hasClass('info'))
            return false;

        var act = $(this).closest('tbody').find('.info');
        if (act.length > 0)
            act.removeClass('info');
        $(this).addClass('info');

        reportDetail = {};
        var txt = $.trim($(this).find('td:first').text());
        if (txt.length > 0 && typeof reportObj !== 'undefined') {
            if ($.isArray(reportObj[txt]) === true) {
                $.each(reportObj[txt], function (i, o) {
                    if (typeof reportDetail[o['city']] === 'undefined')
                        reportDetail[o['city']] = 0;

                    reportDetail[o['city']] += 1;
                });
            }
        }

        var html = '';
        if ($.isEmptyObject(reportDetail) === false) {
            var newso = sortProperties(reportDetail, '', true, true);
            for (var i = 0; i < newso.length; i++) {
                var key = newso[i][0];
                var value = newso[i][1];

                html += '<tr><td>' + key + '</td><td>' + value + '</td></tr>';
            }
        }

        var tab = $('#tab-container .tab-content .active');
        if (tab.length > 0) {
            tab.find('table:last tbody').html(html);
        }
    });

    $('#tab-container .nav-tabs a').on('show.bs.tab', function (event) {
        var x = $(event.target).attr('href');
        //console.log(x.substring(1));
        var y = $(event.relatedTarget).attr('href');
        if ($(y).find('.loading:hidden').length === 0) {
            console.log('waiting for processing.');
            event.preventDefault();
            return false;
        }

        UpdateChartMember(x.substring(1));
    });

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

var dataChart = undefined;
var dataChartDonut = undefined;
var dataChartGeomap = undefined;
var viewSelector = undefined;

var dataChartTable = undefined;
var breakdownChart = undefined;
var mainChartRowClickListener = undefined;

function GetDateRange(id) {
    var arr = [];
    var mome = $('#' + id + 'dpfrom').data("DateTimePicker").date();
    arr.push(mome.format('YYYY-MM-DD'));
    mome = $('#' + id + 'dpto').data("DateTimePicker").date();
    arr.push(mome.format('YYYY-MM-DD'));
    return arr;
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
                console.log('response : ', response);



                viewSelector = new gapi.analytics.ext.ViewSelector2({
                    container: 'view-selector-container',
                    propertyId: specialId
                });


                viewSelector.on('change', function (ids) {
                    $('#mainpnl').removeClass('hide');
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

google.load("visualization", "1", {
    packages: ["corechart"],
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


            LoginServerGA();
        });
    }
});


function UpdateTraffic(type) {
    if (typeof viewSelector === 'undefined')
        return;

    errorCount = 0;
    var metricData = [];
    $('#trafficmetrics input:checkbox').each(function () {
        if ($(this).is(':checked')) {
            var t = $(this).attr('data-type') || '';
            if (t.length > 0)
                metricData.push('ga:' + t);
        }
    });

    if (metricData.length === 0) {
        $('#traffic-chart-container').empty();
        return;
    }

    if (typeof type === 'undefined' || type === null || type.length === 0) {
        var act = $('#charttype input.btn.active');
        if (act.length === 0) {
            act = $('#charttype input.btn:eq(0)');
            act.addClass('active');
        }
        type = act.attr('data-type');

        if (typeof type === 'undefined' || type === null || type.length === 0)
            return;
    }

    if (typeof dataChart !== 'undefined') {
        if (dataChart.get().chart.type !== type)
            dataChart = undefined;
    }

    if (typeof dataChart === 'undefined') {
        dataChart = new gapi.analytics.googleCharts.DataChart();
        dataChart.on('success', function () {
            $('#traffic-chart-container').removeClass('loading');
        }).on('error', function () {
            $('#traffic-chart-container').removeClass('loading');
        });
    }

    $('#traffic-chart-container').addClass('loading');
    /**
      * Create a new DataChart instance with the given query parameters
      * and Google chart options. It will be rendered inside an element
      * with the id "chart-container".
    */
    var dateArr = GetDateRange('traffic');
    //console.log('dateArr : ', dateArr, type);
    var opt = {
        query: {
            metrics: metricData.join(','),
            dimensions: 'ga:date',
            'start-date': dateArr[0],
            'end-date': dateArr[1]
        },
        chart: {
            container: 'traffic-chart-container',
            type: type,
            options: {
                width: '100%',
                colors: ['#4D4D4D', '#5DA5DA', '#FAA43A', '#60BD68', '#F17CB0', '#B2912F', '#B276B2', '#DECF3F', '#F15854']
            }
        }
    };

    //modify class
    if (type === 'BAR')
        opt.chart.options.height = 450;

    opt.query.ids = viewSelector.ids;

    dataChart.set(opt).execute();

}

function UpdateDatatable(dimension) {
    if (typeof viewSelector === 'undefined')
        return;

    errorCount = 0;
    var metricData = [];
    $('#tablemetrics input:checkbox').each(function () {
        if ($(this).is(':checked')) {
            var t = $(this).attr('data-type') || '';
            if (t.length > 0)
                metricData.push('ga:' + t);
        }
    });

    if (metricData.length === 0) {
        $('#table-chart-container,#table-breakdown').empty();
        return;
    }

    if (typeof dimension === 'undefined' || dimension === null || dimension.length === 0) {
        var act = $('#tabledimensions input.btn.active');
        if (act.length === 0) {
            act = $('#tabledimensions input.btn:eq(0)');
            act.addClass('active');
        }
        dimension = act.attr('data-type');

        if (typeof dimension === 'undefined' || dimension === null || dimension.length === 0) {
            $('#table-chart-container,#table-breakdown').empty();
            return;
        }
    }

    $('#table-chart-container').addClass('loading');

    if (typeof dataChartTable === 'undefined') {
        dataChartTable = new gapi.analytics.googleCharts.DataChart();
        breakdownChart = new gapi.analytics.googleCharts.DataChart();

        /**
         * Each time the main chart is rendered, add an event listener to it so
         * that when the user clicks on a row, the line chart is updated with
         * the data from the browser in the clicked row.
         */
        dataChartTable.on('success', function handleCoreAPIResponse(resultsAsObject) {

            // Clean up any event listeners registered on the main chart before
            // rendering a new one.
            if (mainChartRowClickListener) {
                google.visualization.events.removeListener(mainChartRowClickListener);
            }

            var chart = resultsAsObject.chart;
            var dataTable = resultsAsObject.dataTable;
            //console.log('dataTable : ', dataTable, dataChart);
            if (dataTable.getNumberOfRows() > 0) {

                // Store a reference to this listener so it can be cleaned up later.
                mainChartRowClickListener = google.visualization.events.addListener(chart, 'select', function (event) {

                    var dm = '';
                    var act = $('#tabledimensions input.btn.active');
                    if (act.length === 0) {
                        act = $('#tabledimensions input.btn:eq(0)');
                        act.addClass('active');
                    }
                    dm = act.attr('data-type');

                    if (typeof dm === 'undefined' || dm === null || dm.length === 0) {
                        return;
                    }


                    var sel = chart.getSelection();
                    //console.log('chart.getSelection() : ', sel, sel[0].row);

                    // When you unselect a row, the "select" event still fires
                    // but the selection is empty. Ignore that case.
                    if (!sel.length) return;


                    var row = sel[0].row;
                    var val = dataTable.getValue(row, 0);

                    var options = {
                        query: {
                            filters: 'ga:' + dm + '==' + val
                        },
                        chart: {
                            options: {
                                title: val
                            }
                        }
                    };

                    breakdownChart.set(options).execute();
                });

                chart.setSelection([{ row: 0 }]);
                google.visualization.events.trigger(chart, 'select', {});
            }
            else {
                if (typeof breakdownChart.jY !== 'undefined')
                    breakdownChart.jY.clearChart();
            }

            $('#table-chart-container').removeClass('loading');

        })
        .on('error', function (err) {
            //console.log('test', err);
            $('#table-chart-container').removeClass('loading');
        });

    }

    /**
      * Create a new DataChart instance with the given query parameters
      * and Google chart options. It will be rendered inside an element
      * with the id "chart-container".
    */
    var dateArr = GetDateRange('table');
    //console.log('dateArr : ', dateArr, type);

    var opt = {
        query: {
            ids: viewSelector.ids,
            'dimensions': 'ga:' + dimension,
            'metrics': metricData.join(','),
            'sort': '-' + metricData[0],
            'max-results': '20',
            'start-date': dateArr[0],
            'end-date': dateArr[1]
        },
        chart: {
            container: 'table-chart-container',
            type: 'TABLE',
            options: {
                width: '100%'
            }
        }
    };

    /**
   * Create a table chart showing top browsers for users to interact with.
   * Clicking on a row in the table will update a second timeline chart with
   * data from the selected browser.
   */

    dataChartTable.set(opt);

    /**
     * Create a timeline chart showing sessions over time for the browser the
     * user selected in the main chart.
     */
    var opt2 = {
        query: {
            ids: viewSelector.ids,
            'dimensions': 'ga:date',
            'metrics': metricData.join(','),
            'start-date': dateArr[0],
            'end-date': dateArr[1]
        },
        chart: {
            type: 'LINE',
            container: 'table-breakdown',
            options: {
                width: '100%',
                colors: ['#4D4D4D', '#5DA5DA', '#FAA43A', '#60BD68', '#F17CB0', '#B2912F', '#B276B2', '#DECF3F', '#F15854']
                /*
                series: {
                    0: { color: '#316395' },
                    1: { color: '#b82e2e' },
                    2: { color: '#66aa00' },
                    3: { color: '#dd4477' },
                    4: { color: '#0099c6' },
                    5: { color: '#990099' },
                }
                */
            }
        }
    };
    breakdownChart.set(opt2);


    dataChartTable.execute();

}

function UpdateGeomap(metricData) {
    if (typeof viewSelector === 'undefined')
        return;

    errorCount = 0;
    if (typeof metricData === 'undefined' || metricData === null || metricData.length === 0) {
        var act = $('#geomapmetrics input.btn.active');
        if (act.length === 0) {
            act = $('#geomapmetrics input.btn:eq(0)');
            act.addClass('active');
        }
        metricData = act.attr('data-type');

        if (typeof metricData === 'undefined' || metricData === null || metricData.length === 0)
            return;
    }

    if (typeof dataChartGeomap === 'undefined') {
        dataChartGeomap = new gapi.analytics.googleCharts.DataChart();

        dataChartGeomap.on('success', function () {
            $('#geomap-chart-container').removeClass('loading');
        }).on('error', function () {
            $('#geomap-chart-container').removeClass('loading');
        });
    }
    $('#geomap-chart-container').addClass('loading');

    /**
      * Create a new DataChart instance with the given query parameters
      * and Google chart options. It will be rendered inside an element
      * with the id "chart-container".
    */
    var dateArr = GetDateRange('geomap');
    //console.log('dateArr : ', dateArr, type);
    var opt = {
        query: {
            ids: viewSelector.ids,
            dimensions: 'ga:country',
            metrics: 'ga:' + metricData,
            'start-date': dateArr[0],
            'end-date': dateArr[1]
        },
        chart: {
            container: 'geomap-chart-container',
            type: 'GEO',
            options: {
                width: '100%'
            }
        }
    };

    dataChartGeomap.set(opt).execute();
}

function UpdateDonut(dimension) {
    if (typeof viewSelector === 'undefined')
        return;
    
    errorCount = 0;
    var metricData = [];
    $('#donutmetrics input:radio').each(function () {
        if ($(this).is(':checked')) {
            var t = $(this).attr('data-type') || '';
            if (t.length > 0)
                metricData.push('ga:' + t);
        }
    });

    if (metricData.length === 0) {
        $('#donut-chart-container').empty();
        return;
    }

    if (typeof dimension === 'undefined' || dimension === null || dimension.length === 0) {
        var act = $('#donutdimensions input.btn.active');
        if (act.length === 0) {
            act = $('#donutdimensions input.btn:eq(0)');
            act.addClass('active');
        }
        dimension = act.attr('data-type');

        if (typeof dimension === 'undefined' || dimension === null || dimension.length === 0) {
            $('#donut-chart-container').empty();
            return;
        }
    }

    $('#donut-chart-container').addClass('loading');

    /**
      * Create a new DataChart instance with the given query parameters
      * and Google chart options. It will be rendered inside an element
      * with the id "chart-container".
    */
    if (typeof dataChartDonut === 'undefined') {
        dataChartDonut = new gapi.analytics.report.Data();

        dataChartDonut.on('success', function handleCoreAPIResponse(resultsAsObject) {
            //console.log('resultsAsObject : ', resultsAsObject);
            $('#donut-chart-container').removeClass('loading');

            if (resultsAsObject.rows && resultsAsObject.rows.length > 0) {

                var data = new google.visualization.DataTable();
                data.addColumn('string', dimension);
                data.addColumn('number', 'Sessions');

                resultsAsObject.rows.forEach(function pushNumericColumn(row) {
                    data.addRow([row[0], parseInt(row[1])]);
                });

                var options = {
                    title: dimension,
                    pieHole: 0.2,
                    legend: { position: 'right', alignment: 'center' },
                    chartArea: { left: '2%', top: '2%', width: "96%", height: "96%" },
                    height: 350,
                    is3D: is3DRender
                };

                var chart = new google.visualization.PieChart(document.getElementById('donut-chart-container'));
                chart.draw(data, options);
            }

        })
            .on('error', function (err) {
                $('#donut-chart-container').removeClass('loading');
                //console.log('Donut error :', err);
            });

    }

    var dateArr = GetDateRange('donut');
    //console.log('dateArr : ', dateArr, type);

    var mt = 'ga:sessions';
    var ds = 'ga:browser';

    var is3DRender = false;
    var q = {
        ids: viewSelector.ids,
        'dimensions': 'ga:' + dimension,
        'metrics': metricData.join(','),
        'start-date': dateArr[0],
        'end-date': dateArr[1],
        'max-results': 10
    };

    dataChartDonut.set({ query: q });

    dataChartDonut.execute();

}

var errorCount = 0;
function UpdateChartType() {

    try {
        var curtab = $('#mainpnl .nav-tabs li.active').index();
        switch (curtab) {
            case -1:
                //nothing
                break;
            case 0:
                UpdateTraffic();
                break;
            case 1:
                UpdateDatatable();
                break;
            case 2:
                UpdateGeomap();
                break;
            case 3:
                UpdateDonut();
                break;
            case 4:
                UpdateChartMember();
                break;
        }
    }
    catch (ex) {
        errorCount += 1;
        if (errorCount === 5)
            errorCount = 0;
        else {
            console.log('error UpdateChartType() ', ex);
            if (ex.error && ex.error.code && ex.error.code === 401)//Invalid Credentials
            {
                LoginServerGA();
            }
        }
    }
}


var pageSizeUserGet = 350;
var pageSizeSave = 400;
var pageSizeGet = 300;
var dataArr = [];
var arrClientId = [];
var totalClientId = [];
var memberId = [];
var keyReport = '';
var reportObj = {};
var reportDetail = {};

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
            if ($.isArray(obj[key]) === true)
                sortable.push([key, obj[key].length]);
            else
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

function CallSortMain(objs) {

    var newObject = {};

    var sortedArray = sortProperties(objs, '', true, true);
    //console.log('sortedArray : ', sortedArray);
    for (var i = 0; i < sortedArray.length; i++) {
        var key = sortedArray[i][0];
        newObject[key] = objs[key];
    }

    return newObject;
};

function UpdateChartMember(tab) {
    if (typeof viewSelector === 'undefined')
        return;
    if (typeof tab === 'undefined')
        tab = $('#tab-container .tab-content .active').attr('id') || '';

    var ld = $('#tab-container .tab-content .active').find('.loading');
    if (ld.is(':visible') === true) {
        console.log('waiting for processing.');
        return;
    }

    //console.log('process tab ', tab);
    var dimensionFilter = '';
    if (typeof tab !== 'undefined' && tab !== null && tab.length > 0) {
        $('#' + tab).find('.loading').show();

        if (tab.toLowerCase() === 'country')
            dimensionFilter = 'ga:country,ga:city';
        else if (tab.toLowerCase() === 'device')
            dimensionFilter = 'ga:mobileDeviceBranding,ga:mobileDeviceInfo';
        else if (tab.toLowerCase() === 'os')
            dimensionFilter = 'ga:operatingSystem,ga:operatingSystemVersion';
        else if (tab.toLowerCase() === 'browser')
            dimensionFilter = 'ga:browser,ga:browserVersion';
    }

    if (dimensionFilter.length === 0)
        return;

    errorCount = 0;
    dataArr = [];
    totalClientId = [];
    memberId = [];
    reportObj = {};
    reportDetail = {};
    keyReport = UUID.generate() + '-' + Math.random().toString(36).substring(17);
    QueueUser(dimensionFilter, GetDateRange('member'), 1, pageSizeUserGet, RenderMemberResult);
};

function RenderMemberResult() {
    //console.log(totalClientId, dataArr);
    totalClientId = [];

    if (memberId.length > 0 && dataArr.length > 0) {
        $.each(memberId, function (i, o) {
            //console.log(o.g);
            if ($.inArray(o.g, totalClientId) < 0) {
                totalClientId.push(o.g);

                $.each(dataArr, function (ii, oo) {
                    if (oo[0] === o.g) {
                        if (typeof reportObj[oo[1]] === 'undefined')
                            reportObj[oo[1]] = [];

                        reportObj[oo[1]].push({ city: oo[2], user: o.u });
                        return false;
                    }
                });
            }
        });
    }

    console.log(reportObj);

    var html = '';
    if ($.isEmptyObject(reportObj) === false) {

        var newso = CallSortMain(reportObj);

        var count = 0;
        for (var k in newso) {

            for (var ii in newso[k])
                count = newso[k].length;

            html += '<tr><td>' + k + '</td><td>' + count + '</td></tr>';
        }
    }

    var tab = $('#tab-container .tab-content .active');
    //console.log(tab, html);
    if (tab.length > 0) {
        tab.find('table:first tbody').html(html);
        var firstchild = tab.find('table:first tbody tr:first');
        if (firstchild.length > 0)
            firstchild[0].click();
    }
    $('#tab-container .tab-content .active').find('.loading').hide();
};

function QueueUser(dimensionFilter, dateArr, pageIndex, pageSize, cb) {
    //console.log('QueueUser : ', ((pageIndex - 1) * pageSize) + 1);
    var dataQuery = query({
        'ids': viewSelector.ids,
        'dimensions': 'ga:dimension1,' + dimensionFilter,
        'metrics': 'ga:users',
        'start-date': dateArr[0],
        'end-date': dateArr[1],
        'start-index': ((pageIndex - 1) * pageSize) + 1,
        'max-results': pageSize
    });


    Promise.all([dataQuery])
        .then(function (results) {
            //console.log('results : ', results[0].totalResults);
            if (results && results[0])
                ProcessUser(results, dimensionFilter, pageIndex, pageSize, dateArr, cb);
            else if (typeof cb === 'function')
                cb();
        }).catch(function (res) {
            console.log('Error load data.', res);
            if (typeof cb === 'function')
                cb();
        });
};

function ProcessUser(results, dimensionFilter, pageIndex, pageSize, dateArr, cb) {
    //console.log('ProcessUser results : ', results);
    arrClientId = [];

    $.each(results[0].rows, function (i, oo) {
        dataArr.push(oo);
        if ($.inArray(oo[0], totalClientId) < 0) {
            arrClientId.push(oo[0]);
            totalClientId.push(oo[0]);
        }
    });


    //console.log('arrClientId : ', arrClientId, actionType);
    var isEnd = (results[0].totalResults > (pageIndex * pageSize)) ? 0 : 1;
    $.ajax({
        type: "POST",
        url: "/Administration/WebAnalyticReport.aspx/ProcessReport",
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
            if (res && res.d && res.d.UId) {
                ProcessUserReport(res.d);
                QueueUserReport(2, pageSizeGet, cb);
            }
            else {
                if (results[0].totalResults > (pageIndex * pageSize))
                    QueueUser(dimensionFilter, dateArr, ++pageIndex, pageSizeUserGet, cb);
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

};

function QueueUserReport(pageIndex, pageSize, cb) {
    $.ajax({
        type: "POST",
        url: "/Administration/WebAnalyticReport.aspx/ProcessReport",
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
            if (res && res.d && res.d.UId) {
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

    if (data && data.UId) {
        var obj = JSON.parse(data.UId);
        if ($.isArray(obj) === true && obj.length > 0) {
            $.each(obj, function (i, o) {
                memberId.push(o);
            });
        }
    }
    //console.log('memberId : ', memberId);

};