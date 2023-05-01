using Courses.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Courses.Domain.Entity
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        //public string Password { get; set; }

        //public Role Role { get; set; }
    }
}
