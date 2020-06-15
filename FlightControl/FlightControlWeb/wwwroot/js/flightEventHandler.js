class FlightEventHandler {
    constructor() {
        this._flightList = null;
        this._map = null;
        this._flightDetails = null;
        this._currentPressedFlight = null;
    }

    set flightsList(newFlightList) {
        this._flightList = newFlightList;
    }
    set map(newMap) {
        this._map = newMap;
    }
    set flightDetails(newFlightDetails) {
        this._flightDetails = newFlightDetails;
    }

    /**
     * get the flight plan that matches the input id from the server
     * @param {string} flightId the id of the flight plan to get
     */
    async getFlightPlan(flightId) {
        let flightPlan;
        await $.ajax({
            url: "api/FlightPlan/" + flightId,
            type: "get", //send it through get method
            success: function (data) {
                flightPlan = data;
            },
            error: function (xhr) {
                ErrorHandler.showError("Couldn't get flightplan of " + flightId);
            }

        });
        return flightPlan;
    }

    /**
     * notify the program parts to show the pressed flight.
     * don't do anything if the new pressed flight is the flight which is already pressed
     * @param {FlightWrapper} flightWrapper the flight to show
     */
    async showPressedFlight(flightWrapper) {
        //if this specific flight is already pressed, do nothing.
        if (this._currentPressedFlight === null || flightWrapper.id !== this._currentPressedFlight.id) {
            //first make sure nothing else is pressed
            if (this._currentPressedFlight !== null) {
                this.hidePressedFlight(this._currentPressedFlight);
            }
            //now set the new pressed
            this._currentPressedFlight = flightWrapper;
            this._flightList.showPressedFlight(flightWrapper);
            const flightPlan = await this.getFlightPlan(flightWrapper.id);
            this._map.showPressedFlight(flightWrapper, flightPlan);
            this._flightDetails.showPressedFlight(flightPlan);
        }
    }


    /**
     * notify the program parts to hide the pressed flight
     * don't do anything if there is no pressed flight now anyway
     * @param {FlightWrapper} flightWrapper the flight to hide
     */
    hidePressedFlight(flightWrapper) {
        //if no flight is pressed, do nothing
        if (this._currentPressedFlight !== null) {
            this._currentPressedFlight = null;
            this._flightList.hidePressedFlight(flightWrapper);
            this._map.hidePressedFlight();
            this._flightDetails.hidePressedFlight();
        }
    }


    /**
     * notify the program parts to delete a flight.
     * if the deleted flight is pressed, set the current pressed to null,
     * the delete functions of the program parts should handle the rest.
     * if the deleted flight is not pressed, just delete it.
     * @param {FlightWrapper} flightWrapper the flight to hide
     */
    deleteFlight(flightWrapper) {
        if (this._currentPressedFlight !== null && flightWrapper.id === this._currentPressedFlight.id) {
            this._currentPressedFlight = null;
            this._flightDetails.hidePressedFlight();
            this._map.hidePressedFlight();
        }
        this._flightList.deleteFlight(flightWrapper);
        this._map.deleteFlight(flightWrapper);
    }

    /**
     * handle remove of flight
     * @param {string} flightWrapperId
     */
    flightRemoved(flightWrapperId) {
        if (flightWrapperId === null) {
            console.error("tried to remove a null value of flight!");

        }
        else if (this._currentPressedFlight !== null && this._currentPressedFlight.id === flightWrapperId) {
            this._flightDetails.hidePressedFlight();
            this._map.hidePressedFlight();
            this._currentPressedFlight = null;
        }
    }



}