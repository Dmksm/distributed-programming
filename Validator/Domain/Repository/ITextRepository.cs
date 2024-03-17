namespace Validator.Domain.Repository;

using Model;

public interface ITextRepository
{
    public void Store(Text text);
    public Text? Get(string id);
    public List<Text?> GetAll();
}