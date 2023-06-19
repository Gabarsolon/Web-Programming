JQuery(() => {
    $("#login-button").on("click", () => {
        $.get(
            "php/login.php",
            {name : $("name").val()},
            function(data, status){
                $("#login-form")
            });
    })
})