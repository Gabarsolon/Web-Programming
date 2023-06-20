package com.example.ex9.model;

import com.example.ex9.domain.Product;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class DBManager {
    private Connection connection;

    public DBManager(){
        connect();
    }
    private void connect(){
        try{
            Class.forName("com.mysql.cj.jdbc.Driver");
            connection = DriverManager.getConnection("jdbc:mysql://localhost/ex9", "root", "");

        }catch (Exception ex){
            System.out.println(ex);
        }
    }
    public void addProduct(Product product){
        try{
            PreparedStatement statement = connection.prepareStatement(
                    "INSERT INTO Products(name, description) VALUES(?, ?)"
            );
            statement.setString(1, product.getName());
            statement.setString(2, product.getDescription());

            statement.execute();
        }catch (SQLException sqlException){
            System.out.println(sqlException);
        }
    }
    public List<Product> getProducts(String name) {
        try {
            List<Product> products = new ArrayList<>();
            PreparedStatement statement = connection.prepareStatement(
                    "SELECT * FROM products WHERE name LIKE ?"
            );

            statement.setString(1, name + "%");

            ResultSet resultSet = statement.executeQuery();

            while (resultSet.next()) {
                products.add(new Product(
                        resultSet.getInt("id"),
                        resultSet.getString("name"),
                        resultSet.getString("description")
                ));
            }

            resultSet.close();
            return products;
        } catch (SQLException sqlException) {
            System.out.println(sqlException);
        }
        return null;
    }
}
