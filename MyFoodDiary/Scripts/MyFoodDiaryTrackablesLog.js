var home = (function ($) {

    SetDateOnLoad();

    var RefreshUrl = "/trackableslog/refresh";
    var SaveUrl = "/trackableslog/save";

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

                    var trackableItemTable = $("#trackableItemTable");
                    trackableItemTable.empty();
                    $(json.TrackableItems).each(function (index, trackableItem) {
                        drawTrackableItemRow(trackableItem);               
                    });

                    drawTotalCaloriesRow(json.TotalCalories);

                    $('.SaveLink').on('click', SaveLinkClick);
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

            var trackableItemTable = $("#trackableItemTable");
            trackableItemTable.empty();
            $(json.TrackableItems).each(function (index, trackableItem) {
                drawTrackableItemRow(trackableItem);               
            });

            $(".SaveLink").on("click", SaveLinkClick);
            // Put the focus on the first input field.
            $("#trackableItemTable tr:first").find("input").focus();
        }
    });

    function drawTrackableItemRow(rowData) {
        var row = $("<tr />")
        $("#trackableItemTable").append(row);
        row.append($("<td>" + rowData.Name + "</td>"));
        row.append($("<td><input class='input-quantity' id=" + isNull(rowData.Id) + " name=" + isNull(rowData.Quantity) + " type='text' value=" + isNull(rowData.Quantity) + "></td>"));
        row.append($("<td><a class='SaveLink' href=" + SaveUrl + "/" + rowData.Id + ">Save</a>"));
    }

    function isNull(str) {
        return !str || 0 === str.length ? "" : str;
    }

    function SetDateOnLoad() {

        var date = sessionStorage["currentDate"];
        if (date == null) {
            date = GetTodaysDate();
            sessionStorage["currentDate"] = date;
        }

        $("#dateselection").val(date);
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


