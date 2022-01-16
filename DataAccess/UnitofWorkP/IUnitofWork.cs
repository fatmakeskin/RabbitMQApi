using DataAccess.Repositoies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitofWorkP
{
    public interface IUnitofWork : IDisposable
    {
        IRepository<T> repository<T>() where T : class;
        int Complate();
    }
}
