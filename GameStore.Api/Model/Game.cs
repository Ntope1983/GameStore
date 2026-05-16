using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Game
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string GameName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? GameCategory { get; set; }

    [Precision(18, 2)]
    public decimal? GamePrice { get; set; }

    public DateOnly? GameDate { get; set; }
}