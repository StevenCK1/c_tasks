using System;
using System.Collections.Generic;


class MyDictionary
{
    static void Main(string[] args)
    {
        Phonebook phonebook = new Phonebook();

        Console.WriteLine("Phonebook entries:");
        foreach (var entry in phonebook.GetAll())
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

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

                phonebook.Store(name, number);
                Console.WriteLine("OK");
            }
            else if (command == "GET")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                long? number = phonebook.Get(name);
                Console.WriteLine(number.HasValue ? $"OK {number}" : "NOT FOUND");
            }
            else if (command == "DEL")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                long? number = phonebook.Delete(name);
                Console.WriteLine(number.HasValue ? $"OK {number}" : "NOT FOUND");
            }
            else if (command == "UPDATE")
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                Console.Write("Enter new number: ");
                long newNumber = Convert.ToInt64(Console.ReadLine());

                long? oldNumber = phonebook.Update(name, newNumber);
                Console.WriteLine(oldNumber.HasValue ? $"OK last no was - {oldNumber}" : "NOT FOUND");
            }
            else
            {
                Console.WriteLine("INVALID COMMAND");
            }
        }
    }
}
