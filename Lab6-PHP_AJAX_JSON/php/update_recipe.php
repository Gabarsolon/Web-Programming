<?php 
    header("Access-Control-Allow-Origin: *");

    $con = mysqli_connect("localhost", "root", "", "food_recipes");
    if(!$con){
        die('Could not connect: ' . mysql_error());
    }
    
    $json = file_get_contents('php://input');
    $recipe = json_decode($json, true);

    $id = mysqli_real_escape_string($con, $recipe['id']);
    $author = mysqli_real_escape_string($con, $recipe['author']);
    $name = mysqli_real_escape_string($con, $recipe['name']);
    $type = mysqli_real_escape_string($con, $recipe['type']);
    $prep_time = mysqli_real_escape_string($con, $recipe['prep_time']);
    $servings = mysqli_real_escape_string($con, $recipe['servings']);
    $ingredients = mysqli_real_escape_string($con, $recipe['ingredients']);
    $method = mysqli_real_escape_string($con, $recipe['method']);

	$result = mysqli_query($con, 
        "UPDATE Recipes
         SET author='$author', name='$name', type='$type', prep_time='$prep_time', servings=$servings, ingredients='$ingredients', method='$method' 
         WHERE id=$id"
    );
    
    echo "The recipe was updated succesfully";

    mysqli_close($con);
?>