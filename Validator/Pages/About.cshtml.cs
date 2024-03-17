using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Validator.Pages
{
    public class AboutModel(ILogger<AboutModel> logger) : PageModel
    {
        private readonly ILogger<AboutModel> _logger = logger;

        public void OnGet()
        {
        }
    }
}
