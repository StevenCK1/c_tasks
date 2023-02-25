using System;

// Console methods to manage inputs and outputs from/to console
// Not very useful at the moment since only reading and writing strings
public class ConsoleUtils
{
	// create method to write the message to be displayed in the console app
    // return will read the console and return the string

	public string ReadString(string input)
	{
  
        Console.WriteLine(input);
		return Console.ReadLine();
	}
    // create method to write to console
    public void WriteLine(string input)
    {
        Console.WriteLine(input);
    }
}
