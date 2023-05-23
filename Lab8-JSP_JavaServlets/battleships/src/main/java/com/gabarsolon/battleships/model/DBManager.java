package com.gabarsolon.battleships.model;

import com.gabarsolon.battleships.domain.User;

import java.sql.*;
import java.util.ArrayList;

/**
 * Created by forest.
 */
public class DBManager {
    private Statement stmt;

    public DBManager() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            Connection con = DriverManager.getConnection("jdbc:mysql://localhost/battleships", "root", "");
            stmt = con.createStatement();
        } catch(Exception ex) {
            System.out.println("eroare la connect:"+ex.getMessage());
            ex.printStackTrace();
        }
    }

    public User authenticate(String username, String password) {
        ResultSet rs;
        User u = null;
        System.out.println(username+" "+password);
        try {
            rs = stmt.executeQuery("select * from users where user='"+username+"' and password='"+password+"'");
            if (rs.next()) {
                u = new User(rs.getInt("id"), rs.getString("user"), rs.getString("password"));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return u;
    }
}