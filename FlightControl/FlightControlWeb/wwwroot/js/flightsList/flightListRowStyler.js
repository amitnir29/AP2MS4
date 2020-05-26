class FlightListRowStyler {

    constructor() {
        this.pressedClassName = "pressed-row";
        this.unpressedClassName = "unpressed-row";
    }

    /**
     * change the style of the row to be presses
     * @param {HTMLTableRowElement} row
     */
    makePressed(row) {
        if (row.classList.contains(this.unpressedClassName)) {
            row.classList.remove(this.unpressedClassName);
        }
        row.classList.add(this.pressedClassName);
    }

    /**
     * change the style of the row to be unpressed
     * @param {HTMLTableRowElement} row
     */
    makeUnpressed(row) {
        if (row.classList.contains(this.pressedClassName)) {
            row.classList.remove(this.pressedClassName);
        }
        row.classList.add(this.unpressedClassName);
    }

}