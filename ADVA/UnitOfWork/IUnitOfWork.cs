using ADVA.Repository;

namespace ADVA.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Repository { get; }
        void Save();
    }
}
