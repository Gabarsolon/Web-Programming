<?php
    header("Access-Control-Allow-Origin: *");
    
    require_once 'project.php';

    $con = mysqli_connect("localhost","root","", "exam_sample");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

    $result = mysqli_query($con, "SELECT * FROM project");
	mysqli_close($con);

    $projects = array();

    while($row = mysqli_fetch_array($result)){
        $project = new Project(
            $row['id'],
            $row['ProjectManagerID'],
            $row['name'],
            $row['description'],
            $row['members']
        );
        array_push($projects, $project);   
    }

    echo json_encode($projects);
?>