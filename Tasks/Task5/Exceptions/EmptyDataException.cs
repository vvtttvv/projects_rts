namespace Task5.Exceptions;

[Serializable]
public class EmptyDataException : ApplicationException
{
    public EmptyDataException(){}
    public EmptyDataException(string message) : base(message){}
    public EmptyDataException(string message, Exception inner) : base(message, inner){}
}