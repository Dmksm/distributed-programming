namespace Validator.Domain.Repository;

using System.Text.Json;
using StackExchange.Redis;
using Model;

public class TextRepository(IDatabase database) : ITextRepository
{
    private const string RankKey = "RANK";
    private const string SimilarityKey = "SIMILARITY";
    private const string IdsKey = "IDS";
    private const string TextKey = "TEXT";

    public void Store(Text text) {
        text.Id ??= Guid.NewGuid().ToString();
        
        database.StringSet($"{RankKey}-{text.Id}", text.Rank);
        database.StringSet($"{SimilarityKey}-{text.Id}", text.Similarity);
        database.StringSet($"{TextKey}-{text.Id}", text.Content);

        var idsList = GetIds();
        idsList.Add(text.Id);
        database.StringSet(IdsKey, JsonSerializer.Serialize(idsList));
    }

    public Text? Get(string id) {
        var content = database.StringGet($"{TextKey}-{id}");
        if (content.IsNull) {
            return null;
        }

        return new Text {
            Id = id,
            Rank = (double) database.StringGet($"{RankKey}-{id}"),
            Similarity = (int) database.StringGet($"{SimilarityKey}-{id}"),
            Content = content.ToString(),
        };
    }

    public List<Text?> GetAll() 
    {
        List<Text?> res = [];
        res.AddRange(GetIds().Select(Get));

        return res;
    }

    private List<string> GetIds()
    {
        var ids = database.StringGet(IdsKey);
        var stringList = database.StringGet(IdsKey);
        return stringList.IsNull || ids.IsNullOrEmpty
            ? []
            : JsonSerializer.Deserialize<List<string>>(stringList);
    }
}