﻿$(function () {

    if ($('#Tableview').length) 
    {
        $('#Tableview').DataTable({
            dom: '<"row"<"col-md-6"B><"col-md-6"f>> rtp',
            buttons: [
                'excel', 'pdf'],
            pagingType: "full_numbers"
        });
    }

    
    $("tr").not(':first').hover(function () {
        $(this).css("background", "#D6D5C3");
        $(this).css('cursor', 'pointer');
     },function () {
          $(this).css("background", "");
     });   

    $("#Tableview").on("click", "tr", function () {
        var name = $.trim($(this).find('td:eq(0)').text());
        var email = $.trim($(this).find('td:eq(1)').text());
        window.location.href = "/Lateness/LatenessByUser?name=" + name + "&email=" + email ;
    });
    

    //To upload the excel
    $(document).on('change', ':file', function () { $("#lateness").submit(); });
    $("#saveFile").on("click", function () { $("#myModal").modal(); });

    $("#confirm").on("click", function () {
        $.post("/Lateness/SaveLatenessExcel", function (done) {
            if(done == "True")
            {
                var message = "<div class=\"alert alert-success\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "The lateness report has been saved successfully!" +
                              "</div>";
                $("body table:last").before(message);
            } else {
                var message = "<div class=\"alert alert-danger\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "You already save this file, reload the file or select a new file." +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            }
        });
    });


    

    $('input[name="period"]').click(function () {
        var period = $('input[name="period"]:checked').val();
        var email = (location.search.split("email" + "=")[1] || "").split("&")[0];

        $.ajax({
            type: "POST",
            url: "/Lateness/GetLatenessByFilter/",
            data: { period: period, email: email},
            success: function (data) {
                $("#table-content").html("").hide("fast");
                $("#table-content").html(data).fadeOut("normal").fadeIn(1000);

                $('#Tableview').DataTable({
                    dom: '<"row"<"col-md-6"B><"col-md-6"f>>',
                    buttons: [
                        'excel', 'pdf'],
                    pageLength: -1
                });


                if (period == "year") {
                    chart("2016");
                }
                else if (period == "last 5 years") {
                    paintSelect();
                    chart("2016");
                    $('select').on('change', function () {
                        chart(this.value);
                    });
                }


                //AGREGADO RECIENTE **************************
                $('button').click(function () {
                    var id = $(this).attr("id");
                    var row = $(this).closest("tr");

                    $.ajax({
                        type: "POST",
                        url: "/Lateness/LatenessLogicDelete/",
                        data: { id: id },
                        success: function (data) {
                            if (data == "True") {
                                row.remove();
                                alert("Action Complete");

                            }
                            else
                            {
                                alert("An error has ocurred");
                            }
                        }
                    });


                    
                });
                //********************************************
            }
        });
    });

    function paintSelect() {
        var date = new Date();
        var element = "<div class='form-group'>" +
                        "<select class='form-control' id='sYear' style='margin-top:3%; margin-left:5%; width:100px;'>" +
                            "<option>" + date.getFullYear() + "</option>" +
                            "<option>" + (date.getFullYear() - 1) + "</option>" +
                            "<option>" + (date.getFullYear() - 2) + "</option>" +
                            "<option>" + (date.getFullYear() - 3) + "</option>" +
                            "<option>" + (date.getFullYear() - 4) + "</option>" +
                            "<option>" + (date.getFullYear() - 5) + "</option>" +
                        "</select>" +
                       "</div>";
        $("#container").before(element);
    }

    function chart(year) {
        var month = { "January": 0, "February": 0, "March": 0, "April": 0, "May": 0, "June": 0, "July": 0, "August": 0, "September": 0, "October": 0, "November": 0, "December": 0 };

        //select the column date and compare the months
        $('#Tableview tbody tr td:nth-child(1)').each(function () {
            var date = $.trim($(this).text()).toLowerCase();
            for (var key in month) {
                if (date.indexOf(key.toLowerCase()) >= 0 && date.indexOf(year) >= 0) {
                    month[key] += 1;
                }
            }
        });

        Highcharts.chart('container', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Lateness Chart'
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'No. Lateness'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Lateness, <b>{point.y}</b>'
            },
            series: [{
                name: 'Lateness',
                data: [
                    ['January', month["January"]],
                    ['February', month["February"]],
                    ['March', month["March"]],
                    ['April', month["April"]],
                    ['May', month["May"]],
                    ['June', month["June"]],
                    ['July', month["July"]],
                    ['August', month["August"]],
                    ['September', month["September"]],
                    ['October', month["October"]],
                    ['November', month["November"]],
                    ['December', month["December"]]
                ],
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#FFFFFF',
                    align: 'right',
                    format: '{point.y}',
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            }]
        });
    }
});