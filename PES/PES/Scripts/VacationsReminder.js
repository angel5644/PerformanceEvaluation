var filterNumber = parseInt($("#numberFilterTexBox").val());

$(document).ready(function () {
    $("#tableViewReminded").DataTable({
        "order": [[0, "dec"]],
        "columnDefs": [{
            "targets": 2,
            "orderable": false
        }]
    });;
    
});

$("#buttomSearchVacations").on("click", function (e) {
    $('.spinner').css('display', 'block');
    var numberFilterController = $("#numberFilterTexBox").val();
    $.ajax({
        url: "/VacationRequest/VacationsReminderFilter",
        data: { testChar: numberFilterController }
    })
        .done(function (data) {
            $("#magicDiv").html(data);
            $("#tableViewReminded").DataTable({
                "order": [[0, "dec"]],
                "columnDefs": [{
                    "targets": 2,
                    "orderable": false
                }]
            });;
            $('.spinner').hide();
            


        })
        .fail(function () {
            $('.spinner').hide();


        })
        .always(function () { });

})



$("#resetButtomTable").on("click", function (e) {
    $('.spinner').css('display', 'block');
    $.ajax({
        url: "/VacationRequest/ResetVacationRemainder",
        data: { testChar: true }
    })
        .done(function (data) {
            $("#magicDiv").html(data);
            $("#tableViewReminded").DataTable({
                "order": [[0, "dec"]],
                "columnDefs": [{
                    "targets": 2,
                    "orderable": false
                }]
            });;
            $('.spinner').hide();



        })
        .fail(function () {
            $('.spinner').hide();


        })
        .always(function () { });

})

$("#tableViewReminded .btn-send-reminder").on("click", function (e) {
    e.stopPropagation();
    var id = $(this).attr('id');
    sendedImail(id);
    $('.spinner').css('display', 'block');

    //$("#VaReEmail").modal();
});
   

function sendedImail(id) {
    $.ajax({
        url: "/VacationRequest/SendReminderEmail",
        data: { userid: id }
    })
        .done(function (data) {
            $('.spinner').hide();

            if (data) {
                $("#VaReEmail").modal();
            } else {
                $("#VaFailedEmail").modal();
            }           

        })
        .fail(function () {
            $('.spinner').hide();

            $("#VaFailedEmail").modal();
        })
        .always(function () { });
}


