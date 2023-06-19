using System.Linq.Expressions;

namespace AspNetApiPractice.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> All(Expression<Func<T, bool>> predicate);
        Task<T?> GetById(int id);
        T Add(T entity);
        //T Update(T entity); pointless
        void Delete(T entity);
    }
}
