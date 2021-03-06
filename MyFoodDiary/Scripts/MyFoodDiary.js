﻿var home = (function ($) {

    SetDateOnLoad();

    //var HomeUrl = "/food/home/";
    var RefreshUrl = "/food/home/refresh";
    var DeleteUrl = "/food/home/delete";
    var SaveUrl = "/food/home/save";
    var FavouriteUrl = "/food/home/favourite";
    var UseFavouriteUrl = "/food/home/usefavourite";
    var DeleteFavouriteUrl = "/food/home/deletefavourite";

    // This rather dense code is explained here: http://blogs.msdn.com/b/stuartleeks/archive/2012/04/23/asp-net-mvc-amp-jquery-ui-autocomplete.aspx
    $('*[data-autocomplete-url]')
       .each(function () {
           $(this).autocomplete({
               minLength: 3,
               source: $(this).data("autocomplete-url"),
               select: function (event, ui) {
                   
                   var actionlink = $(this).data("selectfood-url");

                   actionlink = actionlink.replace("replace", ui.item.value);                              // Insert the parameter (on the client)

                   actionlink = actionlink.replace("replace-date", ConvertDateToISO8601(sessionStorage["currentDate"]));             // Insert the parameter (on the client)
                   window.location.href = actionlink;                                                           // Go to it...
               }
           });
       });


    // When a date is selected I want to 
    $("#dateselection").datepicker({
        onSelect: function (dateText, inst) {

            sessionStorage["currentDate"] = dateText;

            $.ajax({
                type: "POST",
                url: RefreshUrl,
                dataType: "json",
                data: {                    
                    date: dateText
                },
                success: function (json) {
                    var foodItemTable = $("#foodItemTable");
                    foodItemTable.empty();
                    $(json.FoodItems).each(function (index, foodItem) {
                        drawRow(foodItem);
                    });
                    $('.DeleteLink').on('click', DeleteLinkClick);
                    $('.SaveLink').on('click', SaveLinkClick);
                    $('.FavouriteLink').on('click', FavouriteLinkClick);
                    $('.DeleteFavouriteLink').on('click', DeleteFavouriteLinkClick);
                }
            });

        }
    });


    $.ajax({
        type: "POST",
        url: RefreshUrl,
        dataType: "json",
        data: {
            date: sessionStorage["currentDate"]
        },
        success: function (json) {

            var foodItemTable = $("#foodItemTable");
            foodItemTable.empty();
            $(json.FoodItems).each(function (index, foodItem) {
                drawFoodItemRow(foodItem);               
            });

            var favouriteTable = $("#favouriteTable");
            favouriteTable.empty();
            $(json.Favourites).each(function (index, favourite) {
                drawFavouriteRow(favourite);
            });

            $('.DeleteLink').on('click', DeleteLinkClick);
            $(".SaveLink").on("click", SaveLinkClick);
            $('.FavouriteLink').on('click', FavouriteLinkClick);
            $('.DeleteFavouriteLink').on('click', DeleteFavouriteLinkClick);           
            // Put the focus on the first input field.
            $("#foodItemTable tr:first").find("input").focus();
        }
    });

    function drawFoodItemRow(rowData) {
        var row = $("<tr />")
        $("#foodItemTable").append(row);
        row.append($("<td>" + rowData.Name + "</td>"));
        row.append($("<td><input class='input-quantity' id=" + rowData.Id + " name=" + rowData.Quantity + " type='text' value=" + rowData.Quantity + "></td>"));
        //row.append($("<td>" + (rowData.ValuesArePerItem ? "" : "grams") + "</td>"));
        row.append($("<td><a class='SaveLink' href=" + SaveUrl + "/" + rowData.Id + ">Save</a>" +
                         "<a class='DeleteLink' href=" + DeleteUrl + "/" + rowData.Id + ">Delete</a>" +
                         "<a class='FavouriteLink' href=" + FavouriteUrl + "/" + rowData.Id + ">Favourite</a></td>"));
    }

    function drawFavouriteRow(rowData) {
        var row = $("<tr />")
        $("#favouritesTable").append(row);
        row.append($("<td><a class='UseFavouriteLink' href=" + UseFavouriteUrl + "/" + rowData.Code +
                    "/" + ConvertDateToISO8601(sessionStorage["currentDate"]) +
                    ">" + rowData.Name + "</td>"));
        row.append($("<td><a class='DeleteFavouriteLink' href=" + DeleteFavouriteUrl + "/" + rowData.Code + ">Delete</a></td>"));
    }


    function SetDateOnLoad() {

        var date = sessionStorage["currentDate"];
        if (date == null) {
            date = GetTodaysDate();
            sessionStorage["currentDate"] = date;
        }

        $("#dateselection").val(date);
    }


    function DeleteLinkClick(e) {

        e.preventDefault();

        if (confirm("Delete?"))
            return window.location.href = this.href;
    }



    function SaveLinkClick(e) {

        e.preventDefault();

        // First get the food item id - it is the last bit of the url        
        var parsedUrl = this.href.split("/");
        var foodItemId = parsedUrl[parsedUrl.length - 1];

        // Now get the quantity - the fooditemid is used as the id of the quantity input field        
        var quantity = $("#" + foodItemId).val();

        // Now stick the quantity on the end of the url
        var link = this.href + "/" + quantity;

        // Go to it...
        window.location.href = link;
    }


    function FavouriteLinkClick(e) {

        e.preventDefault();

        // First get the food item id - it is the last bit of the url        
        var parsedUrl = this.href.split("/");
        var foodItemId = parsedUrl[parsedUrl.length - 1];

        // Now get the quantity - the fooditemid is used as the id of the quantity input field        
        var quantity = $("#" + foodItemId).val();

        // Now stick the quantity on the end of the url
        var link = this.href + "/" + quantity;

        // Go to it...
        window.location.href = link;
    }


    function DeleteFavouriteLinkClick(e) {

        e.preventDefault();

        if (confirm("Delete?"))
            return window.location.href = this.href;
    }

    function GetTodaysDate() {

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

    function ConvertDateToISO8601(date) {

        var splitDate = date.split("/");
        return splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
    }


})(jQuery);


