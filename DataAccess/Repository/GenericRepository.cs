using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public abstract class GenericRepository<T> : IRepository<T> where T : Entity
    {
        internal DBContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(DBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual T GetById(object id) 
        {
            return (T)dbSet.Find(id);
        }

        public virtual void Insert(T entity) 
        {
            dbSet.Add(entity);
        }

        public T[] GetAll()
        {
            return dbSet.ToArrayAsync().Result as T[];
        }
    }
}
