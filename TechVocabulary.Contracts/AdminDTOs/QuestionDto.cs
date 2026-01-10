public class QuestionDto
{
    public string CodeSnippet { get; set; }  // The "question"
    public List<string> Options { get; set; } // 4 topic names
    public string CorrectOption { get; set; } // The correct topic name
}
