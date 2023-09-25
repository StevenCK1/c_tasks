namespace Recursion
{
    public class FormElement<T>
    {
        public int Id { get; set; }
        public T Value { get; set; }
        public List<FormElement<T>> Sections { get; set; }

        public FormElement(int id, T value)
        {
            Id = id;
            Value = value;
            Sections = new List<FormElement<T>>();
        }
    }
}
