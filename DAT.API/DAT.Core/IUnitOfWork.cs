namespace DAT.Core
{
    public interface IUnitOfWork
    {
        public int SaveChanges();

        public Task<int> SaveChangesAsync();
    }
}