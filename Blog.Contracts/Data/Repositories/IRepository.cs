using System.Linq.Expressions;

namespace Blog.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        //IEnumerable<T> GetAll();
        IQueryable<T> GetAll();
        T Get(object id);
        T GetByIdInclude(int id, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        int Count();
    }
}
