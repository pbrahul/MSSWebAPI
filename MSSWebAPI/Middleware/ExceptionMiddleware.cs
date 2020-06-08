using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility.Exceptions;
using Utility.Helper;

namespace MSSWebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 await _next(context); 
            }
            catch (Exception e)
            {

                await HendleMyAllExceptions(e, context);
            }
           
            // Call the next delegate/middleware in the pipeline
            

        }

        private async Task HendleMyAllExceptions(Exception e, HttpContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ErrorResponse();
            error.StatusCode = (int) code;
            error.Message = e.Message;

            if (_env.IsDevelopment())
            {
                error.DeveloperMessage = e.StackTrace;
            }
            else
            {
                error.DeveloperMessage = e.Message;
            }

            switch (e)
            {
                case MyAppException myAppException:
                    {
                        error.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    }
                case UnauthorizedAccessException unauthorizedAccessException:
                    error.StatusCode = (int)HttpStatusCode.Unauthorized;
                    error.Message = "You are not authorized";
                    break;

                default:
                    {
                        break;
                    }
                   
            }

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
