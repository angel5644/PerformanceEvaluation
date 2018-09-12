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
            console.log("fail");


        })
        .always(function () { });

})



$("#resetButtomTable").on("click", function (e) {

    $("#numberFilterTexBox").val("");

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

$("#magicDiv").on("click", "#tableViewReminded .btn-send-reminder", function (e) {
    e.stopPropagation();
    
    var id = $(this).attr('id');
    sendedImail(id);
    $('.spinner').css('display', 'block');

    //$("#VaReEmail").modal();
});
   


$("#btn-send-reminder-all").on("click", function (e) {
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

            if (data){

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
                        });
                        $("#numberFilterTexBox").val("");
                    })
                    .fail(function () {

                    })
                    .always(function () { });

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
    var numberFT = $("#numberFilterTexBox").val();
    $.ajax({
        url: "/VacationRequest/SendReminderEmailToAll",
        data: { number: numberFT }
    })
        .done(function (data) {
            $('.spinner').hide();

            if (data) {
                //

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
                        });


                        $("#numberFilterTexBox").val("");
                    })
                    .fail(function () {


                    })
                    .always(function () { });

                //



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


