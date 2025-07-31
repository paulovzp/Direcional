using Direcional.Infrastructure.Exceptions;
using Direcional.Infrastructure.Models;
using System.Net;

namespace Direcional.Api.Filters;

internal sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private void LogException(HttpContext context, Exception exception)
    {
        if (exception is DirecionalDomainException ||
            exception is DirecionalNotFoundException ||
            exception.InnerException is DirecionalDomainException ||
            exception.InnerException is DirecionalNotFoundException)
        {
            return;
        }

        logger.LogError(exception, "NotHandledException");
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ErrorResponse();
        var statusCode = new HttpStatusCode();

        GenerateResponse(exception, ref response, ref statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(response.ToString());
    }

    private void GenerateResponse(Exception exception, ref ErrorResponse errorResponse, ref HttpStatusCode statusCode)
    {
        if (exception is DirecionalDomainException || exception.InnerException is DirecionalDomainException)
        {
            statusCode = HttpStatusCode.UnprocessableEntity;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = ((DirecionalDomainException)exception).errors;
        }
        else if (exception is DirecionalNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = null;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            errorResponse.Message = exception.Message;
            errorResponse.ErrorList = null;
        }
    }
}
