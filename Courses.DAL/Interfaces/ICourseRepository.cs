using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сourses.Domain.Entity;

namespace Courses.DAL.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<List<PracticalPart>> GetPracticalParts(int id);
    }
}
