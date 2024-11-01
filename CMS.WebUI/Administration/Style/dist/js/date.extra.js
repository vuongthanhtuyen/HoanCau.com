Date.prototype.difference = function (dateTo) {
    if (dateTo === null || dateTo === undefined || dateTo.toString() === 'Invalid Date')
        return {};
    if (Object.prototype.toString.call(dateTo) !== "[object Date]")
        return {};

    var dt1 = this;
    /*
     * setup 'empty' return object
     */
    var ret = { Days: 0, Months: 0, Years: 0, Hours: 0, Minutes: 0, Seconds: 0 };

    /*
     * If the dates are equal, return the 'empty' object
     */
    if (dateTo == dt1) return ret;

    /*
     * ensure dt2 > dt1
     */
    if (dt1 > dateTo) {
        var dtmp = dateTo;
        dateTo = dt1;
        dt1 = dtmp;
    }

    /*
     * First get the number of full years
     */

    var year1 = dt1.getFullYear();
    var year2 = dateTo.getFullYear();

    var month1 = dt1.getMonth();
    var month2 = dateTo.getMonth();

    var day1 = dt1.getDate();
    var day2 = dateTo.getDate();

    var hour1 = dt1.getHours();
    var hour2 = dateTo.getHours();

    var minute1 = dt1.getMinutes();
    var minute2 = dateTo.getMinutes();

    var second1 = dt1.getSeconds();
    var second2 = dateTo.getSeconds();

    /*
     * Set initial values bearing in mind the months or days may be negative
     */


    ret['Seconds'] = second2 - second1;

    var hasBorrow = false;
    if (ret['Seconds'] < 0) {
        hasBorrow = true;
        ret['Seconds'] = second2 + 60 - second1;
    }

    if (hasBorrow === true)
        minute1 += 1;

    ret['Minutes'] = minute2 - minute1;

    hasBorrow = false;
    if (ret['Minutes'] < 0) {
        hasBorrow = true;
        ret['Minutes'] = minute2 + 60 - minute1;
    }

    if (hasBorrow === true)
        hour1 += 1;

    ret['Hours'] = hour2 - hour1;

    hasBorrow = false;
    if (ret['Hours'] < 0) {
        hasBorrow = true;
        ret['Hours'] = hour2 + 24 - hour1;
    }

    if (hasBorrow === true)
        day1 += 1;

    ret['Days'] = day2 - day1;

    ret['Years'] = year2 - year1;
    ret['Months'] = month2 - month1;

    /*
     * First if the day difference is negative
     * eg dt2 = 13 oct, dt1 = 25 sept
     */
    if (ret['Days'] < 0) {
        /*
         * Use temporary dates to get the number of days remaining in the month
         */
        var dtmp1 = new Date(dt1.getFullYear(), dt1.getMonth() + 1, 1, 0, 0, -1);

        var numDays = dtmp1.getDate();

        ret['Months'] -= 1;
        ret['Days'] += numDays;

    }

    /*
     * Now if the month difference is negative
     */
    if (ret['Months'] < 0) {
        ret['Months'] += 12;
        ret['Years'] -= 1;
    }

    return ret;
};

Date.prototype.adjust = function (part, amount) {
    part = part.toLowerCase();

    var map = {
        years: 'FullYear', months: 'Month', weeks: 'Hours', days: 'Hours', hours: 'Hours',
        minutes: 'Minutes', seconds: 'Seconds', milliseconds: 'Milliseconds',
        utcyears: 'UTCFullYear', utcmonths: 'UTCMonth', weeks: 'UTCHours', utcdays: 'UTCHours',
        utchours: 'UTCHours', utcminutes: 'UTCMinutes', utcseconds: 'UTCSeconds', utcmilliseconds: 'UTCMilliseconds'
    },
		mapPart = map[part];

    if (part == 'weeks' || part == 'utcweeks')
        amount *= 168;
    if (part == 'days' || part == 'utcdays')
        amount *= 24;

    this['set' + mapPart](this['get' + mapPart]() + amount);

    return this;
}

Date.prototype.each = function (endDate, part, step, fn, bind) {
    var fromDate = new Date(this.getTime()),
		toDate = new Date(endDate.getTime()),
		pm = fromDate <= toDate ? 1 : -1,
		i = 0;

    while ((pm === 1 && fromDate <= toDate) || (pm === -1 && fromDate >= toDate)) {
        if (fn.call(bind, fromDate, i, this) === false) break;
        i += step;
        fromDate.adjust(part, step * pm);
    }
    return this;
}

// return an array of date objects for start (monday)
// and end (sunday) of week based on supplied 
// date object or current date
Date.prototype.getStartAndEndOfWeek = function (date) {
    if (typeof date === 'undefined' || date === null)
        date = this;
    //console.log(date);
    if (date.toString() === 'Invalid Date')
        return null;

    // If no date object supplied, use current date
    // Copy date so don't modify supplied date
    var now = date ? new Date(date) : new Date();

    // set time to some convenient value
    now.setHours(0, 0, 0, 0);

    // Get the previous Monday
    var monday = new Date(now);
    monday.setDate(monday.getDate() - monday.getDay() + 0);

    // Get next Sunday
    var sunday = new Date(now);
    sunday.setDate(sunday.getDate() - sunday.getDay() + 6);
    sunday.setHours(23, 59, 59, 999);
    // Return array of date objects
    return [monday, sunday];
},

// Mon Nov 12 2012 00:00:00
// Sun Nov 18 2012 00:00:00
//alert(new Date().getStartAndEndOfWeek(new Date(2012, 10, 14)).join('\n'));
//alert(new Date().getStartAndEndOfWeek(new Date()).join('\n'));

Date.prototype.getStartAndEndOfMonth = function (today) {
    if (typeof today === 'undefined' || today === null)
        today = this;
    //console.log(date);
    if (today.toString() === 'Invalid Date')
        return null;
    var firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1, 0, 0, 0, 0);
    var lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0, 23, 59, 59, 999);
    // Return array of date objects
    return [firstDayOfMonth, lastDayOfMonth];
},
//alert(new Date().getStartAndEndOfMonth(new Date(2012, 10, 14)).join('\n'));
//alert(new Date().getStartAndEndOfMonth(new Date()).join('\n'));

Date.prototype.convertDateToString = function (date) {
    if (typeof date === 'undefined' || date === null)
        date = this;
    //console.log(date);
    if (date.toString() === 'Invalid Date')
        return null;
    return ("0" + (date.getMonth() + 1)).slice(-2) + '/' + ("0" + date.getDate()).slice(-2) + '/' + date.getFullYear();
}

/*
var d = new Date();

d.adjust('hour', -10);
console.log(d);



d2.adjust('days', 450);

d1.diff(d2,'weeks');
// returns integer 64

d1.diff(d2,['months','weeks','days']);
// returns { months: 14, weeks: 3, days: 2 }




var d1 = new Date(),
	d2 = new Date();

d2.adjust('days', 450);

// loop by weeks
d1.each(d2, 'weeks', 1, function(currentDate, currentStep, thisDate){
	console.log(arguments); // Open your console!
});

// loop by months
d1.each(d2, 'months', 1, function(currentDate, currentStep, thisDate){
	console.log(arguments); // Open your console!
});
*/