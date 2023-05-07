using Courses.DAL.Interfaces;
using Courses.DAL.Repositories;
using Courses.Domain.Entity;
using Courses.Domain.Enum;
using Courses.Domain.Response;
using Courses.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courses.Service.Implementations
{
    public class CompletedCourseService : ICompletedCourseService
    {
        private readonly IBaseRepository<CompletedCourse> _completedCourseRepository;

        public CompletedCourseService(IBaseRepository<CompletedCourse> completedCourseRepository)
        {
            _completedCourseRepository = completedCourseRepository;
        }

        public async Task<IBaseResponse<bool>> CreateCompletedCourse(int idCourse, string idUser)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var completedCourse = new CompletedCourse
                {
                    UserId = idUser,
                    CourseId= idCourse,
                };
                baseResponse.StatusCode = StatusCode.OK;
                await _completedCourseRepository.Create(completedCourse);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateCompletedCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<List<CompletedCourse>>> GetCompletedCourseByIdUser(string idUser)
        {
            var baseResponse = new BaseResponse<List<CompletedCourse>>();
            try
            {
                var completedCourse = await _completedCourseRepository.GetAll().Where(x => x.UserId == idUser).ToListAsync();
                if (completedCourse.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 вопросов данного автора";
                    baseResponse.StatusCode = StatusCode.CompletedPartNotFound;
                    return baseResponse;
                }
                baseResponse.Data = completedCourse;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CompletedCourse>>()
                {
                    Description = $"[GetCompletedCourseByIdUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
    }
}
