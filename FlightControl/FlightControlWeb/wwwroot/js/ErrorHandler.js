class ErrorHandler {
    //save recent error messages presented, to prevent spamming the client
    static messages = [];
    static showError(msg) {
        if (!(ErrorHandler.messages.includes(msg))) {
            let id = 1;
            let item = document.getElementById("error_list");
            let alert = document.createElement('div');
            alert.className = "alert alert-danger alert-dismissible fade in";
            alert.innerHTML = '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>' +
                '<strong>Error!</strong> ' + msg;
            alert.id = "error-alert" + id.toString()
            item.insertBefore(alert, item.childNodes[0]);
            //fadeTo-how much time until it start to disappear
            $("#error-alert" + id.toString()).fadeTo(6000, 500).slideUp(1300, "swing", function () {
                //$("#error-alert"+ id.toString()).slideUp();
            });
            id += 1;
            ErrorHandler.messages.push(msg);
            setTimeout(
                function () {
                    //remove from the list of messages
                    const index = ErrorHandler.messages.indexOf(msg);
                    if (index > -1) {
                        ErrorHandler.messages.splice(index, 1);
                    }
                }, 10000);
        }
    }
}