using Microsoft.AspNetCore.Builder;

namespace YCompany.Claims.ExceptionHandling
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}