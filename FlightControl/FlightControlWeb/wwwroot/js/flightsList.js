/**
 * This class handles the flights list.
 * Flights ids match their table's row's id.
 * */
class flightsList {
    constructor() {
        //list of local flights
        this.localFlightsArray = null;
        //list of external flights
        this.externalFlightsArray = null;
        //dictionary of all flights, with id as keys and FlightWrapper as values
        this.allFlightsDict = {};
        //the local flights html table
        this.localFlightsHtml = document.getElementById("localFlights");
        //the external flights html table
        this.externalFlightsHtml = document.getElementById("externalFlights");
    }

    /**
     * update the flights lists when receiving the new lists.
     * @param {Array[FlightWrapper]} newLocalFlights an array of the new local flights
     * @param {Array[FlightWrapper]} newExternalFlights an array of the external local flights
     */
    updateLists(newLocalFlights, newExternalFlights) {
        const newLocalFlightsIdSet = this.FlightsWrappersListToIdSet(newLocalFlights);
        const newExternalFlightsIdSet = this.FlightsWrappersListToIdSet(newExternalFlights);
        //first remove gone flights: 
        this.removeGoneFlights(newLocalFlightsIdSet, newExternalFlightsIdSet);
        //now add the new flights:
        this.addNewFlights(newExternalFlightsIdSet, newExternalFlightsIdSet, newLocalFlights, newExternalFlights);
        //good. now set the current lists to be the new lists.
        this.localFlightsArray = newLocalFlights;
        this.externalFlightsArray = newExternalFlights;
    }

    /**
     * convert a FlightWrapper array into a set of the FlightWrappers' ids.
     * @param {Array[FlightWrapper]} flightWrapperArray
     * @returns {Set[string]}
     */
    FlightsWrappersListToIdSet(flightWrapperArray) {
        const idSet = new Set();
        //for each flightWrapper, add its id to the set
        for (const flightWrapper of flightWrapperArray) {
            idSet.add(flightWrapper.id); //TODO check if this is the way to access id
        }
        return idSet;
    }

    /**
     * remove form current flights dictionary and from html tables all flights that are not in the new lists.
     * @param {Set[string]} newLocalFlightsIdSet the new local flights set of FlightWrappers ids.
     * @param {Set[string]} newExternalFlightsIdSet the new external flights set of FlightWrappers ids.
     */
    removeGoneFlights(newLocalFlightsIdSet, newExternalFlightsIdSet) {
        const currentLocalFlightsIdSet = this.FlightsWrappersListToIdSet(this.localFlightsArray);
        const currentExternalFlightsIdSet = this.FlightsWrappersListToIdSet(this.externalFlightsArray);
        //loop on all the current flights. 
        //find all the flight that should be removed because they are not in the new flights lists.
        for (const currentFlightWrapperId in this.allFlightsDict) {
            //if the current flight is not in the new lists
            if (!(currentFlightWrapperId in newLocalFlightsIdSet) &&
                !(currentFlightWrapperId in newExternalFlightsIdSet)) {
                //remove the flight from the html and the flights dictionary. find which list it is in
                if (currentFlightWrapperId in currentLocalFlightsIdSet) {
                    this.removeFlightFromTable(currentFlightWrapperId, this.localFlightsHtml);
                    delete this.allFlightsDict[currentFlightWrapperId];
                } else if (currentFlightWrapperId in currentExternalFlightsIdSet) {
                    this.removeFlightFromTable(currentFlightWrapperId, this.externalFlightsHtml);
                    delete this.allFlightsDict[currentFlightWrapperId];
                } else {
                    //TODO error
                }
            }
        }
    }

    /**
     * remove a single flight from a given html table
     * @param {string} flightId the id of the flight to remove
     */
    removeFlightFromTables(flightId) {
        const row = document.getElementById(flightId);
        row.parentNode.removeChild(row);
    }

    /**
     * add to html and flights dictionary the new flights that are not in current page.
     * @param {Set[string]} newLocalFlightsIdSet the new local flights set of FlightWrappers ids.
     * @param {Set[string]} newExternalFlightsIdSet the new external flights set of FlightWrappers ids.
     * @param {Array[FlightWrapper]} newLocalFlightsArray an array of the new local flights
     * @param {Array[FlightWrapper]} newExternalFlightsArray an array of the external local flights
     */
    addNewFlights(newLocalFlightsIdSet, newExternalFlightsIdSet, newLocalFlightsArray, newExternalFlightsArray) {
        //first loop on local flights
        for (const newLocalFlightId of newLocalFlightsIdSet) {
            //if this id is not in the current flights dictionary
            if (!(newLocalFlightId in this.allFlightsDict)) {
                //add it to the dictionary and the html
                flightWrapperOfThisId = newLocalFlightsArray.find(item => item.id === newLocalFlightId);
                if (flightWrapperOfThisId) {
                    this.allFlightsDict[newLocalFlightId] = flightWrapperOfThisId;
                    this.addTolocalFlightsHtml(flightWrapperOfThisId);
                } else {
                    //TODO error
                }
            }
        }
        //now loop in external flights
        for (const newExternalFlightId of newExternalFlightsIdSet) {
            //if this id is not in the current flights dictionary
            if (!(newExternalFlightId in this.allFlightsDict)) {
                //add it to the dictionary and the html
                flightWrapperOfThisId = newExternalFlightsArray.find(item => item.id === newExternalFlightId);
                if (flightWrapperOfThisId) {
                    this.allFlightsDict[newExternalFlightId] = flightWrapperOfThisId;
                    this.addTolocalFlightsHtml(flightWrapperOfThisId);
                } else {
                    //TODO error
                }
            }
        }
    }

    /**
     * convert the input flight into a string to show in the flights list.
     * @param {FlightWrapper} flight the flight to convert.
     * @returns {string}
     */
    flightWrapperDataToShow(flight) {
        //TODO
    }

    /**
     * add the input flight to the local flights html
     * @param {FlightWrapper} flight the flight to add to the html
     */
    addTolocalFlightsHtml(flight) {
        //add a new row
        const newRow = this.localFlightsHtml.insertRow(-1);
        //set the row's id to the flight's is
        newRow.setAttribute("id", flight.id);//TODO make sure we get the id this way
        //add the cell that displays the flight info
        const rowData = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowData.setAttribute("onclick", "check()") //TODO change to call for eOn
        //add text for the cell
        rowData.innerHTML = this.flightWrapperDataToShow(flight);
        //add the cell that deletes the flight
        const rowDeleteButton = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowDeleteButton.setAttribute("onclick", "check2()") //TODO change to call for delete flight
        //add delete image
        rowDeleteButton.innerHTML = "DELETE";//TODO change to image
    }

    /**
     * add the input flight to the external flights html
     * @param {FlightWrapper} flight the flight to add to the html
     */
    addToExternalFlightsHtml(flight) {
        //add a new row
        const newRow = this.externalFlightsHtml.insertRow(-1);
        //set the row's id to the flight's is
        newRow.setAttribute("id", flight.id);//TODO make sure we get the id this way
        //add the cell that displays the flight info
        const rowData = newRow.insertCell(-1);
        //add function call onclick to show this flight
        rowData.setAttribute("onclick", "check()") //TODO change to call for eOn
        //add text for the cell
        rowData.innerHTML = this.flightWrapperDataToShow(flight);
    }


}


function checkL() {
    console.log("local");
}

function checkE() {
    console.log("external");
}

function check2() {
    console.log("delete");
}
let x = 1;
let l = 1;
let e = 1;

function aLoc() {
    //add a new row
    const newRow = document.getElementById("localFlights").insertRow(-1);
    //set the row's id to the flight's is
    newRow.setAttribute("id", x.toString());//TODO make sure we get the id this way
    x++;
    //add the cell that displays the flight info
    const rowData = newRow.insertCell(-1);
    //add function call onclick to show this flight
    rowData.setAttribute("onclick", "checkL()") //TODO change to call for eOn
    //add text for the cell
    rowData.innerHTML = "local" + l;
    l++;
    //add the cell that deletes the flight
    const rowDeleteButton = newRow.insertCell(-1);
    //add function call onclick to show this flight
    rowDeleteButton.setAttribute("onclick", "check2()") //TODO change to call for delete flight
    //add delete image
    rowDeleteButton.innerHTML = "DELETE";//TODO change to image
}

function aExt() {
    //add a new row
    const newRow = document.getElementById("externalFlights").insertRow(-1);
    //set the row's id to the flight's is
    newRow.setAttribute("id", x.toString());//TODO make sure we get the id this way
    x++;
    //add the cell that displays the flight info
    const rowData = newRow.insertCell(-1);
    //add function call onclick to show this flight
    rowData.setAttribute("onclick", "checkE()") //TODO change to call for eOn
    //add text for the cell
    rowData.innerHTML = "external" + e;
    e++;
}

function rrLoc() {
    const index = parseInt(Math.random() * (document.getElementById("localFlights").rows.length - 1)) + 1;
    if (index !== 0) {
        document.getElementById("localFlights").deleteRow(index);
    }
}

function rrExt() {
    const index = parseInt(Math.random() * (document.getElementById("externalFlights").rows.length - 1)) + 1;
    if (index !== 0) {
        document.getElementById("externalFlights").deleteRow(index);
    }
}

function rlLoc() {
    document.getElementById("localFlights").deleteRow(document.getElementById("localFlights").rows.length - 1);
}

function rlExt() {
    document.getElementById("externalFlights").deleteRow(document.getElementById("externalFlights").rows.length - 1);
}