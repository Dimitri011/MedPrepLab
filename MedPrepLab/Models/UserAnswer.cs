namespace MedPrepLab.Models
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }

        public string GivenAnswer { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}