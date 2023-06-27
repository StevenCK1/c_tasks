using System.Diagnostics;
using FuzzySharp;

namespace MultiThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VM _vm = new VM();
            Stopwatch stopwatch = new Stopwatch();


            var results = new NamesByScore();

            //// Compare each name and calc score

            //stopwatch.Start();

            //for (var i = 0; i < _vm.Names.Count; i++)
            //{
            //    var resultToAdd = new NamesAndScore();
            //    var name = _vm.Names[i].ToString();

            //    for (var y = 0; y < _vm.Names.Count; y++)
            //    {
            //        var nameComparedAgainst = _vm.Names[y].ToString();
            //        if (i != y)
            //        {
            //            var ratio = Fuzz.Ratio(_vm.Names[i].ToString(), _vm.Names[y].ToString());

            //            var namesScore = new NamesAndScore() { name = name, nameCompared = nameComparedAgainst, score = ratio };
            //            results.topScores.Add(namesScore);
            //        }

            //    }

            //}

            //stopwatch.Stop();

            //stopwatch.Start();
            object lockObject = new object(); // Object used as a lock

            Parallel.For(0, _vm.Names.Count, (i) =>
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

                        lock (lockObject) // Lock the critical section
                        {
                            results.topScores.Add(namesScore);
                        }
                    }

                }

            });
            //stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            var sortedResults = results.topScores.OrderByDescending(name => name.score).ToList();

            foreach (var result in sortedResults)
            {
                Console.WriteLine($"{result.score}: {result.name} -> {result.nameCompared}");
            }

        }
    }
}