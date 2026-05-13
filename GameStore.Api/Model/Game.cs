using System.Text.Json.Serialization;

public class Game
{
    [JsonPropertyName("gameId")]
    public int GameId { get; set; }
    [JsonPropertyName("gameName")]
    public string GameName { get; set; }
    [JsonPropertyName("gameCategory")]
    public string GameCategory { get; set; }
    [JsonPropertyName("gamePrice")]
    public decimal GamePrice { get; set; }
    [JsonPropertyName("gameDate")]
    public DateOnly GameDate { get; set; }
    public Game() { }
    public Game(int id, string name, string catecory, decimal price, DateOnly date)
    {
        GameId = id;
        GameName = name;
        GameCategory = catecory;
        GamePrice = price;
        GameDate = date;

    }

}
