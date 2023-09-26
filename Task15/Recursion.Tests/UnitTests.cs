namespace Recursion.Tests
{
    public class UnitTests
    {
        [Test]
        public void AddElement_WhenRootIsNull_ShouldReturnFalse()
        {
            // Arrange
            FormElement<int> root = null;

            // Act
            bool result = FormElementOperations<int>.AddElement(root, 1, 42);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddElement_WhenRootHasMatchingId_ShouldAddElementAndReturnTrue()
        {
            // Arrange
            FormElement<int> root = new FormElement<int>(1, 100);

            // Act
            bool result = FormElementOperations<int>.AddElement(root, 1, 42);

            // Assert
            Assert.IsTrue(result);
            Assert.That(root.Sections.Count, Is.EqualTo(1));
            Assert.That(root.Sections[0].Value, Is.EqualTo(42));
        }

        [Test]
        public void AddElement_WhenRootHasNoMatchingId_ShouldReturnFalse()
        {
            // Arrange
            FormElement<int> root = new FormElement<int>(1, 100);

            // Act
            bool result = FormElementOperations<int>.AddElement(root, 2, 42);

            // Assert
            Assert.IsFalse(result);
            Assert.That(root.Sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void UpdateElement_WhenRootIsNull_ShouldReturnFalse()
        {
            // Arrange
            FormElement<int> root = null;

            // Act
            bool result = FormElementOperations<int>.UpdateElement(root, 1, 42);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateElement_WhenRootHasMatchingId_ShouldUpdateElementAndReturnTrue()
        {
            // Arrange
            FormElement<int> root = new FormElement<int>(1, 100);

            // Act
            bool result = FormElementOperations<int>.UpdateElement(root, 1, 42);

            // Assert
            Assert.IsTrue(result);
            Assert.That(root.Value, Is.EqualTo(42));
        }

    }
}