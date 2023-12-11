namespace WebShop.Services
{
    public class FileReader 
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return ""; // Return an empty string if the file does not exist
        }
    }
}