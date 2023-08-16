using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repositories
{
    public interface IRepository<T>
    {
        T? GetById(int id);

        IEnumerable<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
