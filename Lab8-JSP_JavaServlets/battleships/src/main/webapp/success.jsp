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
        var updateUserBoard = (userBoard) => {
            for (var i = 0; i < 6; ++i) {
                for (var j = 0; j < 6; ++j) {
                    if (userBoard[i][j] === 3) {
                        $("#id_" + (j * 6 + i)).css("background-color", "red");
                    }
                    if (userBoard[i][j] === 2) {
                        $("#id_" + (j * 6 + i)).css("background-color", "yellow");
                    }
                    if (userBoard[i][j] === 1) {
                        $("#id_" + (j * 6 + i)).css("background-color", "green");
                    }
                }
            }
        }
        var updateOpponentBoard = (opponentBoard) => {
            for (var i = 0; i < 6; ++i) {
                for (var j = 0; j < 6; ++j) {
                    if (opponentBoard[i][j] === 3) {
                        $("#id2_" + (j * 6 + i)).css("background-color", "red");
                    }
                    if (opponentBoard[i][j] === 2) {
                        $("#id2_" + (j * 6 + i)).css("background-color", "yellow");
                    }
                    if(opponentBoard[i][j] === 1){
                        $("#id_" + (j * 6 + i)).css("background-color", "green");
                    }
                }
            }
        }
        $.post("/PlayController");

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
        var id;
        var myFunction = function() {
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
        id = setInterval(myFunction, 1000);
    });
</script>
<%
    out.print("<div class=\"container\">");
    out.print("<div class=\"board\">");
    out.print("<p>Your Board</p>");
    out.print("<table>");
    for (int i = 0; i < 7; ++i) {
        out.print("<tr>");
        for (int j = 0; j < 7; ++j) {
            if (i != 0 && j != 0) {
                out.print("<td id=\"id_" + ((i - 1) * 6 + (j - 1)) + "\">&nbspo&nbsp</td>");
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
    out.print("<table>");
    for (int i = 0; i < 7; ++i) {
        out.print("<tr>");
        for (int j = 0; j < 7; ++j) {
            if (i != 0 && j != 0) {
                out.print("<td id=\"id2_" + ((i - 1) * 6 + (j - 1)) + "\">&nbspo&nbsp</td>");
            }else {
                out.print("<td>&nbsp" + max(max(i - 1, j - 1), 0) + "&nbsp</td>");
            }
        }
        out.print("</tr>");
    }
    out.print("</table>");
    out.print("</div>");
    out.print("</div>");
    for (int i = 0; i < 15; ++i) {
        out.print("<br>");
    }

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