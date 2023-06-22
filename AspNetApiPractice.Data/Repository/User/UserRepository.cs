using AspNetApiPractice.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspNetApiPractice.Data.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly Entities dbContext;

        public UserRepository(Entities dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AppUser>> All()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> All(Expression<Func<AppUser, bool>> predicate)
        {
            return await dbContext
                .Users
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<AppUser?> GetById(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<bool> Exists(string id)
        {
            return await dbContext.Users.AnyAsync(x => x.Id == id);
        }
    }
}
