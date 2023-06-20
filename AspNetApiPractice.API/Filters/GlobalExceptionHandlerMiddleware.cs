using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AspNetApiPractice.API.Filters
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var response = new ResponseViewModel<object>
                        (null, ex.Message, success: false);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if(ex.Payload is not null)
                {
                    var response = new ResponseViewModel<object>(ex.Payload, message: ex.Message, success: false);
                    await context.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    var response = new ResponseViewModel<string>
                            (ex.Message, success: false);
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);

                var response = new ResponseViewModel<string>
                        ("Something went wrong", success: false);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
