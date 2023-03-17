using NUnit.Framework;
// using Task3_GenericDictionary;
// using Phonebook;

namespace testSuite
{
    private Phonebook _phonebook;
    public class Tests
    {
        

        [SetUp]
        public void Setup()
        {
            _phonebook = new Phonebook();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}