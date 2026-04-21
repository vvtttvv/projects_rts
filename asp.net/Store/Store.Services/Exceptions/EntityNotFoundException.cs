namespace Store.Services.Exceptions;

public class EntityNotFoundException(string message) : ServiceException(message);

