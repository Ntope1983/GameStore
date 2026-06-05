public interface IGameService
{
    Task<List<GameDtoResponse>> GetAllGames();
    Task<GameDtoResponse?> GetGameById(int id);
    Task<int> AddGame(GameDtoCreate game);
    Task DeleteGame(int id);
    Task UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date);

}