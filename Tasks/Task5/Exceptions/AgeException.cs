using System.Runtime.Serialization;

namespace Task5.Exceptions;

[Serializable]
public class AgeException : ApplicationException
{
    public AgeException(){}
    public AgeException(string message) : base(message){}
    public AgeException(string message, Exception inner) : base(message, inner) {}
    // To consult about the last constructor
    //protected AgeException(SerializationInfo info, StreamingContext context) : base(info, context){}
    
}