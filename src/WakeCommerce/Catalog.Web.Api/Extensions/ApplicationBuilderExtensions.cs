using Microsoft.AspNetCore.Rewrite;

namespace Catalog.Web.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

        public static IApplicationBuilder ConfigureRewriter(this WebApplication app)
        {
            var options = new RewriteOptions();
            options.AddRedirect("^$", "/api/healthcheck");
            app.UseRewriter(options);

            return app;
        }
    }
}