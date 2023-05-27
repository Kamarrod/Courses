using Courses.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MinLength(3 , ErrorMessage = "Минимальная длина названия курса 3 символа")]
        [MaxLength(20, ErrorMessage = "Минимальная длина названия курса 20 символа")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Максимальная длина описания курса 50 символа")]
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Theory { get; set; }
        public List<PracticalPart>? PracticalParts { get; set; }
        public string TypeCourse { get; set; }

        [YouTubeUrl(ErrorMessage = "Ссылка должна быть на YouTube видео")]
        [MaxLength(2083, ErrorMessage = "Длинна ссылки не должна привышать 2083 символа")]
        public string? VideoURL { get; set; }
    }
}
