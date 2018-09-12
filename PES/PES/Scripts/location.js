window.onload = initialize();

var currentLocation;
if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(GeoSuccess, geoFail);
}
else {
    alert("Sorry, your browser does not support geolocation services.");
}

function GeoSuccess(position) {
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    codeLatLng(lat, lng);
}

function geoFail() {
    alert("Error while getting position");
}


var geocoder;
function initialize() {
    geocoder = new google.maps.Geocoder();
}

function codeLatLng(lat, lng) {

    var latlng = new google.maps.LatLng(lat, lng);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            console.log(results)
            if (results[1]) {
                //formatted address
                //var address = results[0].formatted_address;
                //alert("address = " + address);
                currentLocation = results[5].formatted_address;
                setUpDropDown(currentLocation);
            } else {
                alert("No results found");
            }
        } else {
            alert("Geocoder failed due to: " + status);
        }
    });
}

function setUpDropDown(location){
    var loc = location.split(",");
    var city = loc[0];
    var country = loc[1];

    if (city == "Colima") {
        $("#SelectedLocation option[value='1']").attr("selected", true);
    }
    else if (city == "Ciudad de México" || city == "Mexico City") {
        $("#SelectedLocation option[value='2']").attr("selected", true);
    }
    else if (city == "Mérida" || city == "Merida") {
        $("#SelectedLocation option[value='3']").attr("selected", true);
    }
    else if(country == "Estados Unidos" || country == "Unated States"){
        $("#SelectedLocation option[value='4']").attr("selected", true);
    }
    else {

    }
}