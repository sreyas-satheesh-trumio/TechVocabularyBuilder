public class GameQuestionResponse
{
    public int TopicId { get; set; }
    public string Definition { get; set; }
    public string RealWorldUsage { get; set; }
    public string CodeSnippet { get; set; }
    public List<string> Options { get; set; }
    public bool IsCompleted { get; set; }
}
