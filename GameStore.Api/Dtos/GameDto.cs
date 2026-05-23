public record GameDtoCreate(string Name, string GameCategory, decimal? GamePrice, DateOnly? GameDate);
public record UserDtoCreate(string Username, string Email);
public record OrderDtoCreate(int Userid, List<int> GamesId);