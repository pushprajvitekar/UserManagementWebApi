using LinqKit;
using System.Linq.Expressions;
using UserManagement.Application.Contracts;
using UserManagement.Domain.Users;
using UserManagement.Domain.Users.Queries;

namespace UserManagement.Application.Users.QueryObjects
{
    public class UsersFilterQueryObject : IQueryObject<ApplicationUser>
    {
        private readonly UserFilter filter;

        public UsersFilterQueryObject(UserFilter filter)
        {
            this.filter = filter;
        }
        public Expression<Func<ApplicationUser, bool>> Query()
        {
            var pred = PredicateBuilder.New<ApplicationUser>(true);
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                var normalisedUserName = filter.UserName.ToUpper();
                pred = pred.Or(u => !string.IsNullOrEmpty(u.NormalizedUserName) && u.NormalizedUserName.Contains(normalisedUserName));
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                var normalisedEmailName = filter.Email.ToUpper();
                pred = pred.Or(u => !string.IsNullOrEmpty(u.NormalizedEmail) && u.NormalizedEmail.Contains(normalisedEmailName));
            }
            //if(filter.Roles?.Any()== true)
            //{
            //    var normalisedEmailName = filter.Email.ToUpper();
            //    pred = pred.Or(u => u.Ro u.NormalizedEmail.Contains(normalisedEmailName));
            //}
            return pred;
        }
    }
}
