using AspNetApiPractice.Models.User;
using System.Linq.Expressions;

namespace AspNetApiPractice.Data.Repository.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> All();
        Task<IEnumerable<AppUser>> All(Expression<Func<AppUser, bool>> predicate);
        Task<AppUser?> GetById(string id);
        Task<bool> Exists(string id);
    }
}
