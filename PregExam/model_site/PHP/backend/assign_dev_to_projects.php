<?php
    header("Access-Control-Allow-Origin: *");
    
    require_once 'project.php';

    $con = mysqli_connect("localhost","root","", "exam_sample");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

    $dev_name = mysqli_real_escape_string($con, $_POST['dev_name']);
    $projects_list = array_unique(explode(",", mysqli_real_escape_string($con, $_POST['projects_list'])));

    $result = mysqli_query($con, "SELECT * FROM softwaredeveloper WHERE name=BINARY '$dev_name'");
    if(mysqli_num_rows($result) == 0){
        die('There is no developer with the given name');
    }

    foreach($projects_list as $project_name){
        $check_existing_project_query = 
            mysqli_query($con, "SELECT * FROM project WHERE name=BINARY '$project_name'");
        if(mysqli_num_rows($check_existing_project_query) == 0){
            mysqli_query($con, "INSERT INTO project(name, members) VALUES('$project_name', '$dev_name')");
        }
        else{
            mysqli_query($con, "UPDATE project SET members=CONCAT(members, ',' '$dev_name') WHERE name='$project_name'");
        }
    }

    echo "Developer assigned successfully";

    mysqli_close($con);
?>