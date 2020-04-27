
using Microsoft.AspNetCore.Builder;

namespace Ottobo.Extensions
{

    public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging <T>(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<T>();
    }
}
}