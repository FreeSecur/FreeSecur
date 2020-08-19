namespace FreeSecur.API.Core
{
    public interface ISerializer
    {
        T Deserialize<T>(string json);
        string Serialize<T>(T value);
    }
}