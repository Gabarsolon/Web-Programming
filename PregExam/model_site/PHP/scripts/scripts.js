jQuery(() => {
    $("#login-button").on("click", () => {
        $.get(
            "backend/login.php",
            {name : $("#name").val()},
            function(data, status){
                console.log(status);
                if(data == "success"){
                    window.location.href="manage.html";
                }
                else{
                    alert("Invalid credentials!");
                }
            });
    })


    $("#see-all-projects-button").on("click", () => {
        $.get(
            "backend/get_all_projects.php",
            function(data, status){
                console.log(data);
                var projects = JSON.parse(data);

                console.log(projects);
                var table = $("<table>");
                var tableHeader = $("<thead>").append(
                    $("<tr>").append(
                        $("<th>").text("ID"),
                        $("<th>").text("ProjectManagerID"),
                        $("<th>").text("Name"),
                        $("<th>").text("Description"),
                        $("<th>").text("Members")
                    )
                );
                table.append(tableHeader); 
                
                projects.forEach(project => {
                    var row = $("<tr>").append(
                        $("<td>").text(project.id),
                        $("<td>").text(project.project_manager_id),
                        $("<td>").text(project.name),
                        $("<td>").text(project.description),
                        $("<td>").text(project.members)
                    );
                    table.append(row);
                });
                $("#main-div").empty().append(table);               
            }
        )
    });

    $("#see-personal-projects-button").on("click", () => {
        $.get(
            "backend/get_personal_projects.php",
            function(data, status){
                console.log(data);
                var projects = JSON.parse(data);

                var list = $("<ul>");
                projects.forEach(projectName => {
                    list.append($("<li>").text(projectName))
                });
                
                $("#main-div").empty().append(list);
            }
        )
    })

    $("#assign-projects-to-dev-button").on("click", () => {
        $.post(
            "backend/assign_dev_to_projects.php",
            {dev_name: $("#dev-name").val(),
             projects_list: $("#projects-list").val()
            },
            function(data, status){
                alert(data);
            }
        )
    })
})