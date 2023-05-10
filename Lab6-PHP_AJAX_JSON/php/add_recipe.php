<?php 
    header('Access-Control-Allow-Origin: *');

    $con = mysqli_connect("localhost", "root", "", "food_recipes");
    if(!$con){
        die('Could not connect: ' . mysql_error());
    }
    
    $json = file_get_contents('php://input');
    $recipe = json_decode($json, true);

    $author = mysqli_real_escape_string($con, $recipe['author']);
    $name = mysqli_real_escape_string($con, $recipe['name']);
    $type = mysqli_real_escape_string($con, $recipe['type']);
    $prep_time = mysqli_real_escape_string($con, $recipe['prep_time']);
    $servings = mysqli_real_escape_string($con, $recipe['servings']);
    $ingredients = mysqli_real_escape_string($con, $recipe['ingredients']);
    $method = mysqli_real_escape_string($con, $recipe['method']);

	$result = mysqli_query($con, 
        "INSERT INTO Recipes(author, name, type, prep_time, servings, ingredients, method)
        VALUES('$author', '$name', '$type', '$prep_time', '$servings', '$ingredients', '$method');" 
    );

    echo "The recipe was added succesfully";

    mysqli_close($con);
?>