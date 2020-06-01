//var dateFormat = require('dateformat');
class FlightsGetter {

    /**
     * gets a flights array as json from the server, converts it to an array of FlightWrappers.
     * @returns {Array[FlightWrapper]}
     * */
    async getFlights() {
        //+ new Date().getTime()
        let now = new Date();
        //now.setUTCSeconds(Date.now);
        //dateFormat(now,"yyyy-MM-ddTHH:mm:ssZ");
        //var formatted = $.datepicker.formatDate("yyyy-MM-ddTHH:mm:ssZ", now);
        let formatted = now.toISOString().split(".")[0] + "Z";

        //TODO server request
        let flightsArray; //should be const and get the return value from the server
        const flightWrappersArray = [];
        console.log("sending GET of " + "api/Flights/" + "?relative_to=" + formatted + "&sync_all");
        //TODO server request

            await $.ajax({
                url: "api/Flights/" + "?relative_to=" + formatted + "&sync_all",
                type: "get", //send it through get method
                //dataType: 'json',

                success: function (data) {
                    // do other actions
                    //TODO here we should get the JSON
                    flightsArray = data;
                    console.log("data", data);
                    ErrorHandler.showError("Hello there general kenobi");
                    for (let flight of flightsArray) {
                        flightWrappersArray.push(new FlightWrapper(flight));
                    }
                },
                error: function (xhr) {
                    console.log("failed get");
                    console.log(xhr);
                    //TODO - pretty alert
                }

            });
            return flightWrappersArray;
        
        /*code for post request TODO add to json adder
       */
        
    }
}