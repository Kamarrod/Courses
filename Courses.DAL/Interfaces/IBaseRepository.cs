using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        //<T> GetAsync(int id);

        Task<bool> Delete(T entity);

        //IEnumerable<T> Select();
        //Task<List<T>> Select();
        Task<T> Update(T entity);

        IQueryable<T> GetAll();
    }
}
