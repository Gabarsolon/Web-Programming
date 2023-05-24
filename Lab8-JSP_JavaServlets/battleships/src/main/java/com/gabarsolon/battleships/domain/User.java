package com.gabarsolon.battleships.domain;

public class User {

    private Integer id;
    private String username;
    private String password;
    private Integer shipsAdded;
    private Integer health;

    public User(Integer id, String username, String password) {
        this.id = id;
        this.username = username;
        this.password = password;
        this.shipsAdded = 0;
        this.health = 1;
    }

    public Integer getId() {
        return id;
    }

    public Integer getShipsAdded() {
        return shipsAdded;
    }

    public Integer getHealth() {
        return health;
    }

    public void setShipsAdded(Integer shipsAdded) {
        this.shipsAdded = shipsAdded;
    }

    public void setHealth(Integer health) {
        this.health = health;
    }
}