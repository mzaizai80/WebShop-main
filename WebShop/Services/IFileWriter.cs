namespace WebShop.Services
{
    public interface IFileWriter
    {
        void WriteAllText(string path, string content);
    }
}