class FlightsDivisor {

    divideFlightsToTwoArrays(flightsArray, divideFunction) {
        const trueFlights = [];
        const falseFlights = [];
        for (flight of flightsArray) {
            if (divideFunction(flight)) {
                trueFlights.push(flight);
            } else {
                falseFlights.push(flight);
            }
        }
        return { trueFlightsArray: trueFlights, falseFlightsArray: falseFlights };
    }

}