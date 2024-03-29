using System.Collections;
using Moq;



public class PhonebookTests
{
    private Phonebook _phonebook;
    private Mock<FileHelpers> mockHelpers;

    [SetUp]
    public void Setup()
    {
        // create a mock object for FileHelpers (so that it doesn't actually interact with the file)
        mockHelpers = new Mock<FileHelpers>();
        // create an instance of phonebook using the mock object. Now I am able to test the phonebook methods without making changes to the actual phonebook.txt file since 
        _phonebook = new Phonebook(mockHelpers.Object);
        

        // Comments
        // 1. create dictionary in set up
        // 2. test that methods gets called nor not! using mock.verify

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

    }

    [Test]
    public void StoreAndGetAllEntry()
    {
        // Arrange
        string name1 = "John";
        long number1 = 123456789;
        string name2 = "Johnny";
        long number2 = 987654321;

        // Act
        _phonebook.Store(name1, number1);
        _phonebook.Store(name2, number2);
        Dictionary<string, long> dic = _phonebook.GetAll();

        // Assert
        Assert.AreEqual(2, dic.Count);
        Assert.AreEqual(number1, dic[name1]);
        Assert.AreEqual(number2, dic[name2]);
    }

    [Test]
    public void DeleteEntry()
    {
        // Arrange
        string name1 = "John";
        long number1 = 123456789;
        string name2 = "Johnny";
        long number2 = 987654321;
        _phonebook.Store(name1, number1);
        _phonebook.Store(name2, number2);

        // Act
        _phonebook.Delete(name1);
        Dictionary<string, long> dic = _phonebook.GetAll();

        // Assert
        Assert.AreEqual(1, dic.Count);
        Assert.AreEqual(number2, dic[name2]);
        Assert.IsFalse(dic.ContainsKey(name1));
    }
}
