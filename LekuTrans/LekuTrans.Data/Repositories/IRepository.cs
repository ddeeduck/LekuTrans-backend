using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LekuTrans.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        void Save();
    }

}
