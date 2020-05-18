namespace Scripts.Manager.Quiz
{
    public interface IQuizDataAcquireable
    {
        string ProblemText { get; }
        string GetCorrectAnswer { get; }
        string GetWrongAnswer(int index);
    }
}