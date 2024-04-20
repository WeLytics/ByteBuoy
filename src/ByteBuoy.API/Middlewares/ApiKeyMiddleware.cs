using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using ByteBuoy.API.Validation;
using ByteBuoy.Domain;

namespace ByteBuoy.API.Middlewares
{
	public partial class ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
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

			// Check if the request is for a specific Page badge url
			if (context.Request.Method.Equals("GET") && context.Request.Path.Value != null && PageBadgeUrlPattern().IsMatch(context.Request.Path.Value))
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


			// ClaimsIdentity und ClaimsPrincipal erstellen

			var claims = new List<Claim>() { new Claim(ClaimTypes.Role, "API") };
			//claims.Add(new Claim(ClaimTypes.Name, extractedApiKey));
			var identity = new ClaimsIdentity(claims, "ApiKey");
			var principal = new ClaimsPrincipal(identity);

			context.User = principal;

			await _next(context);
		}

		[GeneratedRegex("^/api/v1/pages/.+/badge$")]
		private static partial Regex PageBadgeUrlPattern();
	}
}
