class Phonebook
{
    private Dictionary<string, long> _phonebook;
    private const string Path = "C:\\Users\\Steven\\Desktop\\phonebook.txt";

    public Phonebook()
    {
        _phonebook = new Dictionary<string, long>();

        if (!File.Exists(Path))
        {
            File.Create(Path).Close();
        }

        LoadFromFile();
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
            SaveToFile();
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
            SaveToFile();
            return oldNumber;
        }
        return null;
    }

    private void LoadFromFile()
    {
        string[] lines = File.ReadAllLines(Path);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2 && long.TryParse(parts[1], out long number))
            {
                _phonebook[parts[0]] = number;
            }
        }
    }

    private void SaveToFile()
    {
        string[] lines = new string[_phonebook.Count];
        int i = 0;
        foreach (KeyValuePair<string, long> entry in _phonebook)
        {
            lines[i] = $"{entry.Key},{entry.Value}";
            i++;
        }
        File.WriteAllLines(Path, lines);
    }

    public void Store(string name, long number)
    {
        _phonebook[name] = number;
        SaveToFile();
    }
}