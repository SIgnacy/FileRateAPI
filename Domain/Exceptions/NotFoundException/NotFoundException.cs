namespace Domain.Exceptions.NotFoundException;
public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception InnerException) : base(message, InnerException) { }
}
