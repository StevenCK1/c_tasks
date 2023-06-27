using FuzzySharp;

namespace MultiThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VM _vm = new VM();


            var results = new NamesByScore();

            // Compare each name and calc score
            for (var i = 0; i < _vm.Names.Count; i++)
            {
                var resultToAdd = new NamesAndScore();
                var name = _vm.Names[i].ToString();

                for (var y = 0; y < _vm.Names.Count; y++)
                {
                    var nameComparedAgainst = _vm.Names[y].ToString();
                    if (i != y)
                    {
                        var ratio = Fuzz.Ratio(_vm.Names[i].ToString(), _vm.Names[y].ToString());

                        var namesScore = new NamesAndScore() { name = name, nameCompared = nameComparedAgainst, score = ratio };
                        results.topScores.Add(namesScore);
                    }

                }

            }

           var sortedResults = results.topScores.OrderByDescending(name => name.score).ToList();

            foreach (var result in sortedResults)
            {
                Console.WriteLine($"{result.score}: {result.name} -> {result.nameCompared}");
            }

        }
    }
}