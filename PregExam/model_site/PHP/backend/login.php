<?php
    header('Access-Control-Allow-Origin: *');

    session_start();

    $con = mysqli_connect("localhost", "root", "", "exam_sample");
    
    if(!$con){
        die('Could not connect: ' . mysql_error());
    }

    $name = mysqli_real_escape_string($con, $_GET["name"]);
    $result = mysqli_query($con, "SELECT id FROM softwaredeveloper WHERE BINARY name='$name'");
    $row = mysqli_fetch_array($result);

    if(!$row){
        echo "fail";
        exit;
    }
    else{
        $_SESSION["username"] = $name;
        echo "success";
    }

    mysqli_close($con);
?>