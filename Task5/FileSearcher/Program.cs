using System;
using System.Collections.Generic;
using System.IO;


    public class FileSearcher
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the directory path: ");
            string directoryPath = Console.ReadLine();

            Console.Write("Enter the filename: ");
            string fileName = Console.ReadLine();

            List<string> matchingFiles = ProgramHelpers.SearchFiles(directoryPath, fileName);

            if (matchingFiles.Count > 0)
            {
                Console.WriteLine("Matching files:");
                foreach (string file in matchingFiles)
                {
                    Console.WriteLine(file);
                }
            }
            else
            {
                Console.WriteLine("No matching files found.");
            }
        }
    }

