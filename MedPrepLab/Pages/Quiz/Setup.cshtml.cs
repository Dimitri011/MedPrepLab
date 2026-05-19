using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedPrepLab.Pages.Quiz
{
    public class SetupModel : PageModel
    {
        private readonly LessonService _lessonService;

        public List<string> Chapters { get; set; } = new();

        [BindProperty]
        public string SelectedChapter { get; set; } = "Mixed";

        [BindProperty]
        public int QuestionCount { get; set; } = 5;

        public SetupModel(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public void OnGet()
        {
            Chapters = _lessonService.GetChapters();
        }

        public IActionResult OnPost()
        {
            if (QuestionCount <= 0)
            {
                QuestionCount = 5;
            }

            return RedirectToPage("/Quiz/Start", new
            {
                chapter = SelectedChapter,
                count = QuestionCount
            });
        }
    }
}