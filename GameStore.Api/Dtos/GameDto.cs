// input DTOs
public record GameDtoCreate(string Name, string GameCategory, decimal? GamePrice, DateOnly? GameDate);
public record UserDtoCreate(string Username, string Email);
public record OrderDtoCreate(int UserId, List<int> GamesId);
// Output DTOs (νέα)
public record GameDtoResponse(int Id, string GameName, string? GameCategory, decimal? GamePrice, DateOnly? GameDate);
public record UserDtoResponse(int Id, string Username, string Email);
public record OrderDtoResponse(int Id, int UserId, string Username, DateTime CreatedAt, List<GameDtoResponse> Games);