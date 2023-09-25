namespace Recursion
{
    class FormElementOperations<T>
    {
        public static void AddElement(FormElement<T> root, int parentId, T value)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root), "Root element cannot be null.");

            if (root.Id == parentId)
            {
                var newElement = new FormElement<T>(parentId, value);
                root.Sections.Add(newElement);
            }
            else
            {
                foreach (var section in root.Sections)
                {
                    AddElement(section, parentId, value);
                }
            }
        }
    }
}
