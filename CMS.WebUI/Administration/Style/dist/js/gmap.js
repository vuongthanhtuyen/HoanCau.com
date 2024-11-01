/// <reference path="../../js/jquery-1.7.2.js" />

var SweetSoftData = {
    VariableData: {
        geocoder: null,
        Map: null,
        MapOptions: {
            zoom: 12,
            center: null,
            mapTypeId: null,
            scrollwheel: false
        },
        Created: {
            markers: [],
            points: [],
            infos: [],
            distances: []
        },
        DirectionData: {
            count: 0,
            directionsDisplay: null,
            directionsService: null
        },
        timeover: null,
        txt: undefined
    }
}
var SweetSoftScript = {
    commonData: {
        AutocompletePlace: null,
        PlaceService: null
    },
    commonFunction: {
        include: function (url) {
            document.write('<script src="' + url + '" type="text/javascript"></script>');
        },
        sortObjectProperties: function (obj) {
            var arr = [];
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop)) {
                    arr.push({
                        'key': prop,
                        'value': obj[prop]
                    });
                }
            }
            arr.sort(function (a, b) {
                return (a.value.localeCompare(b.value));
            });
            return arr;
        }
    },
    mainFunction: {
        initialize: function () {
            if (typeof google === 'undefined')
                return;

            SweetSoftData.VariableData.MapOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
            SweetSoftData.VariableData.DirectionData.directionsService = new google.maps.DirectionsService();

            $('#btnback').click(function () {
                $(this).css('display', 'none');
                SweetSoftScript.mainFunction.HideDragInfo();
                return false;
            });

            $('#mainform li a[data-toggle="tab"][href="#map"]').on('shown.bs.tab', function (event) {
                setTimeout(function () {
                    google.maps.event.trigger(SweetSoftData.VariableData.Map, 'resize');
                    SweetSoftData.VariableData.Map.setCenter(SweetSoftData.VariableData.Created.markers[0].getPosition());

                    setTimeout('SweetSoftScript.mainFunction.UpdateMarkerAddress(\'' + defAdd + '\', true, false);', 200);

                }, 200);
            });

            //var defaultBounds = new google.maps.LatLngBounds(
            //new google.maps.LatLng(12.206902, 109.215603),
            //new google.maps.LatLng(12.25958, 109.198437));
            //SweetSoftData.VariableData.Map.fitBounds(defaultBounds);
            var txtAddress = $('input:text[id*="txtAddress"]');
            SweetSoftData.VariableData.txt = txtAddress;

            //49.2827291,109.196763 - Vancouver, British Columbia, Canada
            var posLat = 49.2827291;
            var posLong = -123.12073750000002;
            var inputLat = $('input[id*="txtLatitude"]');
            if (inputLat.length > 0 && inputLat.val().length > 0)
                posLat = parseFloat(inputLat.val().replace(',', '.'));
            var inputLong = $('input[id*="txtLongitude"]');
            if (inputLong.length > 0 && inputLong.val().length > 0)
                posLong = parseFloat(inputLong.val().replace(',', '.'));

            var point = new google.maps.LatLng(posLat, posLong);
            SweetSoftData.VariableData.MapOptions.center = point;
            SweetSoftData.VariableData.Map = new google.maps.Map(document.getElementById('map_canvas'), SweetSoftData.VariableData.MapOptions);
            var marker = new google.maps.Marker({
                map: SweetSoftData.VariableData.Map,
                icon: '/Administration/style/dist/img/marker-star-green.png',
                draggable: true,
                position: point
            });

            google.maps.event.addListener(SweetSoftData.VariableData.Map, 'mousedown', function (event) {
                this.setOptions({ scrollwheel: true });
            });
            google.maps.event.addListener(SweetSoftData.VariableData.Map, 'mouseover', function (event) {
                SweetSoftData.VariableData.timeover = setTimeout(function () {
                    SweetSoftData.VariableData.Map.setOptions({ scrollwheel: true });
                }, 2000);
            });
            google.maps.event.addListener(SweetSoftData.VariableData.Map, 'mouseout', function (event) {
                this.setOptions({ scrollwheel: false });
                clearTimeout(SweetSoftData.VariableData.timeover);
            });

            //create info buble
            var infoBubble = new InfoBubble({
                maxWidth: 300,
                minHeight: 52,
                backgroundColorTabActive: '#FE02EE',
                closeButtonClass: 'closebtn',
                borderWidth: 1,
                borderColor: '#10a1b0',
                backgroundColor: '#fff',
                padding: 10,
                borderRadius: 0,
                tabClassName: 'tab-bubble'
            });

            var defAdd = 'Vancouver, British Columbia, Canada';
            if (txtAddress.length > 0 && txtAddress.val().length > 0)
                defAdd = txtAddress.val();
            infoBubble.addTab('Address', defAdd);

            SweetSoftData.VariableData.Created.infos.push(infoBubble);

            //update data
            SweetSoftScript.mainFunction.UpdateMarkerPosition(point);

            //update data
            SweetSoftData.VariableData.Created.markers.push(marker);

            // Add dragging event listeners.
            google.maps.event.addListener(SweetSoftData.VariableData.Created.markers[0], 'dragstart', function () {
                this.setIcon('/Administration/style/dist/img/marker-star-blue.png');
                SweetSoftScript.mainFunction.HideSuggess();
            });

            // Add click event listeners.
            google.maps.event.addListener(SweetSoftData.VariableData.Created.markers[0], 'click', function () {
                //console.log(infoBubble);
                if (typeof SweetSoftData.VariableData.Created.infos[0] !== 'undefined') {
                    if (!SweetSoftData.VariableData.Created.infos[0].isOpen())
                        SweetSoftData.VariableData.Created.infos[0].open(SweetSoftData.VariableData.Map, SweetSoftData.VariableData.Created.markers[0]);
                }
            });

            // Add dragging event listeners.
            google.maps.event.addListener(SweetSoftData.VariableData.Created.markers[0], 'drag', function () {
                SweetSoftScript.mainFunction.UpdateMarkerPosition(SweetSoftData.VariableData.Created.markers[0].getPosition());
                SweetSoftScript.mainFunction.UpdateMarkerAddress('Dragging...', true, true);
            });

            // Add dragging event listeners.
            google.maps.event.addListener(SweetSoftData.VariableData.Created.markers[0], 'dragend', function () {
                this.setIcon('/Administration/style/dist/img/marker-star-green.png');
                this.getMap().setCenter(this.getPosition());
                SweetSoftData.VariableData.geocoder.geocode({
                    latLng: this.getPosition()
                }, function (responses) {
                    if (responses && responses.length > 0) {
                        //console.log(responses);
                        SweetSoftScript.mainFunction.UpdateMarkerAddress(responses[0].formatted_address, responses[0].name, false);
                    } else {
                        SweetSoftScript.mainFunction.UpdateMarkerAddress('Cannot determine at this location.', true, false);
                    }
                });
            });





            SweetSoftData.VariableData.geocoder = new google.maps.Geocoder();

            $(txtAddress).keypress(function (e) {
                var theEvent = e || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                if (theEvent.keyCode === 13) {
                    theEvent.returnValue = false;
                    if (theEvent.preventDefault)
                        theEvent.preventDefault();
                    return false;
                }
            });

            var searchBox = new google.maps.places.SearchBox(txtAddress[0]);
            google.maps.event.addListener(searchBox, 'places_changed', function () {

                //clear data            
                SweetSoftData.VariableData.Created.markers.splice(1, SweetSoftData.VariableData.Created.markers.length - 1);
                SweetSoftData.VariableData.Created.infos.splice(1, SweetSoftData.VariableData.Created.infos.length - 1);

                var places = searchBox.getPlaces();
                //console.log(places);
                if (places.length === 1) {
                    SweetSoftData.VariableData.Created.infos[0].updateTab(0, undefined, places[0].formatted_address);
                    SweetSoftData.VariableData.Created.infos[0].open(SweetSoftData.VariableData.Map, SweetSoftData.VariableData.Created.markers[0]);
                    //save info to global variable
                    SweetSoftData.VariableData.Created.infos.push(infoBubble);
                    SweetSoftData.VariableData.Created.markers[0].setVisible(true);
                    //console.log(bounds.getCenter());
                    var pos = places[0].geometry.location;
                    SweetSoftData.VariableData.Created.markers[0].setTitle(places[0].name);
                    SweetSoftData.VariableData.Created.markers[0].setPosition(pos);
                    SweetSoftData.VariableData.Map.setCenter(pos);
                    SweetSoftData.VariableData.Map.setZoom(14);
                    //update data
                    SweetSoftScript.mainFunction.UpdateMarkerPosition(pos);
                    SweetSoftScript.mainFunction.UpdateMarkerAddress(places[0].formatted_address, places[0].name, false);
                    //update data
                    SweetSoftScript.mainFunction.HideSuggess();
                }
                else {
                    SweetSoftData.VariableData.Created.markers[0].setVisible(false);
                    var resultElem = document.getElementById('results');
                    if (resultElem !== null) {
                        var bounds = new google.maps.LatLngBounds();
                        $(resultElem).children(':gt(0)').remove();
                        //console.log(predictions);
                        var infoBubble = null;
                        for (var i = 0, place; place = places[i]; i++) {
                            //console.log(place);
                            $(resultElem).append('<li><a href="javascript:SweetSoftScript.mainFunction.ShowResult(' + i + ');">' + places[i].name + '</a></li>');
                            bounds.extend(place.geometry.location);
                            //save info to global variable
                            SweetSoftData.VariableData.Created.infos.push(places[i].formatted_address);
                            SweetSoftData.VariableData.Created.markers.push(place.geometry.location);
                        }
                        SweetSoftScript.mainFunction.HideDragInfo();
                        SweetSoftData.VariableData.Map.fitBounds(bounds);
                    }
                }
                //console.log(SweetSoftData.VariableData.Created.markers);

                //$('#dragdata').slideUp();
            });

            //google.maps.event.addListener(SweetSoftData.VariableData.Map, 'bounds_changed', function() {
            //    var bounds = SweetSoftData.VariableData.Map.getBounds();
            //    console.log(bounds);
            //    searchBox.setBounds(bounds);
            //    searchBox.bindTo('bounds', SweetSoftData.VariableData.Map);
            //});
        },
        ShowResult: function (indx) {
            var text = $('#results>li:eq(' + (indx + 1) + ')>a').text();
            $(SweetSoftData.VariableData.txt).val(text);

            if ($('#btnback').length > 0) {
                if ($('#btnback').is(':hidden'))
                    $('#btnback').css('display', 'block');
            }
            //SweetSoftScript.mainFunction.HideSuggess();
            if (typeof SweetSoftData.VariableData.Created.markers[indx] !== 'undefined') {
                SweetSoftData.VariableData.Created.markers[0].setPosition(SweetSoftData.VariableData.Created.markers[indx + 1]);
                SweetSoftData.VariableData.Created.markers[0].setVisible(true);
                SweetSoftData.VariableData.Map.setZoom(17);
                SweetSoftData.VariableData.Map.setCenter(SweetSoftData.VariableData.Created.markers[indx + 1]);
                if (typeof SweetSoftData.VariableData.Created.infos[0] !== 'undefined') {
                    //console.log(SweetSoftData.VariableData.Created.infos[indx + 1]);
                    SweetSoftData.VariableData.Created.infos[0].updateTab(0, undefined, SweetSoftData.VariableData.Created.infos[indx + 1]);
                    if (!SweetSoftData.VariableData.Created.infos[0].isOpen())
                        SweetSoftData.VariableData.Created.infos[0].open(SweetSoftData.VariableData.Map, SweetSoftData.VariableData.Created.markers[0]);
                    SweetSoftScript.mainFunction.UpdateMarkerPosition(SweetSoftData.VariableData.Created.markers[indx + 1]);
                    //console.log(SweetSoftData.VariableData.Created.infos[indx]);                 
                    SweetSoftScript.mainFunction.UpdateMarkerAddress(SweetSoftData.VariableData.Created.infos[0].tabs_[0].content, false, false);
                    SweetSoftScript.mainFunction.HideSuggess();
                }
            }
        },
        UpdateMarkerPosition: function (latLng) {
            var infonew = [
                    latLng.lat(),
                    latLng.lng()
            ].join(', ');
            document.getElementById('info').innerHTML = infonew;
            //SweetSoftData.VariableData.geocoder.geocode({
            //    latLng: latLng
            //}, function(responses) {
            //    if (responses && responses.length > 0) {
            //        //console.log(responses);
            //        SweetSoftScript.mainFunction.UpdateMarkerAddress(responses[0].formatted_address, false);
            //    } else {
            //        SweetSoftScript.mainFunction.UpdateMarkerAddress('Cannot determine add at this location.', false);
            //    }
            //});
        },
        UpdateMarkerAddress: function (str, isChangeName, isHide) {
            if (SweetSoftData.VariableData.Created.infos[0].isOpen())
                SweetSoftData.VariableData.Created.infos[0].close();
            document.getElementById('add').innerHTML = str;
            //console.log(isChangeName, str);
            /*
            if (typeof isChangeName === 'boolean') {
                if (isChangeName === true)
                    $(SweetSoftData.VariableData.txt).val(str);
            }
            else if (typeof isChangeName === 'string')
                $(SweetSoftData.VariableData.txt).val(isChangeName);
            else if (typeof isChangeName === 'undefined')
                $(SweetSoftData.VariableData.txt).val(str);
            */
            $(SweetSoftData.VariableData.txt).val(str);

            if (isHide === false) {
                SweetSoftData.VariableData.Created.infos[0].updateTab(0, undefined, str);
                if (!SweetSoftData.VariableData.Created.infos[0].isOpen())
                    SweetSoftData.VariableData.Created.infos[0].open(SweetSoftData.VariableData.Map, SweetSoftData.VariableData.Created.markers[0]);
            }
        },
        HideSuggess: function () {
            if ($('#results').is(':visible'))
                $('#results').slideUp('normal', function () {
                    $('#dragdata').slideDown();
                });
            else
                $('#dragdata').slideDown();
        },
        HideDragInfo: function () {
            if ($('#dragdata').is(':visible'))
                $('#dragdata').slideUp('normal', function () {
                    $('#results').slideDown();
                });
            else
                $('#results').slideDown();
        },
        reInit: function () {
            SweetSoftData.VariableData.Created.infos = [];
            SweetSoftData.VariableData.Created.markers = [];
            SweetSoftScript.mainFunction.initialize();
        }
    }
};

//SweetSoftScript.commonFunction.include("content/infobubble.js");
$(function () {
    //SweetSoftScript.mainFunction.initialize();
    if (typeof Sys !== 'undefined' && typeof Sys.WebForms.PageRequestManager !== 'undefined')
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(SweetSoftScript.mainFunction.reInit);
});

function SaveGmapData() {
    var text = $('#info').text();
    if (text && text.length > 0) {
        var arr = text.split(',');
        if (arr.length === 2) {

            var inputLat = $('input[id*="txtLatitude"]');
            if (inputLat.length > 0)
                inputLat.val(arr[0]);

            var inputLong = $('input[id*="txtLongitude"]');
            if (inputLong.length > 0)
                inputLong.val(arr[1]);
        }
    }
}

//This function is called from the Explorer.aspx page
function OnFileSelected(wnd, fileSelected, textBoxID) {
    var textbox = $get(textBoxID);
    textbox.value = fileSelected;
    MarkAsChanged();
}