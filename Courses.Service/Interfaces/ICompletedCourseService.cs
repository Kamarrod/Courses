using Courses.Domain.Entity;
using Courses.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сourses.Domain.Entity;

namespace Courses.Service.Interfaces
{
    public interface ICompletedCourseService
    {
        Task<IBaseResponse<bool>> CreateCompletedCourse(int idCourse, string idUser);
        Task<IBaseResponse<List<CompletedCourse>>> GetCompletedCourseByIdUser(string idUser);
        Task<IBaseResponse<List<Course>>> GetCompletedCourses(string idUser);
    }
}
