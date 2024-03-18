namespace UserManagement.WebApi.Auth
{
    public class GenericResponse
    {
        public virtual int StatusCode { get; set; }
        public virtual string? Status { get; set; }
        public string? Message { get; set; }
    }
}
