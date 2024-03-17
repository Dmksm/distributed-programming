using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Validator.Domain.Model;
using Validator.Domain.Repository;

namespace Validator.Pages;
public class SummaryModel : PageModel
{
    private readonly ILogger<SummaryModel> _logger;
    private readonly ITextRepository _textRepository;

    public SummaryModel(ILogger<SummaryModel> logger, ITextRepository textRepository)
    {
        _logger = logger;
        _textRepository = textRepository;
    }

    public double Rank { get; set; }
    public double Similarity { get; set; }

    public void OnGet(string id)
    {
        _logger.LogDebug(id);

        Text? text = _textRepository.Get(id);

        Rank = 0;
        Similarity = 0;
        if (text != null)
        {
            Rank = text.Rank;
            Similarity = text.Similarity;
        }
        //TODO: проинициализировать свойства Rank и Similarity значениями из БД
    }
}
