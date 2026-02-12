namespace Prueba_Completa_NET.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Prueba_Completa_NET.DTOs;
    using System.Net;
    using Prueba_Completa_NET.Exceptions;
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación: {Message}", ex.Message);
                httpContext.Response.ContentType = "application/json";

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error de validación",
                    Errors = ex.Errors,
                    Data = null
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no controlada: {Message}",ex.Message );
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ApiResponse<string>
            {
                Success = false,
                Message = "Ocurrió un error inesperado",
                Errors = new List<string> { exception.Message },
                Data = null
            };
            return context.Response.WriteAsJsonAsync(response);
        }


    }
}
