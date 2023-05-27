using Courses.DAL.Interfaces;
using Courses.Domain.Entity;
using Courses.Domain.Enum;
using Courses.Domain.Response;
using Courses.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сourses.Domain.Entity;

namespace Courses.Service.Implementations
{
    public class CompletedPartService : ICompletedPartService
    {
        protected readonly IBaseRepository<CompletedPart> _completedPartRepository;
        public CompletedPartService(IBaseRepository<CompletedPart> completedPartRepository)
        {
            _completedPartRepository = completedPartRepository;
        }
        public async Task<IBaseResponse<bool>> CreateComletedPart(int idPart, string idUser, int idCourse)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var completedPart = new CompletedPart
                {
                    UserId = idUser,
                    PracticalPartId = idPart,
                    CourseId = idCourse,
                };
                baseResponse.StatusCode = StatusCode.OK;
                await _completedPartRepository.Create(completedPart);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateCompetedPart] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<List<CompletedPart>>> GetCompletedPartByIdUser(string idUser)
        {
            var baseResponse = new BaseResponse<List<CompletedPart>>();
            try
            {
                var completedPart = await _completedPartRepository.GetAll().Where(x => x.UserId == idUser).ToListAsync();
                if (completedPart.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 вопросов данного автора";
                    baseResponse.StatusCode = StatusCode.CompletedPartNotFound;
                    return baseResponse;
                }
                baseResponse.Data = completedPart;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CompletedPart>>()
                {
                    Description = $"[GetCopletedPartBuIdUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<int>> GetCountCompletedPartsCourse(int courseId, string idUser)
        {
            var baseResponse = new BaseResponse<int>();
            try
            {
                var completedPart = await _completedPartRepository.GetAll().Where(x => x.CourseId == courseId && x.UserId == idUser).ToListAsync();
                if (completedPart != null)
                    baseResponse.Data = completedPart.Count();
                else baseResponse.Data = 0;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>()
                {
                    Description = $"[GetCountCompletedPartsCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
    }
}
