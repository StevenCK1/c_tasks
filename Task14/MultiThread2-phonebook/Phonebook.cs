namespace MultiThread_phonebook
{
    public class Phonebook
    {
        public Dictionary<string, long> _phonebook; // since the shared state is phonebook class, do we need to make the phonebook static as well? Throwing off errors when tried to make static
        public static readonly object _lock = new object(); 


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

        // Don't think would need to lock get methods because we are not modifying the state? I suppose there could be a scenario where a thread updates the state and the get method returns the wrong version of the state?
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
                lock (_lock)
                {
                    // same locking logic applies to Update and Store
                    _phonebook.Remove(name); // locking the phonebook as this would be the shared state
                    _fileHelpers.SaveToFile(_phonebook); // locking this as well because if the phonebook is accessed by another thread, the phonebook state could be modified before saving to file. 
                }

                return number;
            }
            return null;
        }

        public long? Update(string name, long newNumber)
        {
            if (_phonebook.ContainsKey(name))
            {
                long oldNumber = _phonebook[name];
                lock (_lock)
                {
                    _phonebook[name] = newNumber;
                    _fileHelpers.SaveToFile(_phonebook);
                }


                return oldNumber;
            }
            return null;
        }

        public void Store(string name, long number)
        {
            lock (_lock)
            {
                _phonebook[name] = number;
                _fileHelpers.SaveToFile(_phonebook);
            }

        }
    }
}
