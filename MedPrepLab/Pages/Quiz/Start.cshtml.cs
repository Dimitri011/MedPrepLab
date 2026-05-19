using MedPrepLab.Models;
using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MedPrepLab.Pages.Quiz
{
    public class StartModel : PageModel
    {
        private readonly QuestionService _questionService;
        private readonly QuizService _quizService;
        private readonly ProgressService _progressService;

        public List<Question> Questions { get; set; } = new();

        [BindProperty]
        public string Chapter { get; set; } = "Mixed";

        [BindProperty]
        public Dictionary<int, string> Answers { get; set; } = new();

        public StartModel(
            QuestionService questionService,
            QuizService quizService,
            ProgressService progressService)
        {
            _questionService = questionService;
            _quizService = quizService;
            _progressService = progressService;
        }

        public void OnGet(string chapter = "Mixed", int count = 5)
        {
            Chapter = chapter;

            // random questions pt test
            Questions = _questionService.GetRandomQuestions(chapter, count);

            // save Id in session
   
            var questionIds = Questions.Select(question => question.Id).ToList();

            HttpContext.Session.SetString(
                "CurrentQuestionIds",
                JsonSerializer.Serialize(questionIds)
            );
        }

        public IActionResult OnPost()
        {
            // Citim din sesiune intrebarile care au fost afisate in test.
            string? questionIdsJson = HttpContext.Session.GetString("CurrentQuestionIds");

            if (string.IsNullOrEmpty(questionIdsJson))
            {
                return RedirectToPage("/Quiz/Setup");
            }

            var questionIds = JsonSerializer.Deserialize<List<int>>(questionIdsJson) ?? new List<int>();

            Questions = _questionService
                .GetAllQuestions()
                .Where(question => questionIds.Contains(question.Id))
                .ToList();

            // Evaluam testul pe baza raspunsurilor primite din formular.
            var result = _quizService.EvaluateQuiz(Questions, Answers, Chapter);

            // Salvam rezultatul in progress.json.
            _progressService.SaveResult(result);

            // Salvam ultimul rezultat in sesiune ca sa il putem afisa pe pagina Result.
            HttpContext.Session.SetString(
                "LastResult",
                JsonSerializer.Serialize(result)
            );

            return RedirectToPage("/Quiz/Result");
        }
    }
}