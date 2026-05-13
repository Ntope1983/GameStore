public class GameService : IGameService
{
    private readonly IGameRepository _repository;

    public GameService(IGameRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Game> GetAllGames() => _repository.GetGames();

    public Game? GetGameById(int id) => _repository.GetById(id);

    public void AddGame(string name, string category, decimal price, DateOnly date)
    {
        int id = _repository.GetGames().Any()
            ? _repository.GetGames().Max(p => p.GameId) + 1
            : 1;

        var game = new Game(id, name, category, price, date);
        _repository.Add(game);
    }

    public void DeleteGame(int id) => _repository.DeleteById(id);

    public void UpdateGame(int id, string? name, string? category, decimal? price, DateOnly date)
    {
        var game = _repository.GetById(id);
        if (game == null) return;

        if (name != null)
            game.GameName = name;
        if (category != null)
            game.GameCategory = category;
        if (price.HasValue)
            game.GamePrice = price.Value;

    }
}