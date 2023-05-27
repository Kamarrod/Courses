using Courses.DAL.Interfaces;
using Courses.DAL.Repositories;
using Courses.Domain.Entity;
using Courses.Domain.Enum;
using Courses.Domain.Response;
using Courses.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;

namespace Courses.Service.Implementations
{
    public class CompletedCourseService : ICompletedCourseService
    {
        private readonly IBaseRepository<CompletedCourse> _completedCourseRepository;
        private readonly ICourseRepository _courseRepository;

        public CompletedCourseService(IBaseRepository<CompletedCourse> completedCourseRepository,
                                      ICourseRepository courseRepository)
        {
            _completedCourseRepository = completedCourseRepository;
            _courseRepository = courseRepository;
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
                    baseResponse.Description = "Найдено 0 курсов";
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

        public async Task<IBaseResponse<List<Course>>> GetCompletedCourses(string idUser)
        {
            var baseResponse = new BaseResponse<List<Course>>();
            try
            {
                var completedCourse = await _completedCourseRepository.GetAll().Where(x => x.UserId == idUser).ToListAsync();

                var listOfCompletedCourses = new List<Course>();

                foreach(var course in completedCourse)
                {
                    listOfCompletedCourses.Add(_courseRepository.GetAll().FirstAsync(x => x.Id == course.CourseId).Result);
                }

                if (listOfCompletedCourses.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 курсов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = listOfCompletedCourses;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Course>>()
                {
                    Description = $"[GetCompletedCourses] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
    }
}
