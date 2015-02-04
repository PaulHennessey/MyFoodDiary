var charts = (function ($) {

    setDateOnLoad();

    var refreshBarChartUrl = "/food/charts/refreshbarchart/";
    var refreshLineChartUrl = "/food/charts/refreshlinechart/";


    $(".ProteinLink").on("click", proteinLinkClick);
    $(".CarbohydratesLink").on("click", carbohydratesLinkClick);
    $(".FatLink").on("click", fatLinkClick);
    $(".CaloriesLink").on("click", caloriesLinkClick);
    $(".AlcoholLink").on("click", alcoholLinkClick);
    $(".TotalEnergyLink").on("click", totalEnergyLinkClick);
    $(".ProteinWeekLink").on("click", proteinWeekLinkClick);
    $(".CarbohydratesWeekLink").on("click", carbohydratesWeekLinkClick);
    $(".FatWeekLink").on("click", fatWeekLinkClick);
    $(".CaloriesWeekLink").on("click", caloriesWeekLinkClick);
    $(".AlcoholWeekLink").on("click", alcoholWeekLinkClick);

    // When a date is selected I want to 
    $("#dateselection").datepicker({
        onSelect: function (dateText, inst) {

            sessionStorage["currentDate"] = dateText;

            refreshBarChart();
        }
    });

    function refreshBarChart(nutrient, yAxis) {

        $.ajax({
            type: "POST",
            url: refreshBarChartUrl,
            dataType: "json",
            data: {
                start: sessionStorage["currentDate"],
                end: sessionStorage["currentDate"],
                nutrient: nutrient
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

                options.series.push({
                    name: json.ChartTitle,
                    data: json.BarData
                });

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
                start: sessionStorage["currentDate"],
                end: sessionStorage["currentDate"],
                nutrient: nutrient
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

                options.series.push({
                    name: json.ChartTitle,
                    data: json.BarData
                });


                var chart = new Highcharts.Chart(options);
            }
        });
    }



    function setDateOnLoad() {

        var date = sessionStorage["currentDate"];
        if (date == null) {
            date = getTodaysDate();
            sessionStorage["currentDate"] = date;
        }

        $("#dateselection").val(date);
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


    function proteinLinkClick(e) {

        e.preventDefault();

        refreshBarChart("Protein", "Grams");
    }

    function carbohydratesLinkClick(e) {

        e.preventDefault();

        refreshBarChart("Carbohydrates", "Grams");
    }

    function fatLinkClick(e) {

        e.preventDefault();

        refreshBarChart("Fat", "Grams");
    }

    function caloriesLinkClick(e) {

        e.preventDefault();

        refreshBarChart("Calories", "kcal");
    }

    function alcoholLinkClick(e) {

        e.preventDefault();

        refreshBarChart("Alcohol", "Units");
    }

    function totalEnergyLinkClick(e) {

        e.preventDefault();

        refreshBarChart("TotalEnergy", "");
    }

    function proteinWeekLinkClick(e) {

        e.preventDefault();

        refreshLineChart("Protein", "");
    }

    function carbohydratesWeekLinkClick(e) {

        e.preventDefault();

        refreshLineChart("Carbohydrates", "");
    }

    function fatWeekLinkClick(e) {

        e.preventDefault();

        refreshLineChart("Fat", "");
    }

    function caloriesWeekLinkClick(e) {

        e.preventDefault();

        refreshLineChart("Calories", "");
    }

    function alcoholWeekLinkClick(e) {

        e.preventDefault();

        refreshLineChart("Alcohol", "");
    }


})(jQuery);


