var products = (function ($) {

    $('#ValuesArePerItem').on('click', ValuesArePerItemCheckboxClick);
    $('.DeleteProductLink').on('click', DeleteLinkClick);

    ValuesArePerItemCheckboxClick();

    function ValuesArePerItemCheckboxClick() {

        if ($('#ValuesArePerItem').is(":checked")) {
            $("#perItemDiv").html("All values are per item.");
        }
        else {
            $("#perItemDiv").html("All values are per 100 gram serving.");
        }

    }


    function DeleteLinkClick(e) {

        if (confirm("Delete?"))
            return window.location.href = this.href;

        e.preventDefault();
    }

})(jQuery);


