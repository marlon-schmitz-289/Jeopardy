namespace BoardJeopardy;

public enum QuestionType
{
    Text,
    Audio,
    Image
}

public class Question
{
    public Question (string question, string answer, QuestionType type = QuestionType.Text)
    {
        Questi = question;
        Answer = answer;
        QuType = type;
    }
    
    
    public string Questi { get; set; }
    public string Answer { get; set; }
    public QuestionType QuType { get; set; }
}