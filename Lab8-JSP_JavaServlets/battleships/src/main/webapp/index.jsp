<%--
  Created by IntelliJ IDEA.
  User: forest
  Date: 4/29/2021
  Time: 12:41 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>

<html>
<head>
  <meta charset="UTF-8">
  <title>Insert title here</title>
  <style>
    form {
      margin-left: auto;
      margin-right: auto;
      width: 400px;
    }
  </style>
</head>
<body>
<form action="LoginController" method="post">
  Enter userrrname : <input type="text" name="username"> <BR>
  Enter password : <input type="password" name="password"> <BR>
  <input type="submit" value="Login"/>
</form>
</body></html>