class FlightWrapper {
    constructor(flightJsonObj) {
        this._flightDetails = flightJsonObj;
        this._planeIconReference  = null;
    }

    get flightDetails() {
        return this._flightDetails;
    }

    get planeIconReference() {
        return this._planeIcon;
    }

    set planeIconReference(newReference) {
        this._planeIconReference = newReference;
    }

    get id() {
        return this._flightDetails.flight_id;
    }

    /**
     * convert a FlightWrapper array into a set of the FlightWrappers' ids.
     * @param {Array[FlightWrapper]} flightWrapperArray
     * @returns {Set[string]}
     */
    static FlightsWrappersListToIdSet(flightWrapperArray) {
        const idSet = new Set();
        //for each flightWrapper, add its id to the set
        for (const flightWrapper of flightWrapperArray) {
            idSet.add(flightWrapper.id); //TODO check if this is the way to access id
        }
        return idSet;
    }

}