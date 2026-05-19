namespace MedPrepLab.Models
{
    public class StudentProgress
    {
        public int TestsCompleted { get; set; }

        public double AverageScore { get; set; }

        public string BestChapter { get; set; } = "N/A";

        public string WeakestChapter { get; set; } = "N/A";

        public List<QuizResult> Results { get; set; } = new();
    }
}