package com.gabarsolon.battleships.model;

import com.gabarsolon.battleships.domain.Board;
import com.gabarsolon.battleships.domain.User;

import javax.swing.plaf.nimbus.State;
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
        User user = null;
        System.out.println(username+" "+password);
        try {
            rs = stmt.executeQuery("select * from users where user='"+username+"' and password='"+password+"'");

            if (rs.next()) {
                user = new User(rs.getInt("id"), rs.getString("user"), rs.getString("password"));
            }
            rs.close();
            stmt.execute("DELETE FROM board WHERE playerId=" + user.getId());

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return user;
    }

    public boolean checkIfBoardExists(User user){
        ResultSet rs;
        boolean answer = false;
        try {
            rs = stmt.executeQuery("select * from board where playerId=" + user.getId());
            if (rs.next())
                answer = true;
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return answer;
    }
    public Board getBoard(User user){
        ResultSet rs;
        Board b = new Board();
        try {
            rs = stmt.executeQuery("select * from board where playerId=" + user.getId());

            rs.next();
            for(int i=0;i<6;i++)
                for(int j=0;j<6;j++){
                    b.setForPosition(i, j, rs.getInt("val"));
                    rs.next();
                }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return b;
    }

    public void addBoard(User user) {
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

    public void updateBoard(User user, Board board) {
        try {
            stmt.execute("UPDATE users SET health=" + user.getHealth() + ", shipsAdded=" + user.getShipsAdded());
            for (int i = 0; i < 6; ++i) {
                for (int j = 0; j < 6; ++j) {

                    String query = "UPDATE Board SET val = ? WHERE playerId = ? AND x = ? AND y = ?";
                    PreparedStatement stmt = con.prepareStatement(query);
                    stmt.setInt(1, board.getForPosition(i, j));
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

    public void deleteBoards() {
        try {

            for (int i = 0; i < 6; ++i) {
                for (int j = 0; j < 6; ++j) {
                    String query = "DELETE FROM board";
                    Statement stmt = con.createStatement();
                    stmt.execute(query);
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}