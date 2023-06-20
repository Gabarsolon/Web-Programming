<%@ page import="java.util.List" %><%--
  Created by IntelliJ IDEA.
  User: Turcu
  Date: 20/06/2023
  Time: 18:32
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
  <h1>Checkout</h1>

<%-- Retrieve the list of orders from the session --%>
    <%
        List<Integer[]> basket = (List<Integer[]>) request.getSession().getAttribute("basket");
    %>

    <%-- Display the list of orders --%>
    <%
        if (basket == null || basket.isEmpty()) {
            out.println("No orders in the basket.");
        } else {
            out.println("<h2>Order Summary:</h2>");
            for (Integer[] item : basket) {
                Integer productID = item[0];
                Integer quantity = item[1];
                out.println("Product ID: " + productID + ", Quantity: " + quantity + "<br>");
            }
        }
    %>

  <form action="/order" method="get">
      <label for="finalize">Finalize Command</label>
      <input id="finalize" type="radio" name="checkout" checked="true" value="finalize"><br>
      <label for="cancel">Cancel Command</label>
      <input id="cancel" type="radio" name="checkout" value="cancel"><br>
      <input type="submit" value="Confirm selection">
  </form>
</body>
</html>
