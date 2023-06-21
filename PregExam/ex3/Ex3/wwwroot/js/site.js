// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQuery(() => {
    function checkNewPost() {
        $.get(
            "CheckNewPost",
            function (product, status) {
                console.log(product);
                if (product != null) {
                    alert("New post has been added\n" +
                        "User: " + product.user + "\n" +
                        "TopicID: " + product.topicID + "\n" +
                        "Text: " + product.text + "\n" +
                        "DateTime: " + product.date + "\n"
                    );
                }
                console.log(product);
            }
       )
    }

    setInterval(checkNewPost, 1000);
});