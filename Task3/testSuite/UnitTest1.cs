using NUnit.Framework;

namespace testSuite
{
   
    public class Tests
    {
        private Phonebook _phonebook;

        [SetUp]
        public void Setup(fileHelpers fileHelpers)
        {
            _phonebook = new Phonebook(fileHelpers);
        }

        [Test]
        public void StoreAndGetEntry()
        {
            // Arrange
            string name = "John";
            long number = 1234567890;

            // Act
            _phonebook.Store(name, number);
            long? result = _phonebook.Get(name);

            // Assert
            Assert.AreEqual(number, result);

            // Delete entry when done
            _phonebook.Delete(name);
        }
    }
}