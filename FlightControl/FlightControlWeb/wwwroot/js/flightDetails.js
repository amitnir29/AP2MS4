class FlightDetails {


    constructor() {
        this._flightDetailsText = document.getElementById("flightDetailsText"); //TODO change to the actual panel.
    }

    /**
     * converts flight plan to the details to show
     * @param {Object} flightPlan the flight plan to convert
     * @returns {string} the flight plan as string
     */
    flightPlanDataToShow(flightPlan) {
        let data = "";
        //TODO decide how to write the flight plan
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