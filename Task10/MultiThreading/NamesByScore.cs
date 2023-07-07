namespace MultiThreading
{
    public class NamesByScore
    {
        public List<NamesAndScore> topScores {get; set; } 

        public NamesByScore()
        {
            topScores = new List<NamesAndScore>();
        }
    }

    public class NamesAndScore
    {
        public string name { get; set; }
        public string nameCompared { get; set; }
        public int score { get; set; }
    }
}
