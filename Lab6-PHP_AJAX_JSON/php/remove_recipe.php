<?php
	$con = mysqli_connect("localhost","root","", "food_recipes");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

	$id = mysqli_real_escape_string($con, $_GET["id"]);
    $result = mysqli_query($con, "DELETE FROM Recipes where id=$id");

    echo "<a href=index.html>Home</a><br>";
    echo "The recipe was removed succesfully";

	mysqli_close($con);
?> 