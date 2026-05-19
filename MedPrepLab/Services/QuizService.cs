using MedPrepLab.Enums;
using MedPrepLab.Models;

namespace MedPrepLab.Services
{
    // Serviciu care contine logica de verificare si evaluare a testelor.
    public class QuizService
    {
        // Verifica daca raspunsul utilizatorului este corect.
        public bool CheckAnswer(Question question, string? givenAnswer)
        {
            // Daca intrebarea lipseste sau raspunsul este gol, raspunsul este considerat gresit.
            // In felul acesta aplicatia nu se blocheaza.
            if (question == null || string.IsNullOrWhiteSpace(givenAnswer))
            {
                return false;
            }

            if (question.Type == QuestionType.FillInBlank)
            {
                // Pentru completare, ignoram literele mari/mici si spatiile de la capete.
                string correctAnswer = question.CorrectTextAnswer.Trim().ToLower();
                string userAnswer = givenAnswer.Trim().ToLower();

                return correctAnswer == userAnswer;
            }

            // Pentru multiple choice si true/false, raspunsul este ID-ul variantei selectate.
            // int.TryParse previne erorile daca raspunsul nu este un numar.
            if (!int.TryParse(givenAnswer, out int selectedOptionId))
            {
                return false;
            }

            var selectedOption = question.Options
                .FirstOrDefault(option => option.Id == selectedOptionId);

            return selectedOption != null && selectedOption.IsCorrect;
        }

        // Evalueaza intreg testul si calculeaza scorul.
        public QuizResult EvaluateQuiz(
            List<Question> questions,
            Dictionary<int, string> userAnswers,
            string chapter)
        {
            var result = new QuizResult
            {
                TotalQuestions = questions.Count,
                Chapter = chapter
            };

            foreach (var question in questions)
            {
                // Daca utilizatorul nu a raspuns la o intrebare,
                // folosim string.Empty ca sa evitam null.
                string givenAnswer = userAnswers != null && userAnswers.ContainsKey(question.Id)
                    ? userAnswers[question.Id]
                    : string.Empty;

                bool isCorrect = CheckAnswer(question, givenAnswer);

                if (isCorrect)
                {
                    result.CorrectAnswers++;
                }

                result.Answers.Add(new UserAnswer
                {
                    QuestionId = question.Id,
                    GivenAnswer = givenAnswer,
                    IsCorrect = isCorrect
                });
            }

            if (result.TotalQuestions > 0)
            {
                result.ScorePercentage = Math.Round(
                    (double)result.CorrectAnswers / result.TotalQuestions * 100,
                    2
                );
            }

            result.Status = GetResultStatus(result.ScorePercentage);

            return result;
        }

        // Transforma scorul intr-un status usor de inteles.
        private ResultStatus GetResultStatus(double score)
        {
            if (score >= 85)
            {
                return ResultStatus.Excellent;
            }

            if (score >= 60)
            {
                return ResultStatus.Good;
            }

            return ResultStatus.NeedsPractice;
        }
    }
}