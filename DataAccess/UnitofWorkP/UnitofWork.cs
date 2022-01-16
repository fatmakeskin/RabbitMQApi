using Data.Context;
using DataAccess.Repositoies;
using DataAccess.UnitofWorkP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitofWork
{
    public class UnitofWork : IDisposable, IUnitofWork
    {
        private MasterContext masterContext;
        public MasterContext _masterContext
        {
            get
            {
                if (masterContext == null)
                    masterContext = new MasterContext();
                return masterContext;
            }
            set { masterContext = value; }
        }
        public int Complate()
        {
            return masterContext.SaveChanges();
        }

        public void Dispose()
        {
            masterContext.SaveChanges();
        }

        public IRepository<T> repository<T>() where T : class
        {
            return new MySqlRepository<T>(_masterContext);
        }



    }
}
