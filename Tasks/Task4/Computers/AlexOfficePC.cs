namespace Task4.Computers;

public class AlexOfficePc : OfficePc
{
    public AlexOfficePc() : this("Alex's PC", 5000, Processor.Apple, new Person("Alex", "Rudoi", 20)){}
    public AlexOfficePc(string newName, int newPrice, Processor newProcessor, Person person) : base(newName, newPrice,
        newProcessor, person){}

    public override void ShowGreeting()
    {
        if (Person.FirstName == "Alex" && Person.LastName == "Rudoi")
        {
            Console.WriteLine("Sir, I want to retire, please...");
        }
        else
        {
            base.ShowGreeting();
        }
        
    }
    
    public override void ShowStatus()
    {
        Console.WriteLine("This old hardware can only open Telegram...");
    }
}