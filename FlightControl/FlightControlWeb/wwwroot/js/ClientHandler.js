class ClientHandler {
    constructor() {
        this._flightsGetter = new FlightsGetter();
        //get all the necessary scripts here, instead of in index.html
        //$.getScript("js/flightsList/flightListRowIdConverter.js");
        /*var newScript = document.createElement('script');
        newScript.type = 'text/javascript';
        newScript.src = 'js/flightsList/flightListRowIdConverter.js';
        document.getElementsByTagName('head')[0].appendChild(newScript);*/

    }

    startClient() {
        //milliseconds between each iteration
        const intervalLength = 1500;
        const secondCallDelay = 250;
        //init the flightEventHandler
        const flightEventHandler = new FlightEventHandler();
        //init the screen parts
        const flightsList = new FlightsList(flightEventHandler);
        const map = new Map(flightEventHandler);
        const flightDetails = new FlightDetails(flightEventHandler);
        //set the flight event handler's references to screen parts
        flightEventHandler.flightsList = flightsList;
        flightEventHandler.map = map;
        flightEventHandler.flightDetails = flightDetails;
        /*
         * now we do not want to first planes to be out of angle, 
         * so the time difference between the first and second should be small
         */
        //call the first time
        this.clientIteration(flightsList, map);
        //call the others with small delay at start
        setTimeout(() => setInterval(() => {
            this.clientIteration(flightsList, map);
        }, intervalLength), secondCallDelay);
    }

    /**
     * 
     * @param {FlightsList} flightsList
     * @param {Map} map
     */
    clientIteration(flightsList, map) {
        newFlightsArray = this._flightsGetter.getFlights();
        flightsList.updateFlights(newFlightsArray);
        //TODO call the function of the map
    }
}