using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Services.Exceptions
{
    public class AppValidationException : AppException
    {
        public IDictionary<string, string>? ErrorDictionary { get; }
        public AppValidationException(string message) : base(message) { }
        public AppValidationException(params string[] errors)
            : base(errors) { }
        public AppValidationException(IEnumerable<string> errors)
            : base(errors) { }

        public AppValidationException(IDictionary<string, string> errorDictionary)
        {
            ErrorDictionary = errorDictionary;
        }
    }
}
