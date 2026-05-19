using MedPrepLab.Enums;

namespace MedPrepLab.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public SubjectType Subject { get; set; }

        public string Chapter { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public DifficultyLevel Difficulty { get; set; }
    }
}