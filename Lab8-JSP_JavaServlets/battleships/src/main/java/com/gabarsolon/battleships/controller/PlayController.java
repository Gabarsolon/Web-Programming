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
    private DBManager dbManager;

    public PlayController() {
        super();
        isFirstPlayer = true;
        dbManager = new DBManager();
    }

    private void flushForUser(Board board, HttpServletResponse response) throws IOException {
        response.getWriter().print("[");
        for (int i = 0; i < 5; ++i) {
            response.getWriter().print("[");
            for (int j = 0; j < 5; ++j) {
                response.getWriter().print(board.getForPosition(i, j) + ",");
            }
            response.getWriter().print(board.getForPosition(i, 5) + "],");
        }
        response.getWriter().print("[");
        for (int j = 0; j < 5; ++j) {
            response.getWriter().print(board.getForPosition(5, j) + ",");
        }
        response.getWriter().print(board.getForPosition(5, 5) + "]]");
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, java.io.IOException {

        User user = (User)request.getSession().getAttribute("user");

        if (player1 == null || player2 == null) {
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"there should be 2 players connected\"}");
            response.getWriter().flush();
            return;
        }

        User currentUser;
        User otherUser;


        if (user.getId() == player1.getId()) {
            currentUser = player1;
            otherUser = player2;
        } else {
            currentUser = player2;
            otherUser = player1;
        }

        if(currentUser.getHealth() == 0){
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"You Lost!\"}");
            response.getWriter().flush();
            dbManager.deleteBoards();
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
        if(otherUser.getHealth() == 0){
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"You Won!\"}");
            response.getWriter().flush();
            request.getSession().invalidate();
            return;
        }


        response.setContentType("application/json");
        response.getWriter().print("{\"response\":\"success\",\"board\":");

        Board currentUserBoard = dbManager.getBoard(currentUser);
        Board otherUserBoard = dbManager.getBoard(otherUser);

        flushForUser(currentUserBoard, response);
        response.getWriter().print(",\"opponent\":");
        flushForUser(otherUserBoard, response);

        response.getWriter().print("}");

    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        User user = (User) request.getSession().getAttribute("user");
        System.out.println(user.getId());
        if (player1 == null) {
            player1 = user;
            if(!dbManager.checkIfBoardExists(player1))
                dbManager.addBoard(player1);
        }
        else if(player2 == null && user.getId() != player1.getId()){
            player2 = user;
            if(!dbManager.checkIfBoardExists(player2))
                dbManager.addBoard(player2);
        }

        User currentUser;
        User otherUser;

        if (user.getId() == player1.getId()) {
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

            Board otherUserBoard = dbManager.getBoard(otherUser);

            Integer x = Integer.parseInt(request.getParameter("x"));
            Integer y = Integer.parseInt(request.getParameter("y"));

            if(!Board.checkValidPosition(x, y)){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"position out of table\"}");
                response.getWriter().flush();
                return;
            }

            if(otherUserBoard.getForPosition(x,y) >= 2){
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"position already attacked\"}");
                response.getWriter().flush();
                return;
            }

            if (otherUser.getShipsAdded() != 2) {
                response.setContentType("application/json");
                response.getWriter().print("{\"response\":\"other player didn't select yet\"}");
                response.getWriter().flush();
                return;
            }
            if (user.getShipsAdded() != 2) {
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

            Boolean hasAttackedPositionWithShipOnIt = otherUserBoard.attack(x, y);
            if(hasAttackedPositionWithShipOnIt)
                otherUser.setHealth(otherUser.getHealth()-1);
            isFirstPlayer = !isFirstPlayer;
            response.setContentType("application/json");
            response.getWriter().print("{\"response\":\"success\"}");
            response.getWriter().flush();
            dbManager.updateBoard(otherUser, otherUserBoard);
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

            Board currentUserBoard = dbManager.getBoard(currentUser);

            if (currentUser.getShipsAdded() != 2) {
                boolean addResult = currentUserBoard.addShip(x, y, orientationVal);
                if(addResult){
                    response.setContentType("application/json");
                    response.getWriter().print("{\"response\":\"success\"}");
                    response.getWriter().flush();
                    currentUser.setShipsAdded(currentUser.getShipsAdded() + 1);
                    dbManager.updateBoard(currentUser, currentUserBoard);
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