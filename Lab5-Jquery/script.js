jQuery(function(){
    $("#button-main-form").on("click", function(){
        $("#modal-window").css("display", "block");
    });

    $("#button-modal-form").on("click", function(){
        $(this).parent().css("display", "none");
        
        let fieldToChangeID = "#name";
        if(Math.floor(Math.random() * 2)){
            fieldToChangeID = "#league";
        }

        let fieldsConcat = "";
        $("#modal-form :input").each(function(){
            fieldsConcat += $(this).val();
        });
        
        $("#main-form").children(fieldToChangeID).val(fieldsConcat);
    });

});