namespace AspNetApiPractice.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}