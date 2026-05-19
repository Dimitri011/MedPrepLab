using MedPrepLab.Models;

namespace MedPrepLab.Services
{
    public class ProgressService
    {
        private readonly JsonDataService _jsonDataService;

        public ProgressService(JsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
        }

        // citeste progresul salvat în progress.json.
        public StudentProgress GetProgress()
        {
            var progressList = _jsonDataService.ReadListFromJson<StudentProgress>("progress.json");

            return progressList.FirstOrDefault() ?? new StudentProgress();
        }

        // saves un rezultat nou si actualizează statisticile.
        public void SaveResult(QuizResult result)
        {
            var progress = GetProgress();

            progress.Results.Add(result);
            progress.TestsCompleted = progress.Results.Count;

            progress.AverageScore = Math.Round(
                progress.Results.Average(r => r.ScorePercentage),
                2
            );

            progress.BestChapter = progress.Results
                .GroupBy(r => r.Chapter)
                .OrderByDescending(group => group.Average(r => r.ScorePercentage))
                .FirstOrDefault()?.Key ?? "N/A";

            progress.WeakestChapter = progress.Results
                .GroupBy(r => r.Chapter)
                .OrderBy(group => group.Average(r => r.ScorePercentage))
                .FirstOrDefault()?.Key ?? "N/A";

            _jsonDataService.WriteListToJson(
                "progress.json",
                new List<StudentProgress> { progress }
            );
        }
    }
}