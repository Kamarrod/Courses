using Courses.Domain.Response;
using Courses.Domain.ViewModules.Course;
using Courses.Domain.ViewModules;
using System;
using System.Collections.Generic;
using Courses.Domain.Response;
using Courses.Domain.ViewModules;
using Сourses.Domain.Entity;
using Сourses.Domain.Entity;

namespace Courses.Service.Interfaces
{
    public interface IPracticalPartService
    {
        Task<IBaseResponse<IEnumerable<PracticalPart>>> GetPracticalParts(int courseId);
        Task<IBaseResponse<bool>> DeletePracticalPart(int courseId, int number);
        Task<IBaseResponse<PracticalPartViewModel>> CreatePracticalPart(PracticalPartViewModel courseViewModule);
        Task<IBaseResponse<PracticalPart>> GetPracticalPart(int courseId, int number);
        Task<IBaseResponse<PracticalPart>> Edit(int courseId, int number, PracticalPartViewModel model);
        Task<IBaseResponse<bool>> CheckAnswer(int courseId, int partId, string answer);
    }
}
