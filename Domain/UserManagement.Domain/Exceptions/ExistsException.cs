namespace UserManagement.Domain.Exceptions
{
    public class ExistsException : DomainException
    {
        public ExistsException(string? message) : base(message, null, DomainErrorCode.Exists)
        {
        }
    }
}
