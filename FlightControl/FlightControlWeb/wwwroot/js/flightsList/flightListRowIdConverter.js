class FlightListRowIdConverter {

    constructor(prefix) {
        this._prefix = prefix;
    }

    /**
     * converts flight wrapper id to list row id
     * @param {string} flightWrapperId the flight wrapper's id
     */
    flightWrapperIdToListRowId(flightWrapperId) {
        return this._prefix + flightWrapperId;
    }

    /**
     * converts flight wrapper id to list row id
     * @param {string} listRowId the flight wrapper's id
     */
    listRowIdToFlightWrapperId(listRowId) {
        return listRowId.substr(this._prefix.length);
    }

}