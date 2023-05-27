using Courses.Domain.Entity;
using Courses.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Service.Interfaces
{
    public interface ICompletedPartService
    {
        Task<IBaseResponse<bool>> CreateComletedPart(int idPart, string idUser, int idCourse);
        Task<IBaseResponse<List<CompletedPart>>> GetCompletedPartByIdUser(string idUser);
        Task<IBaseResponse<int>> GetCountCompletedPartsCourse(int courseId, string idUser);

    }
}
