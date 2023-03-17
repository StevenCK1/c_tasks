using NUnit.Framework;
using FileApp;

namespace TestSuite
{
    public class Tests
    {
        // Why not working? main project name is FileSearcher
        private FileSearcher _fileSearcher;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}