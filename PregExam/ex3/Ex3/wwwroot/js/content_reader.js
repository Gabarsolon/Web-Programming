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
                if (data.hasOwnProperty('nothing_new')) {
                    alert(data.nothing_new);
                }
                else {
                    var list = $("<ul>");
                    $.each(data, function (i, project) {
                        list.append($("<li>").text(
                            "Id -> " + project.id +
                            " Date -> " + project.date +
                            " Title -> " + project.title +
                            " Description -> " + project.description +
                            " URL -> " + project.url));
                    });

                    $("#contents-div").empty().append(list);
                }
            }
        )
    });
});