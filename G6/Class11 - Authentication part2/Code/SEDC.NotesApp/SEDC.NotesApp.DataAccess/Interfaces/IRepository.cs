using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity //T must have a column Id
    {
        //CRUD
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity); // void DeleteById(int id)
    }
}
