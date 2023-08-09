using MultiThread_phonebook;

namespace MultiThread2_phonebook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileHelpers fileHelpers = new FileHelpers();
            Phonebook phonebook = new Phonebook(fileHelpers);
            PhonebookCommands phonebookCommands = new PhonebookCommands();

            phonebookCommands.GetAllEntries(phonebook);
            phonebookCommands.ShowMenu();
            while (true)
            {
                Console.Write("Enter command: ");
                string command = Console.ReadLine().Trim().ToUpper();

                switch (command)
                {
                    case "STORE":
                        phonebookCommands.StoreEntry(phonebook);
                        break;
                    case "GET":
                        phonebookCommands.GetEntry(phonebook);
                        break;
                    case "DEL":
                        phonebookCommands.DeleteEntry(phonebook);
                        break;
                    case "UPDATE":
                        phonebookCommands.UpdateEntry(phonebook);
                        break;
                    case "EXIT":
                        return;
                    default:
                        Console.WriteLine("INVALID COMMAND");
                        break;
                }
            }
        }
    }
}