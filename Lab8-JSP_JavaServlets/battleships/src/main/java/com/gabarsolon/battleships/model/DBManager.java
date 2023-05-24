package com.gabarsolon.battleships.model;

import com.gabarsolon.battleships.domain.User;

import java.sql.*;
import java.util.ArrayList;

/**
 * Created by forest.
 */
public class DBManager {
    private Connection con;
    private Statement stmt;

    public DBManager() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            con = DriverManager.getConnection("jdbc:mysql://localhost/battleships", "root", "");
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


    private void addBoard(User user) {
        try {

            for (int i = 0; i < 6; ++i) {
                for (int j = 0; j < 6; ++j) {
                    String query = "INSERT INTO Board (playerId, x, y, val) VALUES (?, ?, ?, ?)";
                    PreparedStatement stmt = con.prepareStatement(query);
                    stmt.setDouble(1, user.getId());
                    stmt.setInt(2, i);
                    stmt.setInt(3, j);
                    stmt.setInt(4, 0);

                    stmt.execute();
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private void updateBoard(User user) {
        try {

            for (int i = 0; i < 6; ++i) {
                for (int j = 0; j < 6; ++j) {

                    String query = "UPDATE Board SET val = ? WHERE playerId = ? AND x = ? AND y = ?";
                    PreparedStatement stmt = con.prepareStatement(query);
                    stmt.setInt(1, user.board.getForPosition(i, j));
                    stmt.setDouble(2, user.getId());
                    stmt.setInt(3, i);
                    stmt.setInt(4, j);

                    stmt.execute();

                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}