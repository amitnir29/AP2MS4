function uploadFile(file) {
    let reader = new FileReader()
    reader.onload = function (e) {
        // The file's text will be printed here
        //document.write(e.target.result)
        //console.log(e.target.result)
        let file = e.target.result;
        //console.log(file);
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
        //console.log(obj);
        fields = ["passengers", "company_name", 'initial_location', 'segments'];
        let flag = false;
        for (let field of fields) {
            if (!(field in obj)) {
                console.log("missing field", field);
                flag = true;
            }
        }
        //console.log("flag is ", flag);
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
                //console.log("success in posting json");
                //console.log(data);
            },
            error: function (xhr) {
                ErrorHandler.showError("failed to post flightPlan to the server!")
                //console.log("got to error in posting json");
                //console.log(xhr);
            }

        });
    };
    reader.readAsText(file)
}