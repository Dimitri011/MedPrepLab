using MedPrepLab.Models;
using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MedPrepLab.Pages.Quiz
{
    public class ResultModel : PageModel
    {
        private readonly QuestionService _questionService;

        public QuizResult? Result { get; set; }

        public List<Question> Questions { get; set; } = new();

        public ResultModel(QuestionService questionService)
        {
            _questionService = questionService;
        }

        public void OnGet()
        {
            string? resultJson = HttpContext.Session.GetString("LastResult");

            if (!string.IsNullOrEmpty(resultJson))
            {
                Result = JsonSerializer.Deserialize<QuizResult>(resultJson);

                if (Result != null)
                {
                    var questionIds = Result.Answers.Select(answer => answer.QuestionId).ToList();

                    Questions = _questionService
                        .GetAllQuestions()
                        .Where(question => questionIds.Contains(question.Id))
                        .ToList();
                }
            }
        }

        public string GetUserAnswerText(Question question)
        {
            if (Result == null)
            {
                return "N/A";
            }

            var userAnswer = Result.Answers
                .FirstOrDefault(answer => answer.QuestionId == question.Id);

            if (userAnswer == null || string.IsNullOrWhiteSpace(userAnswer.GivenAnswer))
            {
                return "Nu ai raspuns";
            }

            if (question.Type == MedPrepLab.Enums.QuestionType.FillInBlank)
            {
                return userAnswer.GivenAnswer;
            }

            if (int.TryParse(userAnswer.GivenAnswer, out int optionId))
            {
                return question.Options
                    .FirstOrDefault(option => option.Id == optionId)
                    ?.Text ?? "Raspuns necunoscut";
            }

            return "Raspuns necunoscut";
        }

        public bool WasCorrect(Question question)
        {
            if (Result == null)
            {
                return false;
            }

            return Result.Answers
                .FirstOrDefault(answer => answer.QuestionId == question.Id)
                ?.IsCorrect ?? false;
        }
    }
}