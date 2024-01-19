using System.Net;
using ByteBuoy.API.Validation;

namespace ByteBuoy.API.Middlewares
{
	public class ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
	{
		private readonly RequestDelegate _next = next;
		private readonly IApiKeyValidation _apiKeyValidation = apiKeyValidation;

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Method.Equals("OPTIONS"))
			{
				await _next(context);
				return;
			}	

			if (string.IsNullOrWhiteSpace(context.Request.Headers[Constants.ApiKeyHeaderName]))
			{
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				return;
			}

			string? userApiKey = context.Request.Headers[Constants.ApiKeyHeaderName];

			if (!_apiKeyValidation.IsValidApiKey(userApiKey!, context.Request.Method))
			{
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				return;
			}

			await _next(context);
		}
	}
}
