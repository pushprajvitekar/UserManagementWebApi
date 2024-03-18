using UserManagement.WebApi.Auth.Policies;

namespace UserManagement.WebApi.Auth
{
    public static class UserAuthorizationRequirements
    {
        public static SameUserAuthorizationRequirement SameUser = new SameUserAuthorizationRequirement();
    }
}
