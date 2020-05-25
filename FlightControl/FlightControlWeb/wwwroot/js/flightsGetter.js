class FlightsGetter {

    /**
     * gets a flights array as json from the server, converts it to an array of FlightWrappers.
     * @returns {Array[FlightWrapper]}
     * */
    getFlights() {
        console.error("still haven't implemented the actual communication with the server"); //TODO
        let flightsArray; //should be const and get the return value from the server
        const flightWrappersArray = [];
        for (flight of flightsArray) {
            flightWrappersArray.push(new FlightWrapper(flight));
        }
        return flightWrappersArray;
    }
}