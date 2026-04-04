namespace MediatorLocator.Exceptions;

public class HandlerNotRegisteredException : Exception
{
    public HandlerNotRegisteredException()
        : base("Handler wasn't registered!") { }

    public HandlerNotRegisteredException(string message)
        : base(message) { }

    public HandlerNotRegisteredException(string message, Exception innerException)
        : base(message, innerException) { }
}
//