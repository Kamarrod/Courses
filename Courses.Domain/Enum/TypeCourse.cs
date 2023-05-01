
using System.ComponentModel.DataAnnotations;


namespace Сourses.Domain.Enum
{
    public enum TypeCourse
    {
        [Display(Name = "IT")]
        It = 0,
        [Display(Name = "Математика")]
        Math = 1,
        [Display(Name = "Физика")]
        Physics = 2,
        [Display(Name = "Прочие")]
        Other = 3
    }
}
