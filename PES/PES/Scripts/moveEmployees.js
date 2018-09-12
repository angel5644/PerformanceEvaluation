$(document).ready(function () {
    $("#selectedEmployeeA").children().remove();
    $("#selectedEmployeeB").children().remove();

    $("#ManagerA-table").DataTable();
    $("#ManagerB-table").DataTable();

    var buttons = $("#btnToA, #btnToB").attr("disabled", true);

    var transferProfiles = parseInt($("#selectedProfile").val());

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $('#selectedEmployeeA').append("<option value='0'>" + "Select a director" + "</option>");
        $('#selectedEmployeeB').append("<option value='0'>" + "Select a director" + "</option>");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value=0'" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value=0'" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    else {
        $("#EmployeeALabel").text("Manager A");
        $("#EmployeeBLabel").text("Manager B");
        $('#selectedEmployeeA').append("<option value='0'>" + "Select a manager" + "</option>");
        $('#selectedEmployeeB').append("<option value='0'>" + "Select a manager" + "</option>");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    
    //$('#selectedEmployeeA').trigger(showSelectedEmployees(parseInt($("#selectedEmployeeA").val()), 1));
    //showSelectedEmployees(parseInt($("#selectedEmployeeB").val()), 2);

    // Events
    $('#selectedEmployeeA').change(function (e) {
        // Execute function 

        var employeeId = parseInt($("#selectedEmployeeA").val());
        var employeeBId = parseInt($("#selectedEmployeeB").val());
        if (employeeId != 0 && employeeBId != 0) {
            buttons.attr("disabled", false);
        }
        else {
            buttons.attr("disabled", true);
        }
        var option = 1;
        showSelectedEmployees(employeeId, option);
    });

    $('#selectedEmployeeB').change(function (e) {
        // Execute function 

        var employeeId = parseInt($("#selectedEmployeeB").val());
        var employeeAId = parseInt($("#selectedEmployeeA").val());
        if (employeeId != 0 && employeeAId != 0) {
            buttons.attr("disabled", false);
        }
        else {
            buttons.attr("disabled", true);
        }
        var option = 2;
        showSelectedEmployees(employeeId, option);
    });
});

$("#selectedProfile").change(function () {
    var transferProfiles = parseInt($("#selectedProfile").val());

    $("#selectedEmployeeA").children().remove();
    $("#selectedEmployeeB").children().remove();

    if (transferProfiles == 3) {
        $("#EmployeeALabel").text("Director A");
        $("#EmployeeBLabel").text("Director B");
        $('#selectedEmployeeA').append("<option value='0'>" + "Select a director" + "</option>");
        $('#selectedEmployeeB').append("<option value='0'>" + "Select a director" + "</option>");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }
    else {
        $("#EmployeeALabel").text("Manager A");
        $("#EmployeeBLabel").text("Manager B");
        $('#selectedEmployeeA').append("<option value='0'>" + "Select a manager" + "</option>");
        $('#selectedEmployeeB').append("<option value='0'>" + "Select a manager" + "</option>");
        $.getJSON('/Employee/GetEmployeesByProifile?profile=' + transferProfiles, function (data) {
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                $('#selectedEmployeeA').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                $('#selectedEmployeeB').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
            }
        });
    }   
});

function showSelectedEmployees(employeeId, option) {
    $.ajax({
        url: "/Employee/GetEmployeesByManager",
        data: {
            employeeId: employeeId,
            option: option
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            
            if (option == 1) {
                // Populate table with data from ajax call
                $("#tableA-content").html("");
                $("#tableA-content").html(data).fadeOut("fast").fadeIn("slow");

                // Re init datatable
                $("#ManagerA-table").DataTable();              
            }
            else if(option == 2){
                $("#tableB-content").html("");
                $("#tableB-content").html(data).fadeOut("fast").fadeIn("slow");

                // Re init datatable
                $("#ManagerB-table").DataTable();
            }
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

function selectToMove(id) {
    var element = $(".checkbox[value=" + id + "]");
    var check = element.prop("checked");
    if (check != true) {
        element.removeClass("selectedToMove");
    }
    else {
        element.addClass("selectedToMove");
    }
}

function moveToB() {
    //Get selected manager from table B
    var manager = $("#selectedEmployeeB").val();
    //Get all elements to move form a to B
    var rowElements = $("#tableA-content").find(".selectedToMove").parents("tr");
    
    var employeesId = [];
    //Get each employee's id to change his managerId
    rowElements.find(".checkbox").each(function (index) {
        employeesId[index] = parseInt($(this).val());
    });
    //Call Ajax function
    $.ajax({
        url: "/Employee/TransferEmployee",
        data: {
            employeesId: employeesId,
            manager : manager
        },
        type: 'post'
    })
    .done(function (data) {
        //Insert elements into table B
        //$("#tableB-content").find("tr:last").after(rowElements);
        //Hide row when moves to tbale B
        rowElements.fadeOut("fast");
        //Update table B to show news employees
        showSelectedEmployees(manager, 2);
        showSelectedEmployees($("#selectedEmployeeA").val(), 1);
    })
    .fail(function (jqxhr, textStatus, error) {
        if (window.console) {
            console.log("Error: " + jqxhr.responseText);
        }
        alert("Error while moving employees. Please verify your information and try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}

function moveToA() {
    //Get selected manager from table A
    var manager = $("#selectedEmployeeA").val();
    //Get all elements to move form a to A
    var rowElements = $("#tableB-content").find(".selectedToMove").parents("tr");
    
    var employeesId = [];
    //Get each employee's id to change his managerId
    rowElements.find(".checkbox").each(function (index) {
        console.log(parseInt($(this).val()));
        employeesId[index] = parseInt($(this).val());
    });
    //Call Ajax function
    $.ajax({
        url: "/Employee/TransferEmployee",
        data: {
            employeesId: employeesId,
            manager: manager
        },
        type: 'post'
    })
    .done(function (data) {      
        //Insert elements into table B
        //$("#tableA-content").find("tr:last").after(rowElements);
        rowElements.fadeOut("fast");
        showSelectedEmployees(manager, 1);
        showSelectedEmployees($("#selectedEmployeeB").val(), 2);
    })
    .fail(function (jqxhr, textStatus, error) {
        if (window.console) {
            console.log("Error: " + jqxhr.responseText);
        }
        alert("Error while moving employees. Please verify your information and try again later.");
    })
    .always(function () {
        //alert("finished");
    });
}
    