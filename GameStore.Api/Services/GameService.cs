using Microsoft.EntityFrameworkCore;

public class GameService : IGameService
{
    private readonly GameStoreContext _context;

    public GameService(GameStoreContext context)
    {
        _context = context;
    }

    public async Task<List<GameDtoResponse>> GetAllGames()
    => await _context.Game
        .Select(g => new GameDtoResponse(
            g.Id,
            g.GameName,
            g.GameCategory,
            g.GamePrice,
            g.GameDate))
        .ToListAsync();

    public async Task<GameDtoResponse?> GetGameById(int id)
    {
        Game? result = await _context.Game.FirstOrDefaultAsync(g => g.Id == id);
        if (result is not null)
        {
            return new GameDtoResponse(result.Id, result.GameName, result.GameCategory, result.GamePrice, result.GameDate);
        }
        return null;
    }

    public async Task<int> AddGame(GameDtoCreate game)
    {

        Game Newgame = new Game();
        Newgame.GameName = game.Name;
        Newgame.GameCategory = game.GameCategory;
        Newgame.GameDate = game.GameDate;
        Newgame.GamePrice = game.GamePrice;
        await _context.Game.AddAsync(Newgame);
        await _context.SaveChangesAsync();
        return Newgame.Id;
    }

    public async Task DeleteGame(int id)
    {
        var game = _context.Game.Find(id);
        if (game == null) return;

        _context.Game.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGame(int id, string name, string? category, decimal? price, DateOnly? date)
    {
        var game = await _context.Game.FindAsync(id);
        if (game == null) return;

        if (!string.IsNullOrWhiteSpace(name))
            game.GameName = name;

        if (!string.IsNullOrWhiteSpace(category))
            game.GameCategory = category;

        if (price.HasValue)
            game.GamePrice = price.Value;

        game.GameDate = date;

        await _context.SaveChangesAsync();
    }
}