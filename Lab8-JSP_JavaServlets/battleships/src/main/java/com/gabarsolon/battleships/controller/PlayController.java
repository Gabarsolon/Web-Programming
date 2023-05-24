package com.gabarsolon.battleships.controller;

import com.gabarsolon.battleships.domain.Board;
import com.gabarsolon.battleships.domain.User;

import com.gabarsolon.battleships.model.DBManager;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.*;
import java.util.concurrent.TimeUnit;

public class PlayController extends HttpServlet {

    private User player1 = null;
    private User player2 = null;
    private Boolean isFirstPlayer;
    private Connection con = null;
    private DBManager dbManager;

    public PlayController() {
        super();
        isFirstPlayer = true;
        dbManager = new DBManager();
    }


    private void deleteFromDB(User user) {
        try {

            for (int i = 0; i < 6; ++i) {
                for (int j = 0; j < 6; ++j) {
                    String query = "DELETE FROM Board WHERE playerId=" + Double.toString(user.getId());
                    System.out.println(query);
                    Statement stmt = con.createStatement();

                    stmt.execute(query);
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private void flushForUser(User user, HttpServletResponse response) throws IOException {
        response.getWriter().print("[");
        for (int i = 0; i < 5; ++i) {
            response.getWriter().print("[");
            for (int j = 0; j < 5; ++j) {
                response.getWriter().print(user.board.getForPosition(i, j) + ",");
            }
            response.getWriter().print(user.board.getForPosition(i, 5) + "],");
        }
        response.getWriter().print("[");
        for (int j = 0; j < 5; ++j) {
            response.getWriter().print(user.board.getForPosition(5, j) + ",");
        }
        response.getWriter().print(user.board.getForPosition(5, 5) + "]]");
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, java.io.IOException {

        if (player1 == null || player2 == null) {
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"there should be 2 players connected\"}");
            response.getWriter().flush();
            return;
        }

        User currentUser;
        User otherUser;

        if (((PlayData)(request.getSession().getAttribute("PlayData"))).userId == player1.getId()) {
            currentUser = player1;
            otherUser = player2;
        } else {
            currentUser = player2;
            otherUser = player1;
        }

        if(currentUser.board.checkIfBoardIsLost()){
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"You Lost!\"}");
            response.getWriter().flush();
            deleteFromDB(currentUser);
            request.getSession().invalidate();

            //wait for the other user to receive the game over message
            try {
                TimeUnit.SECONDS.sleep(1);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }

            player1=null;
            player2=null;
            isFirstPlayer=true;
            LoginController.resetNrPlayers();
            return;
        }
        if(otherUser.board.checkIfBoardIsLost()){
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"You Won!\"}");
            response.getWriter().flush();
            deleteFromDB(otherUser);
            request.getSession().invalidate();
            return;
        }


        response.setContentType("application/json");
        response.getWriter().print("{\"response\":\"success\",\"board\":");

        flushForUser(currentUser, response);
        response.getWriter().print(",\"opponent\":");
        flushForUser(otherUser, response);

        response.getWriter().print("}");

    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        if (request.getSession().getAttribute("PlayData") == null) {
            PlayData data = new PlayData();
            if (player1 == null) {
                player1 = new User(data.userId);
                addToDb(player1);
            } else {
                player2 = new User(data.userId);
                addToDb(player2);
            }
            request.getSession().setAttribute("PlayData", data);
        }

        User currentUser;
        User otherUser;
        if (((PlayData)(request.getSession().getAttribute("PlayData"))).userId == player1.getId()) {
            currentUser = player1;
            otherUser = player2;
        } else {
            currentUser = player2;
            otherUser = player1;
        }

        if(request.getParameter("orientation") == null) {
            //attack
            if (player1 == null || player2 == null) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"there should be 2 players connected\"}");
                response.getWriter().flush();
                return;
            }

            Integer x = Integer.parseInt(request.getParameter("x"));
            Integer y = Integer.parseInt(request.getParameter("y"));

            if(!Board.checkValidPosition(x, y)){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"position out of table\"}");
                response.getWriter().flush();
                return;
            }

            if(otherUser.board.getForPosition(x,y) >= 2){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"position already attacked\"}");
                response.getWriter().flush();
                return;
            }

            if (otherUser.board.shipsAdded != 2) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"other player didn't select yet\"}");
                response.getWriter().flush();
                return;
            }
            if (currentUser.board.shipsAdded != 2) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"please select ships first\"}");
                response.getWriter().flush();
                return;
            }

            if ((isFirstPlayer && currentUser != player1) || (!isFirstPlayer && currentUser != player2)) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"other player's turn\"}");
                response.getWriter().flush();
                return;
            }

            otherUser.board.attack(x, y);
            isFirstPlayer = !isFirstPlayer;
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"success\"}");
            response.getWriter().flush();
            updateDb(currentUser);
            updateDb(otherUser);
            return;
        } else {
            //position ship
            Integer x = Integer.parseInt(request.getParameter("x"));
            Integer y = Integer.parseInt(request.getParameter("y"));
            String orientation = request.getParameter("orientation");
            Integer orientationVal;

            //check valid position
            if(!Board.checkValidPosition(x,y)) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"position out of table\"}");
                response.getWriter().flush();
                return;
            }

            switch(orientation){
                case "up":
                    orientationVal=0;
                    break;
                case "left":
                    orientationVal=1;
                    break;
                case "down":
                    orientationVal=2;
                    break;
                case "right":
                    orientationVal=3;
                    break;
                default:
                    orientationVal=-1;
            }
            if(orientationVal==-1){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"invalid orientation\"}");
                response.getWriter().flush();
                return;
            }
            if(orientation.equals("left") && x < 2){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"Invalid position for left\"}");
                response.getWriter().flush();
                return;
            }
            if(orientation.equals("right") && x > 3){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"Invalid position for right\"}");
                response.getWriter().flush();
                return;
            }
            if(orientation.equals("up") && y < 2){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"Invalid position for up\"}");
                response.getWriter().flush();
                return;
            }
            if(orientation.equals("down") && y > 3){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"Invalid position for down\"}");
                response.getWriter().flush();
                return;
            }

            if (currentUser.board.shipsAdded != 2) {
                boolean addResult = currentUser.board.addShip(x, y, orientationVal);
                if(addResult){
                    response.setContentType("application/json");
                    response.getWriter().print("{\"response\":\"success\"}");
                    response.getWriter().flush();
                    updateDb(currentUser);
                }
                else{
                    response.setContentType("application/json");
                    response.getWriter().print("{\"response\":\"ships are overlapping\"}");
                    response.getWriter().flush();
                }
            } else {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"there are 2 ships already\"}");
                response.getWriter().flush();
            }
        }
    }

}