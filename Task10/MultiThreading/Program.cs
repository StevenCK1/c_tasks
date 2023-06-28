using System.Diagnostics;
using FuzzySharp;

namespace MultiThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VM names = new VM();
            Helpers _helpers = new Helpers();
            
            var results = _helpers.CompareAndCalcRatio(names);
            var sortedResults = _helpers.SortByScoreHiToLow(results);
            _helpers.PrintToConsole(sortedResults);

        }
    }
}