using POS.BL.Interfaces;
using POS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Source> Sources { get; }
        IBaseRepository<City> Cities { get; }
        IBaseRepository<Country> Countries { get; }
        IBaseRepository<Governorate> Governorates { get; }
        IBaseRepository<SourceType> SourceType { get; }

        int Complete();
    }
}
