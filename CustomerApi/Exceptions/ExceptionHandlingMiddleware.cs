using System.Net;
using CustomerApi.Helpers;

namespace CustomerApi.Exceptions;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (BadRequestException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        ErrorResponse errorResponse = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException :
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = exception.Message;
                break;
            
            case BadRequestException :
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = exception.Message;
                break;
            
            case not null :
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = exception.Message;
                break;
        }

        await context.Response.WriteAsJsonAsync(errorResponse);
    } 
}