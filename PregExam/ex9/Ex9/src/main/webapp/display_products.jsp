<%@ page import="com.example.ex9.domain.Product" %>
<%@ page import="java.util.List" %><%--
  Created by IntelliJ IDEA.
  User: Turcu
  Date: 20/06/2023
  Time: 13:47
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<html>
<head>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
<ul>
    <li><a href="add_product.jsp">Add product</a></li>
    <li><a href="index.jsp">Change username</a></li>
    <li><a href="display_products.jsp">Show products</a></li>
</ul><br>

<h1>Display products that begin with a given name</h1>
<form action="/product" method="get">
    <label for="name">Name of the product:</label>
    <input type="text" name="name">
    <input type="submit" value="Get products">
</form>

<% List<Product> productList = (List<Product>)request.getAttribute("productList"); %>.
<c:if test="${not empty productList}">
    <h2>Product Search Results</h2>
    <table>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
        <c:forEach var="product" items="${productList}">
            <tr>
                <td>${product.name}</td>
                <td>${product.description}</td>
            </tr>
        </c:forEach>
    </table>
</c:if>

<%--<% if (productList != null && !productList.isEmpty()) { %>--%>
<%--<h2>Product Search Results</h2>--%>
<%--<table>--%>
<%--    <tr>--%>
<%--        <th>Name</th>--%>
<%--        <th>Description</th>--%>
<%--    </tr>--%>
<%--    <% for (Product product : productList) { %>--%>
<%--    <tr>--%>
<%--        <td><%= product.getName() %></td>--%>
<%--        <td><%= product.getDescription() %></td>--%>
<%--    </tr>--%>
<%--    <% } %>--%>
<%--</table>--%>
<%--<% } %>--%>

</body>
</html>
