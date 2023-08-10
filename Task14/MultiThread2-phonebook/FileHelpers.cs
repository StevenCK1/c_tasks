using System.Collections.Concurrent;
using MultiThread2_phonebook;

namespace MultiThread_phonebook
{
    public class FileHelpers : IFileHelpers
    {
        public const string Path = "phonebook.txt";

        public void LoadFromFile(ConcurrentDictionary<string, long> _phonebook)
        {
            string[] lines = File.ReadAllLines(Path);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2 && long.TryParse(parts[1], out long number))
                {
                    _phonebook[parts[0]] = number;
                }
            }
        }

        public void SaveToFile(ConcurrentDictionary<string, long> _phonebook)
        {
            // easier to do list and add to list
            string[] lines = new string[_phonebook.Count];
            int i = 0;
            foreach (KeyValuePair<string, long> entry in _phonebook)
            {
                lines[i] = $"{entry.Key},{entry.Value}";
                i++;
            }
            File.WriteAllLines(Path, lines);
        }
    }
}
