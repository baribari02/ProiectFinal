using System.Collections.Generic;

public interface IDataStorage
{
    void SaveProducts(List<Product> products, string filePath);
    List<Product> LoadProducts(string filePath);

    void SaveUsers(List<User> users, string filePath);
    List<User> LoadUsers(string filePath);

    void SaveOrders(List<Order> orders, string filePath);
    List<Order> LoadOrders(string filePath);
}