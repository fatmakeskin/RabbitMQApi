using Data.Context;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositoies
{
    public class MySqlRepository<T> :IDisposable, IRepository<T> where T:class
    {
        private MasterContext masterContext;

        public MySqlRepository()
        {
        }
        public MySqlRepository(MasterContext context)
        {
            masterContext = context;
        }

        public void Add(T model)
        {
            masterContext.Set<T>().Add(model);
        }

        public void Dispose()
        {
            masterContext.SaveChanges();
        }

        public T Get(T model)
        {
           return masterContext.Set<T>().Find(model);
        }
    }
}
