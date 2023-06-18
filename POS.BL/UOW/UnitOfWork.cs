using POS.BL.Interfaces;
using POS.BL.Repositories;
using POS.DAL.Database;
using POS.DAL.Models;

namespace POS.BL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext _context;
        public IBaseRepository<Source> Sources { get; private set; }

        public IBaseRepository<City> Cities { get; private set; }

        public IBaseRepository<Country> Countries { get; private set; }

        public IBaseRepository<Governorate> Governorates { get; private set; }

        public IBaseRepository<SourceType> SourceType { get; private set; }

        public UnitOfWork(POSContext context)
        {
            _context = context;
            Sources = new BaseRepository<Source>(_context);
            Cities = new BaseRepository<City>(_context);
            Governorates = new BaseRepository<Governorate>(_context);
            Countries = new BaseRepository<Country>(_context);
            SourceType = new BaseRepository<SourceType>(_context);
        }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
