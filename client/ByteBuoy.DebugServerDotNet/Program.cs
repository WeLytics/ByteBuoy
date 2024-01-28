var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

// Middleware to log all requests
app.Use(async (context, next) =>
{
	Console.WriteLine($"Received request for {context.Request.Method} {context.Request.Path}");
	await next.Invoke();
});

// Catch-all route handler for any type of request
app.MapFallback(async context =>
{
	var response = new
	{
		Message = $"ByteBuoyDebugServerDotnet: You requested {context.Request.Method} {context.Request.Path}",
		Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
	};

	await context.Response.WriteAsJsonAsync(response);
});

app.Run("http://0.0.0.0:5000");
