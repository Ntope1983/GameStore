public interface IOrderService
{
    Task<List<Order>> GetAllOrders();

    Task<Order?> GetOrderById(int id);

    Task<int> AddOrder(OrderDtoCreate Order);


    Task DeleteOrder(int id);


}