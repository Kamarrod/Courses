using Courses.Domain.Entity;
using Courses.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Service.Interfaces
{
    public interface ISubscribedCourseService
    {
        Task<IBaseResponse<bool>> CreateSubscribedCourse(int idCourse, string idUser);
        Task<IBaseResponse<List<SubscribedCourse>>> GetSubscribedCourseByIdUser(string idUser);

        Task<IBaseResponse<bool>> DeleteSubscribedCourse(int idCourse, string idUser);
    }
}
