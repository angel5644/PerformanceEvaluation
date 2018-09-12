var comboPeriods = $("#SelectedPeriod");
var comboYears = $("#SelectedYear");
var currentProfile = $("#CurrentProfile").val();
var orderValue;
if (currentProfile == 3) {
    orderValue = 6;
}
else {
    orderValue = 5;
}
$(document).ready(function () {
    getValues();
})

$(comboYears).change(function(){
    getValues();
})

$(comboPeriods).change(function () {
    getValues();
})

function getValues() {
    var period = parseInt(comboPeriods.val());
    var year = parseInt(comboYears.val());
    getReport(period, year);
}

function getReport(period, year) {
    var tableOptions = {
        dom: '<"row"<"col-md-12"B>><"row"<"col-md-6"l><"col-md-6"f>>rtip',
        buttons: [
            'excel', 'pdf'],
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        pageLength: 10,
        order: [[orderValue, "desc"]],
    };

    $.ajax({
        url: "/PerformanceEvaluation/GetHistoricalReport",
        data: {
            period : period,
            year : year
        }
    })
    .done(function (data) {
        if (data) {
            //success ajax
            // Populate table with data from ajax call
            $("#performancePartialPeriods").html("");
            $("#performancePartialPeriods").html(data).fadeOut("fast").fadeIn("slow");

            // Re init datatable
            $("#TableviewPeriods").DataTable(tableOptions);
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