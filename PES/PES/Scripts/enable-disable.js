function enableDisable(employeeId) {
    var element = $("a[data-employee-id='" + employeeId + "']");
    option = element.attr("data-option");

    // Call ajax
    $.ajax({
        url: '/Employee/EnableDisableEmployee',
        type: "POST",
        data: { idEmployee: employeeId, option: option }
    })
    .done(function (data) {
        // Get data from json
        // Call was successfully executed
        if (data.success) {
            //var element = $('#Id' + employeeId).siblings('td:last').children('.enableDisable');
            var rowElement = element.parent().siblings();
            var actionsElement = element.parent();

            if (option == "enable") {

                if (rowElement.hasClass("line-through")) {
                    rowElement.removeClass("line-through");
                }

                // Set option as disable
                element.text(" ");
                element.attr("data-option", "disable");
                element.addClass("glyphicon");
                element.switchClass("glyphicon-eye-open", "glyphicon-eye-close");
                element.attr("title", "Disable");

                    
                // Enable edit button
                var editElement = actionsElement.find(".edit-button").first();

                if (editElement.length > 0) {
                    editElement.attr("disabled", false);
                }
            }
            else { // Disable
                if (!rowElement.hasClass("line-through")) {
                    rowElement.addClass("line-through");
                }

                // Set option as enable
                element.text(" ");
                element.attr("data-option", "enable");
                element.addClass("glyphicon");
                element.switchClass("glyphicon-eye-close", "glyphicon-eye-open");
                element.attr("title", "Enable");

                // Disable edit button
                var editElement = actionsElement.find(".edit-button").first();

                if (editElement.length > 0) {
                    //alert("Got element");
                    editElement.attr("disabled", true);
                }
            }
        } else {
            alert("There was an error.");
        }
    })
    .fail(function () {
    });
}

$("a.edit-button").on("click", function () {
    return ($(this).attr('disabled')) ? false : true;
});

//$(".enableDisable").on("click", function (e) {
//    // Link element clicked
//    var element = $(e.target);

//    var option = element.attr("data-option");
//    var employeeId = element.attr("data-employee-id");

//    // Call ajax
//    $.ajax({
//        url: '/Employee/EnableDisableEmployee',
//        type: "POST",
//        data: { idEmployee: employeeId, option: option }
//    })
//    .done(function (data) {
//        // Get data from json
//        // Call was successfully executed
//        if (data.success) {
//            var rowElement = element.parent().siblings();

//            if (option == "enable") {

//                if (rowElement.hasClass("line-through")) {
//                    rowElement.removeClass("line-through");
//                }

//                element.text("Disable");
//                element.attr("data-option", "disable");

//                // Enable edit button
//                var editElement = rowElement.parent().find(".edit-button").first();
//                editElement.attr("disabled", false);
//            }
//            else { // Disable
//                if (!rowElement.hasClass("line-through")) {
//                    rowElement.addClass("line-through");
//                }

//                element.text("Enable");
//                element.attr("data-option", "enable");

//                // Disable edit button
//                var editElement = rowElement.parent().find(".edit-button").first();
//                editElement.attr("disabled", true);
//            }
//        } else {
//            alert("There was an error.");
//        }
//    })
//    .fail(function () {
//    });
//})
