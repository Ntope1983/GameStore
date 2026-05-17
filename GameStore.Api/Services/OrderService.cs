public class OrderService : IOrderService
{
    private readonly GameStoreContext _context;

    public OrderService(GameStoreContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrders()
        => _context.Order.ToList();

    public Order? GetOrderById(int id)
        => _context.Order.Find(id);

    public void AddOrder(Order Order)
    {
        _context.Order.Add(Order);
        _context.SaveChanges();
    }

    public void DeleteOrder(int id)
    {
        var Order = _context.Order.Find(id);
        if (Order == null) return;

        _context.Order.Remove(Order);
        _context.SaveChanges();
    }

}