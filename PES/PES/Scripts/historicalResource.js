

$("#show").click(function () {

    $('#item').fadeIn(1000);

});

$("#hide").click(function () {

    $('#item').fadeOut(1000);

});




$("#newRequestButom").mouseover(function (e) {

    var vacationsDays = $("#availableDays").val();

    if (vacationsDays == "0") {
        $("#noVacationsDays").modal();
    }


})