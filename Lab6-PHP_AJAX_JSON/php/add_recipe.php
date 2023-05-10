<?php 
    header("Access-Control-Allow-Origin: *");

    $con = mysqli_connect("localhost", "root", "", "food_recipes");
    if(!$con){
        die('Could not connect: ' . mysql_error());
    }
    
    $author = mysqli_real_escape_string($con, $_POST['author']);
    $name = mysqli_real_escape_string($con, $_POST['name']);
    $type = mysqli_real_escape_string($con, $_POST['type']);
    $prep_time = mysqli_real_escape_string($con, $_POST['prep_time']);
    $servings = mysqli_real_escape_string($con, $_POST['servings']);
    $ingredients = mysqli_real_escape_string($con, $_POST['ingredients']);
    $method = mysqli_real_escape_string($con, $_POST['method']);

	$result = mysqli_query($con, 
        "INSERT INTO Recipes(author, name, type, prep_time, servings, ingredients, method)
        VALUES('$author', '$name', '$type', '$prep_time', '$servings', '$ingredients', '$method');" 
    );
    
    echo "<a href=index.html>Home</a><br>";
    echo "The recipe was added succesfully";

    mysqli_close($con);
?>