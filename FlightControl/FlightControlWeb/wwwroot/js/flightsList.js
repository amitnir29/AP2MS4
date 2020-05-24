

flightNodesClasses = ["list-group-item", "list-group-item-action", "list-group-item-info"];
fl = document.getElementById("flightsList");
fl.innerHTML += '<h3>List of planes</h3>\
comment <p> Lorem ipsum dolor sit amet, consectetur adipisicing elit...</p>\
    <p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris...</p>\
    <div class="list-group">\
        <li class="list-group-item d-flex justify-content-between align-items-center">\
            Local Flights<span class="badge badge-info badge-pill">3</span>\
        </li>';

let localFlightsList = [
    new flightListNode(flightNodesClasses, "first local plane"),
    new flightListNode(flightNodesClasses, "second local plane"),
    new flightListNode(flightNodesClasses, "third local plane")
]
for (localFlight of localFlightsList) {
    localFlight.addToDiv(fl);
}

fl.innerHTML += '</div>\
    <div class="list-group">\
        <li class="list-group-item d-flex justify-content-between align-items-center">\
            Extrenal Flights <span class="badge badge-primary badge-pill">3</span>\
        </li>';

localFlightsList = [
    new flightListNode(flightNodesClasses, "first external plane"),
    new flightListNode(flightNodesClasses, "second external plane"),
    new flightListNode(flightNodesClasses, "third external plane")
]
for (localFlight of localFlightsList) {
    localFlight.addToDiv(fl);
}

fl.innerHTML += '</div>';
