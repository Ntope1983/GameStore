using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly GameStoreContext _context;

    public OrderService(GameStoreContext context)
    {
        _context = context;
    }

    public async Task<List<GameDtoResponse>> GetAllOrders()
    => await _context.Order
        .Include(o => o.Games)  // ← φόρτωσε και τα games
        .Include(o => o.User)   // ← φόρτωσε και τον user
        .ToListAsync();

    public async Task<Order?> GetOrderById(int id)
     => await _context.Order
         .Include(o => o.Games)
         .Include(o => o.User)
         .FirstOrDefaultAsync(o => o.Id == id); // ← Find() δεν υποστηρίζει Include

    public async Task<int> AddOrder(OrderDtoCreate order)
    {
        var userExists = await _context.User.AnyAsync(u => u.Id == order.UserId);
        if (!userExists)
            throw new Exception($"User with id {order.UserId} not found");

        List<Game> orderGames = await _context.Game
            .Where(g => order.GamesId.Contains(g.Id))
            .ToListAsync();

        Order newOrder = new Order();
        newOrder.UserId = order.UserId;
        newOrder.Games = orderGames;

        await _context.Order.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        return newOrder.Id;
    }

    public async Task DeleteOrder(int id)
    {
        var Order = await _context.Order.FindAsync(id);
        if (Order == null) return;

        _context.Order.Remove(Order);
        await _context.SaveChangesAsync();
    }

}