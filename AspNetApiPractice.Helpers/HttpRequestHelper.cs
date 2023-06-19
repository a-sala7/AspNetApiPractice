using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AspNetApiPractice.Helpers;

public static class HttpRequestHelper
{
    public static HttpContext Current => new HttpContextAccessor().HttpContext;
    public static string GetHeaderValue(string key)
    {
        try
        {
            StringValues header;
            if (Current != null)
            {
                Current.Request.Headers.TryGetValue(key.ToLower(), out header);
                return header.ToString();
            }
            else
                return "";

        }
        catch (Exception ex)
        {
            return "";
        }
    }
}
