using ADVA.DB;
using ADVA.Repository;

namespace ADVA.UnitOfWork
{
    public class UnitOfWork <T> :IUnitOfWork<T> where T : class
    {

        private readonly AppDBContext _context;
        private IGenericRepository<T> _entity;


        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Repository
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
