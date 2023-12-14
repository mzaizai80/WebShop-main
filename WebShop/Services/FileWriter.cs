using System.IO;

namespace WebShop.Services
{
    public class FileWriter : IFileWriter
    {
        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}


