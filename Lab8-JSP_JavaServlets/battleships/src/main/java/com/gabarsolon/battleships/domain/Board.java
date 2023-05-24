package com.gabarsolon.battleships.domain;

public class Board {
    public int[][] board;
    public int shipsAdded;
    public int health;

    public Board() {
        board = new int[6][6];
        shipsAdded = 0;
        health=6;
    }

    public int getForPosition(int x, int y) {
        return board[y][x];
    }
    public boolean checkIfBoardIsLost() {return health==0;}
    public static boolean checkValidPosition(int x, int y) {return x >= 0 && y >= 0 && x <= 5 && y <= 5;}

    public boolean addShip(int x, int y, int orientation) {
        int[] nextY = {-1, 0, 1, 0};
        int[] nextX = {0, -1, 0, 1}; //up, left, down, right
        int currentX = x;
        int currentY = y;

        int checkX = x;
        int checkY = y;

        for(int i=0;i<3;i++){
            if(board[checkY][checkX] == 1)
                return false;
            checkX += nextX[orientation];
            checkY += nextY[orientation];
        }

        for(int i = 0; i < 3; ++i) {
            board[currentY][currentX] = 1;
            currentX += nextX[orientation];
            currentY += nextY[orientation];
        }
        shipsAdded += 1;

        return true;
    }

    public void attack(int x, int y) {
        board[y][x] += 2;
        if(board[y][x] == 3)
            health--;
    }
}
