<%--
  Created by IntelliJ IDEA.
  User: Turcu
  Date: 20/06/2023
  Time: 12:53
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <ul>
        <li><a href="add_product.jsp">Add product</a></li>
        <li><a href="index.jsp">Change username</a></li>
        <li><a href="display_products.jsp">Show products</a></li>
        <li><a href="checkout.jsp">Checkout</a></li>
    </ul><br>

    <h1>Add a new product</h1>
    <form action="/product" method="post">
        <label for="name">Name:</label>
        <input type="text" name="name"><br>
        <label for="description">Description:</label>
        <input type="description"><br>
        <input type="submit" value="Add product">
    </form>
</body>
</html>
