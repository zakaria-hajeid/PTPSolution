using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Infrastructure.Middlwars
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var exceptionStatusCode = GetStatusCode(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionStatusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ex));
        }
        private HttpStatusCode GetStatusCode(Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (ex is ValidationException) code = HttpStatusCode.BadRequest;
            else if (ex is FormatException) code = HttpStatusCode.BadRequest;
            else if (ex is AuthenticationException) code = HttpStatusCode.Forbidden;
            else if (ex is NotImplementedException) code = HttpStatusCode.NotImplemented;
            else if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

            return code;
        }
    }
}
