class ErrorHandler {
    static showError(msg) {
        let id = 1;
        let item = document.getElementById("error_list");
        let alert = document.createElement('div');
        alert.className = "alert alert-danger alert-dismissible fade in";
        alert.innerHTML = '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'+
            '<strong>Error!</strong> '+msg;
        alert.id = "error-alert"+ id.toString() 
        item.insertBefore(alert, item.childNodes[0]);
        //document.getElementById("error-alert").innerHTML += "loll";
        $("#error-alert" + id.toString()).fadeTo(2000, 500).slideUp(500, function () {
            //how much time until it disappears
            $("#error-alert"+ id.toString()).slideUp(7000);
        });
        id += 1;
    }
    //TODO- add a green alert
    static showSuccess(asds){

    }
}