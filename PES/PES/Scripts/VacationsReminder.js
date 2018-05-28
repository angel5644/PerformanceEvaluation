$(document).ready(function (){
    $("#tableViewReminded").DataTable({
        "order": [[0, "dec"]],
        "columnDefs": [{
            "targets": 2,
            "orderable": false
        }]
    });;    
});

$("#tableViewReminded .btn-send-reminder").on("click", function (e) {
    e.stopPropagation();
    var id = $(this).attr('id');
    sendedImail(id);
    $('.spinner').css('display', 'block');

    //$("#VaReEmail").modal();
});


$(".btn-send-reminder-all").on("click", function (e) {
    e.stopPropagation();
    sendedImailToAll();
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


function sendedImailToAll() {
    $.ajax({
        url: "/VacationRequest/SendReminderEmailToAll",
    })
        .done(function (data) {
            $('.spinner').hide();

            if (data) {
                $("#VaReEmailAll").modal();
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


