public class Phonebook
{
    public Dictionary<string, long> _phonebook;
    private FileHelpers _fileHelpers;

    public Phonebook(FileHelpers fileHelpers)
    {
        _phonebook = new Dictionary<string, long>();
        _fileHelpers = fileHelpers;

        if (!File.Exists(FileHelpers.Path))
        {
            File.Create(FileHelpers.Path).Close();
        }

        _fileHelpers.LoadFromFile(_phonebook);
    }

    public long? Get(string name)
    {
        _phonebook.TryGetValue(name, out long number);
        return number != 0 ? number : (long?)null;
    }

    public Dictionary<string, long> GetAll()
    {
        return _phonebook;
    }

    public long? Delete(string name)
    {
        if (_phonebook.TryGetValue(name, out long number))
        {
            _phonebook.Remove(name);
            _fileHelpers.SaveToFile(_phonebook);
            return number;
        }
        return null;
    }

    public long? Update(string name, long newNumber)
    {
        if (_phonebook.ContainsKey(name))
        {
            long oldNumber = _phonebook[name];
            _phonebook[name] = newNumber;
            _fileHelpers.SaveToFile(_phonebook);
            return oldNumber;
        }
        return null;
    }

    public void Store(string name, long number)
    {
        _phonebook[name] = number;
        _fileHelpers.SaveToFile(_phonebook);
    }
}