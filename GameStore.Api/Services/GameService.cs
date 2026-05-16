public class GameService : IGameService
{
    private readonly GameStoreContext _context;

    public GameService(GameStoreContext context)
    {
        _context = context;
    }

    public List<Game> GetAllGames()
        => _context.Games.ToList();

    public Game? GetGameById(int id)
        => _context.Games.Find(id);

    public void AddGame(Game game)
    {
        _context.Games.Add(game);
        _context.SaveChanges();
    }

    public void DeleteGame(int id)
    {
        var game = _context.Games.Find(id);
        if (game == null) return;

        _context.Games.Remove(game);
        _context.SaveChanges();
    }

    public void UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date)
    {
        var game = _context.Games.Find(id);
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