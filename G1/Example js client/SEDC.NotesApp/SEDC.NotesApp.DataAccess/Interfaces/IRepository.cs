using System.Collections.Generic;

namespace SEDC.NotesApp.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        //CRUD
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
