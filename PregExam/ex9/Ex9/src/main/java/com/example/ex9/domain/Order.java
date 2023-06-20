package com.example.ex9.domain;

public class Order {
    private Integer id;
    private String user;
    private Integer productID;
    private Integer quantity;

    public Order(Integer id, String user, Integer productID, Integer quantity) {
        this.id = id;
        this.user = user;
        this.productID = productID;
        this.quantity = quantity;
    }

    public Integer getId() {
        return id;
    }

    public String getUser() {
        return user;
    }

    public Integer getProductID() {
        return productID;
    }

    public Integer getQuantity() {
        return quantity;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public void setUser(String user) {
        this.user = user;
    }

    public void setProductID(Integer productID) {
        this.productID = productID;
    }

    public void setQuantity(Integer quantity) {
        this.quantity = quantity;
    }
}
