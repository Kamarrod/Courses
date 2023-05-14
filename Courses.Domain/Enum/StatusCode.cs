using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.Enum
{
    public enum StatusCode
    {
        CoursesNotFound = 10,
        CompletedPartNotFound = 11,
        PracticsNotFound = 12,
        CourseNotFound = 100,
        OK = 200,
        InternalStatusError = 500
    }
}
