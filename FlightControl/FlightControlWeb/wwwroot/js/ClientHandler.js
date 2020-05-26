class ClientHandler {
    constructor() {
        this._flightsGetter = new FlightsGetter();
    }

    /**
     * 
     * @param {any} flightsList
     * @param {any} map
     * @param {any} flightsDetails
     */
    clientIteration(flightsList, map) {
        newFlightsArray = this._flightsGetter.getFlights();
        flightsList.updateFlights(newFlightsArray);
        //TODO call the function of the map
    }
}