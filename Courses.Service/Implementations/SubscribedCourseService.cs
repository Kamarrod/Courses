using Courses.DAL.Interfaces;
using Courses.DAL.Repositories;
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

namespace Courses.Service.Implementations
{
    public class SubscribedCourseService : ISubscribedCourseService
    {
        public readonly IBaseRepository<SubscribedCourse> _subscribedCourseRepository;

        public SubscribedCourseService(IBaseRepository<SubscribedCourse> subscribedCourseRepository)
        {
            _subscribedCourseRepository = subscribedCourseRepository;
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
                    baseResponse.Description = "Запись не найдена";
                    baseResponse.StatusCode = StatusCode.OK;
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
                if (subscribedCourse.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 вопросов данного автора";
                    baseResponse.StatusCode = StatusCode.CompletedPartNotFound;
                    return baseResponse;
                }
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
    }
}
