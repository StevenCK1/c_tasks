namespace Recursion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = new FormElement<string> { Id = 1, Value = "Root" };
            FormElementOperations<string> operations = new FormElementOperations<string>();

            operations.Add(root, 1, "Child1");
        }
    }
}