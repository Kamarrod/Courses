using Courses.DAL.Interfaces;
using Courses.Domain.Response;
using Courses.Domain.ViewModules.Course;
using Сourses.Domain.Entity;

namespace Courses.Service.Interfaces
{
    public interface ICourseService
    {
        Task<IBaseResponse<IEnumerable<Course>>> GetCourses();
        Task<IBaseResponse<IEnumerable<Course>>> GetAuthorCourses(string authorId);
        Task<IBaseResponse<bool>> DeleteCourse(int id);
        Task<IBaseResponse<CourseViewModel>> CreateCourse(CourseViewModel courseViewModule);
        Task<IBaseResponse<Course>> GetCourseByName(string name);
        Task<IBaseResponse<CourseViewModel>> GetCourse(int id);

        Task<IBaseResponse<Course>> Edit(int id, CourseViewModel model);

        //Task<IBaseResponse<IEnumerable<PracticalPart>>> GetPracticalPart(CourseViewModel model);

    }
}
