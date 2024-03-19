using System.Linq.Expressions;

namespace UserManagement.Application.Contracts
{
    public interface IQueryObject<T>
    {
        public  Expression<Func<T, bool>> Query();
    }
}
