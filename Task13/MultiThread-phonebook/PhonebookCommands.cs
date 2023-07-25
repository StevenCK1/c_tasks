namespace MultiThread_phonebook
{
    public class PhonebookCommands
    {
        public void ShowMenu()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("STORE - Store a new entry");
            Console.WriteLine("GET - Retrieve an entry");
            Console.WriteLine("DEL - Delete an entry");
            Console.WriteLine("UPDATE - Update an entry");
            Console.WriteLine("EXIT - Exit the program");
            Console.WriteLine();
        }

        public void DeleteEntry(Phonebook phonebook)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            long? number = phonebook.Delete(name);
            Console.WriteLine(number.HasValue ? $"OK {number}" : "NOT FOUND");
        }

        public void GetEntry(Phonebook phonebook)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            long? number = phonebook.Get(name);
            Console.WriteLine(number.HasValue ? $"OK {number}" : "NOT FOUND");
        }

        public bool GetValidNumber(out long number)
        {
            Console.Write("Enter number: ");
            string numberInput = Console.ReadLine();
            return long.TryParse(numberInput, out number);
        }

        public void StoreEntry(Phonebook phonebook)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            if (phonebook._phonebook.ContainsKey(name))
            {
                Console.WriteLine("Name already exists in the phonebook.");
                return;
            }

            if (GetValidNumber(out long number))
            {
                phonebook.Store(name, number);
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }

        public void UpdateEntry(Phonebook phonebook)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            if (GetValidNumber(out long newNumber))
            {
                long? oldNumber = phonebook.Update(name, newNumber);
                Console.WriteLine(oldNumber.HasValue ? $"OK last no was - {oldNumber}" : "NOT FOUND");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }

        public void GetAllEntries(Phonebook phonebook)
        {
            Console.WriteLine("Phonebook entries:");
            foreach (var entry in phonebook.GetAll())
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}