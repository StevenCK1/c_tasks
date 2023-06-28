using Moq;
using MultiThreading;

namespace unitTests
{
    public class Tests
    {
        private VM _vm;
        private Helpers _helpers;   

        [SetUp]
        public void Setup()
        {
            _vm = new VM();
            _helpers = new Helpers();
  
        }

        [Test]
        public void CompareAndCalcRatio_ReturnsEmptyResult_WhenNoNamesProvided()
        {
            // Arrange
            _vm.Names = new List<string>();

            // Act
            var result = _helpers.CompareAndCalcRatio(_vm);

            // Assert
            Assert.That(result.topScores.Count, Is.EqualTo(0));
        }

        [Test]
        public void CompareAndCalcRatio_ReturnsCorrectResult()
        {
            // Arrange
            _vm.Names = new List<string>() { "John", "Jane" };

            // Act
            var result = _helpers.CompareAndCalcRatio(_vm);

            // Assert
            Assert.That(result.topScores.Count, Is.EqualTo(2));
            Assert.That(result.topScores[0].name, Is.EqualTo("John"));
            Assert.That(result.topScores[0].nameCompared, Is.EqualTo("Jane"));
            Assert.That(result.topScores[0].score, Is.EqualTo(50));
        }

        [Test]
        public void SortByScoreHiToLow_ReturnsSortedNames()
        {
            // Arrange
            var namesAndScores = new NamesByScore();
            namesAndScores.topScores.Add(new NamesAndScore { name = "John", nameCompared = "Jane", score = 50 });
            namesAndScores.topScores.Add(new NamesAndScore { name = "Alice", nameCompared = "Bob", score = 0 });

            // Act
            var result = _helpers.SortByScoreHiToLow(namesAndScores);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].name, Is.EqualTo("John"));
            Assert.That(result[0].nameCompared, Is.EqualTo("Jane"));
            Assert.That(result[0].score, Is.EqualTo(50));
            Assert.That(result[1].name, Is.EqualTo("Alice"));
            Assert.That(result[1].nameCompared, Is.EqualTo("Bob"));
            Assert.That(result[1].score, Is.EqualTo(0));
        }
    }
}