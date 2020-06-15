//get the item of the list, add to the dragover event to become a d&d zone, and drag leave come back
let dropArea = document.getElementById('flightsLists');
  /*dropArea.addEventListener('dragenter', handlerFunction, false)
  dropArea.addEventListener('dragleave', handlerFunction, false)
  dropArea.addEventListener('dragover', handlerFunction, false)
  dropArea.addEventListener('drop', handlerFunction, false)*/
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropArea.addEventListener(eventName, preventDefaults, false)
    });

    function preventDefaults(e) {
        e.preventDefault()
        e.stopPropagation()
    }
    ;[ 'dragover'].forEach(eventName => {
        dropArea.addEventListener(eventName, highlight, false)
    })

    ;['dragleave', 'drop'].forEach(eventName => {
        dropArea.addEventListener(eventName, unhighlight, false)
    });
    ['dragenter'].forEach(eventName => {
        dropArea.addEventListener(eventName, enterDrag, false)
    })
let counter = 0;
function highlight(e) {
    dropArea.classList.add('highlight');
        
}
function enterDrag(e) {
    counter++;

}
let dragTimer;

function unhighlight(e) {

    counter--;
    if (counter === 0) {
        dropArea.classList.remove('highlight');
    }
        
 }

    dropArea.addEventListener('drop', handleDrop, false)

    function handleDrop(e) {
        let dt = e.dataTransfer
        let files = dt.files

        handleFiles(files)
    }
    function handleFiles(files) {
        ([...files]).forEach(uploadFile)
    }


 