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
    public class SubscribedCourseService : ISubscribedCourseService
    {
        public readonly IBaseRepository<SubscribedCourse> _subscribedCourseRepository;
        public readonly ICourseRepository _courseRepository;
        public SubscribedCourseService(IBaseRepository<SubscribedCourse> subscribedCourseRepository,
                                       ICourseRepository courseRepository)
        {
            _subscribedCourseRepository = subscribedCourseRepository;
            _courseRepository = courseRepository;
        }
        public async Task<IBaseResponse<bool>> CreateSubscribedCourse(int idCourse, string idUser)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var subscribedCourse = new SubscribedCourse
                {
                    UserId = idUser,
                    CourseId = idCourse,
                };
                baseResponse.StatusCode = StatusCode.OK;
                await _subscribedCourseRepository.Create(subscribedCourse);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateSubscribedCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteSubscribedCourse(int idCourse, string idUser)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var course = await _subscribedCourseRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == idUser && x.CourseId == idCourse);
                if (course == null)
                {
                    baseResponse.Description = "Курс не найден";
                    baseResponse.StatusCode = StatusCode.CoursesNotFound;
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                await _subscribedCourseRepository.Delete(course);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSubscribedCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<List<SubscribedCourse>>> GetSubscribedCourseByIdUser(string idUser)
        {
            var baseResponse = new BaseResponse<List<SubscribedCourse>>();
            try
            {
                var subscribedCourse = await _subscribedCourseRepository.GetAll().Where(x => x.UserId == idUser).ToListAsync();
                baseResponse.Data = subscribedCourse;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SubscribedCourse>>()
                {
                    Description = $"[GetSubscribedCourseByIdUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<List<Course>>> GetSubscribedCourse(string idUser)
        {
            var baseResponse = new BaseResponse<List<Course>>();
            try
            {
                var subscribedCourse = await _subscribedCourseRepository.GetAll().Where(x => x.UserId == idUser).ToListAsync();

                var listOfSubscribedCourses = new List<Course>();

                foreach (var course in subscribedCourse)
                {
                    listOfSubscribedCourses.Add(_courseRepository.GetAll().FirstAsync(x => x.Id == course.CourseId).Result);
                }

                if (listOfSubscribedCourses.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 курсов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = listOfSubscribedCourses;
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
