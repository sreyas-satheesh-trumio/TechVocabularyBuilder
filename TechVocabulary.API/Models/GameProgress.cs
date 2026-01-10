using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GameProgress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GameId { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public EndUser User { get; set; }

    [Required]
    public int TopicId { get; set; }

    [ForeignKey(nameof(TopicId))]
    public Topic Topic { get; set; }

    [Required]
    public int Score { get; set; }   // 1 = correct, 0 = wrong

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
}
