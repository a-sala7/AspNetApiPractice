using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.ViewModels.Shared;
public class FormValidationError
{
    public string Field { get; }
    public IEnumerable<string> Errors { get; }
    public FormValidationError(string field, string error)
    {
        Field = field;
        Errors = new string[] {
            error
        };
    }
    public FormValidationError(string field, IEnumerable<string> errors)
    {
        Field = field;
        Errors = errors;
    }
    public static IEnumerable<FormValidationError> FromDictionary(IDictionary<string, IEnumerable<string>> dict)
    {
        return dict.Select(i =>  new FormValidationError(i.Key, i.Value));
    }
}