public interface IGameService
{
    List<Game> GetAllGames();
    Game? GetGameById(int id);
    void AddGame(Game game);
    void DeleteGame(int id);
    void UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date);
}