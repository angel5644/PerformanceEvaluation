$(document).ready(function () {
    $("#table-employees").DataTable();
    $("#Tableview").DataTable({
        "order": [[4, "asc"]]
    } );

    // Events 
    $("#showEnables").on("click", function (e) {

        // Email user
        var emailUser = $("#EmailUser").val();
        // Filter 
        var filter = $(this).val();

        validateFilters(emailUser, filter);
    });

    // Events 
    $("#showDisables").on("click", function (e) {
        // Execute function 

        // Email user
        var emailUser = $("#EmailUser").val();
        // Filter 
        var filter = $(this).val();

        validateFilters(emailUser, filter);
    });

    // Events 
    $("#showBoth").on("click", function (e) {
        // Execute function 

        // Email user
        var emailUser = $("#EmailUser").val();
        // Filter 
        var filter = $(this).val();

        validateFilters(emailUser, filter);
    });

});

function validateFilters(employeeEmail, filter) {
    $.ajax({
        url: "/Employee/GetEmployeesByFilter",
        data: {
            employeeEmail: employeeEmail,
            filter: filter
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#table-content").html("");
            $("#table-content").html(data).fadeOut("fast").fadeIn("slow");

            // Re init datatable
            $("#table-employees").DataTable();
        }
        else {
            // Error
            alert("Error while getting employees");
        }
    })
    .fail(function (jqxhr, textStatus, error) {
        alert("Error while getting employees. Please try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}
