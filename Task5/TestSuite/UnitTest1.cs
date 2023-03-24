using FileSearcher;

namespace TestSuite
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {

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
            List<string> matchingFiles = ProgramHelpers.SearchFiles(directoryPath, fileName);

            // Assert
            Assert.AreEqual(1, matchingFiles.Count);
            Assert.IsTrue(matchingFiles.Contains(filePath));

            // Clean up
            File.Delete(filePath);
        }
    }
}