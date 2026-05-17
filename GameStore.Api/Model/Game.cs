using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Game
{
    // Primary Key
    public int Id { get; set; }

    // Υποχρεωτικό όνομα παιχνιδιού
    // Μέγιστο μήκος 20 χαρακτήρες
    [Required]
    [MaxLength(20)]
    public string GameName { get; set; } = string.Empty;

    // Προαιρετική κατηγορία παιχνιδιού
    [MaxLength(20)]
    public string? GameCategory { get; set; }

    // Τιμή παιχνιδιού
    // Precision(18,2) = έως 18 ψηφία συνολικά
    // και 2 δεκαδικά
    [Precision(18, 2)]
    public decimal? GamePrice { get; set; }

    // Μόνο ημερομηνία χωρίς ώρα
    // nullable γιατί μπορεί να μην υπάρχει τιμή
    public DateOnly? GameDate { get; set; }

    // Navigation Property
    // Ένα Game μπορεί να υπάρχει σε πολλές Orders
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}