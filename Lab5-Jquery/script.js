jQuery(function () {
    $("#button-main-form").on("click", function () {
        $("#modal-window, #overlay").css("display", "block");
        $("#main-form :input, #button-main-form").prop("disabled", true);
    });

    $("#button-modal-form").on("click", function () {
        $(this).parent().css("display", "none");
        $("#overlay").css("display", "none");

        let fieldToChangeID = "#name";
        if (Math.floor(Math.random() * 2)) {
            fieldToChangeID = "#league";
        }

        let fieldsConcat = "";
        $("#modal-form :input").each(function () {
            fieldsConcat += $(this).val();
        });

        $("#main-form").children(fieldToChangeID).val(fieldsConcat);
        $("#modal-form").trigger("reset");

        $("#main-form :input, #button-main-form").prop("disabled", false);
    });

});