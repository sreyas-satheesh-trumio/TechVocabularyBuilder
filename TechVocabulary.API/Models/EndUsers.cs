using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EndUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain 1 uppercase, 1 lowercase, and 1 digit."
    )]
    public string Password { get; set; }

    [Required]
    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
