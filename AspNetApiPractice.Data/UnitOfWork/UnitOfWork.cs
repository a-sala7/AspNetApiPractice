namespace AspNetApiPractice.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Entities dbContext;
    public UnitOfWork(Entities dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await this.dbContext.SaveChangesAsync();
    }
}
