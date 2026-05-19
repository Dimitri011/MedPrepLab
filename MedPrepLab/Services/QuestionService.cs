using MedPrepLab.Models;

namespace MedPrepLab.Services
{
    public class QuestionService
    {
        private readonly JsonDataService _jsonDataService;

        public QuestionService(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
        }

        public List<Question> GetAllQuestions()
        {
            return _jsonDataService.ReadListFromJson<Question>("questions.json");
        }

        public List<Question> GetQuestionsByChapter(string chapter)
        {
            return GetAllQuestions()
                .Where(question => question.Chapter == chapter)
                .ToList();
        }

        // random gen intrb pt un test
        public List<Question> GetRandomQuestions(string chapter, int count)
        {
            var questions = chapter == "Mixed"
                ? GetAllQuestions()
                : GetQuestionsByChapter(chapter);

            return questions
                .OrderBy(question => Guid.NewGuid())
                .Take(count)
                .ToList();
        }
    }
}