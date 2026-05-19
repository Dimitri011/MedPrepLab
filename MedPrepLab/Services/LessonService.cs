using MedPrepLab.Models;

namespace MedPrepLab.Services
{
    public class LessonService
    {
        private readonly JsonDataService _jsonDataService;

        public LessonService(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
        }

        public List<Lesson> GetAllLessons()
        {
            return _jsonDataService.ReadListFromJson<Lesson>("lessons.json");
        }

        public Lesson? GetLessonById(int id)
        {
            return GetAllLessons().FirstOrDefault(lesson => lesson.Id == id);
        }

        public List<string> GetChapters()
        {
            return GetAllLessons()
                .Select(lesson => lesson.Chapter)
                .Distinct()
                .ToList();
        }
    }
}