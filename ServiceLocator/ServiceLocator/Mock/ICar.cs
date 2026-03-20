namespace ServiceLocator.Mock;

public interface ICar
{
    public IPassenger Passenger  { get; }
    void showHello();
}