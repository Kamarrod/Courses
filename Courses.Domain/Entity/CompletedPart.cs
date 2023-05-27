using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.Entity
{
    public class CompletedPart
    {
        public string UserId { get; set; }
        public int PracticalPartId { get; set; }
        public int CourseId { get; set; }

    }
}
