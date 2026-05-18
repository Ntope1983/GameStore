public interface IGameService
{
    List<Game> GetAllGames();
    Game? GetGameById(int id);
    int AddGame(GameDto game);
    void DeleteGame(int id);
    void UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date);
}