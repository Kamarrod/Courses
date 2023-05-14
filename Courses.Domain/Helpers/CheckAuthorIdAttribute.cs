using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain.Helpers
{
    public class CheckAuthorIdAttribute : Attribute
    {
        private string _string1;
        private string _string2;

        public CheckAuthorIdAttribute(string string1, string string2)
        {
            _string1 = string1;
            _string2 = string2;
        }

        public bool CompareStrings()
        {
            if (_string1 == null || _string2 == null) return false;
            return _string1.Equals(_string2);
        }
    }
}
