namespace TechVocabulary.Contracts.AdminDTOs
{
public class CreateTopicDto
{
    public string TopicName { get; set; }
    public string Definition { get; set; }
    public string RealWorldUsage { get; set; }
    public string? CodeSnippet { get; set; }
    public int AdminUserId { get; set; }
}
}
