using FuzzySharp;

namespace MultiThreading
{
    public class Helpers
    {

       public Helpers() { }

        public NamesByScore CompareAndCalcRatio(VM vm)
        {
            var results = new NamesByScore();

            object lockObject = new object(); // Object used as a lock

            Parallel.For(0, vm.Names.Count, (i) =>
                {
                    var resultToAdd = new NamesAndScore();
                    var name = vm.Names[i].ToString();

                    for (var y = 0; y < vm.Names.Count; y++)
                    {
                        var nameComparedAgainst = vm.Names[y].ToString();
                        if (i != y)
                        {
                            var ratio = Fuzz.Ratio(vm.Names[i].ToString(), vm.Names[y].ToString());

                            var namesScore = new NamesAndScore() { name = name, nameCompared = nameComparedAgainst, score = ratio };

                            lock (lockObject) // Lock the critical section to ensure thread safety (modifying the list concurrently from multiple thread leads to errors because of the state)
                            {
                                results.topScores.Add(namesScore);
                            }
                        }

                    }

                });
            return results;
        }

        public List<NamesAndScore> SortByScoreHiToLow(NamesByScore namesAndScores)
        {
            return namesAndScores.topScores.OrderByDescending(name => name.score).ToList();
        }

        public void PrintToConsole(List<NamesAndScore> namesByScores)
        {
            foreach (var item in namesByScores)
            {
                Console.WriteLine($"{item.score}: {item.name} -> {item.nameCompared}");
            }

        }
    }
}
