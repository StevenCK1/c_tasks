namespace GSA_Server.Core.utils
{
    public interface IMyFileReader
    {
        public string[] ReadAllLines(string path);
    }
}