namespace ByteBuoy.API.Installers
{
	internal static class Cors
	{
		private const string CorsPolicy = nameof(CorsPolicy);

		internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
		{
			var corsSettings = config.GetSection("Cors").Get<CorsSettings>();
			if (corsSettings == null)
				return services;
			var origins = new List<string>();
			if (corsSettings.Origins is not null)
				origins.AddRange(corsSettings.Origins.Split(';', StringSplitOptions.RemoveEmptyEntries));

			return services.AddCors(opt =>
				opt.AddPolicy(CorsPolicy, policy =>
					policy.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials()
						.WithOrigins([.. origins])));
		}

		internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
			app.UseCors(CorsPolicy);
	}
}
