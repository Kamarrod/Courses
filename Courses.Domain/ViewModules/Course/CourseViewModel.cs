using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сourses.Domain.Entity;
using Сourses.Domain.Enum;

namespace Courses.Domain.ViewModules.Course
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Theory { get; set; }

        public List<PracticalPart>? PracticalParts { get; set; }

        public string TypeCourse { get; set; }
    }
}
