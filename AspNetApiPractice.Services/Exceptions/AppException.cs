using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Services.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }
        public AppException(params string[] errors) 
            : base(ConcatenateErrors(errors)) { }
        public AppException(IEnumerable<string> errors)
            : base(ConcatenateErrors(errors)) { }

        private static string ConcatenateErrors(params string[] errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string error in errors)
            {
                sb.AppendLine(error);
            }
            return sb.ToString();
        }
        private static string ConcatenateErrors(IEnumerable<string> errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string error in errors)
            {
                sb.AppendLine(error);
            }
            return sb.ToString();
        }
    }
}
