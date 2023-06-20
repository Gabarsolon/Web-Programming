package com.example.ex9.controller;

import com.example.ex9.domain.Order;
import com.example.ex9.model.DBManager;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;
import java.security.KeyPair;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@WebServlet(name="OrderController", value="/order")
public class OrderController extends HttpServlet {
    private DBManager dbManager;

    @Override
    public void init() throws ServletException {
        dbManager = new DBManager();
    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String checkout = req.getParameter("checkout");
        String username = (String)req.getSession().getAttribute("username");
        List<Integer[]> basket = (List<Integer[]>)req.getSession().getAttribute("basket");
        PrintWriter out = resp.getWriter();
        out.println("<a href=\"display_products.jsp\">Display products</a>");

        if(basket == null){
            out.println("Your basket is empty");
        }

        if(checkout.equals("finalize")){
            basket.forEach(productIdQuantityPair->{
                Integer productID = productIdQuantityPair[0];
                Integer quantity = productIdQuantityPair[1];
                Order order = new Order(username, productID, quantity);
                dbManager.addOrder(order);
            });

            out.println("Command finalized successfully");
        }
        else{
            req.getSession().setAttribute("basket", null);
            out.println("Command canceled successfully");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Integer productID = Integer.parseInt(req.getParameter("productID"));
        Integer quantity = Integer.parseInt(req.getParameter("quantity"));

        if(req.getSession().getAttribute("basket") == null){
            List<Integer[]> productsQuantity = new ArrayList<>();
            productsQuantity.add(new Integer[]{productID, quantity});
            req.getSession().setAttribute("basket", productsQuantity);
        }else{
            ((List<Integer[]>)req.getSession().getAttribute("basket")).add(new Integer[]{productID, quantity});

        }
        req.getRequestDispatcher("display_products").forward(req, resp);
    }
}
