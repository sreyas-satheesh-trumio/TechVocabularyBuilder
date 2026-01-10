using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Topic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TopicId { get; set; }

    [Required]
    [StringLength(100)]
    public string TopicName { get; set; }

    [Required]
    public string Definition { get; set; }

    [Required]
    public string RealWorldUsage { get; set; }

    public string? CodeSnippet { get; set; }

    // Foreign Key → EndUser (Admin)
    [Required]
    public int CreatedBy { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public EndUser CreatedByUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
