using Microsoft.EntityFrameworkCore;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Game => Set<Game>();
    public DbSet<User> User => Set<User>();
    public DbSet<Order> Order => Set<Order>();
}