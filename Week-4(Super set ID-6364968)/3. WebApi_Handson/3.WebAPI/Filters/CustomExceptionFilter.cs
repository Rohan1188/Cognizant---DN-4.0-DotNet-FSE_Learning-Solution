using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace Doc3_WebAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "error_log.txt");

            File.AppendAllText(logPath,
                $"[{DateTime.Now}] EXCEPTION: {exception.Message}{Environment.NewLine}");

            context.Result = new ObjectResult("Something went wrong, please try again later.")
            {
                StatusCode = 500
            };
        }
    }
}
