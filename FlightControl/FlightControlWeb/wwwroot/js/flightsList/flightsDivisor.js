class FlightsDivisor {

    divideFlightsToTwoArrays(flightsArray, divideFunction) {
        const trueFlights = [];
        const falseFlights = [];

        //return { trueFlightsArray: [], falseFlightsArray: flightsArray };

        for (const flight of flightsArray) {
            if (divideFunction(flight)) {
                trueFlights.push(flight);
            } else {
                falseFlights.push(flight);
            }
        }
        return { trueFlightsArray: trueFlights, falseFlightsArray: falseFlights };
    }

}