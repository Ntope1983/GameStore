public class GameService : IGameService
{
    private readonly GameStoreContext _context;

    public GameService(GameStoreContext context)
    {
        _context = context;
    }

    public List<Game> GetAllGames()
        => _context.Game.ToList();

    public Game? GetGameById(int id)
        => _context.Game.Find(id);

    public void AddGame(Game game)
    {
        _context.Game.Add(game);
        _context.SaveChanges();
    }

    public void DeleteGame(int id)
    {
        var game = _context.User.Find(id);
        if (game == null) return;

        _context.User.Remove(game);
        _context.SaveChanges();
    }

    public void UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date)
    {
        var game = _context.Game.Find(id);
        if (game == null) return;

        if (!string.IsNullOrWhiteSpace(name))
            game.GameName = name;

        if (!string.IsNullOrWhiteSpace(category))
            game.GameCategory = category;

        if (price.HasValue)
            game.GamePrice = price.Value;

        game.GameDate = date;

        _context.SaveChanges();
    }
}