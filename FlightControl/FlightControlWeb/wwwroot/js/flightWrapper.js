class FlightWrapper {
    constructor(flightJsonObj, mapPlaneIconReference=null) {
        this._flightDetails = flightJsonObj;
        this._planeIconReference = mapPlaneIconReference;
    }

    get flightDetails() {
        return this._flightDetails;
    }

    get planeIconReference() {
        return this._planeIconReference;
    }

    set planeIconReference(newReference) {
        this._planeIconReference = newReference;
    }

    get id() {
        return this._flightDetails.flight_id;
    }

    get is_external() {
        return this._flightDetails.is_external;
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
            idSet.add(flightWrapper.id);
        }
        return idSet;
    }

}