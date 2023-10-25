using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Middlewares
{
    public class DbInitializationMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitializationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var dbMigrationsService = serviceProvider.GetRequiredService<IDbMigrationsService>();

            await dbMigrationsService.InitialMigration();

            await _next(context);
        }
    }

    public static class DbInitializationMiddlewareExtension
    {
        public static IApplicationBuilder UseDbInitialization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DbInitializationMiddleware>();
        }
    }
}