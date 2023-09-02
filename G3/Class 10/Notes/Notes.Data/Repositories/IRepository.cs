using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);

        IEnumerable<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
