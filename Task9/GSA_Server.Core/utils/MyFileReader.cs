namespace GSA_Server.Core.utils
{
    public class MyFileReader : IMyFileReader
    {
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}