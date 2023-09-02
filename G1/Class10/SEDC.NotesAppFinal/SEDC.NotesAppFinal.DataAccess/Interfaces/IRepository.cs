namespace SEDC.NotesAppFinal.DataAccess.Interfaces
{
    using SEDC.NotesAppFinal.Domain.Models;

    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
