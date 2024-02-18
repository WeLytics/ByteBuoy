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
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using ByteBuoy.Domain.Entities.Identity;
using ByteBuoy.Infrastructure.Services.Mails;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;


namespace ByteBuoy.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateSlimBuilder(args);
			var config = builder.Configuration;
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllers();
			builder.Services.AddCorsPolicy(config);
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie()
				.AddJwtBearer("Identity.Bearer", options =>
				{
					// Configure JWT Bearer token validation settings here
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// Your token validation parameters
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "YourIssuer",
						ValidAudience = "YourAudience",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
					};
				});

			builder.Services.AddAuthorization();

			builder.Services.AddDbContext<ByteBuoyDbContext>(options =>
				options.UseSqlite(config.GetConnectionString("Default")));

			builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
			})
							.AddEntityFrameworkStores<ByteBuoyDbContext>()
							.AddDefaultTokenProviders()
							.AddApiEndpoints();

			builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();

			builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
			builder.Services.AddTransient<IMetricsConsolidationService, MetricsConsolidationService>();

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

			app.MapIdentityApi<ApplicationUser>();
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
				await context.Database.MigrateAsync();

				await CreateAdminUserIfNotExist(services);
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
			app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager,
					[FromBody] object empty) =>
							{
								if (empty != null)
								{
									await signInManager.SignOutAsync();
									return Results.Ok();
								}
								return Results.Unauthorized();
							})
				.WithOpenApi()
				.RequireAuthorization();
			app.Run("http://0.0.0.0:5000");
		}

		private static async Task CreateAdminUserIfNotExist(IServiceProvider services)
		{
			var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

			var adminRoleName = "admin";
			var adminEmail = "admin@example.com";
			var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");


			if (await userManager.Users.AnyAsync())
				return;

			// Ensure the admin role exists
			if (!await roleManager.RoleExistsAsync(adminRoleName))
			{
				await roleManager.CreateAsync(new ApplicationRole(adminRoleName));
			}

			// Check if any user exists
			if (!await userManager.Users.AnyAsync())
			{

				if (string.IsNullOrEmpty(adminPassword))
				{
					throw new InvalidOperationException("No admin password provided in the environment variables.");
				}


				// Create the admin user
				var adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
				var result = await userManager.CreateAsync(adminUser, adminPassword);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, adminRoleName);
				}
				else
				{
					await Console.Out.WriteLineAsync("No users available. Failed to create the admin user.");
				}
			}
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
				catch (IOException ex)
				{
					Console.WriteLine($"Failed to create directory: {ex.Message}");
				}
			}
		}
	}
}
