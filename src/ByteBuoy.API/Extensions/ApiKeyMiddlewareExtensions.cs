using ByteBuoy.API.Middlewares;

namespace ByteBuoy.API.Extensions
{
	public static class ApiKeyMiddlewareExtensions
	{
		public static IApplicationBuilder UseApiKeyMiddlewareOnRoutes(this IApplicationBuilder app)
		{
			var routes = new string[] { "/api" }; 

			return app.UseWhen(context =>
			{
				return routes.Any(route => context.Request.Path.StartsWithSegments(route, StringComparison.OrdinalIgnoreCase));
			}, appBuilder =>
			{
				appBuilder.UseMiddleware<ApiKeyMiddleware>();
			});
		}
	}

}
