using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Presentation.Middleware.ContentSecurityPolicy
{
    public static class ContenteSecurityPolicy
    {
        public static void AddCsp(this IApplicationBuilder app, IConfiguration configuration)
        {
            var csp = configuration.GetSection("Csp").ToString();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", csp);
                await next();
            });
        }
    }
}
