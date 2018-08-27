var charts = (function ($) {

    setDateOnLoad();

    var refreshBarChartUrl = "/charts/refreshbarchart/";
    var refreshLineChartUrl = "/charts/refreshlinechart/";

    //$(".ProteinLink").on("click", proteinLinkClick);
    //$(".CarbohydratesLink").on("click", carbohydratesLinkClick);
    //$(".TotalSugarsLink").on("click", totalSugarsLinkClick);
    //$(".FatLink").on("click", fatLinkClick);
    //$(".CaloriesLink").on("click", caloriesLinkClick);
    //$(".AlcoholLink").on("click", alcoholLinkClick);
    //$(".TotalEnergyLink").on("click", totalEnergyLinkClick);
    //$(".ProteinWeekLink").on("click", proteinWeekLinkClick);
    //$(".CarbohydratesWeekLink").on("click", carbohydratesWeekLinkClick);
    //$(".SugarsWeekLink").on("click", sugarsWeekLinkClick);
    //$(".FatWeekLink").on("click", fatWeekLinkClick);
    //$(".CaloriesWeekLink").on("click", caloriesWeekLinkClick);
    //$(".AlcoholWeekLink").on("click", alcoholWeekLinkClick);

    // When a date is selected I want to 
    $("#dateselection").datepicker({
        onSelect: function (dateText, inst) {

            sessionStorage["currentDate"] = dateText;

            //refreshBarChart();
        }
    });

    $('#SelectedDateButton').click(function (e) {
        e.preventDefault();

        sessionStorage["nutrient"] = $('#SelectedNutrientId :selected').text();
       
        refreshBarChart();
    });

    $('#WeekToDateButton').click(function (e) {
        e.preventDefault();

        sessionStorage["nutrient"] = $('#SelectedNutrientId :selected').text();

        refreshLineChart("Grams");
    });
    
    function refreshBarChart(yAxis) {

        $.ajax({
            type: "POST",
            url: refreshBarChartUrl,
            dataType: "json",
            data: {
                start: sessionStorage["currentDate"],
                end: sessionStorage["currentDate"],
                nutrient: sessionStorage["nutrient"]
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


    //https://www.highcharts.com/docs/chart-and-series-types/line-chart

    function refreshLineChart(nutrient, yAxis) {

        $.ajax({
            type: "POST",
            url: refreshLineChartUrl,
            dataType: "json",
            data: {
                start: sessionStorage["currentDate"],
                end: sessionStorage["currentDate"],
                nutrient: sessionStorage["nutrient"]
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

    //function proteinLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Protein";

    //    refreshBarChart("Grams");
    //}

    //function carbohydratesLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Carbohydrates";

    //    refreshBarChart("Grams");
    //}

    //function totalSugarsLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "TotalSugars";

    //    refreshBarChart("Grams");
    //}

    //function fatLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Fat";

    //    refreshBarChart("Grams");
    //}

    //function caloriesLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Calories";

    //    refreshBarChart("kcal");
    //}

    //function alcoholLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Alcohol";

    //    refreshBarChart("Units");
    //}

    //function totalEnergyLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "TotalEnergy";

    //    refreshBarChart("");
    //}

    //function proteinWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Protein";

    //    refreshLineChart("");
    //}

    //function carbohydratesWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Carbohydrates";

    //    refreshLineChart("");
    //}

    //function sugarsWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "TotalSugars";

    //    refreshLineChart("");
    //}

    //function fatWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Fat";

    //    refreshLineChart("");
    //}

    //function caloriesWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Calories";

    //    refreshLineChart("");
    //}

    //function alcoholWeekLinkClick(e) {

    //    e.preventDefault();

    //    sessionStorage["nutrient"] = "Alcohol";

    //    refreshLineChart("");
    //}


})(jQuery);


