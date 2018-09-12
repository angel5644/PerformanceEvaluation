$(function () {
   
    if ($('#Tableview').length) 
    {
        $('#Tableview').DataTable({
            dom: '<"row"<"col-md-6"B><"col-md-6"f>> rtp',
            buttons: [
                'excel', 'pdf'],
            pagingType: "full_numbers"
        });
    }

    if ($('#ExcelTable').length) {
        $('#ExcelTable').DataTable({
            dom: '<"row"<"col-md-6"B><"col-md-6"f>> rtp',
            buttons: [
                'excel', 'pdf'],
            "order": [[ 1, "desc" ]],
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

       $.post("/Lateness/getNickName", { email: email }, function (nickname) {
            window.location.href = "/Lateness/LatenessByUser?name=" + nickname + "&email=" + email + "&vname=" + name;
        });
        
    });
    

    //To upload the excel
    FileSelected = function(sender) {
       if (sender.value.indexOf('xls', 'xlsx') > -1)
        {
            $("#lateness").submit();
        }
        else {
            alert('File not allowed. \nOnly accept .xsl and .xslx');
        }
    };

    $("#saveFile").on("click", function () {
        $("#myModal").modal();
    });

    $("#confirm").on("click", function () {
        $.post("/Lateness/SaveLatenessExcel", { confirmReplace: false }, function (done) {
            if(done == "true")
            {
                var message = "<div class=\"alert alert-success\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "The lateness report has been saved successfully!" +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            } else if(done == "imported") {
                $("#modalImported").modal();
            } else {
                var message = "<div class=\"alert alert-warning\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                done +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            }
        });
    });

    $("#confirmImported").on("click", function () {
        $.post("/Lateness/SaveLatenessExcel", { confirmReplace: true }, function (done) {
            if (done == "true") {
                var message = "<div class=\"alert alert-success\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                "The lateness report has been replace successfully!" +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            }
            else {
                var message = "<div class=\"alert alert-warning\">" +
                                "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                                done +
                              "</div>";
                $(".alert").remove();
                $("body table:last").before(message);
            }
        });
    });
    

    //To select the period with radio button
    $('input[name="period"]').click(function () {
        localStorage['my.checkbox'] = $('input[name="period"]:checked').val();
        var period = $('input[name="period"]:checked').val();
        var email = (location.search.split("name" + "=")[1] || "").split("&")[0];
        email = email.replace(/%20/g, " ");

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
                    chart(new Date().getFullYear().toString());
                }
                else if (period == "last 5 years") {
                    paintSelect();
                    chart(new Date().getFullYear().toString());
                    $('select').on('change', function () {
                        chart(this.value);
                    });
                }


                //To delete registers on the table view.
                var idDelete;
                var idCancel;
                $('.btn-delete').click(function () {
                    id = $(this).attr("id");
                    $("#modalUser").modal();
                });

                $("#deleteConfirm").on("click", function () {    
                    $.ajax({
                        type: "POST",
                        url: "/Lateness/LatenessLogicDelete/",
                        data: { id: id },
                        success: function (data) {
                            if (data == "True") {
                                location.reload();
                            }
                            else {
                                alert("An error has ocurred");
                            }
                        }
                    });
                });

                $('.btn-cancel').click(function () {
                    idCancel = $(this).attr("id");
                    $("#modalCancel").modal();
                });

                $("#cancelConfirm").on("click", function () {
                    $.ajax({
                        type: "POST",
                        url: "/Lateness/LatenessLogicCancel/",
                        data: { id: idCancel },
                        success: function (data) {
                            if (data == "True") {
                                location.reload();
                            }
                            else {
                                alert("An error has ocurred");
                            }
                        }
                    });
                });
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
        var monthB = { "January": 0, "February": 0, "March": 0, "April": 0, "May": 0, "June": 0, "July": 0, "August": 0, "September": 0, "October": 0, "November": 0, "December": 0 };
        var monthJ = { "January": 0, "February": 0, "March": 0, "April": 0, "May": 0, "June": 0, "July": 0, "August": 0, "September": 0, "October": 0, "November": 0, "December": 0 };
        var time;
        var justify;
        var limit =1;
       
        $('#Tableview tbody tr td:nth-child(1)').each(function () {
            var i = 0;

            $('#Tableview tbody tr td:nth-child(2)').each(function () {
                if (i == limit) {
                    return false;
                }
                time = $.trim($(this).text());
                time = parseInt(time.substr(1, 1) + time.substr(3, 2));

                i++;
            });

            i = 0;

            $('#Tableview tbody tr td:nth-child(3)').each(function () {
                if (i == limit) {
                    return false;
                }
                justify = $.trim($(this).text());
                i++;
            });

            limit++;

            var date = $.trim($(this).text()).toLowerCase();


            if (time >= 815 && time <= 930 && justify != "Cancel") {
                for (var key in month) {
                    if (date.indexOf(key.toLowerCase()) >= 0 && date.indexOf(year) >= 0) {
                        month[key] += 1;
                    }
                }
            }
            else if ((time > 930 || time < 815) && justify != "Cancel") {
                for (var key in monthB) {
                    if (date.indexOf(key.toLowerCase()) >= 0 && date.indexOf(year) >= 0) {
                        monthB[key] += 1;
                    }
                }
            }
            else {
                for (var key in monthJ) {
                    if (date.indexOf(key.toLowerCase()) >= 0 && date.indexOf(year) >= 0) {
                        monthJ[key] += 1;
                    }
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
                categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
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
            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
                shared: true
            },
            plotOptions: {
                column: {
                    stacking: 'point'
                }
            },
            series: [{
                name: 'Between 08:15am to 09:30',
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
                    ]
            }, {
                name: 'After 9:30',
                data: [
                    ['January', monthB["January"]],
                    ['February', monthB["February"]],
                    ['March', monthB["March"]],
                    ['April', monthB["April"]],
                    ['May', monthB["May"]],
                    ['June', monthB["June"]],
                    ['July', monthB["July"]],
                    ['August', monthB["August"]],
                    ['September', monthB["September"]],
                    ['October', monthB["October"]],
                    ['November', monthB["November"]],
                    ['December', monthB["December"]]
                ]
            }, {
                name: 'Justified',
                data: [
                    ['January', monthJ["January"]],
                    ['February', monthJ["February"]],
                    ['March', monthJ["March"]],
                    ['April', monthJ["April"]],
                    ['May', monthJ["May"]],
                    ['June', monthJ["June"]],
                    ['July', monthJ["July"]],
                    ['August', monthJ["August"]],
                    ['September', monthJ["September"]],
                    ['October', monthJ["October"]],
                    ['November', monthJ["November"]],
                    ['December', monthJ["December"]]
                ]
            }]
        });
    }

   
    $("input[name='period']").filter("[value='" + localStorage['my.checkbox'] + "']").prop('checked', true);
    $("input[name='period'][value='" + localStorage['my.checkbox'] + "']").trigger("click");
});
