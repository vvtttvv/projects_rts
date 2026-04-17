namespace Task4.Computers;

public class OfficePc : Computer
{
    private Person _person;

    public OfficePc(string newName, int newPrice, Processor newProcessor, Person person) : base(newName, newPrice,
        newProcessor)
    {
        Person = person;
    }

    public Person Person
    {
        get
        {
            return _person;
        }
        set
        {
            if (value is Person)
            {
                _person = value;
            }
            else
            {
                Console.WriteLine("Wrong input");
            }
        }
    }
    
    public override void ShowGreeting()
    {
        Console.WriteLine($"Hello, {_person.FirstName} {_person.LastName}, it's an usual office PC. Please, be accurate with provided hardware!");
    }

    public override void ShowStatus()
    {
        base.ShowStatus();
        Console.WriteLine("But you still need to work with it since you don't have money to update it...");
    }

    public override string ToString()
    {
        return base.ToString() + ", " + _person.ToString();
    }
}