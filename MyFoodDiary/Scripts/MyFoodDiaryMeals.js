var products = (function ($) {

    $(".DeleteMealLink").on("click", DeleteLinkClick);
    $(".DeleteIngredientLink").on("click", DeleteLinkClick);
    $(".SaveIngredientLink").on("click", SaveLinkClick);

    function DeleteLinkClick(e) {
         
        if (confirm("Delete?"))
            return window.location.href = this.href;

        e.preventDefault();
    }

    // This deals with the autocomplete.
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

    function ConvertDateToISO8601(date) {

        var splitDate = date.split("/");
        return splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
    }

    function SaveLinkClick(e) {

        e.preventDefault();

        // First get the food item id - it is the last bit of the url        
        var parsedUrl = this.href.split("/");
        var ingredientId = parsedUrl[parsedUrl.length - 2];

        // Now get the quantity - the ingredientId is used as the id of the quantity input field
        var quantity = $("#" + ingredientId).val();

        // Now stick the quantity on the end of the url
        var link = this.href + "/" + quantity;

        // Go to it...
        window.location.href = link;
    }

})(jQuery);


