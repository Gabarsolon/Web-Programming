<?php
	$con = mysqli_connect("localhost","root","", "food_recipes");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

	$id = mysqli_real_escape_string($con, $_GET["id"]);
    $result = mysqli_query($con, "SELECT * FROM Recipes where id=$id");

    $jsonData = array();
	while ($row = mysqli_fetch_array($result)) {
	    $jsonData['author'] = $row['author'];
	    $jsonData['name'] = $row['name'];
	    $jsonData['type'] = $row['type'];
	    $jsonData['prep_time'] = $row['prep_time'];
	    $jsonData['servings'] = $row['servings'];
	    $jsonData['ingredients'] = $row['ingredients'];
	    $jsonData['method'] = $row['method'];
	}
	echo json_encode($jsonData);

	mysqli_close($con);
?> 