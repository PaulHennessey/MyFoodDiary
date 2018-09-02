var charts = (function ($) {

    setDateOnLoad();

    var refreshBarChartUrl = "/micronutrients/refreshbarchart/";
    var refreshLineChartUrl = "/micronutrients/refreshlinechart/";

    $("#fromdate").datepicker();
    $("#todate").datepicker();

    $('#GenerateChartButton').click(function (e) {
        e.preventDefault();

        var from = $("#fromdate").val();
        var to = $("#todate").val();

        if (from === to) {
            refreshBarChart();
        }
        else {
            refreshLineChart("Grams");
        }
    });
    
    function refreshBarChart(yAxis) {

        $.ajax({
            type: "POST",
            url: refreshBarChartUrl,
            dataType: "json",
            data: {
                start: $("#fromdate").val(),
                end: $("#todate").val(),
                nutrient: $('#SelectedNutrientId :selected').text()
            },
            success: function (json) {

                var options = {
                    chart: {
                        renderTo: "foodChart",
                        type: "bar"
                    },
                    title: {
                        text: json.Title
                    },
                    xAxis: {
                        categories: json.BarNames
                    },
                    yAxis: {
                        title: {
                            text: yAxis
                        }
                    },
                    series: []

                };

                for (i = 0; i < json.BarData.length; i++) {
                    options.series.push({
                        name: json.ChartTitle[i],
                        data: json.BarData[i]
                    });
                }

                var chart = new Highcharts.Chart(options);
            }
        });
    }


    function refreshLineChart(nutrient, yAxis) {

        $.ajax({
            type: "POST",
            url: refreshLineChartUrl,
            dataType: "json",
            data: {
                start: $("#fromdate").val(),
                end: $("#todate").val(),
                nutrient: $('#SelectedNutrientId :selected').text()
            },
            success: function (json) {

                var options = {
                    chart: {
                        renderTo: 'foodChart',
                        type: 'line'
                    },
                    title: {
                        text: json.Title
                    },
                    xAxis: {
                        categories: json.BarNames
                    },
                    yAxis: {
                        title: {
                            text: yAxis
                        }
                    },
                    series: []

                };

                for (i = 0; i < json.BarData.length; i++) {
                    options.series.push({
                        name: json.ChartTitle[i],
                        data: json.BarData[i]
                    });
                }

                var chart = new Highcharts.Chart(options);
            }
        });
    }



    function setDateOnLoad() {

        var date = sessionStorage["currentDate"];
        if (date === null) {
            date = getTodaysDate();
            sessionStorage["currentDate"] = date;
        }

        $("#dateselection").val(date);
        $("#fromdate").val(date);
        $("#todate").val(date);
    }



    function getTodaysDate() {

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd;
        }

        if (mm < 10) {
            mm = '0' + mm;
        }

        return dd + "/" + mm + "/" + yyyy;
    }



})(jQuery);


