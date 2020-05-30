using Microsoft.AspNetCore.Builder;

namespace Ottobo.Extensions
{

    public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging <T>(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<T>();
    }
}
}