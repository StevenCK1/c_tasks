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
            _fileSearcher = new FileSearcher();
        }

        [Test]
        public void SearchFiles_WithValidDirectoryAndFileName_ReturnsMatchingFiles()
        {
            // Arrange
            string directoryPath = Path.GetTempPath();
            string fileName = "testfile.txt";
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllText(filePath, "Test content");

            // Act
            List<string> matchingFiles = _fileSearcher.SearchFiles(directoryPath, fileName);

            // Assert
            Assert.AreEqual(1, matchingFiles.Count);
            Assert.IsTrue(matchingFiles.Contains(filePath));

            // Clean up
            File.Delete(filePath);
        }
    }
}