<%@ page import="static java.lang.Math.max" %>
<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Play Game</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.4.1.js" integrity="sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU=" crossorigin="anonymous"></script>
</head>

<body>
<style>
    table, th, td {
        border: 1px solid black;
        border-collapse: collapse;
        float: left;
        margin: 5px;
    }

    .board {
        float: left;
        padding: 15px;
    }

</style>
<script>
    $(document).ready(function() {
        let updateUserBoard = (userBoard) => {
            for (var i = 0; i < 6; ++i) {
                for (var j = 0; j < 6; ++j) {
                    let color = "";
                    switch(userBoard[i][j]) {
                        case 1:
                            color = "green";
                            break;
                        case 2:
                            color = "yellow"
                            break;
                        case 3:
                            color = "red"
                        default:
                            break;
                    }
                    $("#user-board tr:eq(" + (j + 1) + ") td:eq(" + (i + 1) + ")").css("background-color", color);
                }
            }
        }
        let updateOpponentBoard = (opponentBoard) => {
            for (var i = 0; i < 6; ++i) {
                for (var j = 0; j < 6; ++j) {
                    let color = "";
                    switch(opponentBoard[i][j]) {
                        case 2:
                            color = "yellow"
                            break;
                        case 3:
                            color = "red"
                        default:
                            break;
                    }
                    $("#opponent-board tr:eq(" + (j + 1) + ") td:eq(" + (i + 1) + ")").css("background-color", color);
                }
            }
        }
        let id;
        let updateBoards = function() {
            $.get("/PlayController", function (response) {
                if(response["response"] === "success" || response["response"] === "only one player connected")
                    updateUserBoard(response["board"]);
                if(response["response"] === "success")
                    updateOpponentBoard(response["opponent"]);
                if(response["response"] === "You Won!" || response["response"] === "You Lost!"){
                    clearInterval(id);
                    alert(response["response"]);
                    window.location.href = 'index.jsp';
                }
            });
        };
        id = setInterval(updateBoards, 1000);

        $.post(
            "/PlayController",
            function(response){
                if(response["response"] === "not logged in")
                    window.location.href = "login-error.jsp";
            }
        );

        $("#submit_position").click(function() {
            var x = $("#x_position").val();
            var y = $("#y_position").val();
            var orientation = $("#orientation").val();
            $.post(
                "/PlayController/placeShip",
                {
                    x, y, orientation
                },
                function(response) {
                if (response["response"] !== "success")
                    alert(response["response"]);
                updateBoards();
            });
        });
        $("#submit_attack").click(function() {
            var x = $("#x_attack").val();
            var y = $("#y_attack").val();
            $.post("/PlayController/attackShip",
                {
                    x, y
                },
                function (response) {
                if (response["response"] !== "success")
                    alert(response["response"]);
            });
        });
    });
</script>
<%
    out.print("<div class=\"container\">");
    out.print("<div class=\"board\">");
    out.print("<p>Your Board</p>");
    out.print("<table id=\"user-board\">");
    for (int i = 0; i < 7; ++i) {
        out.print("<tr>");
        for (int j = 0; j < 7; ++j) {
            if (i != 0 && j != 0) {
                out.print("<td>&nbspo&nbsp</td>");
            }else {
                out.print("<td>&nbsp" + max(max(i - 1, j - 1), 0) + "&nbsp</td>");
            }
        }
        out.print("</tr>");
    }
    out.print("</table>");
    out.print("</div>");

    out.print("<div class=\"board\">");
    out.print("<p>Opponent Board</p>");
    out.print("<table id=\"opponent-board\">");
    for (int i = 0; i < 7; ++i) {
        out.print("<tr>");
        for (int j = 0; j < 7; ++j) {
            if (i != 0 && j != 0) {
                out.print("<td>&nbspo&nbsp</td>");
            }else {
                out.print("<td>&nbsp" + max(max(i - 1, j - 1), 0) + "&nbsp</td>");
            }
        }
        out.print("</tr>");
    }
    out.print("</table>");
    out.print("</div>");
    out.print("</div>");

%>

<div class="container">
    <div class="board">
        <p>Add Ship</p>
        x:<br>
        <input type="text" name="x" id="x_position"><br>
        y:<br>
        <input type="text" name="y" id="y_position"><br>
        orientation:<br>
        <input type="text" name="orientation" id="orientation"><br>
        <button id="submit_position">submit</button>
        *Input one of these options: up, left, down, right*
    </div>
    <div class="board">
        <p>Attack Ship</p>
        x:<br>
        <input type="text" name="x" id="x_attack"><br>
        y:<br>
        <input type="text" name="y" id="y_attack"><br>
        <button id="submit_attack">submit</button>
    </div>
</div>
</body>
</html>