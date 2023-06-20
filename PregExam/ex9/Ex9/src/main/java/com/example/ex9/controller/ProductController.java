package com.example.ex9.controller;

import com.example.ex9.domain.Product;
import com.example.ex9.model.DBManager;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.List;

@WebServlet(name = "ProductController", value = "/product")
public class ProductController extends HttpServlet {
    DBManager dbManager;

    @Override
    public void init() throws ServletException {
        dbManager = new DBManager();
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String productName = request.getParameter("name");
        List<Product> productList = dbManager.getProducts(productName);
        request.setAttribute("productList", productList);
        request.getRequestDispatcher("display_products.jsp").forward(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        Product product = new Product(request.getParameter("name"), request.getParameter("description"));
        dbManager.addProduct(product);
        response.sendRedirect("display_products.jsp");
    }
}
