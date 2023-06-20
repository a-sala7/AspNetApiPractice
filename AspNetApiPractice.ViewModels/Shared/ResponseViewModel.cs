using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.ViewModels.Shared;
public class ResponseViewModel<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }

    public ResponseViewModel(T? data, string message = "", bool success = true)
    {
        Data = data;
        Success = success;
        Message = message;
    }
}
