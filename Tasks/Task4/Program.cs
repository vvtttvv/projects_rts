using Task4.Computers;

namespace Task4;

public class Program
{
    public static void Main(string[] args)
    {
        // 1
        Person p1 = new Person("Fill", "Jorfan", 15);
        Person p2 = p1;
        Console.WriteLine(p1.Equals(p2));
        Console.WriteLine();
        
        // 2
        //Computer ac = new AlexOfficePc("Somebody", 10000, Processor.Intel, p2);
        Computer ac = new AlexOfficePc();
        ac.ShowGreeting();
        ac.ShowStatus();
        Console.WriteLine();
        Computer gc = new GamingPC();
        gc.ShowGreeting();
        gc.ShowStatus();
        Console.WriteLine();
        
        // 3
        CheckIfThePerson(p1);
        object[] hzcito = {new Person(), "Tesla"};
        CheckIfThePerson(hzcito[0]);
        CheckIfThePerson(hzcito[1]);


    }

    public static void CheckIfThePerson(object maybePerson)
    {

        static void ShowInfo(Person person)
        {
            Console.WriteLine(person.ToString());
        }
        if (maybePerson is Person)
        {
            ShowInfo((Person)maybePerson);
        }
        else
        {
            Console.WriteLine("Not a person :(");
        }
    }
}

