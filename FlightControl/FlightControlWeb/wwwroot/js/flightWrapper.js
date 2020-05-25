class FlightWrapper {
    constructor(flightJsonObj) {
        this._flightDetails = flightJsonObj;
        this._planeIconReference  = null;
        this._flightsListNode = null;
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

    get flightsListNode() {
        return this._flightsListNode;
    }

    set flightsListNode(newListNode) {
        this._flightsListNode = newListNode;
    }

    get id() {
        return this._flightDetails.flight_id;
    }

}