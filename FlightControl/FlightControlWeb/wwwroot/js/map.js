
const xMiddleRatio = 0.568;
const yMiddleRatio = 0.417;

let planeGenericIcon = L.Icon.extend({
    options: {
        shadowUrl: '../assets/blank-pic.png',

        iconSize: [70, 70], // size of the icon
        shadowSize: [50, 64], // size of the shadow
       
        iconAnchor: [35, 35], // point of the icon which will correspond to marker's location
        shadowAnchor: [4, 62],  // the same for the shadow
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    }
});

const deselectedPlaneIcon = new planeGenericIcon({ iconUrl: '../assets/plane2.webp' }),
    selectedPlaneIcon = new planeGenericIcon({ iconUrl: '../assets/plane-selected.png' });

class Map {
    constructor(flightEventHandler) {
        this.flightEventHandler = flightEventHandler;
        //initialize map and its fields
        this.mymap = L.map('mapid').setView([51.505, -0.09], 2);
        L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoidW5kdmlrIiwiYSI6ImNrYWg3amZiNDBkcjcyeW81d3JibHRyaTgifQ.67Dwm_vni6DbznyF0bB1ZA', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1,
            accessToken: 'your.mapbox.access.token'
        }).addTo(this.mymap);
        this.mymap.on('click', this.onMapClick,this);
        this.flightWrappers = [];
        this.polySegments = [];
        this.selected = null;
        this.selectedId = -1;
        this.locations = {};
        this.colors = [ 'black']
    }
    /**
     * updates the map with a new array of flights
     * @param {Array[FlightWrapper]} flightWrappers
     */
    updateFlights(flightWrappers) {
        let flightWrappers2 = [...flightWrappers];
        //erase the previous icons
        for (let wrapper of this.flightWrappers) {
            wrapper.planeIconReference.remove();
        }
        let flag = true;
        //erase the current segments and flight details, only if the plane didn't disspear
        for (let newWrapper of this.flightWrappers) {
            if (newWrapper.id === this.selectedId) {
                flag = false;
            }
        }
        //if didn't find the plane
        if (flag) {
            this.removeSegmentsPoly();
        }

        //update with the new flights
        this.flightWrappers = flightWrappers2;
        //draw new icons and link them to the wrappers
        for (let wrapper of this.flightWrappers) {
            //calculate the angle of the plane, the slope is log-log/lat-lat
            
            let long = wrapper.flightDetails.longitude;
            let lat = wrapper.flightDetails.latitude;
            //if this is the selected one, keep its texture selected
            let icon = deselectedPlaneIcon;
            if (wrapper.id === this.selectedId) {
                icon = selectedPlaneIcon;
            }
            let angle = this.calculateAngle(wrapper);
            let planeMarker = L.marker([lat, long], { icon: icon, rotationAngle: angle, rotationOrigin: "center" });
            planeMarker.on('click', this.onClickPlane,this)
            planeMarker.addTo(this.mymap);
            wrapper.planeIconReference = planeMarker;
            let cords = {
                "latitude": lat,
                "longitude": long,
            }

            this.locations[wrapper.id] = cords;
        }
    }

    /**
     * event handler for clicking on an empty space in the map, deselectes the plane
     * @param {Object} event
     */
    onMapClick(event) {
        if (this.selected !== null) {
            this.flightEventHandler.hidePressedFlight(this.selected);
        }
    }
    /**
     * deselects the current selected plane
     * */
    deselectPlane() {
        try {
            //use the deselected texture
            let wrap = this.getWrapper(this.selected);
            wrap.planeIconReference.setIcon(deselectedPlaneIcon);
            //erase the lines
            this.removeSegmentsPoly(this.polySegments);
        } catch (e) {
            console.log(e);
        }
    }
    /**
     * removes the polygons on the map that represent the flight plan
     * */
    removeSegmentsPoly() {
        for (let poly of this.polySegments) {
            poly.remove();
        }
        this.polySegments = []
    }
    //function of event clicking on plane
    /**
     * event handler for clicking on a plane
     * @param {any} e
     */
    onClickPlane(e) {
        //ErrorHandler.showError("Wasn't able to get flightplan");
        //if we clicked on the same plane, do nothing
        if (this.selected !== e.sourceTarget) {
            let wrap = this.getWrapper(e.sourceTarget);
            this.flightEventHandler.showPressedFlight(wrap);
        }
    }
    /**
     * finds a wrapper that has the same pointer as an object
     * @param {Object} object
     */
    getWrapper(object) {
        //first try comparing by id
        if ("id" in object) {
            for (let wrapper of this.flightWrappers) {
                if (object.id === wrapper.id)
                    return wrapper;
            }
        }
        //if id doesn't exist, compare by pointers
        for (let wrapper of this.flightWrappers) {
            if (object === wrapper.planeIconReference)
                return wrapper;
        }
        return null;
    }
    /**
     * calculates the angle to draw the plane of a flight
     * @param {FlightWrapper} wrapper of the flight
     * @returns {Number} angle of the plane
     */
    calculateAngle(wrapper) {
        //default val for first run
        let angle = 45;
        let idkey = wrapper.id;
        //if we have previous location, calculate the angle
        if (idkey in this.locations) {
            const y = (wrapper.flightDetails.longitude - this.locations[idkey].longitude);
            const x = (wrapper.flightDetails.latitude - this.locations[idkey].latitude);
            let theta = Math.atan2(y, x);
            //the default picture is in 45 degrees angle
            angle = (theta * 180 / Math.PI) - 45;
        }
        return angle;
    }
    /**
     * makes a plane be selected on the map
     * @param {FlightWrapper} wrapper
     * @param {any} plan
     */
    showPressedFlight(wrapper, plan) {
        //save the new selected
        this.selected = wrapper;
        this.selectedId = wrapper.id;
        //remove the last iteration polygons
        this.removeSegmentsPoly(this.polySegments);
        //change the icon to be a selected one
        wrapper.planeIconReference.setIcon(selectedPlaneIcon);
        this.drawSegments(plan, this.polySegments);
    }
    /**
     * hides the current selected flight
     * */
    hidePressedFlight() {
        this.deselectPlane();
        this.selected = null;
        this.selectedId = -1;
    }
    /**
     * draws the flight plan of a flight onto the map
     * @param {FlightPlan} plan of a flight
     * @param {Array} poly_segments an array to store the polygons of the segments
     */
    drawSegments(plan, poly_segments) {
        let i;
        let segments = plan.segments;
        //loop over the rest of the segments
        for (i = -1; i < segments.length-1 ; i++) {
            try {
                let start;
                let end;
                if (i === -1) {
                    start = [plan.initial_location.latitude, plan.initial_location.longitude];
                    end = [segments[0].latitude, segments[0].longitude];
                } else {
                    start = [segments[i].latitude, segments[i].longitude];
                    end = [segments[i + 1].latitude, segments[i + 1].longitude];
                }
                let polygon = L.polygon([
                    start,
                    end
                ], {
                    color: this.colors[Math.floor(Math.random() * this.colors.length)],
                    fillColor: '#f03',
                    fillOpacity: 1.0,
                    weight: 8,
                    radius: 500
                }).addTo(this.mymap);
                poly_segments.push(polygon);
            } catch (e) {
                console.log(e);
            }
           
        }
    }
    /**
     * deletes a flight from the map
     * @param {FlightWrapper} wrapper of the flight to delete
     */
    deleteFlight(wrapper) {
        //erase the flight icon
        wrapper.planeIconReference.remove();
        //remove from the list of flights
        const index = this.flightWrappers.indexOf(wrapper);
        if (index > -1) {
            this.flightWrappers.splice(index, 1);
        }

    }
}