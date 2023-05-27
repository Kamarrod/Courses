
namespace Courses.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
        IQueryable<T> GetAll();
    }
}
