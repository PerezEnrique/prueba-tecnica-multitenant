using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Middlewares
{
    public class TenantIdMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var slugTenant = context.GetRouteValue("slugTenant")?.ToString();

            var baseString = configuration.GetConnectionString("TenantProductsBase");
            var connectionString = string.Format(baseString, slugTenant);

            var productsService = serviceProvider.GetRequiredService<IProductsService>();
            productsService.ConnectionString = connectionString;

            await _next(context);
        }
    }

    public static class TenantIdMiddlewareExtension
    {
        public static IApplicationBuilder UseTenantId(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TenantIdMiddleware>();
        }
    }
}