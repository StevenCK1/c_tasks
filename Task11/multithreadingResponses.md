1. Moneybox  
a. The section that is thread safe. Why is it thread safe?  
**There is only one instance of the lock created in the class since the lock is created outside of the 2 methods where the state is mutating and the state is shared between the 2 methods**
b. I have added a proposed transfer method. What is wrong with it. Then fix it. - This was too hard for senior devs so don't include  
**A method can be declared as static when it needs to be accessed or called without creating an instance of the class in which it is defined. Since the method is static and not creating an instance of the class, the lock object will not be instantiated.**

```csharp
//C#

class BankAccount {
    // This bit is thread safe
    private decimal m_balance = 0.0M;
    private object m_balanceLock = new object();
    internal void Deposit(decimal delta) {
        lock (m_balanceLock) { m_balance += delta; }
    }
    internal void Withdraw(decimal delta) {
        lock (m_balanceLock) {
            if (m_balance < delta)
                throw new Exception("Insufficient funds");
            m_balance -= delta;
        }
    }
    // End This bit is thread safe
    
    // Proposed transfer method
    internal static void Transfer(
      BankAccount a, BankAccount b, decimal delta) {
        Withdraw(a, delta);
        Deposit(b, delta);
    }
}
```
Answer is here: https://learn.microsoft.com/en-us/archive/msdn-magazine/2008/october/concurrency-hazards-solving-problems-in-your-multithreaded-code
Don't read until you've done the ex!

Answer is starvation

```
// Solution is to make acquire both locks up front and then make the method calls // Deadlock issue?
class BankAccount {
    internal static void Transfer(
      BankAccount a, BankAccount b, decimal delta) {
        lock (a.m_balanceLock) {
            lock (b.m_balanceLock) {
                Withdraw(a, delta);
                Deposit(b, delta);
            }
        }
    }
    // As before 
}
```
2. Make this class immutable  
You can remove any method that causes mutation, but you must retain that fuctionality (e.g if you remove UpdateDetails method, there must still be a way of setting the name and location, immutabily). All public properties must be available for reading.
```csharp
    public class User1
    {
        protected int _id = 0;
        public string _name;

        public string GetUserDetails(int uid, string userName)
        {
            return $"{_id} - {uid} - {userName} - {_name}";
        }

        public void UpdateDetails(string newName, string location)
        {
            _name = newName;
            Location = location;
        }

        public int Designation { get; set; }
        public string Location { get; set; }
    }
```

**MySolution**
```csharp
    public class User1
    {
        protected int _id = 0;
        public string _name;

        public string GetUserDetails(int uid, string userName)
        {
            return $"{_id} - {uid} - {userName} - {_name}";
        }

        public User1 UpdateDetails(string newName, string location)
        {
        // v1
        // string.replace is an immutable method, because strings are immutable?
             _name.Replace(_name, newName);
             Location.Replace(Location, location);
        }

        public int Designation { get; set; }
        public string Location { get; set; }
    }

    //v2
     public class User1
    {
// need to make readonly
        protected int _id = 0;
        public readonly string _name;

        public User1(string name, string location)
        {
        _name = name
        Location = location
        }

        public string GetUserDetails(int uid, string userName)
        {
            return $"{_id} - {uid} - {userName} - {_name}";
        }

        public User1 UpdateDetails(string newName, string location)
        {
            return new User1(newName, location)
        }

// remove setter
        public int Designation { get; set; }
        public string Location { get; private set; }
    }
```

3. Make this class immutable.  
You can remove any method that causes mutation, but you must retain that fuctionality (e.g if you remove UpdateDetails method, there must still be a way of setting the name and location, immutabily). All public properties must be available for reading.

```csharp
    public class User2
    { 
        private int _id;
        public Name _name;

        public User2(int id)
        {
            _id = id;
        }
        
        public void UpdateDetails(string newName, string location)
        {
            _name = new Name(newName);
            Location = location;
        }

        public bool SearchForUser(string search)
        {
            // We have to make lower before we search so we search case insensitive
            search = search.ToLowerInvariant();
            _name.Last = _name.Last.ToLowerInvariant();
            _name.First = _name.First.ToLowerInvariant();
            return _name.Search(search);
        }

        public string Location { get; set; }
    }

    // You cannot change this class: It is in an external library just provided you the code so you can see it.
    public class Name
    {
        public Name(Name name)
        {
            First = name.First;
            Initials = name.Initials;
            Last = name.Last;
        }

        public Name(string name)
        {
            var split = name.Split(" ");
            First = split[0];
            Initials = split[1];
            Last = split[2];
        }

        public string First { get; set; }
        public string Initials { get; set; }
        public string Last { get; set; }

        public bool Search(string search)
        {
            return First.Contains(search) || Last.Contains(search);
        }

        public override string ToString() => $"{First} {Initials} {Last}";
    }
```

**MySolution**
```csharp
    public class User2
    { 
        private readonly int _id;
        public readonly Name _name;

        public User2(Name name, string location)
        {
            _name = name
            Location = location
        }
        
        public User2 UpdateDetails(string newName, string location)
        {
           return new User2(newName, location)
        }

        public bool SearchForUser(string search)
        {
            // We have to make lower before we search so we search case insensitive
            search = search.ToLowerInvariant();
            _name.Last = _name.Last.ToLowerInvariant();
            _name.First = _name.First.ToLowerInvariant();
            return _name.Search(search);
        }

        public string Location { get; private set; }
    }
```

4. What is wrong with the locking here. And how would you fix it:
**
```csharp
        public class Randomness
    {
        /// <summary>
        /// Moves characters to a new random location on the console.
        /// This gets called by a multithreaded method that randomly picks a Loc from a list and moves it. But the characters keep get duplicated. But I'm locking on the Loc to make sure that while its moving another thread can't move it!
        /// </summary>
        /// <param name="state"></param>
        public Loc MoveCharacter(Loc state)
        {
            // We make a copy as we're going to change X and Y in a sec and don't want to have side effects on original
            var copy = new Loc(state.X, state.Y, state.Value);

            lock (copy)
            {
                // erase the previous position
                WriteAt(" ", copy.X, copy.Y);
            }

            // pick a new random position
            copy.X = new Random().Next(1000);
            copy.Y = new Random().Next(1000);

            lock (copy)
            {
                WriteAt(copy.Value, copy.X, copy.Y);
            }

            return copy;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // This is the definition for the Loc class. Don't make changes to this class
        public class Loc
        {
            public Loc(int x, int y, string value)
            {
                X = x;
                Y = y;
                Value = value;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public string Value { get; set; }  // Will just be one character
        }
    }
```
   
**MySolution**

```csharp
        public class Randomness
    {
    // lockObject created here because else the method will create an instance of the lock for every thread?
        public static object _lock = new object();
        
        /// <summary>
        /// Moves characters to a new random location on the console.
        /// This gets called by a multithreaded method that randomly picks a Loc from a list and moves it. But the characters keep get duplicated. But I'm locking on the Loc to make sure that while its moving another thread can't move it!
        /// </summary>
        /// <param name="state"></param>
        public Loc MoveCharacter(Loc state)
        {
            // We make a copy as we're going to change X and Y in a sec and don't want to have side effects on original
            var copy = new Loc(state.X, state.Y, state.Value);

            lock (_lock)
            {
                // erase the previous position
                WriteAt(" ", copy.X, copy.Y);
            }

            // pick a new random position
            copy.X = new Random().Next(1000);
            copy.Y = new Random().Next(1000);

            lock (_lock)
            {
                WriteAt(copy.Value, copy.X, copy.Y);
            }

            return copy;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // This is the definition for the Loc class. Don't make changes to this class
        public class Loc
        {
            public Loc(int x, int y, string value)
            {
                X = x;
                Y = y;
                Value = value;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public string Value { get; set; }  // Will just be one character
        }
    }
```
 5. What is the problem here: 
```csharp
object locker1 = new object();
object locker2 = new object();
 
new Thread (() => {
                    lock (locker1)
                    {
                      Thread.Sleep (1000);
                      lock (locker2);
                    }
                  }).Start();
lock (locker2)
{
  Thread.Sleep (1000);
  lock (locker1);  
}
```

**Deadlock because a thread is created for locker1 but needs to wai to locker2. Locker is created but needs to wait for locker1. **

6. What is wrong with locking in this code: 
```csharp
    public class PhoneBook
    {
// lock object does not need to be static 
        private Dictionary<string, long> _phonebook;
        private object _lock = new object();

        public void AddNumber(string name, long number)
        {
            lock (_lock)
            {
                if (!_phonebook.ContainsKey(name))
                {
                    _phonebook.Add(name, number);
                }
                else
                {
                    _phonebook[name] = number;
                }
            }
        }

        public void Clear()
        {
// ANSWER need to lock here
            _phonebook.Clear();
        }
    }
```
