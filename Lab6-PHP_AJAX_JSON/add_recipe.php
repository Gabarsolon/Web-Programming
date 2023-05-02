<?php 
    $con = mysqli_connect("localhost", "root", "", "food_recipes");
    if(!$con){
        die('Could not connect: ' . mysql_error());
    }
    
	$result = mysql_query(
        "INSERT INTO Recipes(author, name, type, time, servings, ingredients, method)
        VALUES(" . $_POST['author'] . ',' . $_POST['name']
    );
?>