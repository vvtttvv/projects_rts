using MediatorLocator.Command;
using MediatorLocator.Exceptions;
using MediatorLocator.Handler;
using MediatorLocator.Mediation;
namespace MediatorTest;


public class Tests
{
    private readonly ICommand<string, int> _command = new GetLengthCommand();
    private readonly IHandler<GetLengthCommand, string, int> _handler = new ConcreteCommandHandler();
    private IMediator _mediator = null!;

    [SetUp]
    public void Setup()
    {
        _mediator = new Mediator();
    }

    [Test]
    public void Register_SameCommandTwice_ThrowsHandlerAlreadyRegisteredException()
    {
        _mediator.Register(_handler);
        Assert.Throws<HandlerAlreadyRegisteredException>(() => _mediator.Register(_handler));
    }
    
    [Test]
    public void Send_CommandWithoutHandler_ThrowsHandlerNotRegisteredException()
    {
        Assert.Throws<HandlerNotRegisteredException>(() => _mediator.Send(new TestCommand(), true));
    }

    [Test]
    public void Register_CommandHandler_SuccessfullyRegisters()
    {
        _mediator.Register(new TestCommandHandler());
        int result = _mediator.Send(new TestCommand(), true);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void Send_RegisteredCommand_ExecutesHandlerSuccessfully()
    {
        _mediator.Register(_handler);
        int result = _mediator.Send(_command, "hello");
        Assert.That(result, Is.EqualTo(5));
    }
}