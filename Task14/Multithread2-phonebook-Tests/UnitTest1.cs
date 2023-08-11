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

        [Test]
        public void ConcurrentAccess_StoreAndDelete()
        {
            // Arrange
            const int concurrentTasks = 1000;
            const int iterationsPerTask = 10000;

            List<Task> tasks = new List<Task>();

            //Act
            for (int i = 0; i < concurrentTasks; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < iterationsPerTask; j++)
                    {
                        _phonebook.Store($"Name_{j}", j);
                        _phonebook.Delete($"Name_{j}");
                    }
                }));
            }

            // Need to wait for tasks to complete before the assertion, if not the test could end before all concurrent operations are done
            Task.WhenAll(tasks).Wait();

            var actualDict = _phonebook.GetAll();

            //Assert
            Assert.That(actualDict.Count, Is.EqualTo(0)); 
            
        }
    }
}