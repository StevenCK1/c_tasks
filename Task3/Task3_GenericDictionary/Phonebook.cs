public class Phonebook : fileHelpers
{
    // classes need to do one thing only
    // seaparate save file functions into its own class!!! from phonebook re   
    public Dictionary<string, long> _phonebook;

    public Phonebook(fileHelpers fileHelpers)
    {
        _phonebook = new Dictionary<string, long>();

        if (!File.Exists(Path))
        {
            File.Create(Path).Close();
        }

        fileHelpers.LoadFromFile(_phonebook);
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
            SaveToFile(_phonebook);
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
            SaveToFile(_phonebook);
            return oldNumber;
        }
        return null;
    }

    public void Store(string name, long number)
    {
        _phonebook[name] = number;
        SaveToFile(_phonebook);
    }
}