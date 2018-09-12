$(function () {

    $('#Tableview').DataTable({
        dom: '<"row"<"col-md-6"B><"col-md-6"f>>',
        buttons: [
            'excel', 'pdf'],
        pageLength: -1
    });

    $('input[name="period"]').click(function () {
        var period = $('input[name="period"]:checked').val();
        $.ajax({
            type: "POST",
            url: "/Lateness/GetLatenessByFilter/",
            data: { period: period },
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
        var month = {"January":0, "February":0, "March":0, "April":0, "May":0, "June":0, "July":0, "August":0, "September":0, "October":0, "November":0, "December":0};
        var limit = 1;
        var justify;

        //select the column date and compare the months
        $('#Tableview tbody tr td:nth-child(1)').each(function () {

            var i = 0;
            $('#Tableview tbody tr td:nth-child(3)').each(function () {
                if (i == limit) {
                    return false;
                }
                justify = ($.trim($(this).text()) == "Justified") ? true : false;
                i++;
            });
            console.log(justify);
            limit++;


            var date = $.trim($(this).text()).toLowerCase();
            for (var key in month) {
                if (date.indexOf(key.toLowerCase()) >= 0 && date.indexOf(year) >= 0 && !justify) {
                    month[key] += 1;
                }
            }
        });

      
        Highcharts.chart('container', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Leness Chart'
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