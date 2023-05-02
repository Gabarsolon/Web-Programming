<?php
	$con = mysqli_connect("localhost","root","", "food_recipes");
	if (!$con) {
		die('Could not connect: ' . mysql_error());
	}

	$type = $_GET["type"];

	//mysql_query("insert into Students values(3,'aaa','pass',23)");
	$result = mysqli_query($con, "SELECT * FROM Recipes where type LIKE '%" . $type . "%'");

	//echo "<html><body>";
	echo "<table border='1'>
	<tr>
		<th>Modify</th>
		<th>ID</th>
		<th>Author</th>
		<th>Name</th>
		<th>Type</th>
		<th>Preparation Time</th>
		<th>Servings</th>
		<th>Ingredients</th>
		<th>Method</th>
	</tr>";

	while($row = mysqli_fetch_array($result)){
		echo "<tr>";
		echo "<td><a href=\"modify.html?id=" . $row['id'] . "\">Modify</td>";  
		echo "<td>" . $row['id'] . "</td>";
		echo "<td>" . $row['author'] . "</td>";
		echo "<td>" . $row['name'] . "</td>";
		echo "<td>" . $row['type'] . "</td>";
		echo "<td>" . $row['prep_time'] . "</td>";
		echo "<td>" . $row['servings'] . "</td>";
		echo "<td>" . $row['ingredients'] . "</td>";
		echo "<td>" . $row['method'] . "</td>";
		echo "</tr>";
	}
	echo "</table>";
	//echo "</body></html>";
	mysqli_close($con);
?> 