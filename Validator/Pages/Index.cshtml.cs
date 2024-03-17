using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Validator.Domain.Model;
using Validator.Domain.Repository;

namespace Validator.Pages;

public class IndexModel(ILogger<IndexModel> logger, ITextRepository textRepository) : PageModel
{
    public IActionResult OnPost(string text)
    {
        logger.LogDebug("{text}", text);
        
        Text textModel = new() {
            Rank = CalculateRank(text),
            Similarity = CalculateSimilarity(text),
            Content = text
        };
        textRepository.Store(textModel);

        return Redirect($"summary?id={textModel.Id}");
    }
    
    private int CalculateSimilarity(string text)
    {
        return textRepository.GetAll().Find(textModel => textModel.Content == text) != null ? 1 : 0;
    }
    
    private static double CalculateRank(string text)
    {   
        var alphabetSymbolsCount = 0;
        var chEnum = text.GetEnumerator();
        while (chEnum.MoveNext())
        {
            if (char.IsLetter(chEnum.Current))
            {
                ++alphabetSymbolsCount;
            }
        }
        chEnum.Dispose();
        return alphabetSymbolsCount / text.Length;
    }
}
