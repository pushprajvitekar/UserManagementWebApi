namespace UserManagement.Domain.Users.Queries
{
    public class UserFilter
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        //public IEnumerable<string> Roles { get; set; }
    }
}
