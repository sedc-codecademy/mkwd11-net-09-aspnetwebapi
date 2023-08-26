namespace SEDC.NotesApp.DataAccess
{
    public interface IRepository<T>
    {
        //CRUD
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //This method is custom method for showing sql injection attack
        T GetByTag(string tag);
    }
}
