<?php
    header("Access-Control-Allow-Origin: *");
    
    require_once 'project.php';

    session_start();

    $con = mysqli_connect("localhost","root","", "exam_sample");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

    $username = $_SESSION['username'];
    $result = mysqli_query($con, 
        "SELECT name 
         FROM project 
         WHERE members LIKE '$username,%' OR members LIKE '%,$username,%' OR members LIKE '%,$username' OR members LIKE '$username'");
	mysqli_close($con);

    $projects = array();

    while($row = mysqli_fetch_array($result)){
        array_push($projects, $row['name']);   
    }

    echo json_encode($projects);
?>