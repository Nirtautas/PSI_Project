using System.Net;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Data.Exceptions;

public class SessionCredentialExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public SessionCredentialExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IDataLogger logger)
    {
        try
        {
            await _next(context);
        }
        catch (SessionCredentialException ex)
        {
            context.Response.StatusCode = 401; // Unauthorized
            logger.Log($"Unauthorized access with Session Credential Exception: { ex.Message}");
            context.Response.Redirect("/Home");
        }
    }
}