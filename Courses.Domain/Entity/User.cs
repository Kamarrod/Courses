using Courses.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Courses.Domain.Entity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}