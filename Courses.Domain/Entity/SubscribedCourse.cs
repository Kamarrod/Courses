using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.Entity
{
    public class SubscribedCourse
    {
        public int CourseId { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
