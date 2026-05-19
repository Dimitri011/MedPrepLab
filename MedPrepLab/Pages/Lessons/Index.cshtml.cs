using MedPrepLab.Models;
using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedPrepLab.Pages.Lessons
{
    public class IndexModel : PageModel
    {
        private readonly LessonService _lessonService;

        public List<Lesson> Lessons { get; set; } = new();

        public IndexModel(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public void OnGet()
        {
            Lessons = _lessonService.GetAllLessons();
        }
    }
}