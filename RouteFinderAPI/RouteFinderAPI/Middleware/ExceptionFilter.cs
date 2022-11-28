using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RouteFinderAPI.Middleware;
//TODO: add libaries for commented out code. Please remove this comment when it is done.
public class ExceptionFilter
{
    [ExcludeFromCodeCoverage]
    internal class GeneralExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public GeneralExceptionFilter(IWebHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;

        public void OnException(ExceptionContext context)
        {
            if (context == null) return;

            var response = context.HttpContext.Response;
            response.StatusCode = (int)RetrieveStatusCodeForException(context.Exception);
            //response.ContentType = MimeTypes.Application.Json;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = _hostingEnvironment.IsDevelopment() ? context.Exception.StackTrace : string.Empty,
            });
        }
        
        private static HttpStatusCode RetrieveStatusCodeForException(System.Exception exception)
        {
            if (exception is ArgumentException) return HttpStatusCode.BadRequest;
            //if (exception is NotFoundException) return HttpStatusCode.NotFound; 

            return HttpStatusCode.InternalServerError;
        }
    }
}