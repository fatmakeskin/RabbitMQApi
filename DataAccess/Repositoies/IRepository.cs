using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositoies
{
    public interface IRepository<T> where T : class
    {
        T Get(T model);
        void Add(T model);

    }
}
