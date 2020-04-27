
using Microsoft.AspNetCore.Builder;
using Ottobo.Api.Middlewares;

namespace Ottobo.Api.Extensions
{

    public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}
}