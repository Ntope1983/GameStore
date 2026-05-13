public class GameDto
{
    public string GameNameDto { get; set; }
    public string GameCategoryDto { get; set; }
    public decimal GamePriceDto { get; set; }
    public DateOnly GameDateDto { get; set; }
    public GameDto(string name, string category, decimal price, DateOnly date)
    {
        GameNameDto = name;
        GameCategoryDto = category;
        GamePriceDto = price;
        GameDateDto = date;
    }

}
