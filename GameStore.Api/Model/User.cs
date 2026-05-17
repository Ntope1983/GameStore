using System.ComponentModel.DataAnnotations;

public class User
{
    // Primary Key
    public int Id { get; set; }

    // Υποχρεωτικό username
    // Μέγιστο μήκος 15 χαρακτήρες
    [Required]
    [MaxLength(15)]
    public string Username { get; set; } = string.Empty;

    // Υποχρεωτικό email
    [Required]
    public string Email { get; set; } = string.Empty;

    // Navigation Property
    // Ένας User μπορεί να έχει πολλές Orders
    // Αρχικοποιούμε με new() για να μην είναι null
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}