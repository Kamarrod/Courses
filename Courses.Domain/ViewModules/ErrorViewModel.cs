using Courses.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.ViewModules
{
    public class ErrorViewModel
    {
        public string Desctiption { get; set; }
        public StatusCode statusCode { get; set; }
    }
}
