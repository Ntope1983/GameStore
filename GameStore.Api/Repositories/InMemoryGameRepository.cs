public class InMemoryGameRepository : IGameRepository
{
    private readonly List<Game> _game = new();

    public IEnumerable<Game> GetGames() => _game;

    public Game? GetById(int id) => _game.FirstOrDefault(p => p.Id == id);

    public void Add(Game game)
    {
        _game.Add(game);
        Console.WriteLine($"The Game {game.GameName} with id {game.Id} has been added.");
    }

    public void DeleteById(int id)
    {
        var game = GetById(id);
        if (game != null)
        {
            _game.Remove(game);
            Console.WriteLine($"The Game with id {id} has been removed.");
        }
        else
        {
            Console.WriteLine($"The Game with id {id} was not found.");
        }
    }


    public void Update(Game game)
    {
        var existing = GetById(game.Id);
        if (existing == null) return;
        existing.GameName = game.GameName;
        existing.GamePrice = game.GamePrice;
        Console.WriteLine($"The Game with id {existing.Id} has been Updated.");
    }
}