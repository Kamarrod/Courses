using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сourses.Domain.Entity
{
    public class PracticalPart
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
