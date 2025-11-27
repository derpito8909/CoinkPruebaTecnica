using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql;
using Coink.Presentation.Common.Errors;

namespace Coink.Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (PostgresException ex)
        {
            await HandlePostgresExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleUnknownExceptionAsync(context, ex);
        }
    }

    private async Task HandleValidationExceptionAsync(
        HttpContext context,
        ValidationException ex)
    {
        _logger.LogWarning(ex, "Validation error");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var traceId = context.TraceIdentifier;

        var response = new ErrorResponse
        {
            Status = context.Response.StatusCode,
            Message = "Validation error",
            TraceId = traceId,
            Errors = ex.Errors
                .Select(e => new ValidationError
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                })
                .ToList()
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

    private async Task HandlePostgresExceptionAsync(
        HttpContext context,
        PostgresException ex)
    {
        _logger.LogWarning(ex, "Database error");

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var traceId = context.TraceIdentifier;

        var response = new ErrorResponse
        {
            Status = context.Response.StatusCode,
            Message = ex.MessageText,
            TraceId = traceId
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

    private async Task HandleUnknownExceptionAsync(
        HttpContext context,
        Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var traceId = context.TraceIdentifier;

        var response = new ErrorResponse
        {
            Status = context.Response.StatusCode,
            Message = "Internal server error",
            TraceId = traceId
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
