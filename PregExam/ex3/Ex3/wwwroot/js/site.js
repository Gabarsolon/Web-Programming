// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQuery(() => {
    var contents = [];

    $("#save-content").on("click", () => {
        const title = $("#title").val();
        const description = $("#description").val();
        const url = $("#url").val();

        contents.push({ title: title, description: description, url: url });
        $("#title").val("");
        $("#description").val("");
        $("#url").val("");

        alert("Content saved temporarly");
        console.log(contents);
    })

    $("#add-content").on("click", () => {
        $.post(
            "AddNewContent",
            { contents: contents },
            function (data) {
                alert("Everything was added to the server");
                contents = [];
            }
        )
    })
});