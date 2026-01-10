using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TopicLearned
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TopicLearnedId { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public EndUser User { get; set; }

    [Required]
    public int TopicId { get; set; }

    [ForeignKey(nameof(TopicId))]
    public Topic Topic { get; set; }

    public DateTime LearnedAt { get; set; } = DateTime.UtcNow;
}
