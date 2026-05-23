public interface IOrderService
{
    List<Order> GetAllOrders();

    Order? GetOrderById(int id);

    int AddOrder(OrderDtoCreate Order);


    void DeleteOrder(int id);


}