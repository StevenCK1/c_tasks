namespace GSA_Server.utils
{
    public class MyFileReader : IMyFileReader
    {
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}