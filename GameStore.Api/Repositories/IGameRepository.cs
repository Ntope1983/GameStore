public interface IGameRepository
{
    public IEnumerable<Game> GetGames();
    public Game? GetById(int id);
    public void Add(Game game);
    public void DeleteById(int id);
    public void Update(Game game);
}
