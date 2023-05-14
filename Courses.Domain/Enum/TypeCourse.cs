
using System.ComponentModel.DataAnnotations;


namespace Сourses.Domain.Enum
{
    public enum TypeCourse
    {
        [Display(Name = "IT")]
        It = 1,
        [Display(Name = "Математика")]
        Math = 2,
        [Display(Name = "Физика")]
        Physics = 3,
        [Display(Name = "Прочие")]
        Other = 4,
        [Display(Name = "История")]
        History = 5,
        [Display(Name = "География")]
        Geography = 6,
       
    }
}
