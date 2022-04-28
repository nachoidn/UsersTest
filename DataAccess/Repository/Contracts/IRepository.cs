using System.Collections.Generic;

namespace DataAccess
{
    interface IRepository<T> where T : Entity
    {
        public T GetById(object id);

        public void Insert(T entity);

        public T[] GetAll();
    }
}
