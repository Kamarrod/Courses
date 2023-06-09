﻿using Courses.DAL.Interfaces;
using Courses.Domain.Enum;
using Courses.Domain.Response;
using Courses.Domain.ViewModules.Course;
using Courses.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;
using Сourses.Domain.Enum;
using Courses.Domain.Helpers;

namespace Courses.Service.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IBaseResponse<CourseViewModel>> CreateCourse(CourseViewModel courseViewModule)
        {
            var baseResponse = new BaseResponse<CourseViewModel> ();
            try
            {
                var course = new Course()
                {
                    Id = courseViewModule.Id,
                    Description = courseViewModule.Description,
                    CreatedDate = DateTime.Now,
                    Name = courseViewModule.Name,
                    Theory = courseViewModule.Theory,
                    AuthorId = courseViewModule.AuthorId,
                    TypeCourse = (TypeCourse)Convert.ToInt32(courseViewModule.TypeCourse),
                    VideoURL = courseViewModule.VideoURL,
                };
                await _courseRepository.Create(course);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CourseViewModel>()
                {
                    Description = $"[CreateCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteCourse(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var course = await _courseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (course == null)
                {
                    baseResponse.Description = "Курс не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                await _courseRepository.Delete(course);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<Course>>> GetCoursesWithSimilarName(string name)
        {
            var baseResponse = new BaseResponse<IEnumerable<Course>>();

            try
            {
                var course = await _courseRepository.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
                if (course == null)
                {
                    baseResponse.Description = "Курс не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = course;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Course>>()
                {
                    Description = $"[GetCourseWithSimilarName] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<Course>> GetCourseByName(string name)
        {
            var baseResponse = new BaseResponse<Course>();

            try
            {
                var course = await _courseRepository.GetAll().FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                if (course == null)
                {
                    baseResponse.Description = "Курс не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = course;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Course>()
                {
                    Description = $"[GetCourseByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }

        }
        public async Task<IBaseResponse<CourseViewModel>> GetCourse(int id)
        {
            var baseResponse = new BaseResponse<CourseViewModel>();

            try
            {
                var course = await _courseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (course == null)
                {
                    baseResponse.Description = "Курс не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                var data = new CourseViewModel()
                {
                    Id = course.Id,
                    Name = course.Name,
                    AuthorId = course.AuthorId,
                    Theory = course.Theory,
                    CreatedDate = DateTime.Now,//course.CreatedDate,
                    Description = course.Description,
                    TypeCourse = course.TypeCourse.ToString(),
                    VideoURL = course.VideoURL,
                };
                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CourseViewModel>()
                {
                    Description = $"[GetCourse] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }

        }
        public async Task<IBaseResponse<IEnumerable<Course>>> GetCourses()
        {
            var baseResponse = new BaseResponse<IEnumerable<Course>>();

            try
            {
                var courses = await _courseRepository.GetAll().ToListAsync();

                if (courses.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }


                baseResponse.Data = courses;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Course>>()
                {
                    Description = $"[GetCourses] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<Course>>> GetAuthorCourses(string authorId)
        {
            var baseResponse = new BaseResponse<IEnumerable<Course>>();

            try
            {
                var courses = await _courseRepository.GetAll().Where(x => x.AuthorId == authorId).ToListAsync();

                if (courses.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 курсов данного автора";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = courses;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Course>>()
                {
                    Description = $"[GetAuthorCurses] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public async Task<IBaseResponse<Course>> Edit(int id, CourseViewModel model)
        {
            var baseResponse = new BaseResponse<Course>();
            try
            {
                var course = await _courseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (course == null) 
                {
                    baseResponse.StatusCode = StatusCode.CourseNotFound;
                    baseResponse.Description = "Course not found";
                    return baseResponse;
                }

                course.Description = model.Description;
                course.CreatedDate = DateTime.Now;
                course.AuthorId = model.AuthorId;
                course.Theory = model.Theory;
                course.Name = model.Name;
                course.PracticalParts = await _courseRepository.GetPracticalParts(id);
                course.TypeCourse = (TypeCourse)Convert.ToInt32(model.TypeCourse);
                course.VideoURL = model.VideoURL;
                course.PracticalParts = model.PracticalParts.ToList();
                baseResponse.StatusCode = StatusCode.OK;
                await _courseRepository.Update(course);
                return baseResponse;
            }
            catch (Exception ex) 
            {
                return new BaseResponse<Course>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalStatusError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((TypeCourse[])Enum.GetValues(typeof(TypeCourse)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalStatusError,
                };
            }
        }
    }
}
