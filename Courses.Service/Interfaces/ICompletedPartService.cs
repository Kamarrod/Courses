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
        Task<IBaseResponse<bool>> CreateComletedPart(int idPart, string idUser);
        Task<IBaseResponse<List<CompletedPart>>> GetCompletedPartBuIdUser(string idUser);

    }
}
