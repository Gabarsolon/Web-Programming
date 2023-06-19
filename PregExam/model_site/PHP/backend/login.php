<?php
    header("Access-Control-Allow-Origin :*");

    session_start();

    $con = mysqli_connect("localhost", "root", "", "exam_sample");
    if(!$con){
        die('Could not connect: ' . mysqli_close());
    }

    $name = mysqli_real_escape_string($con, $_GET["name"]);
    $result = mysqli_query($con, "SELECT id FROM softwaredeveloper WHERE name=$name");

    if(!$result){
        echo "fail";
    }
    else{
        $row = mysqli_fetch_array($result);
        $_SESSION["user_id"] = $row['id'];
        echo "success";
    }

    mysqli_close($con);
?>