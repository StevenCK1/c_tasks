using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var people = new List<Person>()
        {
            new Person("Bill", "Smith", 41, true),
            new Person("Sarah", "Jones", 22, false),
            new Person("Stacy","Baker", 21, false),
            new Person("Vivianne","Dexter", 19, false),
            new Person("Bob","Smith", 49 , true),
            new Person("Brett","Baker", 51 , true),
            new Person("Mark","Parker", 19, true),
            new Person("Alice","Thompson", 18, false),
            new Person("Evelyn","Thompson", 58 , false),
            new Person("Mort","Martin", 58, true),
            new Person("Eugene","deLauter", 84 , true),
            new Person("Gail","Dawson", 19 , false),
            new Person("Allain","deLauter", 51 , true),
            new Person("Natalie","deLauter", 19 , false),
            new Person("Peter","deLauter", 22 , true),
        };

        // testing useage of ConsoleUtils methods

        // ```
        // string testing = ConsoleUtils.ReadString("enter test");
        // ConsoleUtils.WriteLine($"HELLO WORLD!!!  { testing}");
        // ```


        //0. write linq display every name ordered alphabetically

        // using var will create the variable without needed to explicitly specifying the type, without using var. Would have needed to create variable with List<Person>

        var orderedAlphabet = people.OrderBy(p => p.FirstName).ToList();
        // Query syntax/expresions exists for most linq operation, looks very much like SQL (uhhhh)



        //1. write linq statement for the people with last name that starts with the letter D
        //Console.WriteLine("Number of people who's last name starts with the letter D " + people1.Count());

        // Declaring variable with explicit type,
        // Declaring with explicit type not very useful if I need the variable type to change
        List<Person> lastNameDs = people.Where(p => p.LastName.ToLower().First() == 'd').ToList();


        //2. write linq statement for all the people who are have the surname Thompson and Baker. Write all the first names to the console

        List<Person> lastNameThompsonAndBaker = people.Where(p => p.LastName == "Thompson" || p.LastName == "Baker").ToList();

        // need to iterate through the list 
        foreach(var Person in lastNameThompsonAndBaker)
        {
            ConsoleUtils.WriteLine($"{Person.FirstName}");
        }


        //3. write linq to convert the list of people to a dictionary keyed by first name

        Dictionary<string, Person> peopleDic  = people.ToDictionary(p => p.FirstName);

        // Testing that the conversion to dictionary is working
        // KeyValuePair<string, Person> has direct access to both the key and the value of each entry

        // ```
        // foreach (KeyValuePair<string, Person> entry in peopleDic)
        // {
        //    Console.WriteLine($"Key: {entry.Key} & Value: {entry.Value.FirstName}, {entry.Value.LastName}, {entry.Value.Age}, {entry.Value.Male}" );
        // }
        // ```

        // 4. Write linq statement for first Person Older Than 40 In Descending Alphabetical Order By First Name
        //Console.WriteLine("First Person Older Than 40 in Descending Order by First Name " + person2.ToString());

        List<Person> over40s = people.Where(p => p.Age > 40).OrderByDescending(p => p.FirstName).ToList();


        //5. write a linq statement that finds all the people who are part of a family. (aka there is at least one other person with the same surname.

        // Need to groupBy key first and then chain with Where (group needs to have more than one value)
        List<IGrouping<string, Person>> sameFamily = people.GroupBy(p => p.LastName).Where(group => group.Count() > 1).ToList();
   
        // Testing grouping is correct
         // foreach to iterate through the IGrouping and 2nd foreach to iterate through every object 

        // ```
        // foreach (var familyGroup in sameFamily)
        // {
        //    Console.WriteLine("Last Name: " + familyGroup.Key);

        //    foreach (var person in familyGroup)
        //    {
        //        Console.WriteLine("  " + person.FirstName + " " + person.LastName);
        //    }
        // }
        // ```



        //6. Write a linq statement that finds which of the following numbers are multiples of 4 or 6
        List<int> mixedNumbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };

        List<int> DivByFourOSix = mixedNumbers.Where(n => n % 4 == 0 || n % 6 == 0).ToList();

        // Testing list of int is correct

        // ```
        // foreach(var number in DivByFourOSix)
        // {
        //    Console.WriteLine(number.ToString());
        // }
        // ```

        // 7. How much money have we made?
        List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };

        double sum = purchases.Sum();


        // 8. Write a linq statement to copy the List<Person>() as a List<SimplePerson>(). You will need to join the first and last names of the person for the name field

        // need to create new object with property of Name and Age
        var simplePerson = people.Select(p => new SimplePerson { Name = $"{p.FirstName} {p.LastName}", Age = p.Age }).ToList();

        // Testing simplePerson object is correct

        // ```
        // foreach (var p in simplePerson)
        // {
        //    Console.WriteLine($"{p.Name} is {p.Age} old" );
        // }
        // ```

        // 9. Write a linq statement to copy the List<Person>() as a List<PersonWithName>(). Then print to the console the NameAndTitle of each person
        List<PersonWithName> peopleWithName = people.Select(p => new PersonWithName
        {
            Name = new NameWithTitle(p.FirstName, p.LastName, p.Male),
            Age = p.Age
        }).ToList();

        foreach (var person in peopleWithName)
        {
            Console.WriteLine(person.Name.NameAndTitle());
        }


        // 10. Write a linq statement get all the families as a List<Family> class below. You will need to find all the members and group them by family (see question 5). Then create a family class for each family with the relevant details

        List<Family> families= sameFamily.Select(p => new Family { LastName = p.Key, Members = p.Select(p => new FamilyMember { FirstName = p.FirstName, Age = p.Age, Male = p.Male})}).ToList();


    }
}


public class Person
{
    public Person(string firstName, string lastName, int age, bool isMale)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Male = isMale;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public bool Male { get; set; }
}

public class SimplePerson
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PersonWithName
{
    public NameWithTitle Name { get; set; }
    public int Age { get; set; }
}

public class NameWithTitle
{
    public NameWithTitle(string firstName, string lastName, bool isMale)
    {
        FirstName = firstName;
        LastName = lastName;
        IsMale = isMale;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }

    public string NameAndTitle()
    {
        var title = "Ms.";
        if (IsMale)
        {
            title = "Mr.";
        }
        return $"{title} {FirstName} {LastName}";
    }
}

public class Family
{
    public Family()
    {
        Members = new List<FamilyMember>();
    }

    public Family(string lastName, List<FamilyMember> members)
    {
        LastName = lastName;
        Members = members;
    }

    public string LastName { get; set; }
    List<FamilyMember> Members { get; set; }
}

public class FamilyMember
{
    public FamilyMember(string firstName, int age, bool isMale)
    {
        FirstName = firstName;
        Age = age;
        Male = isMale;
    }

    public string FirstName { get; set; }
    public int Age { get; set; }
    public bool Male { get; set; }
}

