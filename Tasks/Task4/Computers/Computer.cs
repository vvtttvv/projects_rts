namespace Task4.Computers;

public abstract class Computer
{
    private string _name = "Basic Computer";
    private int _price;
    private Processor _processor;
    
    public Computer() : this("Default", 10_000, Processor.Amd){}

    public Computer(string newName, int newPrice, Processor newProcessor)
    {
        Name = newName;
        Price = newPrice;
        Processor = newProcessor;
    }


    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Price
    {
        get => _price;
        set => _price = value;
    }

    public Processor Processor
    {
        get => _processor;
        set => _processor = value;
    }

    public override string ToString()
    {
        return $"[Name: {Name}, Price: {Price}, Processor: {Processor}]";
    }

    public abstract void ShowGreeting();

    public virtual void ShowStatus()
    {
        Console.WriteLine("You are an ordinary PC user...");
    }
}