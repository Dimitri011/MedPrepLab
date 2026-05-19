using MedPrepLab.Enums;

namespace MedPrepLab.Models
{
    public class QuizResult
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public int TotalQuestions { get; set; }

        public int CorrectAnswers { get; set; }

        public double ScorePercentage { get; set; }

        public string Chapter { get; set; } = string.Empty;

        public ResultStatus Status { get; set; }

        public List<UserAnswer> Answers { get; set; } = new();
    }
}