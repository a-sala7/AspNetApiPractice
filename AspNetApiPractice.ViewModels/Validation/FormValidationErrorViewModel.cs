using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.ViewModels.Validation
{
    public class FormValidationErrorViewModel
    {
        public string Field { get; }
        public string Error { get; }
        public FormValidationErrorViewModel(string field, string error)
        {
            Field = field;
            Error = error;
        }
        public IEnumerable<FormValidationErrorViewModel> FromDictionary(Dictionary<string, string> dict)
        {
            return dict.Select(i =>  new FormValidationErrorViewModel(i.Key, i.Value));
        }
    }
}
