namespace FreeSecur.Core
{
    public interface IFsSerializer
    {
        T Deserialize<T>(string json);
        string Serialize<T>(T value);
    }
}