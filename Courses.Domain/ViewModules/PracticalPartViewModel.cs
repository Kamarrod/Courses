using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.ViewModules
{
    public class PracticalPartViewModel
    {
        public int CourseId { get; set; }

        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}

