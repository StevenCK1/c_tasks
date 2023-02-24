using System;

class MyDictionary
{
    static void Main(string[] args)
    {

        // refactor the code, separate phonebook and console

        // Dictionary<TKey, TValue>
        // using long because int only goes from -2,147,483,648 to 2,147,483,647 (Size: Signed 32-bit integer)
        // phone number is 11 digit
        Dictionary<string, long> phonebook = new Dictionary<string, long>();

        // Within while loop to continuously prompt user for commands, STOP breaks the loop 
        while (true)
        {
            Console.Write("Enter command (STORE, GET, DEL, UPDATE, STOP): ");
            string command = Console.ReadLine();

            if (command == "STOP")
            {
                break;
            }

            if (command == "STORE")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                Console.Write("Enter number: ");
                long number = Convert.ToInt64(Console.ReadLine());

                // Can also use syntax below to add to dictionary
                // phonebook[name] = number; // name being the key and number being the value
                phonebook.Add(name, number);

                Console.WriteLine("OK");
            }
            else if (command == "GET")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                
                // Check that key exists
                if (phonebook.ContainsKey(name))
                {
                    Console.WriteLine("OK " + phonebook[name]);
                }
                else
                {
                    Console.WriteLine("NOT FOUND");
                }
            }
            else if (command == "DEL")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                if (phonebook.ContainsKey(name))
                {
                    // Store deleted number in variable so that can be used and printed on Console.writeLine
                    long number = phonebook[name];
                    phonebook.Remove(name);

                    Console.WriteLine("OK " + number);
                }
                else
                {
                    Console.WriteLine("NOT FOUND");
                }
            }
            else if (command == "UPDATE")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                if (phonebook.ContainsKey(name))
                {
                    Console.Write("Enter new number: ");
                    long newNumber = Convert.ToInt64(Console.ReadLine());

                    // store old number in variable so that can be used and printed on Console.writeLine
                    long oldNumber = phonebook[name];

                    // Update dictionary with new number 
                    phonebook[name] = newNumber;

                    Console.WriteLine("OK last no was - " + oldNumber);
                }
                else
                {
                    Console.WriteLine("NOT FOUND");
                }
            }
            else
            {
                Console.WriteLine("INVALID COMMAND");
            }
        }
    }

}