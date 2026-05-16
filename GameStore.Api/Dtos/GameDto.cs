public class GameDto
{
    public string GameName { get; set; } = string.Empty;

    public string? GameCategory { get; set; }

    public decimal? GamePrice { get; set; }

    public DateOnly? GameDate { get; set; }
}