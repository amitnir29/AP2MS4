class flightListNode {
    /**
     * 
     * @param {Array[string]} classes an array of all css classes that this object implements
     * @param {string} text the text inside the node
     */
    constructor(classes, text) {
        this.classes = classes;
        this.text = text;
    }

    classesToString() {
        let s = "";
        for (let i = 0; i < this.classes.length - 1; i++) {
            s += (this.classes[i] + " ");
        }
        s += this.classes[this.classes.length - 1];
        return s;
    }

    /**
     * 
     * @param {Element} elem an html element to add this to
     */
    addToDiv(elem) {
        let cssClasses = this.classesToString();
        let s = '<a href="#" class="' + cssClasses + '">' + this.text + "</a>";
        console.log(cssClasses);
        console.log(s);
        elem.innerHTML += s;
    }
}