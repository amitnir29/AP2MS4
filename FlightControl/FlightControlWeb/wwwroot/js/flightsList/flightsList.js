/**
 * This class handles the flights list.
 * */
class FlightsList {
    constructor(flightEventHandler) {
        //list of local flights
        this._localFlightsArray = [];
        //list of external flights
        this._externalFlightsArray = [];
        //dictionary of all flights, with FlightWrapper's id as keys and FlightWrapper as values
        this._allFlightsDict = {};
        //the local flights html table
        this._localFlightsHtml = document.getElementById("localFlights");
        //the external flights html table
        this._externalFlightsHtml = document.getElementById("externalFlights");
        //the flight info click event handler
        this._flightEventHandler = flightEventHandler;
        //id converter for the list rows
        this._idConverter = new FlightListRowIdConverter("list_");
        //styler for the rows
        this._rowStyler = new FlightListRowStyler();
    }

    //main handling function
    /**
     * update the flights data when receiving the new flights list.
     * @param {Array[FlightWrapper]} newLocalFlights an array of the new flights
     */
    updateFlights(newFlights) {
        const flightsDivisor = new FlightsDivisor();
        const twoFlightsObject = flightsDivisor.divideFlightsToTwoArrays(newFlights, flight => flight.is_external);
        const newLocalFlights = twoFlightsObject.falseFlightsArray;
        const newExternalFlights = twoFlightsObject.trueFlightsArray;
        //first remove gone flights: 
        this.removeGoneFlights(newLocalFlights, newExternalFlights);
        //now add the new flights:
        this.addNewFlights(newLocalFlights, newExternalFlights);
        //good. now set the current lists to be the new lists.
        this._localFlightsArray = newLocalFlights;
        this._externalFlightsArray = newExternalFlights;
    }

    //handle flights that should be removed
    /**
     * remove form current flights dictionary and from html tables all flights that are not in the new lists.
     * @param {Array[FlightWrapper]} newLocalFlightsArray an array of the new local flights
     * @param {Array[FlightWrapper]} newExternalFlightsArray an array of the external local flights
     */
    removeGoneFlights(newLocalFlightsArray, newExternalFlightsArray) {
        const newLocalFlightsIdSet = FlightWrapper.FlightsWrappersListToIdSet(newLocalFlightsArray);
        const newExternalFlightsIdSet = FlightWrapper.FlightsWrappersListToIdSet(newExternalFlightsArray);
        const currentLocalFlightsIdSet = FlightWrapper.FlightsWrappersListToIdSet(this._localFlightsArray);
        const currentExternalFlightsIdSet = FlightWrapper.FlightsWrappersListToIdSet(this._externalFlightsArray);
        //loop on all the current flights. 
        //find all the flight that should be removed because they are not in the new flights lists.
        for (const currentFlightWrapperId in this._allFlightsDict) {
            //if the current flight is not in the new lists
            if (!newLocalFlightsIdSet.has(currentFlightWrapperId) &&
                !newExternalFlightsIdSet.has(currentFlightWrapperId)) {
                //remove the flight from the html and the flights dictionary. find which list it is in
                if (currentLocalFlightsIdSet.has(currentFlightWrapperId)) {
                    this.removeFlightFromTables(currentFlightWrapperId);
                    delete this._allFlightsDict[currentFlightWrapperId];
                    this._flightEventHandler.flightRemoved(currentFlightWrapperId);
                } else if (currentExternalFlightsIdSet.has(currentFlightWrapperId)) {
                    this.removeFlightFromTables(currentFlightWrapperId);
                    delete this._allFlightsDict[currentFlightWrapperId];
                    this._flightEventHandler.flightRemoved(currentFlightWrapperId);
                } else {
                    console.error("at flightsList.removeGoneFlights, reached 'else' condition in loop");
                }
            }
        }
    }

    /**
     * remove a single flight from a given html table
     * @param {string} flightId the id of the flight wrapper to remove
     */
    removeFlightFromTables(flightId) {
        const row = document.getElementById(this._idConverter.flightWrapperIdToListRowId(flightId));
        row.parentNode.removeChild(row);
    }

    //handle flights that should be added
    /**
     * add to html and flights dictionary the new flights that are not in current page.
     * @param {Array[FlightWrapper]} newLocalFlightsArray an array of the new local flights
     * @param {Array[FlightWrapper]} newExternalFlightsArray an array of the external local flights
     */
    addNewFlights(newLocalFlightsArray, newExternalFlightsArray) {
        //first loop on local flights
        for (const newLocalFlight of newLocalFlightsArray) {
            //if the new flight is not in the dictionary, meaning it is new
            if (!(newLocalFlight.id in this._allFlightsDict)) {
                //add it to the html
                this.addTolocalFlightsHtml(newLocalFlight);
            } else {
                this.updateFlightHtml(newLocalFlight);
            }
            //add to the dictionary if new, update if existed
            this._allFlightsDict[newLocalFlight.id] = newLocalFlight;
        }
        //now loop in external flights
        for (const newExternalFlight of newExternalFlightsArray) {
            //if the new flight is not in the dictionary, meaning it is new
            if (!(newExternalFlight.id in this._allFlightsDict)) {
                //add it to the html
                this.addToExternalFlightsHtml(newExternalFlight);
            } else {
                this.updateFlightHtml(newExternalFlight);
            }
            //add to the dictionary if new, update if existed
            this._allFlightsDict[newExternalFlight.id] = newExternalFlight;
        }
    }

    /**
     * convert the input flight into a string to show in the flights list.
     * @param {FlightWrapper} flight the flight to convert.
     * @returns {string}
     */
    flightWrapperDataToShow(flight) {
        let s = "";
        s += "fight id: " + flight.id;
        s += ", company: " + flight.flightDetails.company_name;
        return s;
    }

    /**
     * update the cell data
     * @param {FlightWrapper} flight
     */
    updateFlightHtml(flight) {
        const flightRow = document.getElementById(this._idConverter.flightWrapperIdToListRowId(flight.id));
        const flightData = flightRow.cells[0];
        flightData.innerHTML = this.flightWrapperDataToShow(flight);
    }

    /**
     * add the input flight to the local flights html
     * @param {FlightWrapper} flight the flight wrapper to add to the html
     */
    addTolocalFlightsHtml(flight) {
        //add a new row
        const newRow = this._localFlightsHtml.insertRow(-1);
        //set the row to unpressed
        this._rowStyler.makeUnpressed(newRow);
        //set the row's id to the flight's is
        newRow.setAttribute("id", this._idConverter.flightWrapperIdToListRowId(flight.id));
        newRow.classList.add("flight-row");
        //add the cell that displays the flight info
        const rowData = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowData.onclick = e => this.callFlightOnClick(this.clickEventToFlightWrapperId(e));
        //add text for the cell
        rowData.innerHTML = this.flightWrapperDataToShow(flight);
        //set the css styling
        rowData.classList.add("local-flight-data");
        rowData.classList.add("flight-row-data");
        //add the cell that deletes the flight
        const rowDeleteButton = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowDeleteButton.onclick = e => this.callFlightDeleteEvent(flight.id);
        //add delete image
        //rowDeleteButton.innerHTML = "DELETE";
        let deleteImage = document.createElement("img");
        deleteImage.src = "assets/red-x.png";
        deleteImage.classList.add("red-x");
        rowDeleteButton.appendChild(deleteImage);
        //set the css styling
        rowDeleteButton.classList.add("flight-delete");
    }

    /**
     * add the input flight to the external flights html
     * @param {FlightWrapper} flight the flight wrapper to add to the html
     */
    addToExternalFlightsHtml(flight) {
        //add a new row
        const newRow = this._externalFlightsHtml.insertRow(-1);
        //set the row to unpressed
        this._rowStyler.makeUnpressed(newRow);
        //set the row's id to the flight's is
        newRow.setAttribute("id", this._idConverter.flightWrapperIdToListRowId(flight.id));
        newRow.classList.add("flight-row");
        //add the cell that displays the flight info
        const rowData = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowData.onclick = e => this.callFlightOnClick(this.clickEventToFlightWrapperId(e));
        //set the css styling
        rowData.classList.add("flight-row-data");
        //add text for the cell
        rowData.innerHTML = this.flightWrapperDataToShow(flight);
    }

    //handle calling of click events
    /**
     * gets a click event and returns the id of the flight wrapper that belongs to the clicked row
     * @param {any} event
     */
    clickEventToFlightWrapperId(event) {
        return this._idConverter.listRowIdToFlightWrapperId(event.target.parentNode.id)
    }

    /**
     * call the event handler for "on" event
     * @param {string} elementId the id of the pressed row, which is also the id of the matching flight wrapper
     */
    callFlightOnClick(elementId) {
        this._flightEventHandler.showPressedFlight(this._allFlightsDict[elementId]);
    }

    /**
     * call the event handler for "delete" event
     * @param {string} elementId the id of the pressed row, which is also the id of the matching flight wrapper
     */
    callFlightDeleteEvent(elementId) {
        this._flightEventHandler.deleteFlight(this._allFlightsDict[elementId]);
    }

    //handle action of click events
    /**
     * handle event of pressed flight. make the flight's row pressed.
     * @param {FlightWrapper} flight
     */
    showPressedFlight(flight) {
        const flightRow = document.getElementById(this._idConverter.flightWrapperIdToListRowId(flight.id));
        this._rowStyler.makePressed(flightRow);
    }

    /**
     * handle event of unpressed flight. make the flight's row unpressed.
     * @param {FlightWrapper} flight
     */
    hidePressedFlight(flight) {
        const flightRow = document.getElementById(this._idConverter.flightWrapperIdToListRowId(flight.id));
        this._rowStyler.makeUnpressed(flightRow);
    }

    /**
     * handle event of deleted flight. delete the flight's row.
     * @param {FlightWrapper} flight
     */
    deleteFlight(flight) {
        let id = (' ' + flight.id).slice(1);
        $.ajax({
            url: "api/Flights/" + id,
            type: 'DELETE', //send it through get method

            success: function (response) {
                //return JSON.parse(data);

            },
            error: function (xhr) {
                ErrorHandler.showError("Couldn't delete flight " + id + " from the server!");
            }

        });
        this.removeFlightFromTables(flight.id);
        delete this._allFlightsDict[flight.id];
    }

}
