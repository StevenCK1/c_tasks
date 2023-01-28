using System;
using System.Collections.Generic;

namespace Task2_CustomDictionary
{
    class Program
    {
        // internal class because of "Main" method which is the main entry point of c# program
        internal class MyDictionary
        {
            // private because variables are used inside of the constructors, variables would not be used externally
            private int arraySize = 100;
            // Creat variable "data" which is an array of keyValuePair, both key and value are strings
            private KeyValuePair<string, string>[] data;

            // create public constructor called "MyDictionary" and initialize with KeyValuePair array
            public MyDictionary()
            {
                data = new KeyValuePair<string, string>[arraySize];
            }

            // create Hash method, take string and convert the first character to a string 
            public int Hash(string key)
            {
                // Use the first character in the key to hash
                // using remainder operator to ensure that the generated int is within the arraySize
                return (int)key[0] % arraySize;
            }

            public bool Add(string key, string value)
            {
                int index = Hash(key);
                data[index] = new KeyValuePair<string, string>(key, value);
                return true;
            }

            public string Get(string key)
            {
                int index = Hash(key);
                if (data[index].Key != null)
                    return data[index].Value;
                else
                    return "Key not found";
            }

            public bool Remove(string key)
            {
                int index = Hash(key);
                if (data[index].Key != null)
                {
                    data[index] = new KeyValuePair<string, string>();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }


        static void Main(string[] args)
        {
            var dict = new MyDictionary();
            Console.WriteLine(dict.Add("Month", "Feb")); // expect to return true
            Console.WriteLine(dict.Add("Colour", "Red")); // expect to return true
            Console.WriteLine(dict.Get("Month")); // expect to return "Feb"
            Console.WriteLine(dict.Get("Colour")); // expect to return "Red"
            Console.WriteLine(dict.Add("Month", "Jan")); // expect to return true
            Console.WriteLine(dict.Get("Month")); // expect to return "Jan"
            Console.WriteLine(dict.Remove("Month")); // expect to return true
            Console.WriteLine(dict.Get("Month")); // expect to return "Key not found"
        }

    }

}
