namespace WebShop.Services
{
    public interface IFileReader
    {
        bool Exists(string path);
        string ReadAllText(string path);
    }
}