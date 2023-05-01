using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Courses.Domain.ViewModules.Account
{
    public class LoginViewModel
    {
        ////[Required(ErrorMessage = "Введите имя")]
        ////[MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        ////[MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        ////public string Name { get; set; }

        ////[Required(ErrorMessage = "Введите пароль")]
        ////[DataType(DataType.Password)]
        ////[Display(Name = "Пароль")]
        ////public string Password { get; set; }
        /// [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
