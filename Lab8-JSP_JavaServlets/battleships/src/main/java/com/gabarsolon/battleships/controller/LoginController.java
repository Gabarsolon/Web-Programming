package com.gabarsolon.battleships.controller;

import com.gabarsolon.battleships.domain.User;
import com.gabarsolon.battleships.model.DBManager;
import jakarta.servlet.RequestDispatcher;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;

public class LoginController extends HttpServlet {

    private static int nrOfPlayers;
    private DBManager dbManager;
    public LoginController() {
        super();
        dbManager = new DBManager();
        nrOfPlayers = 0;
    }

    public static void resetNrPlayers() {
        nrOfPlayers = 0;
    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {
        String username = request.getParameter("username");
        String password = request.getParameter("password");
        System.out.println(username + " " + password);
        User result = dbManager.authenticate(username, password);
        if(result==null) {
            request.getRequestDispatcher("login-error.jsp")
                    .forward(request, response);
        }
        else {
            RequestDispatcher rd = null;

            if (nrOfPlayers < 2) {
                nrOfPlayers += 1;
                rd = request.getRequestDispatcher("/success.jsp");
                request.getSession().setAttribute("user", result);

            } else {
                rd = request.getRequestDispatcher("/error.jsp");
            }
//            System.out.println(this.players);
            rd.forward(request, response);
        }
    }

}
