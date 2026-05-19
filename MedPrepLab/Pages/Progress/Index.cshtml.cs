using MedPrepLab.Models;
using MedPrepLab.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedPrepLab.Pages.Progress
{
    public class IndexModel : PageModel
    {
        private readonly ProgressService _progressService;

        public StudentProgress Progress { get; set; } = new();

        public IndexModel(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void OnGet()
        {
            Progress = _progressService.GetProgress();
        }
    }
}