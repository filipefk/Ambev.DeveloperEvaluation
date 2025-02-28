using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        else if (exception is ExceptionBase exceptionBase)
        {
            await HandleProjectException(context, exceptionBase);
        }
        else
        {
            await ThrowUnknowException(context, exception);
        }
    }

    private static JsonSerializerOptions GetJsonOptions()
    {
        return new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ApiResponse
        {
            Success = false,
            Message = "Validation Failed",
            Errors = exception.Errors
                .Select(error => (ValidationErrorDetail)error)
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, GetJsonOptions()));
    }

    private static Task HandleProjectException(HttpContext context, ExceptionBase exception)
    {
        context.Response.StatusCode = (int)exception.GetStatusCode();

        var response = new ApiResponse
        {
            Success = false,
            Message = exception.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, GetJsonOptions()));
    }

    private static Task ThrowUnknowException(HttpContext context, Exception exception)
    {
        var msg = exception.Message;
        if (exception.InnerException != null) msg += $" - {exception.InnerException.Message}";
        Log.Error($"Unhandled error - {msg}");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ApiResponse
        {
            Success = false,
            Message = "Unknown error"
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, GetJsonOptions()));
    }
}
