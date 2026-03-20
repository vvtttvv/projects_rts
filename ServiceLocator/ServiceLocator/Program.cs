using ServiceLocator.Mock;

namespace ServiceLocator;

public static class Program
{
    public static void Main()
    {
        ServiceLocator sl = new ServiceLocator();
        bool result = sl.Register<ICar, Car>(ServiceLifeTime.Transient);
        bool result2 = sl.Register<IPassenger, Passenger>(ServiceLifeTime.Singleton);
        Console.WriteLine(result + " " +  result2);
        var element = sl.Get<ICar>();
        var el2 = sl.Get<IPassenger>();
        var el3 = sl.Get<IPassenger>();
        element.showHello();
    }
}
