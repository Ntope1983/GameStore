using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
public class Order
{
    public int Id { get; set; }
    [Required]
    public User OrderUser { get; set; }
    [Required]
    public List<Game> OrderGames { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}