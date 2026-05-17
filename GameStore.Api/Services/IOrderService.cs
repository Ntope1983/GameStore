public interface IOrderService
{
    List<Order> GetAllOrders();

    Order? GetOrderById(int id);

    void AddOrder(Order Order);


    void DeleteOrder(int id);


}