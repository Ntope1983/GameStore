using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
public class User
{
    
    public int Id { get; set; }
    [Required]
    [MaxLength(15)]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
}