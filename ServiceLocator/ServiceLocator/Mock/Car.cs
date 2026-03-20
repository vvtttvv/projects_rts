namespace ServiceLocator.Mock;

public class Car : ICar
{
    private IPassenger passenger;
    public Car(IPassenger passenger)
    {
        this.passenger = passenger;
    }
    public void showHello()
    {
        Console.WriteLine("Hello");
    }

    public IPassenger Passenger
    {
        get
        {
            return passenger;
        }
    }
}