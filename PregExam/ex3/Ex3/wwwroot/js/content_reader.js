// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQuery(() => {
    $("#check-content").on("click", () => {
        lastID = $("#check-content").val();
        $.get(
            "CheckNewContent",
            { lastID: lastID },
            function (data) {
                console.log(data);
                var projects = JSON.parse(data);

                var list = $("<ul>");
                projects.forEach(project => {
                    list.append($("<li>").text(
                        "Id -> " + project.id +
                        " Date -> " + project.date +
                        " Title -> " + project.title +
                        " Description -> " + project.description +
                        " URL -> " + project.url));
                });
           
                $("#main-div").empty().append(list);
            }
        )
    });
});