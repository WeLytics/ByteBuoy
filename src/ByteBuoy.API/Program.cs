using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.OpenApi.Models;
using ByteBuoy.API.Validation;
using ByteBuoy.API.Extensions;
using System.Reflection;
using FluentValidation;
using ByteBuoy.API.Installers;
using Microsoft.Data.SqlClient;


namespace ByteBuoy.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateSlimBuilder(args);
			var config = builder.Configuration;
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllers();
			builder.Services.AddCorsPolicy(config);
			builder.Services.AddAuthentication();
			builder.Services.AddAuthorization();

			builder.Services.AddDbContext<ByteBuoyDbContext>(options =>
				options.UseSqlite(config.GetConnectionString("Default")));

			builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("V1", new OpenApiInfo() { Title = "ByteBuoy", Version = "V1.0" });
				options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
				options.CustomSchemaIds(x => x.FullName);
			}
			);
			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Serilog
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration.GetSection("Logging"))
				.WriteTo.Console()
				.WriteTo.File("logs/bytebuoy_web.log", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			builder.Host.UseSerilog();

			//builder.Services.ConfigureHttpJsonOptions(options =>
			//{
			//	options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
			//});


			var app = builder.Build();

			app.MapGet("/health", () => "OK");


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint($"/swagger/V1/swagger.json", "Vr1.0");
				}
				);
			}

			app.UseCorsPolicy();
			app.UseApiKeyMiddlewareOnRoutes();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

			PrepareDatabase(builder);
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<ByteBuoyDbContext>();
				context.Database.Migrate();
			}

			if (app.Environment.IsDevelopment())
			{
				app.MapFallback(async context =>
				{
					var response = new
					{
						Message = $"ByteBuoy: Request DEBUG Fallback for {context.Request.Method} {context.Request.Path}",
						Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
					};

					await context.Response.WriteAsJsonAsync(response);
				});
			};

			app.Run("http://0.0.0.0:5000");
		}

		private static void PrepareDatabase(WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration.GetConnectionString("Default");
			var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
			var databaseDirectory = Path.GetDirectoryName(connectionBuilder.DataSource);

			if (databaseDirectory != null && !Directory.Exists(databaseDirectory))
			{
				try
				{
					Directory.CreateDirectory(databaseDirectory!);
					Console.WriteLine("Database directory created successfully.");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Failed to create directory: {ex.Message}");
				}
			}
		}
	}
}
