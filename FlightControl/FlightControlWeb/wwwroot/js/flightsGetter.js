//var dateFormat = require('dateformat');
class FlightsGetter {

    
    constructor() {
        //dictionary from flight id to flight wrappers. saves the flights of previous iteration.
        this._prevFlights = {};
    }
    

    /**
     * gets a flights array as json from the server, converts it to an array of FlightWrappers.
     * @returns {Array[FlightWrapper]}
     * */
    async getFlights() {
        let now = new Date();
        let formatted = now.toISOString().split(".")[0] + "Z";
        
        let flightsArray;
        const flightWrappersArray = [];
        let getPlaneIconFunc = this.getPlaneIcon;

        await $.ajax({
            url: "api/Flights/" + "?relative_to=" + formatted + "&sync_all",
            type: "get", //send it through get method
            //dataType: 'json',

            success: function (data) {
                //parse the json to this variable.
                flightsArray = data;
            },
            error: function (xhr) {
                console.log(xhr);//TODO
                ErrorHandler.showError("Couldn't get flights from the server");
            }

        });
        for (let flight of flightsArray) {
            const planeIcon = this.getPlaneIcon(flight.flight_id);
            flightWrappersArray.push(new FlightWrapper(flight, planeIcon));
        }
        this.setNewFlightsDict(flightWrappersArray);
        return flightWrappersArray;

    }

    

    /**
     * return the current saved plane icon of this flight from the dict of previous flights.
     * return null if this flight is not in previous flights dict.
     * @param {string} id of the flight to get its plane icon.
     */
    
    getPlaneIcon(id) {
        if (id in this._prevFlights) {
            return this._prevFlights[id].planeIconReference;
        }
        return null;
    }
    
    
    /**
     * get an array of the new flight wrappers and set the dictionary
     * @param {Array[FlightWrapper]} flightWrappersArray
     */
    
    setNewFlightsDict(flightWrappersArray) {
        //empty the dict.
        this._prevFlights = {};
        //now fill with new flights
        for (const flightWrapper of flightWrappersArray) {
            this._prevFlights[flightWrapper.id] = flightWrapper;
        }
    }
    
    
}