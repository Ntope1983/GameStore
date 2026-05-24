using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly GameStoreContext _context;

    public OrderService(GameStoreContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrders()
    => _context.Order
        .Include(o => o.Games)  // ← φόρτωσε και τα games
        .Include(o => o.User)   // ← φόρτωσε και τον user
        .ToList();

    public Order? GetOrderById(int id)
        => _context.Order.Find(id);

    public int AddOrder(OrderDtoCreate order)
    {
        var userExists = _context.User.Any(u => u.Id == order.UserId);
        if (!userExists)
            throw new Exception($"User with id {order.UserId} not found");

        List<Game> orderGames = _context.Game
            .Where(g => order.GamesId.Contains(g.Id))
            .ToList();

        Order newOrder = new Order();
        newOrder.UserId = order.UserId;
        newOrder.Games = orderGames;

        _context.Order.Add(newOrder);
        _context.SaveChanges();
        return newOrder.Id;
    }

    public void DeleteOrder(int id)
    {
        var Order = _context.Order.Find(id);
        if (Order == null) return;

        _context.Order.Remove(Order);
        _context.SaveChanges();
    }

}