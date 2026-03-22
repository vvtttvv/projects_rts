using MediatorLocator.Command;
namespace MediatorLocator.Handler;

public class TestCommandHandler :  IHandler<TestCommand, bool, int>
{
    private TestCommand _cmd = new TestCommand();
    public int Handle(bool param)
    {
        return _cmd.Execute(param);
    }
}