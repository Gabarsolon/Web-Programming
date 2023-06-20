package com.example.ex9.controller;

import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

@WebServlet(name = "MainController", value = "/main")
public class MainController extends HttpServlet {
    public void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, java.io.IOException{

        String username = request.getParameter("username");
        request.getSession().setAttribute("username", username);
        response.sendRedirect("add_product.jsp");
    }
}
