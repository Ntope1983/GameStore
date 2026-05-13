public interface IGameService
{
    IEnumerable<Game> GetAllGames();
    Game? GetGameById(int id);
    void AddGame(string name, string category, decimal price, DateOnly date);
    void DeleteGame(int id);
    void UpdateGame(int id, string? name, string? category, decimal? price, DateOnly date);
}