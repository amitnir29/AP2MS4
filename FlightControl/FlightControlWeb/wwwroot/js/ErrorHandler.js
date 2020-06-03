class ErrorHandler {
    //TODO- add a feature to prevent spamming the same messages
    static showError(msg) {
        let id = 1;
        let item = document.getElementById("error_list");
        let alert = document.createElement('div');
        alert.className = "alert alert-danger alert-dismissible fade in";
        alert.innerHTML = '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'+
            '<strong>Error!</strong> '+msg;
        alert.id = "error-alert"+ id.toString() 
        item.insertBefore(alert, item.childNodes[0]);
        //fadeTo-how much time until it start to disappear
        $("#error-alert" + id.toString()).fadeTo(6000, 500).slideUp(1300, "swing" , function () {    
            //$("#error-alert"+ id.toString()).slideUp();
        });
        id += 1;
    }
    //TODO- add a green alert
    static showSuccess(asds){

    }
}