//var dateFormat = require('dateformat');
class FlightsGetter {

    /**
     * gets a flights array as json from the server, converts it to an array of FlightWrappers.
     * @returns {Array[FlightWrapper]}
     * */
    getFlights() {
        //+ new Date().getTime()
        let now = new Date();
        now.setUTCSeconds(Date.now);
        //dateFormat(now,"yyyy-MM-ddTHH:mm:ssZ");
        var formatted = $.datepicker.formatDate("yyyy-MM-ddTHH:mm:ssZ", now);

        console.error("still haven't implemented the actual communication with the server"); //TODO server request
        let flightsArray; //should be const and get the return value from the server
        //TODO server request
        $.ajax({
            url: "api/Flights/" + "?relative_to=UTC" + formatted + "&sync_all",
            type: "get", //send it through get method

            success: function (data) {
                flightsArray= JSON.parse(data);
            },
            error: function (xhr) {
                //TODO - pretty alert
            }

        });
        /*code for post request TODO add to json adder
        $.ajax({
            url: "api/FlighPlan",
            type: "post", //send it through get method
            data://the json file
            ,success: function (data) {
                flightsArray = JSON.parse(data);
            },
            error: function (xhr) {
                //TODO - pretty alert
            }

        });*/
        const flightWrappersArray = [];
        for (flight of flightsArray) {
            flightWrappersArray.push(new FlightWrapper(flight));
        }
        return flightWrappersArray;
    }
}