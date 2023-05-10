<?php
	require_once 'recipe.php';

	$con = mysqli_connect("localhost","root","", "food_recipes");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

	$type = mysqli_real_escape_string($con ,$_GET["type"]);

	$result = mysqli_query($con, "SELECT * FROM Recipes where type LIKE '%" . $type . "%'");
	mysqli_close($con);
	
	$recipes = array();
	while($row = mysqli_fetch_array($result)){
		$recipe = new Recipe($row['id'], $row['author'], $row['name'], $row['type'],
			$row['prep_time'], $row['servings'], $row['ingredients'], $row['method']);
		array_push($recipes, $recipe);
	}
	

	echo json_encode($recipes);
?> 