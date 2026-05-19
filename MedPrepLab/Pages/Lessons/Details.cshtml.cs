using MedPrepLab.Models;
using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedPrepLab.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly LessonService _lessonService;

        public Lesson? Lesson { get; set; }

        public DetailsModel(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public void OnGet(int id)
        {
            Lesson = _lessonService.GetLessonById(id);
        }
    }
}