namespace MediatorLocator.Command;

public class TestCommand : ICommand<bool, int>
{ 
    public int Execute(bool bo)
    {
        return bo ? 1 : 0;
    }
}