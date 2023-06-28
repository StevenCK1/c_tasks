

using System.Collections.Concurrent;

namespace MultiThreading
{
    public class NamesByScore
    {
        public ConcurrentBag<NamesAndScore> topScores {get; set; } 

        public NamesByScore()
        {
            topScores = new ConcurrentBag<NamesAndScore>();
        }
    }

    public class NamesAndScore
    {
        public string name { get; set; }
        public string nameCompared { get; set; }
        public int score { get; set; }
    }
}
