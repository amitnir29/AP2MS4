//get the item of the list, add to the dragover event to become a d&d zone, and drag leave come back
/*let dropArea = document.getElementById('drop-area')

  dropArea.addEventListener('dragenter', handlerFunction, false)
  dropArea.addEventListener('dragleave', handlerFunction, false)
  dropArea.addEventListener('dragover', handlerFunction, false)
  dropArea.addEventListener('drop', handlerFunction, false)

['dragenter', 'dragover'].forEach(eventName => {
  dropArea.addEventListener(eventName, highlight, false)
})

;['dragleave', 'drop'].forEach(eventName => {
  dropArea.addEventListener(eventName, unhighlight, false)
})

function highlight(e) {
  dropArea.classList.add('highlight')
}

function unhighlight(e) {
  dropArea.classList.remove('highlight')
}

  */