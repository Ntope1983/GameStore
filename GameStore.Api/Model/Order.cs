using System.ComponentModel.DataAnnotations;

public class Order
{
    // Primary Key
    public int Id { get; set; }

    // Foreign Key προς User
    // Δηλώνει σε ποιον χρήστη ανήκει η Order
    [Required]
    public int UserId { get; set; }

    // Navigation Property
    // Η Order ανήκει σε έναν User
    // = null! λέμε στον compiler ότι
    // το EF Core θα το γεμίσει αργότερα
    public User User { get; set; } = null!;

    // Navigation Property (Many-to-Many)
    // Μία Order μπορεί να περιέχει πολλά Games
    // Το EF Core θα δημιουργήσει αυτόματα
    // ενδιάμεσο join table
    public ICollection<Game> Games { get; set; } = new List<Game>();

    // Ημερομηνία δημιουργίας Order
    // UtcNow για universal time
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}