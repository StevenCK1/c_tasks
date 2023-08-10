using System.Collections.Concurrent;

namespace MultiThread2_phonebook
{
    public interface IFileHelpers
    {
        void LoadFromFile(ConcurrentDictionary<string, long> dict);
        void SaveToFile(ConcurrentDictionary<string, long> dict);
    }
}
