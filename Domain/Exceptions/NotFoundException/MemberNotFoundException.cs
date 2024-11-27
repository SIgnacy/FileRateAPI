namespace Domain.Exceptions.NotFoundException;
public class MemberNotFoundException : NotFoundException
{
    public MemberNotFoundException() { }
    public MemberNotFoundException(string message) : base(message) { }
    public MemberNotFoundException(string message, Exception InnerException) : base(message, InnerException) { }
}
