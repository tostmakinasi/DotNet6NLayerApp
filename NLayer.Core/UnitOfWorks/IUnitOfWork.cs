namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
        void CommitChanges();
    }
}
