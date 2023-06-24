using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
