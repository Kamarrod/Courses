using System.ComponentModel.DataAnnotations;
using Сourses.Domain.Enum;

namespace Сourses.Domain.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Theory { get; set; }
        public List<PracticalPart> PracticalParts { get; set; }
        public TypeCourse TypeCourse { get; set; }
        public string? VideoURL { get; set; }

    }
}
