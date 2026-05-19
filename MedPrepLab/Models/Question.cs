using MedPrepLab.Enums;

namespace MedPrepLab.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public SubjectType Subject { get; set; }

        public string Chapter { get; set; } = string.Empty;

        public DifficultyLevel Difficulty { get; set; }

        public QuestionType Type { get; set; }

        public List<AnswerOption> Options { get; set; } = new();

        // intrb de completat text
        public string CorrectTextAnswer { get; set; } = string.Empty;

        // explicatie dp test
        public string Explanation { get; set; } = string.Empty;
    }
}