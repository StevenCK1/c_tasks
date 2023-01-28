using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number: ");
        int userNumber = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= userNumber; i++)
        {
            string output = "This is " + i + " out of " + userNumber;
            if (i % 3 == 0 && i % 5 == 0)
            {
                output = "Fizz Buzz";
            }
            else if (i % 3 == 0)
            {
                output = "Fizz";
            }
            else if (i % 5 == 0)
            {
                output = "Buzz";
            }

            Console.WriteLine(output);
        }
    }
}
