function uploadFile(file) {
    let reader = new FileReader()
    reader.onload = function (e) {
        // The file's text will be printed here
        let file = e.target.result;
        //check if it's a json file
        try {
            JSON.parse(file);
        } catch (e) {
            //output that it's wrong
            ErrorHandler.showError("Wrong file format! Please submit a JSON file!");
            return;
        }
        //if it's a json, check if has fileds
        let obj = JSON.parse(file);
        fields = ["passengers", "company_name", 'initial_location', 'segments'];
        let flag = false;
        for (let field of fields) {
            if (!(field in obj)) {
                flag = true;
            }
        }
        //if there's at least one missing flag
        if (flag) {
            ErrorHandler.showError("JSON file is missing some fields!");
            return;
        }
        $.ajax({
            url: "api/FlightPlan",
            type: "post", //send it through get method
            data: file,//the json file
            contentType: "application/json",

            success: function (data) {
                //flightsArray = JSON.parse(data);
                //consloe
            },
            error: function (xhr) {
                console.log(xhr);
                ErrorHandler.showError("failed to post flightPlan to the server!");
            }

        });
    };
    reader.readAsText(file)
}