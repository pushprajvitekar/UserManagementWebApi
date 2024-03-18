namespace UserManagement.Domain.Exceptions
{
    public class DoesNotExistException : DomainException
    {
        public DoesNotExistException(string? message, Exception? innerException) : base(message, innerException, DomainErrorCode.NotFound)
        {
        }
    }
}
