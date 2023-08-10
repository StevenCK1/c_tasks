using Moq;
using MultiThread_phonebook;
using MultiThread2_phonebook;

namespace Multithread2_phonebook_Tests
{
    public class Tests
    {
        private Mock<IFileHelpers> _fileHelperMock;
        private Phonebook _phonebook;

        [SetUp]
        public void Setup()
        {
            _fileHelperMock = new Mock<IFileHelpers>();
            _phonebook = new Phonebook(_fileHelperMock.Object);
        }

        [Test]
        public void store10kNumbers()
        {
            // Arrange
            var defaultName = "name";

            //Act
            Parallel.For(0, 10000, (index) =>
            {
                var currentNum = 100000000 + index;
                var currentName = defaultName + currentNum.ToString();
                _phonebook.Store(currentName, currentNum);
                
            });

            var actualDict = _phonebook.GetAll();

            //Assert
            Assert.That(actualDict.Count, Is.EqualTo(10000));

        }
    }
}