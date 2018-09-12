// Events 
$("#btnVerify").on("click", function (e) {
    var year = parseInt($('#SelectedYear').val());
    var period = parseInt($('#SelectedPeriod').val());
    var employee = parseInt($("#SelectedEmployee").val());
    var evaluator = parseInt($("#SelectedEvaluator").val());

    // Execute function 
    verifyPEfile(employee, evaluator, period, year);
});

function verifyPEfile(employee, evaluator, period, year) {

    $.getJSON('/PerformanceEvaluation/VerifyPE?employee='+ employee +'&&evaluator='+ evaluator +'&&period='+ period +'&&year=' + year, function (data) {

        if (data.exist == true && data.idPe != 0) {
            var override = confirm("This Performance Evaluation File has already been uploaded. Do you want to replace it?");

            if (override) {
                $("#Replace").val(data.idPe);
                $("#Replace").attr("value", data.idPe);

                // Submit
                $("#UploadForm").submit();
            }
            else {
                $("#Replace").val(00);
                $("#Replace").attr("value", 00);
            }

        }
        else {
            // Submit
            $("#UploadForm").submit();
        }
    });
}

$("#UploadForm").on("submit", function (e) {
    
    //e.preventDefault();
    //alert("Submit");
    return true;
});