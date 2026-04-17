namespace Task4.Computers;

public class GamingPC : Computer
{
    public GamingPC() : base("Gaming PC", 20000, Processor.Intel) { }

    public GamingPC(string newName, int newPrice, Processor newProcessor) : base(newName, newPrice, newProcessor) { }

    public override void ShowGreeting()
    {
        Console.WriteLine("Welcome to the world of gaming!");
    }

    public override void ShowStatus()
    {
        Console.WriteLine("You are a gamer! Enjoy your games!");
    }
}