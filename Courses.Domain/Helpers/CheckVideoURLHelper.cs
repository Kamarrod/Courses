using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Courses.Domain.Helpers
{
    public class YouTubeUrlAttribute : ValidationAttribute
    {
        private const string regex = @"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$";
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }
            string url = (string)value;
            return Regex.IsMatch(url, regex);
        }
    }
}
