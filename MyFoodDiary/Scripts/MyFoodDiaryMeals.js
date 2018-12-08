var products = (function ($) {

    $(".DeleteProductLink").on("click", DeleteLinkClick);

    function DeleteLinkClick(e) {

        if (confirm("Delete?"))
            return window.location.href = this.href;

        e.preventDefault();
    }

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

})(jQuery);


