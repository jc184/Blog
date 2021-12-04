using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        int Count();
    }
}
