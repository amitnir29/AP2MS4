/* code from github- https://github.com/bbecquet/Leaflet.RotatedMarker/blob/master/leaflet.rotatedMarker.js */
(function () {
    // save these original methods before they are overwritten
    var proto_initIcon = L.Marker.prototype._initIcon;
    var proto_setPos = L.Marker.prototype._setPos;

    var oldIE = (L.DomUtil.TRANSFORM === 'msTransform');

    L.Marker.addInitHook(function () {
        var iconOptions = this.options.icon && this.options.icon.options;
        var iconAnchor = iconOptions && this.options.icon.options.iconAnchor;
        if (iconAnchor) {
            iconAnchor = (iconAnchor[0] + 'px ' + iconAnchor[1] + 'px');
        }
        this.options.rotationOrigin = this.options.rotationOrigin || iconAnchor || 'center bottom';
        this.options.rotationAngle = this.options.rotationAngle || 0;

        // Ensure marker keeps rotated during dragging
        this.on('drag', function (e) { e.target._applyRotation(); });
    });

    L.Marker.include({
        _initIcon: function () {
            proto_initIcon.call(this);
        },

        _setPos: function (pos) {
            proto_setPos.call(this, pos);
            this._applyRotation();
        },

        _applyRotation: function () {
            if (this.options.rotationAngle) {
                this._icon.style[L.DomUtil.TRANSFORM + 'Origin'] = this.options.rotationOrigin;

                if (oldIE) {
                    // for IE 9, use the 2D rotation
                    this._icon.style[L.DomUtil.TRANSFORM] = 'rotate(' + this.options.rotationAngle + 'deg)';
                } else {
                    // for modern browsers, prefer the 3D accelerated version
                    this._icon.style[L.DomUtil.TRANSFORM] += ' rotateZ(' + this.options.rotationAngle + 'deg)';
                }
            }
        },

        setRotationAngle: function (angle) {
            this.options.rotationAngle = angle;
            this.update();
            return this;
        },

        setRotationOrigin: function (origin) {
            this.options.rotationOrigin = origin;
            this.update();
            return this;
        }
    });
})();

/* end of code from github*/
let planeGenericIcon = L.Icon.extend({
    options: {
        shadowUrl: '../assets/blank-pic.png',

        iconSize: [84, 85], // size of the icon
        shadowSize: [50, 64], // size of the shadow
        iconAnchor: [42, 42], // point of the icon which will correspond to marker's location
        shadowAnchor: [4, 62],  // the same for the shadow
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    }
});

const deselectedPlaneIcon = new planeGenericIcon({ iconUrl: '../assets/plane2.webp' }),
    selectedPlaneIcon = new planeGenericIcon({ iconUrl: '../assets/plane-selected.png' });

class Map {
    constructor(flightEventHandler) {
        this.flightEventHandler = flightEventHandler;
        //initialize map
        this.mymap = L.map('mapid').setView([51.505, -0.09], 13);
        L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoidW5kdmlrIiwiYSI6ImNrYWg3amZiNDBkcjcyeW81d3JibHRyaTgifQ.67Dwm_vni6DbznyF0bB1ZA', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1,
            accessToken: 'your.mapbox.access.token'
        }).addTo(mymap);
        mymap.on('click', this.onMapClick);

        this.polySegments = [];
        this.selected = null;
        this.selectedId = -1;
        this.locations = {};
        this.colors = ['red', 'blue', 'purple', 'Indigo', 'green', 'cyan', 'olive', 'pink', 'Teal', 'Chocolate', 'Black', 'gray', 'LawnGreen','DarkBlue']
    }

    updateFlights(flightWrappers) {
        //erase the previous icons
        for (let wrapper in this.flightWrappers) {
            if (!wrapper in flightWrappers)
                wrapper.planeIconReference.remove();
        }
        let flag = true;
        //erase the current segments and flight details, only if the plane didnt disspear
        for (let newWrapper in this.flightWrappers) {
            if (newWrapper.id == this.selectedId) {
                flag = false;
            }
        }
        //if didn't find the plane
        if (flag) {
            removeSegmentsPoly();
            this.flightEventHandler.eraseFlightDetails();
        }

        //update with the new flights
        this.flightWrappers = flightWrappers;
        //draw new icons and link them to the wrappers
        for (wrapper of this.flightWrappers) {

            //caculate the angle of the plane
            //log-log/lat-lat
            //default val for first run
            let angle = 45;
            let idkey = wrapper.id;
            if ((idkey in this.locations)) {
                m = (wrapper.flightDetails.longitude - this.locations.idkey.longitude) / (wrapper.flightDetails.latitude - this.locations.idkey.latitude);
                let theta = Math.atan2(m);
                //the default picure is in 45 degress angle
                angle = (theta * 180 / Math.PI) - 45;
            }
            let long = wrapper.flightDetails.longitude;
            let lat = wrapper.flightDetails.latitude;
            let planeMarker = L.marker([lat, long], { icon: deselectedPlaneIcon, rotationAngle: angle, rotationOrigin: "center" }).on('click', this.clickOnPlane).addTo(this.mymap);
            wrapper.planeIconReference = planeMarker;
            cords = {
                "latitude": lat,
                "longitude": long,
            }
            this.locations.idkey = cords;
        }

    }

    //deselect the plane if clicked on map
    onMapClick(event) {
        if (selected != null) {
            this.flightEventHandler.hidePressedFlight(selected);
            /*deselectPlane();
            selected = null;
            this.selectedId = -1;*/
            

        }
        //this.selected = null;

        //get the correct 
        //const fd = document.getElementById("flightDetails");
        //fd.innerHTML = "";
    }

    deselectPlane() {
        //use the deselected texture
        let wrap = this.getWrapper(selected);
        wrap.planeIconReference.setIcon(PlaneIconDeselected);
        //erase the lines
        removeSegmentsPoly(this.polySegments);
    }

    removeSegmentsPoly() {
        for (let poly of this.polySegments) {
            poly.remove();
        }
        this.polySegments = []
    }
    //function of event clicking on plane
    onClickPlane(e) {
        //console.log(document.getElementById("flightDetails").children.item(1).innerHTML = "this is a flight details");
        //console.log(e.target);
        //if we clicked on the same plane, do nothing
        //if (selected === e.sourceTarget) { }
        if (selected != e.sourceTarget) {
            
           
            
            let wrap = this.getWrapper(selected);
            this.flightEventHandler.showPressedFlight(wrap);
            

            
        }
    }
    getWrapper(object) {
        for (let wrapper of this.flightWrappers) {
            if (object == marker.planeIconReference)
                return marker;
        }
        return null;
    }
    calculateAngle(flight) {

    }
    showPressedFlight(wrapper, plan) {
        //save the new selected
        this.selected = wrapper;
        this.selectedId = wrapper.id;
        //remove the last one polys
        removeSegmentsPoly(this.poly_segments);
        //change the icon to be a selected one
        wrap.planeIconReference.setIcon(selectedPlaneIcon);
        //segments = []
        this.drawSegments(plan, this.poly_segments);
    }
    hidePressedFlight() {
        deselectPlane();
        this.selected = null;
        this.selectedId = -1;
    }
    drawSegments(segments, poly_segments) {
        for (let i = 0; i < segments.length - 1; i++) {
            console.log(segments[i]);
            let polygon = L.polygon([
                [segments[i].latitude, segments[i].longitude],
                [segments[i + 1].latitude, segments[i + 1].longitude]
                // [51.503, -0.06],
            ], {
                color: array[Math.floor(Math.random() * array.length)],
                fillColor: '#f03',
                fillOpacity: 1.0,
                weight: 8,
                radius: 500
            }).addTo(mymap);
            console.log(polygon);
            poly_segments.push(polygon);
        }
    }
    deleteFlight(wrapper) {
        //erase the flight icon
        wrapper.planeIconReference.remove();
        //remove from the list of flights
        const index = this.flightWrappers.indexOf(wrapper);
        if (index > -1) {
            this.flightWrappers.splice(index, 1);
        }
        //if it was pressed, it was unselected before already

    }

}
//let mymap = 









//takes plane object- TBD
function addPlaneToMap() {
    //add to Markers array
    //draw it in correct position
}

//important VARS
//pointer to the selected plane
//let selected = null;
//an example of segments of a flightplan
//segments = [{ "longitude": -0.08, "latitude": 51.509, }, { "longitude": -0.06, "latitude": 51.503, }, { "longitude": -0.00, "latitude": 53, }, { "longitude": 1, "latitude": 53, }]
//array of current drawn segments
//poly_segments = []

//our test plane marker
//let planeMarker = L.marker([51.5, -0.09], { icon: deselectedPlaneIcon, rotationAngle: 45 }).on('click', clickOnPlane).addTo(mymap);
//list of all plane markers
//let Markers = [planeMarker];



/*let planeIcon = L.Icon.extend({
    options: {
        shadowUrl: '../assets/blank-pic.png',
        rotationAngle: 90,
        iconSize: [84, 85], // size of the icon
        shadowSize: [50, 64], // size of the shadow
        iconAnchor: [45, 45], // point of the icon which will correspond to marker's location
        shadowAnchor: [4, 62],  // the same for the shadow
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    }
});*/

//let markersLayer = L.featureGroup([planeMarker])
    //.bindPopup('Hello world!')
    //.on('click', function () { alert('Clicked on a member of the group!'); })
    //.addTo(mymap);
//L.featureGroup().addTo(map);

// populate map from stops…

/*markersLayer.on("click", function (event) {
    // Use the event to find the clicked element
    var el = $(event.srcElement || event.target),
        id = el.attr('id');

    alert('Here is the markers ID: ' + id + '. Use it as you wish.')
    let clickedMarker = event.layer;
    console.log(typeof (clickedMarker));
    console.log(`event creator is ${clickedMarker},`);
    console.log(typeof (this));
    console.log(`event creator is ${this},`);
    console.log(typeof (this.options));
    console.log(`event creator is ${this.options},`);
    console.log(this.options.clickedMarker);
    var attributes = event.layer.properties;
    //console.log(attributes.name, attributes.desctiption, attributes.othervars);
    //console.log(L.stamp(event.target));
    console.log(L.stamp(event.sourceTarget));
    console.log(L.stamp(planeMarker));
    console.log(planeMarker);
    if (event.sourceTarget == selected) {
        console.log("yay");
    }
    // do some stuff…
    const PlaneIconDeselected = L.icon({
    iconUrl: '../assets/plane2.webp',
    shadowUrl: '../assets/blank-pic.png',

    iconSize: [84, 85], // size of the icon
    shadowSize: [50, 64], // size of the shadow
    iconAnchor: [42, 42], // point of the icon which will correspond to marker's location
    shadowAnchor: [4, 62],  // the same for the shadow
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});


const PlaneIconSelected = L.icon({
    iconUrl: '../assets/plane-selected.png',
    shadowUrl: '../assets/blank-pic.png',

    iconSize: [84, 85], // size of the icon
    shadowSize: [50, 64], // size of the shadow
    iconAnchor: [42, 42], // point of the icon which will correspond to marker's location
    shadowAnchor: [4, 62],  // the same for the shadow
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});
});*/
//order is long,lat 

//let popup = L.popup();



/*mymap.on('zoomend', function () {
    let currentZoom = mymap.getZoom();
    console.log(`current zoom ${currentZoom}`);
    //console.log(testPlane);
});*/
//mymap.animate = false;