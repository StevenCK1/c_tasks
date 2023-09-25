namespace Recursion
{
    class FormElementOperations<T>
    {
        public static bool AddElement(FormElement<T> root, int parentId, T value) // returns bool to indicate success or failure
        {
            if (root == null)
                return false;

            if (root.Id == parentId)
            {
                var newElement = new FormElement<T>(parentId, value);
                root.Sections.Add(newElement);
                return true;
            }
            else
            {
                foreach (var section in root.Sections)
                {
                    if (AddElement(section, parentId, value))
                        return true;
                }
            }

            return false;
        }

        public static bool UpdateElement(FormElement<T> root, int id, T newValue)
        {
            if (root == null)
                return false;

            if (root.Id == id)
            {
                root.Value = newValue;
                return true;
            }

            foreach (var section in root.Sections)
            {
                if (UpdateElement(section, id, newValue))
                    return true;
            }

            return false;
        }

        public static bool RemoveElement(FormElement<T> root, int id)
        {
            if (root == null)
                return false;

            for (int i = 0; i < root.Sections.Count; i++)
            {
                var section = root.Sections[i];
                if (section.Id == id)
                {
                    root.Sections.RemoveAt(i);
                    return true;
                }

                if (RemoveElement(section, id))
                    return true;
            }

            return false;
        }
    }
}
