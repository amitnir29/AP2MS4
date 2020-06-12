class FlightDetails {


    constructor(flightEventHandler) {
        this._flightDetailsText = document.getElementById("flightDetailsText");
        //the flight info click event handler
        this._flightEventHandler = flightEventHandler;
    }

    /**
     * converts flight plan to the details to show
     * @param {Object} flightPlan the flight plan to convert
     * @returns {string} the flight plan as string
     */
    flightPlanDataToShow(flightPlan) {
        let data = "";
        //add number of passengers
        data += "#passengers: " + flightPlan.passengers;
        //add company name
        data += ", company: " + flightPlan.company_name;
        //add initial location
        data += ", from (lon,lat) (" + flightPlan.initial_location.longitude
            + "," + flightPlan.initial_location.latitude + ")";
        //add finish location
        data += " to (" + flightPlan.segments[flightPlan.segments.length - 1].longitude + ","
            + flightPlan.segments[flightPlan.segments.length - 1].latitude + ")";
        //add start time
        data += ", start time: " + flightPlan.initial_location.date_time;
        //calculate finish time
        let secondsCounter = 0;
        for (let i = 0; i < flightPlan.segments.length; i++) {
            secondsCounter += flightPlan.segments[i].timespan_seconds;
        }
        //add finish time
        const initalTimeDate = new Date(flightPlan.initial_location.date_time);
        const finishTime = new Date(initalTimeDate.getTime() + 1000 * secondsCounter);
        data += ", finish time: " + finishTime.toISOString().split(".")[0] + "Z";
        return data;
    }

    /**
     * show the flight plan of the pressed flight
     * @param {Object} flightPlan the flight plan of the flight to show
     */
    showPressedFlight(flightPlan) {
        this._flightDetailsText.textContent = this.flightPlanDataToShow(flightPlan);
    }

    hidePressedFlight() {
        this._flightDetailsText.textContent = "";
    }


}