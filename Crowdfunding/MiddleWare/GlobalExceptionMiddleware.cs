
using DataStore.Abstraction.Exceptions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
using System.Net;
//using System.Threading.Tasks;

namespace Crowdfunding.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DatabaseException dbEx)
            {
                _logger.LogError($"Database Exception: {dbEx.Message}");
                await HandleDatabaseExceptionAsync(context, dbEx);
            }

            catch (NullException ex)
            {
                _logger.LogError($"NullValue Exception:{ex.Message}");
                await HandleNullReferenceExceptionAsync(context, ex);
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error: {ex.Message}");
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private static Task HandleDatabaseExceptionAsync(HttpContext context, DatabaseException dbEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Error = "Database Error",
                Message = dbEx.Message}.ToString());
        }

        private static Task HandleNullReferenceExceptionAsync(HttpContext context, NullException nullEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Error = "Null Reference Error",
                Message = nullEx.Message
            }.ToString());
        }

      


        private static Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Error = "An unexpected error occurred",
                Message = "Please try again later."
            }.ToString());
        }
    }
}
